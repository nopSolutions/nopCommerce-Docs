---
標題: appsettings.json 中的設定
uid: zh-Hant/developer/tutorials/appsettings-json-file
作者: git.nopsg
貢獻者: git.nopsg, git.DmitriyKulagin, git.mariannk
---

# appsettings.json 中的設定

## 概覽

本文包含 *appsettings.json* 檔案的說明。我們將解釋此檔案中可用的設定、它們的用途，以及如何使用這些設定來變更 nopCommerce 專案的功能或行為。

>[!IMPORTANT]
>所有設定皆可透過環境變數進行覆寫。

## appsettings.json 檔案概覽

如果您之前曾參與 *ASP.NET Core* 專案，或是熟悉 `ASP.NET Core`，那麼您可能已經使用過 *appsettings.json* 檔案，並對此檔案及其用途有一定程度的了解。

>[!NOTE]
>您也可以從 **設定 → 設定 → App settings** 頁面編輯此檔案。

*appsettings.json* 檔案通常用於儲存應用程式的設定，例如資料庫連接字串、任何應用程式範圍的全域變數以及許多其他資訊。實際上，在 *ASP.NET Core* 中，應用程式設定可以儲存在不同的來源中，例如 *appsettings.json* 檔案、**`appsettings.{EnvironmentName}.json`** 檔案（其中 `{Environment}` 為應用程式當前的託管環境，如 Development、Staging 或 Production）、`User Secrets`（我們通常在此儲存機密資訊）等。

## appsettings.json 檔案中的可用設定

### DataConfig

資料庫的連接透過此區段進行設定。

* **ConnectionString** 此設定需要一個字串值。
  >[!NOTE]
  >連接字串的格式會根據所選的資料庫提供程序而有所不同。
  > 例如，**SqlServer** 的連接字串如下所示：
  >
  >```powershell
  >Data Source={localhost};Initial Catalog={your_data_base_name}};Integrated >Security=False;Persist Security Info=False;User ID={your_user_id}};Password={your_password}};>Trust Server Certificate=True
  >```

* **DataProvider** 您可以指定其中一個支援的資料提供程序：
  * [**SqlServer**](https://www.microsoft.com/sql-server)
  * [**MySql**](https://www.mysql.com/)
  * [**PostgreSQL**](https://www.postgresql.org/)
* **SQLCommandTimeout** 設定終止執行指令嘗試並產生錯誤前的等待時間（以秒為單位）。預設情況下，不會設定逾時，而是使用當前提供程序的預設值。設定為 **`0`** 以使用無限逾時。
* **WithNoLock** 指示是否在 SELECT 語句中加入 NoLock 提示（僅適用於 SQL Server）。

### AzureBlobConfig

>[!IMPORTANT]
>此設定區段適用於 4.80 及以下版本。對於 nopCommerce 4.90 及以上版本，[Azure 設定說明請見此處](xref:zh-Hant/developer/plugins/cloudflare-images)。

我們可以使用 *Azure Blob Storage* 來儲存 Blob 資料。nopCommerce 已整合此功能，我們只需正確設定以下資訊即可使用。當您建立儲存體帳戶時，即可取得這些設定的值。

* **ConnectionString** 此設定需要一個字串值。您需要在此加入您的 `AzureBlobStorage` 連接字串。
* **ContainerName** 此設定的值也是字串類型。在此設定中，我們設定 *Azure BLOB storage* 的容器名稱。
* **EndPoint** 此設定也需要一個字串值。我們需要在此設定 *Azure BLOB storage* 的端點。
* **AppendContainerName** 此設定需要一個布林值。根據建構 URL 時是否將容器名稱附加到 `EndPoint`，將值設定為 **`true`** 或 **`false`**。
* **StoreDataProtectionKeys** 此設定需要一個布林值。如果您想將 *Windows Azure BLOB storage* 用於 *Data Protection Keys*（資料保護金鑰），請將值設定為 **`true`**。
* **DataProtectionKeysContainerName** 此設定需要一個字串值。您需要在此設定一個 Azure 容器名稱，用於儲存 *Data Protection Keys*（此容器應與用於媒體的容器分開，且應為私有）。
* **DataProtectionKeysVaultId (選填)** 此設定也需要一個字串值。如果您需要加密 *Data Protection Keys*，請設定 `Azure key vault ID`。

### CacheConfig

快取設定。

* **DefaultCacheTime** 此設定決定快取資料的存留時間。預設為 **`60`** 分鐘。
* **LinqDisableQueryCache** 指示是否停用查詢的 LINQ 表達式快取。此快取可減少查詢剖析所需的時間，但有一些副作用。例如，快取的 LINQ 表達式可能包含對外部物件的參數引用，如果其他程式碼不再使用這些物件，可能會導致記憶體洩漏。或者，快取存取同步可能會導致比節省的時間更大的延遲。

### CommonConfig

*CommonConfig* 包含用於設定 nopCommerce 本身行為的設定。它是一個 JSON 物件，包含一些可以調整以改變 nopCommerce 行為的設定。

* **DisplayFullErrorStack** 此設定需要一個布林值。預設值為 **`false`**。如果您希望在生產環境中查看完整的錯誤堆疊，可以將值設定為 **`true`**。通常我們不建議這樣做，但如果您有充分的理由在生產環境中顯示完整錯誤，可以透過此設定進行調整。對於開發環境，此設定會被忽略，無論您設定為何，都會顯示完整錯誤。我們可以說，此設定對於開發環境始終是啟用的。
* **UserAgentStringsPath** 此設定儲存 `Browspcap.xml` 檔案的路徑。如檔名所示，`Browscap.xml` 是一個瀏覽器功能資料庫。它本質上是所有已知瀏覽器和機器人的列表，以及它們的預設功能與限制。
  
  ```powershell
  ~/App_Data/browscap.xml
  ```

    >[!NOTE]
    > 在運算領域中，使用者代理程式（User Agent）是代表使用者運作的軟體（軟體代理程式），例如「擷取、呈現並促進終端使用者與 Web 內容互動」的網頁瀏覽器。更多資訊請參閱 [UserAgent](https://en.wikipedia.org/wiki/User_agent)。
* **CrawlerOnlyUserAgentStringsPath** 此設定儲存 `browscap.crawlersonly.xml` 的位置/路徑。它僅儲存「僅爬蟲」（CrawlerOnly）的使用者代理程式。
  
  ```powershell
  ~/App_Data/browscap.crawlersonly.xml
  ```

* **CrawlerOnlyAdditionalUserAgentStringsPath** 此設定儲存 `additional.crawlers.xml` 的位置/路徑。它僅儲存「爬蟲」（Crawlers）的使用者代理程式。
  
  ```powershell
  ~/App_Data/additional.crawlers.xml
  ```

* **UseSessionStateTempDataProvider** 此設定需要一個布林值。此設定的預設值為 **`false`**。如果您想將 `TempData` 儲存在工作階段狀態（Session State）中，您可能需要將值設定為 **`true`**。預設情況下，會使用基於 Cookie 的 `TempData` 提供程序將 `TempData` 儲存在 Cookie 中。
* **ScheduleTaskRunTimeout** 允許您設定排程工作執行的逾時時間（以毫秒為單位）。設定為 **`null`** 以使用預設值。
* **StaticFilesCacheControl** 指定靜態內容 'Cache-Control' 標頭的值（以秒為單位）。

  ```powershell
  public,max-age=31536000
  ```

* **ServeUnknownFileTypes** 設定指定一個值，指示是否服務未識別內容類型的檔案。預設值為 **`false`**。
* **UseAutofac** 指示是否使用 *Autofac IoC 容器*的值。如果停用，則會使用預設的 *.Net IoC 容器*。
* **PermitLimit** 在一個時間視窗（1 分鐘）內允許的最大許可計數器數量。在這些選項傳遞給 `FixedWindowRateLimiter` 的建構函式時，必須設定為 `> 0` 的值。如果設定為 **`0`**，則關閉限制。
* **QueueCount** 佇列採購請求的最大累計許可計數。在這些選項傳遞給 `FixedWindowRateLimiter` 的建構函式時，必須設定為 `>= 0` 的值。如果設定為 **`0`**，則關閉佇列。
* **RejectionStatusCode** 當請求被拒絕時，在回應上設定的預設狀態碼。

### DistributedCacheConfig

分散式快取是由多個應用程式伺服器共用的快取，通常作為存取它的應用程式伺服器之外部服務來維護。分散式快取可以提高 ASP.NET Core 應用程式的效能和擴充性，特別是在應用程式由雲端服務或伺服器陣列託管時。

* **DistributedCacheType** 您可以選擇下列其中一種實作方式：
  * **Memory** - 這是框架提供的 `IDistributedCache` 實作，將項目儲存在記憶體中。分散式記憶體快取並非真正的分散式快取。快取的項目由執行應用程式的伺服器上的應用程式執行個體儲存。
  * **SQL Server** - 分散式 SQL Server 快取實作允許分散式快取使用 SQL Server 資料庫作為其後端儲存。若要在 SQL Server 執行個體中建立 SQL Server 快取項目表，您可以使用 SQL-cache 工具。該工具會以您指定的名稱和結構描述建立一個表。建議為此目的使用單獨的資料庫。
  
    ```sh
    dotnet sql-cache create "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DistCache;Integrated   Security=True;" dbo nopCache
    ```

  * **Redis** - nopCommerce 開箱即支援 *Redis*。若要啟用應用程式中的 *Redis*，您必須設定相應的設定。關於 [Redis](https://azure.microsoft.com/documentation/articles/cache-dotnet-how-to-use-azure-redis-cache) 的更多資訊。
  * **Redis Synchronized Memory** - nopCommerce 4.70 版引入了一種新的快取方式，透過 *Redis* 中的訊息機制進行同步。此選項效能極高，因為快取本身儲存在記憶體中，而 *Redis* 僅用作同步器。若要在應用程式中啟用此選項，您必須設定與基礎 *Redis* 快取相同的設定。
  
* **Enabled** 此設定需要一個布林值。如果您想啟用 `Distributed cache`，請將值設定為 **`true`**。系統預設使用 In-Memory 快取，因此此設定用於指示我們是否應該使用 `Distributed Cache` 進行快取，而不是預設的 `in-memory caching`。因此，如果您想使用例如 *Redis* 進行快取，請使用此設定。
* **ConnectionString (選填)** 此設定僅與 *Redis* 或 *SQL Server* 結合使用。此設定需要一個字串值。此設定的預設值為

  ```powershell
  127.0.0.1:{PORT},ssl=False
  ```

* **SchemaName (選填)** 此設定僅與 `SQL Server` 結合使用。
* **TableName (選填)** 此設定僅與 `SQL Server` 結合使用。SQL Server 資料庫名稱。
* **InstanceName (選填)** 指定執行個體名稱（預設為 "nopCommerce"）。
* **PublishIntervalMs (選填)** 指定發佈金鑰變更事件的時間間隔（以毫秒為單位）。

### HostingConfig

Hosting 包含用於設定託管行為的設定。它是一個 JSON 物件，包含一些可以調整以改變託管行為的屬性設定。

* **UseProxy** 此設定需要一個布林值。啟用此設定以將轉發標頭（Forwarded Headers）套用到目前 HTTP 請求上的對應欄位。
* **ForwardedProtoHeaderName** 此設定需要一個字串值。指定自訂 HTTP 標頭名稱，用於識別用戶端連接到您的代理伺服器或負載平衡器時所使用的協定（HTTP 或 HTTPS）。
* **ForwardedForHeaderName** 此設定需要一個字串值。指定自訂 HTTP 標頭名稱，用於確定來源 IP 位址（例如 **`CF-Connecting-IP`**，**`X-ProxyUser-Ip`**）。
* **KnownProxies** 此設定需要一個字串值。指定 IP 位址列表（以逗號分隔）以接受轉發標頭。
* **KnownNetworks** 此設定需要一個字串值。指定 IP CIDR 標記法列表（以逗號分隔）以接受轉發標頭。例如 172.64.0.0/13,162.158.0.0/15

### InstallationConfig

它包含用於設定 nopCommerce 在安裝期間行為的設定。

* **DisableSampleData** 此設定需要一個布林值。此設定指示商店擁有者在安裝期間是否可以安裝範例資料。如果您不希望商店擁有者在安裝期間安裝範例資料，只需將此設定的值設為 **`true`**。
* **DisabledPlugins** 此設定需要一個字串值。指定安裝期間忽略的外掛列表（以逗號分隔）。
* **InstallRegionalResources** 此設定需要一個布林值。此設定允許在安裝期間選擇額外的語言資源。國家的選擇決定了將套用到商店的設定（匯率、稅率、度量單位等區域性功能）。

### PluginConfig

* **UseUnsafeLoadAssembly** 此設定需要一個布林值。如果您想將組件載入到 load-from 內容中並繞過某些安全性檢查，您可能需要將值設定為 **`true`**。

### WebOptimizer

我們使用 [WebOptimizer](https://github.com/ligershark/WebOptimizer) 工具來進行 *CSS* 和 *JavaScript* 程式碼的縮減（Minification）與合併（Bundling），這是一個 *ASP.NET Core* 中介軟體。最佳化在執行階段執行，並透過伺服器端和用戶端快取來實現高效能。

* **EnableJavaScriptBundling** 此設定需要一個布林值。如果您希望啟用 JS 檔案合併與縮減，可以將其設定為 **`true`**。
* **EnableCssBundling** 此設定需要一個布林值。如果您希望啟用 CSS 檔案合併與縮減，可以將其設定為 **`true`**。
* **JavaScriptBundleSuffix** 此設定需要一個字串值。您可以為產生的 Bundle 的 js 檔名設定後綴（預設為 **`.scripts`**）。
* **CssBundleSuffix** 此設定需要一個字串值。您可以為產生的 Bundle 的 CSS 檔名設定後綴（預設為 **`.styles`**）。
* **EnableCaching** 此設定需要一個布林值。您可以設定一個值來指示是否啟用伺服器端快取（預設為 **`true`**）。
* **EnableMemoryCache** 此設定需要一個布林值。您可以設定一個值來指示是否啟用基於 *Microsoft.Extensions.Caching.Memory.IMemoryCache* 的快取（預設為 **`true`**）。
* **EnableDiskCache** 此設定需要一個布林值。決定管線資產是否快取到磁碟。這可以透過從磁碟載入管線資產而不是重新執行管線來加速應用程式重新啟動。在開發模式下停用可能會有幫助。
* **EnableTagHelperBundling** 此設定需要一個布林值。您可以設定是否啟用合併（預設為 **`false`**）。
* **CdnUrl** 此設定需要一個字串值。您可以設定用於 TagHelpers 的 CDN URL（預設為 **`null`**）。
* **CacheDirectory** 此設定需要一個字串值。如果 **EnableDiskCache** 為 **`true`**，則設定資產儲存的目錄（預設為 **`{ContentRootPath}\\wwwroot\\bundles`**）。
* **AllowEmptyBundle** 此設定需要一個布林值。您可以設定是否允許產生空 Bundle 而不是拋出例外（預設為 **`true`**）。
* **HttpsCompression** 此設定需要一個整數值。當回應壓縮（Response Compression）中介軟體可用時，您可以設定一個值來指示是否應針對 HTTPS 請求壓縮檔案。預設值為 **`2`**。您可以選擇下列其中一種實作方式：
  * **1** - 選擇不在 HTTPS 上進行壓縮。
  * **2** - 選擇在 HTTPS 上進行壓縮。
    >[!NOTE]
    > 在 HTTPS 請求上啟用遠端可操作內容的壓縮可能會產生安全性問題。
* **MemoryCacheTimeToLive** 如果啟用了 *EnableMemoryCache* 記憶體快取，此設定控制項目在記憶體中的儲存時間。預設為 60 分鐘。