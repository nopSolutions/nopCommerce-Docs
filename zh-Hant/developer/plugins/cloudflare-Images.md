---
標題: Cloudflare Images
uid: zh-Hant/developer/plugins/cloudflare-images
作者: git.DmitriyKulagin
貢獻者: git.DmitriyKulagin
---

# Cloudflare Images 整合

## 概述

從 4.90 版本開始，縮圖管理機制已進行重構以提供更高的靈活性。我們引入了新的介面 `IThumbService` 來處理先前由 `IPictureService` 所管理的所有縮圖相關操作。

以下方法已移至 `IThumbService`：

- `GetThumbLocalPathAsync`
- `GeneratedThumbExistsAsync`
- `SaveThumbAsync`
- `GetThumbLocalPathByFileNameAsync`
- `GetThumbUrlAsync`
- `DeletePictureThumbsAsync`

作為此變更的一部分，縮圖儲存邏輯已從核心應用程式中解耦，拆分為獨立的外掛。現有的 **Azure Blob Storage** 功能現在已成為獨立的外掛。此外，我們開發了一個新的外掛，將 **Cloudflare Images** 整合為縮圖儲存解決方案。

> [!NOTE]
>
> 預設情況下，Azure Blob 與 Cloudflare Images 外掛（`Misc.AzureBlob` 與 `Misc.CloudflareImages`）皆列於 `InstallationConfig.DisabledPlugins` 屬性中。這確保了這些外掛在全新安裝時不會被自動啟用，讓商店擁有者可以根據自身需求選擇並安裝適合的儲存解決方案。

## 設定 Azure Blob Storage

我們可以使用 *Azure Blob Storage* 來儲存 Blob 資料。nopCommerce 已經內建了此功能的整合，您只需要正確設定以下資訊即可使用。這些設定值可以在您於 *Azure* 建立儲存體帳戶時取得。

![Image](_static/cloudflare-images/azure-blob.png)

- **ConnectionString** 此設定需要一個字串值。您需要在此輸入您的 `AzureBlobStorage` 連接字串。
- **ContainerName** 此設定的值亦為字串類型。在此設定中，我們指定 *Azure BLOB storage* 的容器名稱。
- **EndPoint** 此設定同樣需要一個字串值。我們需要在此設定 *Azure BLOB storage* 的端點（Endpoint）。
- **AppendContainerName** 此設定需要一個布林值。請根據建構 URL 時是否需要將容器名稱附加到 `EndPoint` 後方，將此值設為 **`true`** 或 **`false`**。

## 設定 Cloudflare Images

除了總開關外，外掛設定非常直觀，僅包含四個欄位。

![Image](_static/cloudflare-images/cloudflare-images.png)

其中一個關鍵欄位是 **Delivery URL**（發佈 URL）。它必須按照以下特定格式進行設定：

```bash
https://imagedelivery.net/[account_hash]/<image_id>/<variant_name>
```

此 URL 作為範本使用。外掛將會動態插入所需的 `image_id` 與 `variant_name`，以產生網站上所顯示縮圖的最終 URL。

## 使用方式

一旦完成設定並啟用，該外掛就會在背景自動運作，其機制類似於 Azure Blob Storage 整合。它會無縫處理：

- 將新產生的縮圖上傳至 Cloudflare Images 服務。
- 在所有前台頁面中，將原本的本地縮圖 URL 取代為對應的 Cloudflare Images URL。