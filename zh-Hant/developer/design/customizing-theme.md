---
標題: 自訂 nopCommerce 佈景主題
uid: zh-Hant/developer/design/customizing-theme
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.exileDev
---

# 自訂 nopCommerce 佈景主題

## 上傳商店標誌 (Logo)

若要將商店標誌上傳到 nopCommerce 網站，有兩種方法：

### 第一種方法

透過後台管理上傳您的標誌。請參閱 [上傳您的標誌](xref:zh-Hant/getting-started/design-your-store/uploading-your-logo) 文章了解操作方式。

### 第二種方法

1. 前往 nopCommerce 根目錄下的 `/Themes/YOUR THEME/Content/images/`
1. 找到 `logo.png` 影像檔
1. 將該 `logo.png` 取代為您的商店標誌，並將其命名為 `logo.png`（維持相同的寬度：250px 與高度：50px）

如果您希望對標誌的樣式表進行更改，請在您的 `styles.css` 中尋找以下程式碼：

```css
.header-logo {
    margin: 0 0 20px;
    text-align: center;
}
.header-logo a {
    display: inline-block;
    max-width: 100%;
    line-height: 0; /*firefox line-height bug fix*/
}
.header-logo a img {
    max-width: 100%;
    opacity: 1;
}
```

> [!IMPORTANT]
> 您可能需要清除瀏覽器快取（或使用 Ctrl+F5 鍵重新整理頁面）才能看到變更後的內容（新的商店標誌）。

## 如何更改版面配置

1. 如果您想要自訂或變更 nopCommerce 網站的基礎版面配置（例如 `_Root.cshtml`），請在您的 `styles.css` 檔案中尋找以下 CSS 程式碼：

    ```css
    .master-wrapper-content {
        position: relative;
        z-index: 0;
        width: 90%;
        margin: 0 auto;
    }
    .master-column-wrapper {
        position: relative;
        z-index: 0;
    }
    .master-column-wrapper:after {
        content: "";
        display: block;
        clear: both;
    }
    ```

1. 如果您想要自訂或變更 `_ColumnOne.cshtml` 的版面配置，請在您的 `style.css` 中尋找以下 CSS 程式碼：

    ```css
    .center-1 {
        margin: 0 0 100px;
    }
    ```

1. 如果您想要自訂或變更 `_ColumnTwo.cshtml` 的版面配置，請在您的 `style.css` 中尋找以下 CSS 程式碼：

    ```css
    .center-2, .side-2 {
        margin: 0 0 50px;
    }
    .side-2:after {
        content: "";
        display: block;
        clear: both;
    }
    ```

## 如何變更頁首選單（頂部選單）

1. 如果您想要自訂或變更 nopCommerce 網站的頁首選單（頂部選單），請前往以下位置：

    前往 nopCommerce 根目錄下的 `/Views/Shared/Components/TopMenu/Default.cshtml`
1. 開啟 `Default.cshtml` 檔案 - 您可以根據需求在 `<li>` 中新增或移除選單項目。

## 如何變更頁尾（或頁尾連結）

1. 如果您想要自訂或變更 nopCommerce 網站的頁尾（或頁尾連結），請前往以下位置：

    前往 nopCommerce 根目錄下的 `/Views/Shared/Components/Footer/Default.cshtml`
1. 開啟 `Default.cshtml` 檔案 - 您可以根據需求在 `<li>` 中新增或移除連結，或是變更整個 `<ul>`。