---
標題: 使用原始碼與貢獻
uid: zh-Hant/developer/contribute/source-code
作者: git.AndreiMaz
貢獻者: git.RomanovM, git.exileDev
---

# 使用原始碼與貢獻

## 簽出原始碼

nopCommerce 在 GitHub 上維護著儲存庫 ([https://github.com/nopSolutions/nopCommerce](https://github.com/nopSolutions/nopCommerce))。因此，您可以隨時簽出最新的原始碼！Git SCM (原始碼管理) 的存取權限是公開的，讓您可以即時獲取 nopCommerce 的最新版本！這讓您能跟上 nopCommerce 每日的開發進度與改進。您不必等待下一個發行版本，即可取得最新的修補程式與修正。如果您不熟悉 Git，可以在[此處](https://git-scm.com/docs)找到優質且免費的文件。此外，關於 GitHub 支援的更多資訊請見[此處](https://opensource.guide/how-to-contribute/)。請注意，這些版本不應在生產環境中使用。我們不保證我們 SCM (原始碼管理) 中出現的任何功能或程式碼都會包含在正式發行版本中。獲取原始碼的最佳方式是複製 (clone) 儲存庫。Git 內建了用於提交 (git-gui) 和瀏覽 (gitk) 的 GUI 工具，但也有許多第三方工具可供偏好特定平台體驗的使用者選擇。請在 [https://git-scm.com/downloads/guis](https://git-scm.com/downloads/guis) 找到它們（我們使用 [SourceTree](https://www.sourcetreeapp.com/)）。

## 分支說明與命名

我們近期開始採用 Vincent Driessen 的分支模型（詳見此處：[http://nvie.com/posts/a-successful-git-branching-model/](https://nvie.com/posts/a-successful-git-branching-model/)），包括使用功能分支 (feature branches)、開發分支 (development branch，用於整合) 以及主分支 (master branch，用於發布/生產環境)。在此之前（2016 年 1 月前），我們僅有一個 "master" 分支。

* 生產分支：master
* 開發分支：develop
* 工作項目 (問題) 分支：應以 "issue" 開頭，後接問題 ID（根據我們的 Github 問題清單）以及一些易讀的名稱（例如 "multistore"）。最終看起來應為 "issue-35-paypal-redirection-bug"
* 發布分支：應以 "release" 開頭，後接版本號（例如 "3.00"）。最終看起來應為 "release-3.00"

## Fork 與提交 Pull Request

如果您想為 nopCommerce 核心貢獻原始碼（修正問題或開發新功能），請遵循以下方式。以下是貢獻步驟的簡表：

* 首先，您必須建立一個 fork。請至 GitHub 了解更多關於儲存庫 fork 的資訊：[https://help.github.com/articles/fork-a-repo/](https://help.github.com/articles/fork-a-repo/)。
* 在本機複製 (Clone) 該儲存庫。
* 從 "develop" 分支建立一個新分支。請務必為每個貢獻建立一個新分支。您應該僅從我們的 "develop" 分支進行建立。不要使用 "master"。
* 編寫程式碼並推送到您的 GitHub fork。
* 建立 pull request。請至 [https://help.github.com/articles/using-pull-requests/](https://help.github.com/articles/using-pull-requests/) 閱讀更多相關資訊。在執行此操作前，請務必先與我們的儲存庫進行同步。