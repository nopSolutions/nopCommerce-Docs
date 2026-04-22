---
標題: 理解佈局 / 設計
uid: zh-Hant/developer/design/understanding-layout
作者: git.AndreiMaz
貢獻者: git.exileDev, git.DmitriyKulagin
---

# 理解佈局 / 設計

什麼是佈局（Layout）？每位網站開發人員或設計師都希望在網站的所有頁面中維持一致的外觀與風格。過去，ASP.NET 2.0 引入了「主版頁面（Master Pages）」的概念，透過將其與 .aspx 頁面進行映射，有助於維護網站的一致性。

Razor 也支援類似的概念，稱為「佈局」。它允許您定義一個通用的網站範本，然後讓網站上的所有檢視（View）/頁面繼承其外觀與風格。

在 nopCommerce 中，有兩種不同的佈局：

* `_ColumnsOne.cshtml`
* `_ColumnsTwo.cshtml`

這兩種佈局皆繼承自一個名為 `_Root.cshtml` 的主佈局。`_Root.cshtml` 本身則繼承自 `_Root.Head.cshtml`。如果您需要連結 CSS 樣式表或 jQuery 檔案，`_Root.Head.cshtml` 就是您需要查看的檔案（您可以在此處新增/連結更多的 `.css` 和 `.js` 檔案）。nopCommerce 中所有這些佈局的位置如下：`[nopCommerce 根目錄]/Views/Shared/...`。如果您使用的是原始碼版本，則路徑為：`\Presentation\Nop.Web\Views\Shared\...`

* **`_Root.cshtml` 的佈局**

    ![root-layout](_static/understanding-layout/root-layout.jpg)

* **`_Root.cshtml` 的佈局（關於 CSS 類別）**

    ![root-layout-css](_static/understanding-layout/root-layout-css.jpg)

現在，以下兩種佈局會覆寫 `_Root.cshtml` 的主體內容：

* `_ColumnsOne.cshtml`

    在此情況下，主體佈局沒有變化，因此結構與 `_Root.cshtml` 幾乎相同：

    ![columns-one](_static/understanding-layout/column-one.jpg)

* `_ColumnsTwo.cshtml`

    在此情況下，主體結構中有兩欄：

    ![column-two](_static/understanding-layout/column-two.jpg)