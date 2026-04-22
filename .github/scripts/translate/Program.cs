using System.CommandLine;
using System.Text;
using System.Text.RegularExpressions;
using Mscc.GenerativeAI;
using Polly;
using Polly.Retry;

// ── CLI 參數定義 ──────────────────────────────────────────────────────────────
var sourceOption = new Option<string>("--source-dir", () => "en", "英文原始目錄");
var targetOption = new Option<string>("--target-dir", () => "zh-Hant", "繁體中文輸出目錄");
var apiKeyOption = new Option<string>("--api-key", () => Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? "", "Gemini API Key");
var specificFileOption = new Option<string>("--specific-file", () => "", "只翻譯單一檔案（選填）");
var forceOption = new Option<bool>("--force", () => false, "強制重新翻譯（忽略已存在的檔案）");

var rootCommand = new RootCommand("nopCommerce Docs 繁體中文自動翻譯工具");
rootCommand.AddOption(sourceOption);
rootCommand.AddOption(targetOption);
rootCommand.AddOption(apiKeyOption);
rootCommand.AddOption(specificFileOption);
rootCommand.AddOption(forceOption);

rootCommand.SetHandler(async (sourceDir, targetDir, apiKey, specificFile, force) =>
{
    if (string.IsNullOrWhiteSpace(apiKey)) {
        Console.Error.WriteLine("❌ 缺少 GEMINI_API_KEY");
        Environment.Exit(1);
    }
    var translator = new Translator(apiKey, sourceDir, targetDir, force);
    await translator.RunAsync(specificFile);
}, sourceOption, targetOption, apiKeyOption, specificFileOption, forceOption);

return await rootCommand.InvokeAsync(args);

// ── PlaceholderContext ─────────────────────────────────────────────────────────
public class PlaceholderContext
{
    private readonly Dictionary<string, string> _map = new();
    private int _counter;

    public string Extract(string content)
    {
        content = ProtectFencedCodeBlocks(content);
        content = ProtectYamlFrontMatter(content);
        content = ProtectLiquidTags(content);
        content = ProtectHtmlTags(content);
        content = ProtectMarkdownUrls(content);
        return content;
    }

    public string Restore(string content)
    {
        foreach (var (placeholder, original) in _map)
            content = content.Replace(placeholder, original);
        return content;
    }

    private string Store(string original)
    {
        var key = $"[[PROTECT_{_counter++:D4}]]";
        _map[key] = original;
        return key;
    }

    private string ProtectFencedCodeBlocks(string content) =>
        Regex.Replace(content, @"(```|~~~)[^\n]*\n[\s\S]*?\n\1", m => Store(m.Value), RegexOptions.Multiline);

    private string ProtectYamlFrontMatter(string content) =>
        Regex.Replace(content, @"^---\n([\s\S]*?)\n---", m => {
            var lines = m.Groups[1].Value.Split('\n');
            var processed = lines.Select(line => {
                if (Regex.IsMatch(line, @"^\s*uid\s*:")) return Store(line);
                return Regex.Replace(line, @"^(\s*[\w\.\-]+\s*:)", keyPart => Store(keyPart.Value));
            });
            return $"---\n{string.Join("\n", processed)}\n---";
        }, RegexOptions.Multiline);

    private string ProtectLiquidTags(string content) =>
        Regex.Replace(content, @"\{%-?[\s\S]*?-?%\}|\{\{[\s\S]*?\}\}", m => Store(m.Value));

    private string ProtectHtmlTags(string content) =>
        Regex.Replace(content, @"<[a-zA-Z/][^>]*?>", m => Store(m.Value));

    private string ProtectMarkdownUrls(string content) =>
        Regex.Replace(content, @"(!?\[[^\]]*\])\(([^)]+)\)", m => $"{m.Groups[1].Value}({Store(m.Groups[2].Value)})");
}

// ── Translator ────────────────────────────────────────────────────────────────
public class Translator(string apiKey, string sourceDir, string targetDir, bool force)
{
    private const int CooldownMs = 4_000;
    private const int ChunkThreshold = 24_000;

    private readonly GenerativeModel _model = CreateModel(apiKey);

    private static GenerativeModel CreateModel(string key) {
        var googleAI = new GoogleAI(key);
        // 優化：將 SystemPrompt 注入 SystemInstruction 減少 Token 消耗
        return googleAI.GenerativeModel(
            model: Model.Gemini15Flash, 
            systemInstruction: new Content { Parts = new List<Part> { new Part { Text = SystemPrompt.Text } } }
        );
    }

    private readonly AsyncRetryPolicy _retryPolicy = Policy
        .Handle<Exception>(ex => ex.Message.Contains("429") || ex.Message.Contains("quota") || ex.Message.Contains("RESOURCES_EXHAUSTED"))
        .WaitAndRetryAsync(4, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt) * 5),
            (ex, wait, attempt, _) => Console.WriteLine($"  ⏳ 頻率限制，重試中 ({attempt}/4)，等待 {wait.TotalSeconds}秒..."));

    public async Task RunAsync(string specificFile)
    {
        var files = !string.IsNullOrWhiteSpace(specificFile) ? new List<string>{specificFile} : 
                    Directory.EnumerateFiles(sourceDir, "*.md", SearchOption.AllDirectories).OrderBy(f => f).ToList();

        if (!files.Any()) return;
        Console.WriteLine($"\n📚 找到 {files.Count} 個檔案\n{new string('─', 40)}");

        int success = 0, skipped = 0, failed = 0;
        for (int i = 0; i < files.Count; i++)
        {
            var relPath = Path.GetRelativePath(sourceDir, files[i]);
            var targetPath = Path.Combine(targetDir, relPath);
            Console.WriteLine($"[{i + 1}/{files.Count}] {relPath}");

            if (!force && File.Exists(targetPath)) {
                skipped++; continue;
            }

            if (await TranslateFileAsync(files[i], targetPath)) success++; else failed++;
            if (i < files.Count - 1) await Task.Delay(CooldownMs);
        }
        Console.WriteLine($"\n✅ 成功：{success}  ⏭️  略過：{skipped}  ❌ 失敗：{failed}");
    }

    private async Task<bool> TranslateFileAsync(string sourcePath, string targetPath)
    {
        try {
            var content = await File.ReadAllTextAsync(sourcePath, Encoding.UTF8);
            if (content.Length < 10) return true;

            var ctx = new PlaceholderContext();
            var protectedContent = ctx.Extract(content);

            var raw = protectedContent.Length > ChunkThreshold 
                ? await TranslateInChunksAsync(protectedContent) 
                : await TranslateWithRetryAsync(protectedContent);

            var translated = ctx.Restore(raw);
            Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);
            await File.WriteAllTextAsync(targetPath, translated, Encoding.UTF8);
            return true;
        } catch (Exception ex) {
            Console.WriteLine($"  ❌ 失敗: {ex.Message}");
            return false;
        }
    }

    private async Task<string> TranslateWithRetryAsync(string content) =>
        await _retryPolicy.ExecuteAsync(async () => {
            var response = await _model.GenerateContent(content); // 已包含 SystemInstruction
            return response.Text ?? throw new Exception("空回應");
        });

    private async Task<string> TranslateInChunksAsync(string content)
    {
        var sections = Regex.Split(content, @"(?=^## )", RegexOptions.Multiline).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
        var results = new List<string>();
        foreach (var section in sections) {
            results.Add(await TranslateWithRetryAsync(section));
            await Task.Delay(CooldownMs);
        }
        return string.Join("\n\n", results);
    }
}

public static class SystemPrompt {
    public const string Text = @"你是一位精通 ASP.NET Core 與 nopCommerce 的專業翻譯員。將內容翻譯為繁體中文（台灣用語）。
【絕對不翻譯】：Markdown語法、程式碼區塊、Liquid標籤({%...%})、HTML標籤、YAML鍵名與uid值、[[PROTECT_NNNN]]佔位符。
【術語一致性】：Plugin->外掛, Widget->區塊, Theme->佈景主題, Dependency Injection->依賴注入, Attribute->屬性, Specification->規格, Message Template->訊息範本。";
}
