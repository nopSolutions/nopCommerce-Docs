using System.CommandLine;
using System.Text;
using System.Text.RegularExpressions;
using Mscc.GenerativeAI;
using Polly;
using Polly.Retry;

// ── CLI 參數定義 ──────────────────────────────────────────────────────────────
var sourceOption = new Option<string>(
    "--source-dir", () => "en", "英文原始目錄");

var targetOption = new Option<string>(
    "--target-dir", () => "zh-Hant", "繁體中文輸出目錄");

var apiKeyOption = new Option<string>(
    "--api-key", () => Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? "",
    "Gemini API Key");

var specificFileOption = new Option<string>(
    "--specific-file", () => "", "只翻譯單一檔案（選填）");

var forceOption = new Option<bool>(
    "--force", () => false, "強制重新翻譯（忽略已存在的 zh-Hant 檔案）");

var rootCommand = new RootCommand("nopCommerce Docs 繁體中文自動翻譯工具");
rootCommand.AddOption(sourceOption);
rootCommand.AddOption(targetOption);
rootCommand.AddOption(apiKeyOption);
rootCommand.AddOption(specificFileOption);
rootCommand.AddOption(forceOption);

rootCommand.SetHandler(async (sourceDir, targetDir, apiKey, specificFile, force) =>
{
    if (string.IsNullOrWhiteSpace(apiKey))
    {
        Console.Error.WriteLine("❌ 錯誤：缺少 GEMINI_API_KEY");
        Environment.Exit(1);
    }

    var translator = new Translator(apiKey, sourceDir, targetDir, force);
    await translator.RunAsync(specificFile);

}, sourceOption, targetOption, apiKeyOption, specificFileOption, forceOption);

return await rootCommand.InvokeAsync(args);


// ── Translator 類別 ──────────────────────────────────────────────────────────
public class Translator
{
    private readonly string _sourceDir;
    private readonly string _targetDir;
    private readonly bool _force;
    private const int CooldownMs = 4_000;

    private readonly GenerativeModel _model;

    // Polly 重試策略：應對 429 頻率限制
    private readonly AsyncRetryPolicy _retryPolicy = Policy
        .Handle<Exception>(ex => 
            ex.Message.Contains("429") || 
            ex.Message.Contains("503") || 
            ex.Message.Contains("quota") ||
            ex.Message.Contains("RESOURCE_EXHAUSTED"))
        .WaitAndRetryAsync(
            retryCount: 4,
            sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt) * 5),
            onRetry: (ex, wait, attempt, _) =>
                Console.WriteLine($"  ⏳ 第 {attempt} 次重試，等待 {wait.TotalSeconds:F0} 秒... ({ex.Message[..Math.Min(50, ex.Message.Length)]})")
        );

    public Translator(string apiKey, string sourceDir, string targetDir, bool force)
    {
        _sourceDir = sourceDir;
        _targetDir = targetDir;
        _force = force;

        // ── 核心修正：強制指定 v1 版本以解決 404 NotFound ──
        var googleAI = new GoogleAI(apiKey);
        googleAI.ApiVersion = "v1"; 

        _model = googleAI.GenerativeModel(
            model: "gemini-1.5-flash", 
            systemInstruction: new Content 
            { 
                Parts = [new() { Text = SystemPrompt.Text }] 
            }
        );
    }

    public async Task RunAsync(string specificFile)
    {
        List<string> files;

        if (!string.IsNullOrWhiteSpace(specificFile))
        {
            if (!File.Exists(specificFile))
            {
                Console.Error.WriteLine($"❌ 找不到指定檔案：{specificFile}");
                Environment.Exit(1);
            }
            files = [specificFile];
        }
        else
        {
            files = Directory
                .EnumerateFiles(_sourceDir, "*.md", SearchOption.AllDirectories)
                .OrderBy(f => f)
                .ToList();
        }

        if (files.Count == 0)
        {
            Console.WriteLine("✅ 沒有找到任何待翻譯的 .md 檔案");
            return;
        }

        Console.WriteLine($"\n📚 找到 {files.Count} 個檔案\n{new string('─', 55)}");

        for (int i = 0; i < files.Count; i++)
        {
            var sourcePath = files[i];
            var relPath    = Path.GetRelativePath(_sourceDir, sourcePath);
            var targetPath = Path.Combine(_targetDir, relPath);

            Console.WriteLine($"\n[{i + 1}/{files.Count}] {relPath}");

            if (!_force && File.Exists(targetPath))
            {
                Console.WriteLine("  ⏭️  已存在，略過");
                continue;
            }

            var result = await TranslateFileAsync(sourcePath, targetPath);
            
            if (i < files.Count - 1)
                await Task.Delay(CooldownMs);
        }
    }

    private async Task<bool> TranslateFileAsync(string sourcePath, string targetPath)
    {
        string content;
        try { content = await File.ReadAllTextAsync(sourcePath, Encoding.UTF8); }
        catch (Exception ex) { Console.WriteLine($"  ❌ 讀取失敗：{ex.Message}"); return false; }

        // ── 標籤保護 ──
        var placeholders = new Dictionary<string, string>();
        int placeholderIdx = 0;
        content = Regex.Replace(content, @"\{%.*?%\}|\{\{.*?\}\}", m => {
            string key = $"[[TAG_{placeholderIdx++}]]";
            placeholders[key] = m.Value;
            return key;
        });

        Console.WriteLine($"  🔤 翻譯中（{content.Length:N0} 字元）...");

        string translated;
        try
        {
            translated = content.Length > 15_000
                ? await TranslateInChunksAsync(content)
                : await TranslateWithRetryAsync(content);

            foreach (var kvp in placeholders)
                translated = translated.Replace(kvp.Key, kvp.Value);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ❌ 翻譯失敗：{ex.Message}");
            return false;
        }

        Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);
        await File.WriteAllTextAsync(targetPath, translated, Encoding.UTF8);
        Console.WriteLine($"  ✅ 儲存至 -> {targetPath}");
        return true;
    }

    private async Task<string> TranslateWithRetryAsync(string content)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var response = await _model.GenerateContent(content);
            var text = response.Text;
            if (string.IsNullOrWhiteSpace(text)) throw new Exception("Gemini 回傳為空");
            return text;
        });
    }

    private async Task<string> TranslateInChunksAsync(string content)
    {
        var sections = Regex.Split(content, @"(?=^## )", RegexOptions.Multiline)
                            .Where(s => s.Trim().Length > 0).ToList();
        var results = new List<string>();
        for (int i = 0; i < sections.Count; i++)
        {
            results.Add(await TranslateWithRetryAsync(sections[i]));
            if (i < sections.Count - 1) await Task.Delay(CooldownMs);
        }
        return string.Join("\n\n", results);
    }
}

public static class SystemPrompt
{
    public const string Text = """
        你是一位精通 ASP.NET Core 與 nopCommerce 的資深開發者，同時也是專業的繁體中文翻譯員。
        你的任務是將 nopCommerce 英文官方文件翻譯成台灣習慣的繁體中文技術用語。

        【絕對禁止變動】
        1. Markdown 語法（標題、連結、圖片、表格等）
        2. Liquid / Hugo 語法：[[TAG_X]] 佔位符請保留原樣
        3. YAML Front Matter（--- 區塊）：鍵名(key)不翻譯；uid 的值不翻譯
        4. 程式碼區塊內的所有內容：一字不改

        【專業術語對照表】
        Plugin              → 外掛
        Widget              → 區塊
        Theme               → 佈景主題
        Dependency Injection → 依賴注入
        Entity              → 實體
        Repository          → 儲存庫
        Attribute           → 屬性
        Specification       → 規格
        AJAX Cart           → AJAX 購物車
        Bundled Products    → 組合商品
        Message Template    → 訊息範本
        Log                 → 紀錄檔
        Store               → 商店
        Admin panel         → 後台管理

        【輸出規則】
