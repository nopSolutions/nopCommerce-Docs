---
標題: robots.txt
uid: zh-Hant/getting-started/advanced-configuration/robots-txt
作者: git.DmitriyKulagin
---

# robots.txt 設定

若要管理 *robots.txt* 設定，請前往 **組態 → 設定 → 一般設定**。

此頁面支援多商店設定；這意味著可以為所有商店定義相同的設定，或為不同商店設定不同的值。如果您想管理特定商店的設定，請從多商店設定下拉選單中選擇該商店名稱，並勾選左側所需的核取方塊以設定自訂值。有關更多詳細資訊，請參閱 [多商店](xref:zh-Hant/getting-started/advanced-configuration/multi-store)。

## robots.txt

*robots.txt* 檔案會告知搜尋引擎爬蟲您的網站中哪些 URL 是允許存取的。這主要用於避免網站因過多的請求而不堪負荷；它並非將網頁從 Google 搜尋結果中排除的機制。若要讓網頁不被 Google 收錄，請使用 `noindex` 標籤封鎖索引，或是為頁面設定密碼保護。

請依照下列方式定義 *robots.txt* 設定：
![Security](_static/robots-txt/robots-txt.jpg)

- **允許 sitemap.xml** - 勾選此項以允許機器人存取 sitemap.xml 檔案。
- **不允許的語言** - 設定不允許存取的語言清單。若您不想對特定語言的機器人進行限制，請將此欄位留空。
- **不允許的路徑** - 設定不允許存取的路徑清單。
- **可在地化的不允許路徑** - 設定可在地化的不允許存取路徑清單。
- **附加規則** - 輸入 robots.txt 檔案的額外規則。這些規則是用於指示爬蟲關於您網站中哪些部分可以爬取的指令。請閱讀此頁面關於 [Google 對 robots.txt 規範的解讀](https://developers.google.com/search/docs/crawling-indexing/robots/robots_txt)，以獲取各項規則的完整說明。

> [!NOTE]
>
> 您也可以透過在網站的 wwwroot 目錄中新增 *robots.additions.txt* 檔案，來擴充 robots.txt 的資料。