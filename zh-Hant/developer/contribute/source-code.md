---
標題: 使用原始碼與參與貢獻
uid: zh-Hant/developer/contribute/source-code
作者: git.AndreiMaz
貢獻者: git.RomanovM, git.exileDev
---

# 使用原始碼與參與貢獻

## 檢出原始碼

nopCommerce 在 GitHub 上維護一個儲存庫 ([https://github.com/nopSolutions/nopCommerce](https://github.com/nopSolutions/nopCommerce))。因此，您可以隨時檢出最新的原始碼！Git SCM (原始碼管理) 存取權限是公開的，允許您即時獲取 nopCommerce 的最新版本！這讓您可以跟隨 nopCommerce 每日的開發進度與改進。無需等待下一個版本發布，即可取得最新的修補程式與修復。如果您不熟悉 Git，這裡有很棒且免費的文件可供參考 [這裡](https://git-scm.com/docs)。此外，您可以在 [這裡](https://opensource.guide/how-to-contribute/) 找到更多關於 GitHub 支援的資訊。請注意，這些版本不應在生產環境中使用。我們無法保證在我們的 SCM (原始碼管理) 中找到的任何功能或程式碼都會出現在我們的正式版本中。取得原始碼的最佳方式是複製 (clone) 儲存庫。Git 內建了用於提交 (git-gui) 和瀏覽 (gitk) 的 GUI 工具，但對於尋求特定平台體驗的使用者，還有許多第三方工具可供選擇。請前往 [https://git-scm.com/downloads/guis](https://git-scm.com/downloads/guis) 尋找（我們使用 [SourceTree](https://www.sourcetreeapp.com/)）。

## 分支描述與命名

最近我們開始使用 Vincent Driessen 的分支模型（請見此處：[http://nvie.com/posts/a-successful-git-branching-model/](https://nvie.com/posts/a-successful-git-branching-model/)），包括使用功能分支 (feature branches)、開發分支 (development branch，用於整合) 和主分支 (master branch，用於發布/生產)。在此之前（直到 2016 年 1 月），我們僅有一個 "master" 分支。

* 生產分支：master
* 開發分支：develop
* 工作項目 (問題) 分支：應以 "issue" 開頭。後接問題 ID（根據我們的 Github 問題清單）以及易讀的名稱（例如 "multistore"）。最終名稱看起來應像這樣："issue-35-paypal-redirection-bug"
* 發布分支：應以 "release" 開頭。後接版本號（例如 "3.00"）。最終名稱看起來應像這樣："release-3.00"

## Fork 與提交 Pull Request

如果您想為 nopCommerce 核心貢獻一些原始碼（修復問題或新增功能），則應遵循以下方法。以下是貢獻步驟的簡表：

* 首先，您必須建立一個 fork。請在 GitHub 上了解更多關於儲存庫 fork 的資訊：[https://help.github.com/articles/fork-a-repo/](https://help.github.com/articles/fork-a-repo/)。
* 在本地端進行 clone。
* 從 "develop" 分支建立一個新分支。請務必為每一項貢獻建立一個新分支。您只能從我們的 "develop" 分支進行建立。請勿使用 "master"。
* 編寫程式碼並推送到您的 GitHub fork。
* 建立 Pull Request。請閱讀更多相關資訊：[https://help.github.com/articles/using-pull-requests/](https://help.github.com/articles/using-pull-requests/)。在執行此動作前，請務必先與我們的儲存庫進行同步。