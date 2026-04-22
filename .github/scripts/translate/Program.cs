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

    private string NextPlaceholder()
    {
        var key = $"[[PROTECT_{_counter++:D4}]]";
        return key;
    }

    private string Store(string original)
    {
        var key = NextPlaceholder();
        _map[key] = original;
        return key;
    }

    private string ProtectFencedCodeBlocks(string content)
    {
        return Regex.Replace(
            content,
            @"(```|~~~)[^\n]*\n[\s\S]*?\n\1",
            m => Store(m.Value),
            RegexOptions.Multiline
        );
    }

    private string ProtectYamlFrontMatter(string content)
    {
        // 統一換行符，確保 regex 能正確匹配（處理 BOM 與 \r\n）
        bool hasCrLf = content.Contains("\r\n");
        content = content.Replace("\r\n", "\n");

        content = Regex.Replace(
            content,
            @"\A---\n([\s\S]*?)\n---[ \t]*\n?",
            m =>
            {
                var body = m.Groups[1].Value;
                var lines = body.Split('\n');
                var processed = lines.Select(line =>
                {
                    if (Regex.IsMatch(line, @"^\s*uid\s*:"))
                        return Store(line);
                    return Regex.Replace(
                        line,
                        @"^(\s*[\w\.\-]+\s*:)",
                        keyPart => Store(keyPart.Value)
                    );
                });
                return $"---\n{string.Join("\n", processed)}\n---\n";
            }
        );

        if (hasCrLf)
            content = content.Replace("\n", "\r\n");

        return content;
    }

    private string ProtectLiquidTags(string content)
    {
        return Regex.Replace(
            content,
            @"\{%-?[\s\S]*?-?%\}|\{\{[\s\S]*?\}\}",
            m => Store(m.Value)
        );
    }

    private string ProtectHtmlTags(string content)
    {
        return Regex.Replace(
            content,
            @"<[a-zA-Z/][^>]*?>",
            m => Store(m.Value)
        );
    }

    private string ProtectMarkdownUrls(string content)
    {
        return Regex.Replace(
            content,
            @"(!?\[[^\]]*\])\(([^)]+)\)",
            m =>
            {
                var textPart = m.Groups[1].Value;
                var url      = m.Groups[2].Value;
                return $"{textPart}({Store(url)})";
            }
        );
    }
}


// ── Translator ────────────────────────────────────────────────────────────────
public class Translator(string apiKey, string sourceDir, string targetDir, bool force)
{
    private const int CooldownMs = 4_000;
    private const int ChunkThreshold = 24_000;

    private readonly GenerativeModel _model = new GoogleAI(apiKey)
        .GenerativeModel(model: "gemini-flash-latest");

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
                Console.WriteLine($"  ⏳ 第 {attempt} 次重試，等待 {wait.TotalSeconds:F0} 秒... ({ex.Message[..Math.Min(60, ex.Message.Length)]})")
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
            Console.WriteLine("✅ 沒有找到任何 .md 檔案");
            return;
        }

        Console.WriteLine($"\n📚 找到 {files.Count} 個 .md 檔案\n{new string('─', 55)}");

        int success = 0, skipped = 0, failed = 0;

        for (int i = 0; i < files.Count; i++)
        {
            var sourcePath = files[i];
            var relPath    = Path.GetRelativePath(sourceDir, sourcePath);
            var targetPath = Path.Combine(targetDir, relPath);

            Console.WriteLine($"\n[{i + 1}/{files.Count}] {relPath}");

            if (!force && File.Exists(targetPath))
            {
                Console.WriteLine("  ⏭️  已存在，略過（用 --force 可強制重翻）");
                skipped++;
                continue;
            }

            var result = await TranslateFileAsync(sourcePath, targetPath);
            if (result) success++; else failed++;

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
        try { content = await File.ReadAllTextAsync(sourcePath, new UTF8Encoding(false)); }
        catch (Exception ex) { Console.WriteLine($"  ❌ 讀取失敗：{ex.Message}"); return false; }

        if (content.Trim().Length < 10)
        {
            Console.WriteLine("  ⏭️  內容過短，略過");
            Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);
            await File.WriteAllTextAsync(targetPath, content, new UTF8Encoding(false));
            return true;
        }

        Console.WriteLine($"  🔤 翻譯中（{content.Length:N0} 字元）...");

        string translated;
        try
        {
            var ctx = new PlaceholderContext();
            var protected_content = ctx.Extract(content);

            var raw = protected_content.Length > ChunkThreshold
                ? await TranslateInChunksAsync(protected_content)
                : await TranslateWithRetryAsync(protected_content);

            translated = ctx.Restore(raw);
            translated = PostProcess(translated);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ❌ 翻譯失敗：{ex.Message}");
            return false;
        }

        Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);
        await File.WriteAllTextAsync(targetPath, translated, new UTF8Encoding(false));
        Console.WriteLine($"  ✅ → {targetPath}");
        return true;
    }

    /// <summary>
    /// 翻譯完成後的後處理：
    /// 1. xref:en/ → xref:zh-Hant/
    /// 2. uid: en/ → uid: zh-Hant/
    /// </summary>
    private static string PostProcess(string content)
    {
        content = Regex.Replace(content, @"xref:en/", "xref:zh-Hant/");
        content = Regex.Replace(content, @"(uid:\s*)en/", "$1zh-Hant/");
        return content;
    }

    private async Task<string> TranslateWithRetryAsync(string content)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var response = await _model.GenerateContent($"{SystemPrompt.Text}\n\n翻譯以下內容：\n\n{content}");
            var text = response.Text;
            if (string.IsNullOrWhiteSpace(text))
                throw new Exception("Gemini 回傳空內容");
            return text;
        });
    }

    private async Task<string> TranslateInChunksAsync(string content)
    {
        var sections = SplitSafely(content);

        var results = new List<string>();
        for (int i = 0; i < sections.Count; i++)
        {
            Console.WriteLine($"    段落 {i + 1}/{sections.Count}（{sections[i].Length:N0} 字元）...");
            try
            {
                results.Add(await TranslateWithRetryAsync(sections[i]));
                if (i < sections.Count - 1)
                    await Task.Delay(CooldownMs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ⚠️  段落翻譯失敗，保留原文：{ex.Message}");
                results.Add(sections[i]);
            }
        }
        return string.Join("\n\n", results);
    }

    private static List<string> SplitSafely(string content)
    {
        var rawSections = Regex
            .Split(content, @"(?=^## )", RegexOptions.Multiline)
            .Where(s => s.Trim().Length > 0)
            .ToList();

        var result = new List<string>();
        foreach (var section in rawSections)
        {
            if (section.Length <= ChunkThreshold)
            {
                result.Add(section);
                continue;
            }
            var subChunks = SplitOnBlankLines(section);
            result.AddRange(subChunks);
        }
        return result;
    }

    private static List<string> SplitOnBlankLines(string section)
    {
        var chunks = new List<string>();
        var sb = new StringBuilder();
        bool inCodeBlock = false;

        foreach (var line in section.Split('\n'))
        {
            if (Regex.IsMatch(line, @"^(```|~~~)"))
                inCodeBlock = !inCodeBlock;

            sb.AppendLine(line);

            if (!inCodeBlock && line.Trim().Length == 0 && sb.Length >= ChunkThreshold)
            {
                chunks.Add(sb.ToString().TrimEnd());
                sb.Clear();
            }
        }

        if (sb.Length > 0)
            chunks.Add(sb.ToString().TrimEnd());

        return chunks.Where(c => c.Trim().Length > 0).ToList();
    }
}


// ── System Prompt ─────────────────────────────────────────────────────────────
public static class SystemPrompt
{
    public const string Text = """
        你是一位精通 ASP.NET Core 與 nopCommerce 的資深開發者，同時也是專業的技術文件翻譯員。
        你的任務是將 nopCommerce 英文官方文件翻譯成繁體中文（台灣用語）。

        【絕對禁止變動的內容】
        1. Markdown 語法：# 標題、**粗體**、*斜體*、[連結]()、![圖片]()、``` 程式碼區塊、> 引用、表格 |---|
        2. 程式碼區塊（``` 包住的部分）內的所有程式碼，一字不改
        3. Liquid / Hugo 語法：{% include ... %}、{{ variable }}、{%- ... -%} 等，完全保留原樣
        4. YAML Front Matter（--- 包住的部分）：
           - 所有「鍵名」(key) 絕對不翻譯：uid、title、author、description、ms.date 等
           - 「uid」的值（如 developer/tutorials/index）絕對不翻譯
           - 「title」和「description」的值可以翻譯成中文
        5. HTML 標籤與屬性（如 <div class="...">）
        6. 類別名稱、方法名稱、命名空間（如 Nop.Core、IPlugin、BasePlugin、INopStartup）
        7. 連結的 URL（href/src 的值不翻譯，只翻譯顯示文字）
        8. 內容中出現 [[PROTECT_NNNN]] 格式的佔位符，請原樣保留，不要翻譯、不要移除

        【術語對照表（務必統一使用）】
        Plugin              → 外掛
        Widget              → 區塊
        Theme               → 佈景主題
        Store               → 商店
        Catalog             → 商品目錄
        Customer            → 顧客
        Order               → 訂單
        Vendor              → 供應商
        Shipping            → 配送
        Payment             → 付款
        Discount            → 折扣
        Tax                 → 稅率
        Warehouse           → 倉庫
        Newsletter          → 電子報
        Reward Points       → 紅利點數
        Dependency Injection → 依賴注入
        Entity              → 實體
        Repository          → 儲存庫
        Service             → 服務
        Admin panel         → 後台管理
        Storefront          → 前台網站
        SEO                 → SEO（不翻譯）
        Cache               → 快取
        Middleware          → 中介軟體
        Scheduled Task      → 排程工作
        Event               → 事件
        Attribute           → 屬性
        Specification       → 規格
        AJAX Cart           → AJAX 購物車
        Bundled Products    → 組合商品
        Message Template    → 訊息範本

        【輸出規則】
        - 直接輸出翻譯後的完整 Markdown 內容
        - 不要加任何說明、前言、或額外的 ``` 包裝
        - 保持原始換行與空行結構不變
        """;
}
