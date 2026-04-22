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
public class Translator(string apiKey, string sourceDir, string targetDir, bool force)
{
    // Gemini Flash 免費版限制：15 RPM -> 每次請求冷卻 4 秒
    private const int CooldownMs = 4_000;

    // 針對 Mscc.GenerativeAI 2.3.0 修正初始化語法
    private readonly GenerativeModel _model = new GoogleAI(apiKey)
        .GenerativeModel(
            model: "gemini-1.5-flash", 
            systemInstruction: new Content 
            { 
                Parts = [new() { Text = SystemPrompt.Text }] 
            }
        );

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
                .EnumerateFiles(sourceDir, "*.md", SearchOption.AllDirectories)
                .OrderBy(f => f)
                .ToList();
        }

        if (files.Count == 0)
        {
            Console.WriteLine("✅ 沒有找到任何待翻譯的 .md 檔案");
            return;
        }

        Console.WriteLine($"\n📚 找到 {files.Count} 個檔案\n{new string('─', 55)}");

        int success = 0, skipped = 0, failed = 0;

        for (int i = 0; i < files.Count; i++)
        {
            var sourcePath = files[i];
            var relPath    = Path.GetRelativePath(sourceDir, sourcePath);
            var targetPath = Path.Combine(targetDir, relPath);

            Console.WriteLine($"\n[{i + 1}/{files.Count}] {relPath}");

            if (!force && File.Exists(targetPath))
            {
                Console.WriteLine("  ⏭️  已存在，略過（使用 --force 可強制重翻）");
                skipped++;
                continue;
            }

            var result = await TranslateFileAsync(sourcePath, targetPath);
            if (result) success++; else failed++;

            // 固定冷卻，確保不超過 15 RPM
            if (i < files.Count - 1)
                await Task.Delay(CooldownMs);
        }

        Console.WriteLine($"\n{new string('─', 55)}");
        Console.WriteLine($"✅ 成功：{success}  ⏭️  略過：{skipped}  ❌ 失敗：{failed}");

        if (failed > 0) Environment.Exit(1);
    }

    private async Task<bool> TranslateFileAsync(string sourcePath, string targetPath)
    {
        string content;
        try { content = await File.ReadAllTextAsync(sourcePath, Encoding.UTF8); }
        catch (Exception ex) { Console.WriteLine($"  ❌ 讀取失敗：{ex.Message}"); return false; }

        if (content.Trim().Length < 10)
        {
            Console.WriteLine("  ⏭️  內容過短，直接複製");
            Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);
            await File.WriteAllTextAsync(targetPath, content, Encoding.UTF8);
            return true;
        }

        // ── 標籤保護：使用 Regex 佔位符保護 Liquid/Hugo 語法 ──
        var placeholders = new Dictionary<string, string>();
        int placeholderIdx = 0;

        // 保護 {% ... %} 和 {{ ... }}，避免路徑被 AI 翻譯
        content = Regex.Replace(content, @"\{%.*?%\}|\{\{.*?\}\}", m => {
            string key = $"[[TAG_{placeholderIdx++}]]";
            placeholders[key] = m.Value;
            return key;
        });

        Console.WriteLine($"  🔤 翻譯中（{content.Length:N0} 字元）...");

        string translated;
        try
        {
            // Flash 支援長文本，將切片門檻調升至 15,000 字元
            translated = content.Length > 15_000
                ? await TranslateInChunksAsync(content)
                : await TranslateWithRetryAsync(content);

            // ── 還原標籤 ──
            foreach (var kvp in placeholders)
            {
                translated = translated.Replace(kvp.Key, kvp.Value);
            }
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
            // Mscc.GenerativeAI 2.3.0：系統指令已綁定，直接傳入內文
            var response = await _model.GenerateContent(content);
            var text = response.Text;
            
            if (string.IsNullOrWhiteSpace(text))
                throw new Exception("Gemini 回傳內容為空");
                
            return text;
        });
    }

    private async Task<string> TranslateInChunksAsync(string content)
    {
        // 以 ## 標題作為切分點
        var sections = Regex
            .Split(content, @"(?=^## )", RegexOptions.Multiline)
            .Where(s => s.Trim().Length > 0)
            .ToList();

        var results = new List<string>();
        for (int i = 0; i < sections.Count; i++)
        {
            Console.WriteLine($"    🔄 段落翻譯中 {i + 1}/{sections.Count}...");
            try
            {
                results.Add(await TranslateWithRetryAsync(sections[i]));
                if (i < sections.Count - 1)
                    await Task.Delay(CooldownMs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ⚠️ 段落翻譯出錯，保留原文：{ex.Message}");
                results.Add(sections[i]);
            }
        }
        return string.Join("\n\n", results);
    }
}


// ── System Prompt (整合 nopCommerce 專業術語) ──────────────────────────────────
public static class SystemPrompt
{
    public const string Text = """
        你是一位精通 ASP.NET Core 與 nopCommerce 4.70+ 的資深開發者，同時也是專業的繁體中文翻譯員。
        你的任務是將 nopCommerce 英文官方文件翻譯成台灣習慣的繁體中文技術用語。

        【絕對禁止變動】
        1. Markdown 語法（標題、連結、圖片、表格代碼區塊等）
        2. Liquid / Hugo 語法：{% ... %}、{{ ... }} 等（已被替換為 [[TAG_X]]，請保留該佔位符）
        3. YAML Front Matter（--- 區塊）：鍵名(key)如 uid, author 等不翻譯；uid 的值不翻譯
        4. 程式碼區塊內的所有內容：一字不改
        5. 類別名稱、方法名稱、命名空間（如 Nop.Web, IPlugin）

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
        Bundled Products    →
