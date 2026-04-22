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
                    // uid 整行保護（key 與值都不能動）
                    if (Regex.IsMatch(line, @"^\s*uid\s*:"))
                        return Store(line);
                    // 其他所有 key 完整交給 AI 翻譯（key 名稱與值都可翻）
                    return line;
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
        .GenerativeModel(model: "gemini-3.1-flash-lite-preview");

    // 429 專用：等待 66 秒後重試一次，若還是 429 則視為今日 quota 耗盡
    private const int QuotaWaitMs = 66_000;

    private readonly AsyncRetryPolicy _retryPolicy = Policy
        .Handle<Exception>(ex =>
            ex.Message.Contains("503") ||
            ex.Message.Contains("RESOURCE_EXHAUSTED"))
        .WaitAndRetryAsync(
            retryCount: 2,
            sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt) * 5),
            onRetry: (ex, wait, attempt, _) =>
                Console.WriteLine($"  ⏳ 第 {attempt} 次重試，等待 {wait.TotalSeconds:F0} 秒... ({ex.Message[..Math.Min(60, ex.Message.Length)]})")
        );

    public async Task RunAsync(string specificFile)
    {
        List<string> files;

        if (!string.IsNullOrWhiteSpace(specificFile))
        {
            if (Directory.Exists(specificFile))
            {
                // 指定目錄：翻譯該目錄下所有 .md
                files = Directory
                    .EnumerateFiles(specificFile, "*.md", SearchOption.AllDirectories)
                    .OrderBy(f => f)
                    .ToList();
                if (files.Count == 0)
                {
                    Console.Error.WriteLine($"❌ 指定目錄下沒有找到任何 .md 檔案：{specificFile}");
                    Environment.Exit(1);
                }
                Console.WriteLine($"📂 指定目錄：{specificFile}（共 {files.Count} 個檔案）");
            }
            else if (File.Exists(specificFile))
            {
                // 指定單一檔案
                files = [specificFile];
            }
            else
            {
                Console.Error.WriteLine($"❌ 找不到指定的檔案或目錄：{specificFile}");
                Environment.Exit(1);
                return;
            }
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
        int consecutiveFailures = 0;
        const int MaxConsecutiveFailures = 20;
        const int PushEvery = 1; // 每翻成功 1 個就 push 一次
        bool earlyStop = false;
        var failedFiles = new List<string>();

        for (int i = 0; i < files.Count; i++)
        {
            var sourcePath = files[i];
            var relPath    = Path.GetRelativePath(sourceDir, sourcePath);
            var targetPath = Path.Combine(targetDir, relPath);

            Console.WriteLine($"\n[{i + 1}/{files.Count}] {relPath}");

            if (!force && File.Exists(targetPath))
            {
                // 若來源比目標新（上游有更新），自動重翻
                var sourceTime = File.GetLastWriteTimeUtc(sourcePath);
                var targetTime = File.GetLastWriteTimeUtc(targetPath);
                if (sourceTime <= targetTime)
                {
                    Console.WriteLine("  ⏭️  已存在且無更新，略過");
                    skipped++;
                    continue;
                }
                Console.WriteLine("  🔄 來源已更新，重新翻譯...");
            }

            var result = false;
            try
            {
                result = await TranslateFileAsync(sourcePath, targetPath);
            }
            catch (QuotaExhaustedException ex)
            {
                Console.WriteLine($"\n⛔ {ex.Message}");
                Console.WriteLine($"   今日已成功：{success}  已略過：{skipped}");
                Console.WriteLine($"   已翻好的檔案將會 commit，明天排程會繼續補翻。");
                earlyStop = true;
                break;
            }

            if (result)
            {
                success++;
                consecutiveFailures = 0;
            }
            else
            {
                failed++;
                failedFiles.Add(sourcePath);
                consecutiveFailures++;
                if (consecutiveFailures >= MaxConsecutiveFailures)
                {
                    Console.WriteLine($"\n⛔ 連續失敗 {MaxConsecutiveFailures} 次，今日 API quota 可能已耗盡，提早結束。");
                    Console.WriteLine($"   已成功：{success}  已略過：{skipped}  失敗：{failed}");
                    Console.WriteLine($"   下次排程執行時會繼續補翻剩餘檔案。");
                    earlyStop = true;
                    break;
                }
            }

            if (i < files.Count - 1)
            {
                // 記錄 API 完成時間，push 利用冷卻時間執行，結束後補足剩餘冷卻
                var apiDoneAt = DateTime.UtcNow;

                if (result && success % PushEvery == 0)
                    await PushProgressAsync(success);

                var elapsed = (int)(DateTime.UtcNow - apiDoneAt).TotalMilliseconds;
                var remaining = CooldownMs - elapsed;
                if (remaining > 0)
                    await Task.Delay(remaining);
            }
        }

        Console.WriteLine($"\n{new string('─', 55)}");
        Console.WriteLine($"✅ 成功：{success}  ⏭️  略過：{skipped}  ❌ 失敗：{failed}");

        // 寫入失敗清單，方便手動補翻
        var failedListPath = Path.Combine(targetDir, ".translation-failed.txt");
        if (failedFiles.Count > 0)
        {
            await File.WriteAllLinesAsync(failedListPath, failedFiles, new UTF8Encoding(false));
            Console.WriteLine($"\n📋 失敗清單已寫入：{failedListPath}");
            foreach (var f in failedFiles)
                Console.WriteLine($"   - {f}");
        }
        else if (File.Exists(failedListPath))
        {
            // 全部成功就刪掉舊的失敗清單
            File.Delete(failedListPath);
        }

        // earlyStop 時正常結束（讓 workflow 繼續執行 commit），否則有失敗才報錯
        if (!earlyStop && failed > 0) Environment.Exit(1);

        // 複製非 .md 檔案（圖片、PDF 等），來源比目標新才複製
        await CopyNonMarkdownFilesAsync();
    }

    private static async Task PushProgressAsync(int count)
    {
        Console.WriteLine($"\n  💾 中途儲存：已完成 {count} 個，推送至 GitHub...");
        try
        {
            await RunGitAsync("add zh-Hant/");
            await RunGitAsync($"commit -m \"🌐 翻譯進度：已完成 {count} 個檔案\"");
            await RunGitAsync("pull origin master --rebase");
            await RunGitAsync("push origin master");
            Console.WriteLine($"  ✅ 中途推送成功");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ⚠️  中途推送失敗（不影響繼續翻譯）：{ex.Message}");
        }
    }

    private static async Task RunGitAsync(string args)
    {
        var psi = new System.Diagnostics.ProcessStartInfo("git", args)
        {
            RedirectStandardOutput = true,
            RedirectStandardError  = true,
            UseShellExecute        = false,
        };
        using var proc = System.Diagnostics.Process.Start(psi)!;
        await proc.WaitForExitAsync();
        if (proc.ExitCode != 0)
        {
            var err = await proc.StandardError.ReadToEndAsync();
            throw new Exception($"git {args} 失敗：{err.Trim()}");
        }
    }

    private async Task CopyNonMarkdownFilesAsync()
    {
        var nonMdFiles = Directory
            .EnumerateFiles(sourceDir, "*", SearchOption.AllDirectories)
            .Where(f => !f.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
            .OrderBy(f => f)
            .ToList();

        if (nonMdFiles.Count == 0) return;

        int copied = 0, skipped = 0;
        Console.WriteLine($"\n📁 複製非 .md 檔案...");

        foreach (var sourcePath in nonMdFiles)
        {
            var relPath    = Path.GetRelativePath(sourceDir, sourcePath);
            var targetPath = Path.Combine(targetDir, relPath);

            // 來源比目標新（或目標不存在）才複製
            if (File.Exists(targetPath))
            {
                var sourceTime = File.GetLastWriteTimeUtc(sourcePath);
                var targetTime = File.GetLastWriteTimeUtc(targetPath);
                if (sourceTime <= targetTime)
                {
                    skipped++;
                    continue;
                }
            }

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(targetPath)!);
                File.Copy(sourcePath, targetPath, overwrite: true);
                Console.WriteLine($"  📄 {relPath}");
                copied++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ⚠️  複製失敗 {relPath}：{ex.Message}");
            }
        }

        Console.WriteLine($"  📁 複製：{copied}  略過：{skipped}");
        await Task.CompletedTask;
    }

    private async Task<bool> TranslateFileAsync(string sourcePath, string targetPath)
    {
        string content;
        try { content = await File.ReadAllTextAsync(sourcePath, new UTF8Encoding(false)); }
        catch (Exception ex) { Console.WriteLine($"  ❌ 讀取失敗：{ex.Message}"); return false; }
        // 移除 BOM，確保 YAML front matter regex 能正確匹配
        content = content.TrimStart('\uFEFF');

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
        catch (QuotaExhaustedException)
        {
            // 往上拋，由 RunAsync 處理 earlyStop
            throw;
        }
        catch (Exception ex)
        {
            // 404 表示模型不存在，繼續重試也沒用，立刻終止整個程式
            if (ex.Message.Contains("404") || ex.Message.Contains("NOT_FOUND") || ex.Message.Contains("not found for API"))
            {
                Console.Error.WriteLine($"\n⛔ 致命錯誤：模型不存在或 API 版本不支援，請確認模型名稱。");
                Console.Error.WriteLine($"   錯誤訊息：{ex.Message[..Math.Min(200, ex.Message.Length)]}");
                Environment.Exit(2);
            }
            Console.WriteLine($"  ❌ 翻譯失敗：{ex.Message}");
            // 刪除半成品，下次排程會重新翻譯
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
                Console.WriteLine($"  🗑️  已刪除半成品，下次排程重翻");
            }
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

        // 只在 YAML front matter 內翻譯特定 key 名稱
        // 條件：行首、完整 key 名稱、後接空白或行尾（避免誤中 author_name: 等）
        content = Regex.Replace(
            content,
            @"\A(---\n[\s\S]*?)\n---",
            m =>
            {
                var fm = m.Groups[1].Value;
                fm = Regex.Replace(fm, @"(?m)^author:(?=\s|$)", "作者:");
                fm = Regex.Replace(fm, @"(?m)^contributors:(?=\s|$)", "貢獻者:");
                fm = Regex.Replace(fm, @"(?m)^title:(?=\s|$)", "標題:");
                return $"{fm}\n---";
            }
        );

        return content;
    }

    private async Task<string> TranslateWithRetryAsync(string content)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            try
            {
                var translated = await CallGeminiAsync(content, forceful: false);

                // 驗證翻譯完整性：若仍有大量英文句子，用更強的 prompt 重試一次
                if (HasSignificantEnglish(content, translated))
                {
                    Console.WriteLine("  ⚠️  偵測到未翻譯段落，使用加強模式重試...");
                    translated = await CallGeminiAsync(content, forceful: true);
                }

                return translated;
            }
            catch (Exception ex) when (ex.Message.Contains("429") || ex.Message.Contains("quota"))
            {
                Console.WriteLine($"  ⚠️  429 Too Many Requests，等待 {QuotaWaitMs / 1000} 秒後重試一次...");
                await Task.Delay(QuotaWaitMs);

                try
                {
                    return await CallGeminiAsync(content, forceful: false);
                }
                catch (Exception retryEx) when (retryEx.Message.Contains("429") || retryEx.Message.Contains("quota"))
                {
                    throw new QuotaExhaustedException("今日 API 免費 quota 已耗盡，請明天再試。");
                }
            }
        });
    }

    private async Task<string> CallGeminiAsync(string content, bool forceful)
    {
        var prompt = forceful
            ? $"{SystemPrompt.Text}\n\n【緊急提醒】\n- [[PROTECT_NNNN]] 是佔位符，原樣保留即可\n- 佔位符前後的所有英文說明文字，都必須翻譯成繁體中文\n- 程式碼區塊以外的英文，一個字都不能漏\n\n翻譯以下內容：\n\n{content}"
            : $"{SystemPrompt.Text}\n\n注意：[[PROTECT_NNNN]] 格式是佔位符請原樣保留，但佔位符前後的所有英文說明文字都必須翻譯成繁體中文。\n\n翻譯以下內容：\n\n{content}";

        var response = await _model.GenerateContent(prompt);
        var text = response.Text;
        if (string.IsNullOrWhiteSpace(text))
            throw new Exception("Gemini 回傳空內容");
        return text;
    }

    /// <summary>
    /// 檢查翻譯後內容是否仍有大量未翻譯的英文文字。
    /// 方法：移除佔位符和程式碼區塊後，計算英文單字佔所有單字的比例。
    /// </summary>
    private static bool HasSignificantEnglish(string original, string translated)
    {
        // 移除佔位符和程式碼區塊
        var strip = new Regex(@"\[\[PROTECT_\d+\]\]|```[\s\S]*?```|`[^`]+`", RegexOptions.Multiline);
        var cleaned = strip.Replace(translated, " ");

        // 計算英文單字數（3字母以上，排除常見縮寫如 nopCommerce、URL 等）
        var englishWords = Regex.Matches(cleaned, @"\b[A-Za-z]{4,}\b").Count;
        var totalTokens = Regex.Matches(cleaned, @"\S+").Count;

        if (totalTokens < 20) return false;

        // 英文單字超過總 token 的 25% 且超過 30 個，視為翻譯不完整
        double ratio = (double)englishWords / totalTokens;
        return ratio > 0.25 && englishWords > 30;
    }

    private async Task<string> TranslateInChunksAsync(string content)
    {
        var sections = SplitSafely(content);

        var results = new List<string>();
        for (int i = 0; i < sections.Count; i++)
        {
            Console.WriteLine($"    段落 {i + 1}/{sections.Count}（{sections[i].Length:N0} 字元）...");
            // 段落失敗直接往上拋，由 TranslateFileAsync 刪除目標檔案並標記為失敗
            // 下次排程會從頭重翻，不留半成品
            results.Add(await TranslateWithRetryAsync(sections[i]));
            if (i < sections.Count - 1)
                await Task.Delay(CooldownMs);
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
           - 「uid」的 key 與值都絕對不翻譯（如 uid: en/getting-started/index）
           - 其他所有 key 名稱與值都可以翻譯成中文（如 title、description、author、contributors）
           - author 的值若為 git 使用者名稱格式（如 git.AndreiMaz）則保留原樣不翻譯
        5. HTML 標籤與屬性（如 <div class="...">）
        6. 類別名稱、方法名稱、命名空間（如 Nop.Core、IPlugin、BasePlugin、INopStartup）
        7. 連結的 URL（href/src 的值不翻譯，只翻譯顯示文字）
        8. 內容中出現 [[PROTECT_NNNN]] 格式的佔位符，請原樣保留，不要翻譯、不要移除

        【術語對照表（務必統一使用）】
        ── 核心架構 ──
        Plugin              → 外掛
        Widget              → 小部件
        Theme               → 佈景主題
        Middleware          → 中介軟體
        Scheduled Task      → 排程工作
        Event               → 事件
        Dependency Injection → 依賴注入
        Entity              → 實體
        Repository          → 儲存庫
        Service             → 服務
        Factory             → 工廠（指 ModelFactory 等類別）
        Mapping             → 映射（指 AutoMapper 或 DB Mapping）
        Cache               → 快取

        ── 商店管理 ──
        Store               → 商店
        Admin panel         → 後台管理
        Storefront          → 前台網站
        Multi-store         → 多商店
        Maintenance         → 維護模式
        Activity Log        → 活動日誌
        ACL                 → 權限控制（Access Control List）
        SEO                 → SEO（不翻譯）

        ── 商品目錄 ──
        Catalog             → 商品目錄
        Category            → 分類（與 Catalog 商品目錄區隔）
        Manufacturer        → 製造商
        Attribute           → 屬性
        Specification       → 規格
        Product Tag         → 商品標籤
        Product Review      → 商品評論
        Bundled Products    → 組合商品
        Downloadable Product → 可下載商品
        Recurring Product   → 定期購商品
        Rental Product      → 租借商品
        Back-in-stock       → 補貨通知
        Pre-order           → 預購
        SKU                 → SKU（不翻譯）
        Inventory           → 庫存

        ── 顧客與訂單 ──
        Customer            → 顧客
        Customer Role       → 顧客角色
        Order               → 訂單
        Order Status        → 訂單狀態
        Payment Status      → 付款狀態
        Shipment            → 出貨單
        Return Request      → 退貨申請
        Shopping Cart       → 購物車
        AJAX Cart           → AJAX 購物車
        Wishlist            → 願望清單
        Checkout            → 結帳
        Pickup Point        → 取貨點

        ── 配送與付款 ──
        Shipping            → 配送
        Shipping Method     → 配送方式
        Payment             → 付款
        Warehouse           → 倉庫
        Vendor              → 供應商
        Drop shipping       → 直運

        ── 行銷與促銷 ──
        Discount            → 折扣
        Coupon Code         → 優惠碼
        Tier Price          → 階梯價格（依數量變動的價格）
        Gift Card           → 禮品卡
        Reward Points       → 紅利點數
        Affiliate           → 推廣夥伴
        Cross-sell          → 交叉銷售
        Up-sell             → 向上銷售
        Newsletter          → 電子報

        ── 內容管理 ──
        Topic               → 內容頁面（nopCommerce 特有稱呼）
        Message Template    → 訊息範本（主要指自動發出的 Email）
        Blog                → 部落格
        News                → 最新消息
        Poll                → 投票
        Forum               → 論壇

        ── 財務 ──
        Tax                 → 稅率
        Tax Category        → 稅率類別
        Currency            → 貨幣
        Exchange Rate       → 匯率

        ── 系統與在地化 ──
        Language            → 語言
        Localization        → 在地化
        Multi-Factor Authentication → 多重驗證
        Request for Quote   → 報價申請
        Mega Menu           → 大型選單
        Cookie Consent      → Cookie 同意聲明
        GDPR Compliance     → GDPR 合規性
        Privacy Settings    → 隱私設定

        ── AI 與智慧化 ──
        AI Integration      → AI 整合
        AI Assistant        → AI 助手
        Semantic Search     → 語意搜尋
        AI-generated Content → AI 生成內容
        Vector Database     → 向量資料庫

        ── 無頭電商與 API ──
        Web API             → Web API（不翻譯）
        Headless Commerce   → 無頭電商
        Swagger             → Swagger（不翻譯）
        JWT                 → JWT（不翻譯）
        Webhook             → Webhook（不翻譯）

        ── 效能與技術 ──
        Redis Cache         → Redis 快取
        Distributed Cache   → 分散式快取
        Response Compression → 回應壓縮
        Lazy Loading        → 延遲載入
        Bundling & Minification → 合併與縮減
        WebP Support        → WebP 支援
        Multi-tenant        → 多租戶

        ── 介面與體驗 ──
        One-page Checkout   → 一頁式結帳
        Multi-step Checkout → 多步驟結帳
        Responsive Admin    → 回應式管理後台
        Dark Mode           → 深色模式

        ── 配送與付款（補充）──
        Real-time Shipping Rate → 即時運費計算
        Payment Provider    → 付款提供程序
        Tax Provider        → 稅務提供程序
        Shipping Provider   → 配送提供程序

        【輸出規則】
        - 直接輸出翻譯後的完整 Markdown 內容
        - 不要加任何說明、前言、或額外的 ``` 包裝
        - 保持原始換行與空行結構不變

        【重要：翻譯完整性】
        - 程式碼區塊（``` 包住的部分）以外的所有英文文字，無論長短，都必須翻譯成繁體中文
        - 就算段落中有大量程式碼，程式碼以外的說明文字仍然必須翻譯
        - 絕對不可以把英文段落原樣輸出，除非整段都是程式碼
        - 如果你不確定某段文字是否需要翻譯，預設就是翻譯
        - 有序列表（1. 2. 3.）和無序列表（* -）中的說明文字，必須全部翻譯
        - Important / Note / Tip 提示區塊內的文字，必須翻譯
        """;
}

// ── QuotaExhaustedException ───────────────────────────────────────────────────
public class QuotaExhaustedException(string message) : Exception(message);
