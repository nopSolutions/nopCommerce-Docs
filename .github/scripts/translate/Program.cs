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
    "Gemini API Key（多組以逗號分隔）");

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
    // 預編譯的 Regex（class 級別共用，避免每次呼叫重建）
    private static readonly Regex _reFencedCodeBlock = new(
        @"(```|~~~)[^\n]*\n[\s\S]*?\n\1[ \t]*(?:\n|$)",
        RegexOptions.Compiled | RegexOptions.Multiline);

    private static readonly Regex _reYamlFrontMatter = new(
        @"\A---\n([\s\S]*?)\n---[ \t]*\n?",
        RegexOptions.Compiled);

    private static readonly Regex _reUidLine = new(
        @"^\s*uid\s*:",
        RegexOptions.Compiled);

    private static readonly Regex _reLiquidTag = new(
        @"\{%-?[\s\S]*?-?%\}|\{\{[\s\S]*?\}\}",
        RegexOptions.Compiled);

    private static readonly Regex _reHtmlTag = new(
        @"<[a-zA-Z/][^>]*?>",
        RegexOptions.Compiled);

    private static readonly Regex _reMarkdownLink = new(
        @"(!?)\[([^\]]*)\]\(([^)]+)\)",
        RegexOptions.Compiled);

    // 常見英文佔位連結文字（Gemini 翻譯不穩定，整塊保護）
    private static readonly HashSet<string> _placeholderLinkWords = new(StringComparer.OrdinalIgnoreCase)
    {
        "", "here", "this", "link", "this link", "click here",
        "read more", "more", "see here", "see"
    };

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
        // 反向順序還原：最後 Store 的先還原，避免嵌套 placeholder 的替換順序問題
        // 用 ToList + for 迴圈反向走，避免 Reverse() 產生中間集合
        var entries = _map.ToList();
        for (int i = entries.Count - 1; i >= 0; i--)
        {
            content = content.Replace(entries[i].Key, entries[i].Value);
        }
        return content;
    }

    private string NextPlaceholder() => $"[[PROTECT_{_counter++:D4}]]";

    private string Store(string original)
    {
        var key = NextPlaceholder();
        _map[key] = original;
        return key;
    }

    private string ProtectFencedCodeBlocks(string content)
        => _reFencedCodeBlock.Replace(content, m => Store(m.Value));

    private string ProtectYamlFrontMatter(string content)
    {
        // 統一換行符，確保 regex 能正確匹配（處理 BOM 與 \r\n）
        bool hasCrLf = content.Contains("\r\n");
        content = content.Replace("\r\n", "\n");

        content = _reYamlFrontMatter.Replace(content, m =>
        {
            var body = m.Groups[1].Value;
            var lines = body.Split('\n');
            var processed = lines.Select(line =>
            {
                // uid 整行保護（key 與值都不能動）
                if (_reUidLine.IsMatch(line))
                    return Store(line);
                // 其他所有 key 完整交給 AI 翻譯（key 名稱與值都可翻）
                return line;
            });
            return $"---\n{string.Join("\n", processed)}\n---\n";
        });

        if (hasCrLf)
            content = content.Replace("\n", "\r\n");

        return content;
    }

    private string ProtectLiquidTags(string content)
        => _reLiquidTag.Replace(content, m => Store(m.Value));

    private string ProtectHtmlTags(string content)
        => _reHtmlTag.Replace(content, m => Store(m.Value));

    private string ProtectMarkdownUrls(string content)
    {
        return _reMarkdownLink.Replace(content, m =>
        {
            var isImage  = m.Groups[1].Value == "!";
            var textPart = m.Groups[2].Value;
            var url      = m.Groups[3].Value;

            // 圖片：整塊保護（alt 不翻譯）
            if (isImage)
                return Store(m.Value);

            // 連結文字為空或是常見的英文佔位詞（here/this/link/click here 等）
            // 這類文字 Gemini 翻譯結果不穩定，整塊保護讓結構不被破壞
            if (_placeholderLinkWords.Contains(textPart.Trim()))
                return Store(m.Value);

            // 一般連結：只保護 URL，讓 Gemini 翻譯文字部分
            return $"[{textPart}]({Store(url)})";
        });
    }
}


// ── Translator ────────────────────────────────────────────────────────────────
public class Translator
{
    private const int CooldownMs = 4_000;
    private const int ChunkThreshold = 8_000;
    private const string ModelName = "gemini-3.1-flash-lite-preview";

    // 429 專用：等待 66 秒後重試一次，若還是 429 則切換 key
    private const int QuotaWaitMs = 66_000;

    private readonly string _sourceDir;
    private readonly string _targetDir;
    private readonly bool _force;

    // ── 多 Key 輪替 ──
    private readonly List<string> _apiKeys;
    private int _currentKeyIndex;
    private GenerativeModel _model;

    public Translator(string apiKeyCsv, string sourceDir, string targetDir, bool force)
    {
        _sourceDir = sourceDir;
        _targetDir = targetDir;
        _force = force;

        // 解析逗號分隔的 API keys（單組或多組皆可）
        _apiKeys = (apiKeyCsv ?? "")
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Distinct()
            .ToList();

        if (_apiKeys.Count == 0)
        {
            Console.Error.WriteLine("❌ 沒有提供 GEMINI_API_KEY");
            Environment.Exit(1);
        }

        Console.WriteLine($"🔑 已載入 {_apiKeys.Count} 組 API Key");

        _currentKeyIndex = 0;
        _model = CreateModel(_apiKeys[_currentKeyIndex]);
    }

    private static string MaskKey(string key)
    {
        if (string.IsNullOrEmpty(key)) return "(empty)";
        return key.Length <= 10 ? "..." : $"{key[..8]}...{key[^4..]}";
    }

    private static GenerativeModel CreateModel(string apiKey)
    {
        return new GoogleAI(apiKey)
            .GenerativeModel(model: ModelName);
    }

    /// <summary>
    /// 切換到下一組 API Key。回傳 true 表示成功切換，false 表示所有 key 都用完了。
    /// </summary>
    private bool SwitchToNextKey()
    {
        _currentKeyIndex++;
        if (_currentKeyIndex >= _apiKeys.Count)
            return false;

        Console.WriteLine($"\n  🔄 切換到第 {_currentKeyIndex + 1}/{_apiKeys.Count} 組 API Key（{MaskKey(_apiKeys[_currentKeyIndex])}）");
        _model = CreateModel(_apiKeys[_currentKeyIndex]);
        return true;
    }

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
                .EnumerateFiles(_sourceDir, "*.md", SearchOption.AllDirectories)
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
        const int MaxConsecutiveFailures = 5;   // 連續 5 次翻譯失敗就停（可能是 API 有系統性問題）
        const int PushEvery = 1; // 每翻成功 1 個就 push，避免程式中斷時浪費已翻譯的 token
        bool earlyStop = false;
        var failedFiles = new List<string>();

        for (int i = 0; i < files.Count; i++)
        {
            var sourcePath = files[i];
            var relPath    = Path.GetRelativePath(_sourceDir, sourcePath);
            var targetPath = Path.Combine(_targetDir, relPath);

            Console.WriteLine($"\n[{i + 1}/{files.Count}] {relPath}");

            if (!_force && File.Exists(targetPath))
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
                Console.WriteLine($"   今日已成功：{success}  已略過：{skipped}（使用了 {_currentKeyIndex + 1}/{_apiKeys.Count} 組 Key）");
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

                // 保護 1：連續失敗過多
                if (consecutiveFailures >= MaxConsecutiveFailures)
                {
                    Console.WriteLine($"\n⛔ 連續失敗 {MaxConsecutiveFailures} 次，提早結束以節省 API 配額。");
                    Console.WriteLine($"   已成功：{success}  已略過：{skipped}  失敗：{failed}");
                    Console.WriteLine($"   失敗的檔案清單會寫入 .translation-failed.txt，下次排程會重試。");
                    earlyStop = true;
                    break;
                }

                // 保護 2：整體失敗率過高（至少嘗試 10 個之後才檢查）
                var attempted = success + failed;
                if (attempted >= 10 && (double)failed / attempted > 0.5)
                {
                    Console.WriteLine($"\n⛔ 失敗率過高（{failed}/{attempted} = {(double)failed / attempted:P0}），提早結束以節省 token。");
                    Console.WriteLine($"   已成功：{success}  已略過：{skipped}  失敗：{failed}");
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
        var failedListPath = Path.Combine(_targetDir, ".translation-failed.txt");
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
        CopyNonMarkdownFiles();
    }

    private static int _consecutivePushFailures = 0;
    private const int MaxPushFailures = 3;

    private static async Task PushProgressAsync(int count)
    {
        // 連續 push 失敗超過上限，不再嘗試（避免浪費時間，翻譯結果保留在本地）
        if (_consecutivePushFailures >= MaxPushFailures)
        {
            if (_consecutivePushFailures == MaxPushFailures)
            {
                Console.WriteLine($"\n  🚫 已連續 {MaxPushFailures} 次 push 失敗，後續不再嘗試中途推送。翻譯結果保留在本地，由 workflow 最後統一推送。");
                _consecutivePushFailures++; // 只印一次提示
            }
            return;
        }

        Console.WriteLine($"\n  💾 中途儲存：已完成 {count} 個，推送至 GitHub...");

        // 在 auto-translate branch 上，只有 workflow 自己在操作
        // 不需要 pull，直接 add → commit → push --force

        try { await RunGitAsync("add zh-Hant/"); }
        catch (Exception ex)
        {
            Console.WriteLine($"  ⚠️  git add 失敗，跳過本次推送：{ex.Message}");
            _consecutivePushFailures++;
            return;
        }

        try { await RunGitAsync($"commit -m \"🌐 翻譯進度：已完成 {count} 個檔案\""); }
        catch
        {
            Console.WriteLine("  ℹ️  沒有新變更需要 commit");
            return; // 沒東西 commit 不算失敗
        }

        try
        {
            await RunGitAsync("push origin auto-translate --force");
            Console.WriteLine($"  ✅ 中途推送成功");
            _consecutivePushFailures = 0; // 成功就歸零
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  ⚠️  push 失敗：{ex.Message}");
            _consecutivePushFailures++;
            if (_consecutivePushFailures >= MaxPushFailures)
                Console.WriteLine($"  🚫 已連續 {MaxPushFailures} 次 push 失敗，後續將停止中途推送。");
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

    private void CopyNonMarkdownFiles()
    {
        var nonMdFiles = Directory
            .EnumerateFiles(_sourceDir, "*", SearchOption.AllDirectories)
            .Where(f => !f.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
            .OrderBy(f => f)
            .ToList();

        if (nonMdFiles.Count == 0) return;

        int copied = 0, skipped = 0;
        Console.WriteLine($"\n📁 複製非 .md 檔案...");

        foreach (var sourcePath in nonMdFiles)
        {
            var relPath    = Path.GetRelativePath(_sourceDir, sourcePath);
            var targetPath = Path.Combine(_targetDir, relPath);

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
            // 先在整篇層面做 Placeholder 保護（保護 code block、URL 等）
            var ctx = new PlaceholderContext();
            var protectedContent = ctx.Extract(content);

            string raw;
            if (protectedContent.Length > ChunkThreshold)
            {
                // 大檔案：按標題切段，每段獨立翻譯
                raw = await TranslateInChunksAsync(protectedContent);
            }
            else
            {
                // 小檔案：整篇一次翻
                raw = await TranslateWithRetryAsync(protectedContent);
            }

            // 先還原 placeholder，再做 PostProcess
            translated = PostProcess(ctx.Restore(raw));
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

    // PostProcess 用到的預編譯 Regex
    private static readonly Regex _reHeadFrontMatter = new(
        @"\A---\n[\s\S]*?\n---[ \t]*\n?", RegexOptions.Compiled);
    private static readonly Regex _reFakeFrontMatter = new(
        @"(?m)^---\n(?:[^\n]*:[^\n]*\n)+---[ \t]*\n?", RegexOptions.Compiled);
    private static readonly Regex _reConsecutiveDashes = new(
        @"(?m)^---[ \t]*\n(?=---[ \t]*\n)", RegexOptions.Compiled);
    private static readonly Regex _reDashBeforeHeading = new(
        @"(?m)^---[ \t]*\n(?=#{1,6} )", RegexOptions.Compiled);
    private static readonly Regex _reBlankLineBeforeHeading = new(
        @"(?m)^(?<prev>[^\n].*)\n(?=#{1,6} )", RegexOptions.Compiled);
    private static readonly Regex _reBlankLineBeforeAlert = new(
        @"(?m)^(?<prev>[^>\n].*)\n(?=> \[!)", RegexOptions.Compiled);
    private static readonly Regex _reXrefEn = new(
        @"xref:en/", RegexOptions.Compiled);
    private static readonly Regex _reUidEn = new(
        @"(uid:\s*)en/", RegexOptions.Compiled);

    /// <summary>
    /// 翻譯完成後的後處理：
    /// 1. 移除 Gemini 在分段翻譯時擅自插入的額外 YAML front matter 區塊（非檔案開頭的 --- ... ---）
    /// 2. 移除 Gemini 偶爾吐回的成對孤立 --- 分隔線（來自 prompt 裡的 ---\n{content}\n--- 結構）
    /// 3. xref:en/ → xref:zh-Hant/
    /// 4. uid: en/ → uid: zh-Hant/
    /// 5. 將首段 front matter 的 key 名稱翻成中文
    /// </summary>
    private static string PostProcess(string content)
    {
        // 先統一換行方便 regex 處理
        bool hasCrLf = content.Contains("\r\n");
        content = content.Replace("\r\n", "\n");

        // 1. 先抽出「檔案開頭」的合法 front matter（以 \A--- 開頭）
        //    暫時保留，避免後續步驟誤刪
        string? headFrontMatter = null;
        var headMatch = _reHeadFrontMatter.Match(content);
        if (headMatch.Success)
        {
            headFrontMatter = headMatch.Value;
            content = content[headMatch.Length..];
        }

        // 2. 移除「中間位置」Gemini 擅自生成的偽 front matter 區塊
        //    特徵：以 --- 獨立一行開頭，內含至少一行 key: value，以 --- 獨立一行結尾
        content = _reFakeFrontMatter.Replace(content, "");

        // 3. 移除 Gemini 從 prompt 結構吐回的「成對孤立 ---」
        //    只在它們與區塊邊界相鄰時移除，避免誤刪合法的 Markdown 水平分隔線。
        content = _reConsecutiveDashes.Replace(content, "");
        content = _reDashBeforeHeading.Replace(content, "");

        // 3.5 確保段落結構正確：標題與區塊引用前要有空行
        content = _reBlankLineBeforeHeading.Replace(content, "${prev}\n\n");
        content = _reBlankLineBeforeAlert.Replace(content, "${prev}\n\n");

        // 4. 在「還原 headFrontMatter 之前」先對它做 xref/uid + key 翻譯
        //    這樣後續只要把處理好的 headFrontMatter 貼回去即可，避免切片位移 bug
        if (headFrontMatter != null)
        {
            headFrontMatter = _reXrefEn.Replace(headFrontMatter, "xref:zh-Hant/");
            headFrontMatter = _reUidEn.Replace(headFrontMatter, "$1zh-Hant/");
            headFrontMatter = Regex.Replace(headFrontMatter, @"(?m)^author:(?=\s|$)", "作者:");
            headFrontMatter = Regex.Replace(headFrontMatter, @"(?m)^contributors:(?=\s|$)", "貢獻者:");
            headFrontMatter = Regex.Replace(headFrontMatter, @"(?m)^title:(?=\s|$)", "標題:");
        }

        // 5. body 部分也做 xref/uid 替換（針對內文中的 xref:en/...、uid: en/...）
        content = _reXrefEn.Replace(content, "xref:zh-Hant/");
        content = _reUidEn.Replace(content, "$1zh-Hant/");

        // 6. 還原 headFrontMatter
        if (headFrontMatter != null)
            content = headFrontMatter + content;

        if (hasCrLf)
            content = content.Replace("\n", "\r\n");

        return content;
    }

    // 高頻使用的 Regex（每次翻譯都會跑）
    private static readonly Regex _rePlaceholder = new(
        @"\[\[PROTECT_\d+\]\]", RegexOptions.Compiled);
    private static readonly Regex _reInputMarker = new(
        @"<<<INPUT>>>\s*\n?", RegexOptions.Compiled);
    private static readonly Regex _reEndMarker = new(
        @"<<<END>>>\s*\n?", RegexOptions.Compiled);
    private static readonly Regex _reMarkdownFenceStart = new(
        @"\A\s*```(?:markdown|md)?\s*\n", RegexOptions.Compiled);
    private static readonly Regex _reMarkdownFenceEnd = new(
        @"\n\s*```\s*\z", RegexOptions.Compiled);

    private async Task<string> TranslateWithRetryAsync(string content)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            try
            {
                var translated = await CallGeminiAsync(content);

                // 移除佔位符後再判斷，避免「只有佔位符」的段落誤觸發
                var cleanOriginal   = _rePlaceholder.Replace(content,    "").Trim();
                var cleanTranslated = _rePlaceholder.Replace(translated, "").Trim();

                // 統計原文中「需翻譯的英文字母字元」數量
                var enChars = cleanOriginal.Count(c => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'));
                // 統計譯文中文字元數量
                var zhChars = cleanTranslated.Count(c => c >= 0x4E00 && c <= 0x9FFF);
                // 統計譯文英文字元數量
                var enCharsTranslated = cleanTranslated.Count(c => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'));

                if (enChars > 50)
                {
                    // 條件 1：譯文完全沒有中文
                    var noZh = zhChars < 10;

                    // 條件 2：譯文英文字元相比原文幾乎沒有減少（翻譯根本沒發生）
                    // 正常翻譯後英文字元應大幅減少（保留術語仍會減少 40% 以上）
                    var enReductionRatio = enChars > 0 ? (double)(enChars - enCharsTranslated) / enChars : 1.0;
                    var notTranslated = enChars > 300 && enReductionRatio < 0.4;

                    if (noZh || notTranslated)
                    {
                        var reason = noZh ? "無中文" : $"英文減少量不足({enReductionRatio:P0})";
                        Console.WriteLine($"  ⚠️ 偵測到翻譯不全（{reason}，原文英文:{enChars} 譯文英文:{enCharsTranslated} 中文:{zhChars}），觸發重試...");
                        throw new Exception("Translation failed: Chinese output insufficient relative to English input.");
                    }
                }

                return translated;
            }
            catch (Exception ex) when (IsInvalidKeyError(ex))
            {
                // 當前這組 key 無效（被撤銷、格式錯誤、權限不足），直接切下一組
                Console.WriteLine($"  ⚠️  API Key 無效：{ex.Message[..Math.Min(80, ex.Message.Length)]}");
                if (SwitchToNextKey())
                {
                    return await CallGeminiAsync(content);
                }
                throw new QuotaExhaustedException(
                    $"所有 {_apiKeys.Count} 組 API Key 都無效或已耗盡，請檢查 GEMINI_API_KEY 設定。");
            }
            catch (Exception ex) when (ex.Message.Contains("429") || ex.Message.Contains("quota"))
            {
                // 先等一下再試一次（可能是短暫的速率限制，不是日配額用完）
                Console.WriteLine($"  ⚠️  429 Too Many Requests，等待 {QuotaWaitMs / 1000} 秒後重試...");
                await Task.Delay(QuotaWaitMs);
                try
                {
                    return await CallGeminiAsync(content);
                }
                catch (Exception retryEx) when (retryEx.Message.Contains("429") || retryEx.Message.Contains("quota"))
                {
                    // 這組 key 的日配額確定用完了，嘗試切換下一組
                    if (SwitchToNextKey())
                    {
                        // 切換成功，用新 key 重試
                        return await CallGeminiAsync(content);
                    }
                    // 所有 key 都用完了
                    throw new QuotaExhaustedException(
                        $"所有 {_apiKeys.Count} 組 API Key 的今日配額皆已耗盡，請明天再試。");
                }
            }
        });
    }

    /// <summary>
    /// 判斷是否為 API Key 無效的錯誤（認證/授權失敗）。
    /// </summary>
    private static bool IsInvalidKeyError(Exception ex)
    {
        var msg = ex.Message;
        return msg.Contains("API_KEY_INVALID")
            || msg.Contains("API key not valid")
            || msg.Contains("PERMISSION_DENIED")
            || msg.Contains("401")
            || msg.Contains("403");
    }

    private async Task<string> CallGeminiAsync(string content)
    {
        var prompt = $"""
            {SystemPrompt.Text}
            【任務示範】
            即使段落中包含 [[PROTECT_NNNN]]，也必須翻譯其前後的說明。
            輸入：The [[PROTECT_0001]] is a plugin interface.
            輸出：[[PROTECT_0001]] 是一個外掛介面。
            【目前任務內容】（以下 <<<INPUT>>> 與 <<<END>>> 之間為待翻譯內容，這兩個標記本身不是內容，也不要輸出）
            <<<INPUT>>>
            {content}
            <<<END>>>
            【最終提醒】
            1. 請直接輸出繁體中文翻譯後的 Markdown。禁止保留任何原始英文句子。
            2. 不要輸出 <<<INPUT>>>、<<<END>>> 這兩個標記。
            3. 不要自行新增 YAML front matter（即 --- 開頭與結尾的區塊），除非原文本身就有。
            4. 不要在輸出開頭或結尾加上多餘的 --- 分隔線。
            5. 所有英文的 ## 章節標題、段落說明、清單項目都必須翻譯成繁體中文，不得保留英文原文。
            6. 程式碼區塊（``` 包住的部分）以外，絕對不允許出現整段英文句子或英文段落。
            """;
        var response = await _model.GenerateContent(prompt);
        var text = response.Text;
        if (string.IsNullOrWhiteSpace(text))
            throw new Exception("Gemini 回傳空內容");

        // 偵測輸出是否被截斷（finishReason 不是 STOP）
        var finishReason = response.Candidates?.FirstOrDefault()?.FinishReason;
        if (finishReason?.ToString()?.Contains("MaxTokens", StringComparison.OrdinalIgnoreCase) == true ||
            finishReason?.ToString()?.Contains("MAX_TOKENS", StringComparison.OrdinalIgnoreCase) == true)
        {
            throw new Exception(
                $"Gemini 輸出被截斷（finishReason={finishReason}），段落太大。已輸出 {text.Length} 字元。");
        }

        // 清除 Gemini 可能回吐的 prompt 分隔標記
        text = _reInputMarker.Replace(text, "");
        text = _reEndMarker.Replace(text, "");

        // 清除 Gemini 有時外包的 markdown fence（如 ```markdown ... ```）
        text = _reMarkdownFenceStart.Replace(text, "");
        text = _reMarkdownFenceEnd.Replace(text, "");

        return text;
    }

    private async Task<string> TranslateInChunksAsync(string content)
    {
        var sections = SplitSafely(content);

        var results = new List<string>();
        for (int i = 0; i < sections.Count; i++)
        {
            Console.WriteLine($"    段落 {i + 1}/{sections.Count}（{sections[i].Length:N0} 字元）...");
            results.Add(await TranslateWithRetryAsync(sections[i]));
            if (i < sections.Count - 1)
                await Task.Delay(CooldownMs);
        }
        return string.Join("\n\n", results);
    }

    private static List<string> SplitSafely(string content)
    {
        // 用 ## / ### 標題作為切割點，每個章節獨立翻譯（不合併）
        var rawSections = Regex
            .Split(content, @"(?=^#{2,3} )", RegexOptions.Multiline)
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
            // 超過上限的大段落，在空行處切開
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

        【核心原則】
        1. 使用台灣繁體中文用語，避免中國大陸用語
        2. 技術名詞若台灣業界常用原文（如 API、SDK、cookie），保留原文
        3. 類別名、介面名、方法名、設定鍵、檔案路徑必須原樣保留，不翻譯也不加空格
           例：IPlugin、BasePlugin、INopStartup、Nop.Core、Configure()、plugin.json、App_Data/Install.txt
        4. 保留所有 Markdown 語法（標題、清單、粗體、連結、表格、引用、程式碼區塊）
        5. 程式碼區塊內（``` 包住的部分）一字不改
        6. 遇到 [[PROTECT_NNNN]] 格式的佔位符，原樣保留不動

        【台灣 vs 中國用語對照（嚴格使用台灣用語）】
        軟體（非「软件/软体」）、程式（非「程序」，除非指 Windows「程序」進程）
        資訊（非「信息」）、設定（非「设置/配置」）、預設（非「默认」）
        網路（非「网络/网路」簡體）、介面（非「接口」，除非指程式介面 interface）
        登入 / 登出（非「登陆 / 退出」）、檔案（非「文件」）、資料（非「数据」）
        伺服器（非「服务器」）、註解（非「注释」）、變數（非「变量」）
        函式（非「函数」）、除錯（非「调试」）、部署（非「发布」）
        範本（非「模板」）、驗證（非「验证」）、最佳化（非「优化」）
        使用者（非「用户」，但面向商店的顧客用「顧客」）
        建立（非「创建」）、顯示（非「显示」簡體字型）

        【技術術語對照】
        Provider        → 提供者（非「提供程序」）
        Middleware      → 中介層
        Mapping         → 對應 / 映射
        Dependency Injection → 相依性注入（非「依賴注入」）
        Repository      → Repository（架構層級，保留原文）
        Factory         → 工廠（設計模式）
        Widget          → 小工具（非「小部件」）
        Cache           → 快取（非「緩存」）
        Entity          → 實體
        Service         → 服務

        【nopCommerce 特有術語】
        Topic           → 內容頁面（注意：不是「主題」，Theme 才是佈景主題）
        Theme           → 佈景主題
        Plugin          → 外掛
        Admin area / Admin panel → 後台 / 管理後台
        Storefront      → 前台網站
        Store           → 商店
        Vendor          → 供應商
        Tier Price      → 階梯價格
        Pickup Point    → 取貨點
        Product Attribute → 商品屬性
        Specification Attribute → 規格屬性
        Reward Points   → 紅利點數
        Customer        → 顧客（非「客戶」或「用戶」）
        Message Template → 訊息範本
        Shopping Cart   → 購物車
        Wishlist        → 願望清單
        Checkout        → 結帳
        Gift Card       → 禮品卡
        Coupon Code     → 優惠碼

        【保留原文（不翻譯）】
        - 縮寫：API、SDK、URL、HTTP、HTTPS、JSON、XML、YAML、CSS、HTML、DOM、UI、UX、CLI、IDE、SEO、SKU、JWT、MVC、REST、GDPR、ACL
        - 產品 / 服務名：PayPal、FedEx、UPS、USPS、Swagger、Redis、NuGet、GitHub、Visual Studio、VS Code、Authorize.NET
        - 技術平台：nopCommerce、ASP.NET Core、.NET、Razor、Entity Framework、Blazor、Liquid
        - 版本號、路徑、檔名：3.90、4.60、~/Plugins/、plugin.json、web.config、Description.txt

        【YAML Front Matter 特別處理】
        - uid 行：原樣保留，不翻譯 key 也不翻譯值
        - author / contributors：如果值是 git 使用者名稱（如 git.AndreiMaz），保留原樣
        - title、description 的值可翻譯

        【輸出規則】
        - 直接輸出翻譯後的完整 Markdown，不要加前言、說明或額外的 ``` 包裝
        - 保持原始換行與段落結構
        - 標題、列表、引用區塊（> [!NOTE]、> [!IMPORTANT]、> [!TIP]、> [!WARNING]）的內文也要翻譯
        - 整段逐句翻譯，不可因技術詞多就整段保留英文

        【翻譯範例】

        範例 1 — 類別名混雜敘述：
        原文：The `IPlugin` interface is the base for all plugins. You can create custom widgets by implementing `IWidgetPlugin`.
        ✅ 正確：`IPlugin` 介面是所有外掛的基礎。您可以透過實作 `IWidgetPlugin` 來建立自訂小工具。
        ❌ 錯誤：`IPlugin`介面是所有插件的基础。您可以通过实现`IWidgetPlugin`来创建自定义小部件。
        （錯處：中國用語「插件 / 基础 / 通过 / 实现 / 创建 / 小部件」、類別名前後應有空格分隔）

        範例 2 — UI 路徑翻譯：
        原文：Configure your payment provider in **Admin → Configuration → Payment methods**.
        ✅ 正確：在 **後台 → 設定 → 付款方式** 中設定您的付款提供者。
        ❌ 錯誤：在**Admin → Configuration → Payment methods**中配置您的付款提供程序。
        （錯處：UI 路徑未翻譯、「配置」與「提供程序」是中國用語）

        範例 3 — 引用區塊：
        原文：> [!IMPORTANT]
        > Make sure "Copy local" is set to **False** for third-party assembly references.
        ✅ 正確：> [!IMPORTANT]
        > 請確保第三方組件參考的「Copy local」屬性設為 **False**。
        （注意：`> [!IMPORTANT]` 標記保留；UI 屬性名 "Copy local" 保留原文加中文引號；值 False 保留原文）

        範例 4 — 產品名與連結：
        原文：This plugin allows customers to pay using [PayPal Standard](https://paypal.com).
        ✅ 正確：此外掛允許顧客使用 [PayPal Standard](https://paypal.com) 付款。
        （注意：產品名 PayPal Standard 保留原文、連結結構完整保留、中英文之間加空格）
        """;
}

// ── QuotaExhaustedException ───────────────────────────────────────────────────
public class QuotaExhaustedException(string message) : Exception(message);
