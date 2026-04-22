---
標題: Cloudflare Images
uid: zh-Hant/developer/plugins/cloudflare-images
作者: git.DmitriyKulagin
貢獻者: git.DmitriyKulagin
---

# Cloudflare Images 整合

## 概述

從 4.90 版本開始，縮圖管理經過了重構，以實現更高的靈活性。我們引入了一個新的介面 `IThumbService`，用於處理先前由 `IPictureService` 管理的所有與縮圖相關的操作。

以下方法已移至 `IThumbService`：

- `GetThumbLocalPathAsync`
- `GeneratedThumbExistsAsync`
- `SaveThumbAsync`
- `GetThumbLocalPathByFileNameAsync`
- `GetThumbUrlAsync`
- `DeletePictureThumbsAsync`

作為此變更的一部分，縮圖儲存邏輯已從核心應用程式中解耦，轉而成為獨立的外掛。現有的 **Azure Blob Storage** 功能現在已成為一個獨立的外掛。我們開發了一個新的外掛，將 **Cloudflare Images** 整合為一種縮圖儲存解決方案。

> [!NOTE]
>
> 預設情況下，Azure Blob 和 Cloudflare Images 外掛（`Misc.AzureBlob` 和 `Misc.CloudflareImages`）會被列在 `InstallationConfig.DisabledPlugins` 屬性中。這確保了這些外掛不會在全新安裝期間自動安裝，讓商店擁有者可以選擇並安裝最符合其需求的儲存解決方案。

## 設定 Azure Blob Storage

我們可以使用 *Azure Blob Storage* 來儲存二進位大型物件（Blob）資料。nopCommerce 已經內建了此功能，我們只需要正確設定以下資訊即可使用。這些設定值可以在您建立 *Azure* 儲存體帳戶時取得。

![Image](_static/cloudflare-images/azure-blob.png)

- **ConnectionString**：此設定需要一個字串值。您需要在這裡填入您的 `AzureBlobStorage` 連接字串。
- **ContainerName**：此設定的值同樣為字串類型。在此設定中，我們指定 *Azure BLOB storage* 的容器名稱。
- **EndPoint**：此設定同樣需要一個字串值。我們需要在此設定 *Azure BLOB storage* 的端點（Endpoint）。
- **AppendContainerName**：此設定需要一個布林值。根據在建構 URL 時是否需要將容器名稱附加到 `EndPoint` 後方，將此值設為 **`true`** 或 **`false`**。

## 設定 Cloudflare Images

此外掛的設定相當簡單，除了主要的啟用/停用開關外，還包含四個欄位。

![Image](_static/cloudflare-images/cloudflare-images.png)

一個關鍵欄位是 **Delivery URL（傳遞 URL）**。它必須以以下特定格式進行設定：

```bash
https://imagedelivery.net/[account_hash]/<image_id>/<variant_name>
```

此 URL 作為範本使用。此外掛將動態插入所需的 `image_id` 和 `variant_name`，以產生顯示在網站上之縮圖的最終 URL。

## 使用方式

一旦設定並啟用，此外掛就會自動在背景運作，類似於 Azure Blob storage 的整合。它能流暢地處理：

- 將新縮圖上傳至 Cloudflare Images 服務。
- 在所有面向公眾的頁面上，將本地縮圖 URL 取代為對應的 Cloudflare Images URL。