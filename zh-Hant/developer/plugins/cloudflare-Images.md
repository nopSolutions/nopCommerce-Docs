---
標題: Cloudflare Images
uid: zh-Hant/developer/plugins/cloudflare-images
作者: git.DmitriyKulagin
貢獻者: git.DmitriyKulagin
---

# Cloudflare Images 整合

## 概述

從 4.90 版本開始，縮圖管理機制已進行重構以提升靈活性。系統引入了全新的 `IThumbService` 介面，用於處理先前由 `IPictureService` 管理的所有縮圖相關操作。

以下方法已移至 `IThumbService`：

- `GetThumbLocalPathAsync`
- `GeneratedThumbExistsAsync`
- `SaveThumbAsync`
- `GetThumbLocalPathByFileNameAsync`
- `GetThumbUrlAsync`
- `DeletePictureThumbsAsync`

作為此變更的一部分，縮圖儲存邏輯已從核心應用程式中解耦，改為獨立的外掛。現有的 **Azure Blob Storage** 功能現在是一個獨立的外掛。同時，官方開發了一個新的外掛，將 **Cloudflare Images** 整合為縮圖儲存解決方案。

> [!NOTE]
>
> 預設情況下，Azure Blob 和 Cloudflare Images 外掛（`Misc.AzureBlob` 與 `Misc.CloudflareImages`）都會列在 `InstallationConfig.DisabledPlugins` 屬性中。這確保了這些外掛不會在全新安裝時自動啟用，讓商店管理者能自行選擇並安裝最符合需求的儲存解決方案。

## 設定 Azure Blob Storage

我們可以使用 *Azure Blob Storage* 來儲存 Blob 資料。nopCommerce 已經內建了此功能的整合，您只需要正確設定以下資訊即可使用。這些設定值可以在您建立 *Azure* 儲存體帳戶時取得。

![Image](_static/cloudflare-images/azure-blob.png)

- **ConnectionString** 此設定需要一個字串值。您需要在此處填入您的 `AzureBlobStorage` 連接字串。
- **ContainerName** 此設定的值同樣為字串類型。在此處設定 *Azure BLOB storage* 的容器名稱。
- **EndPoint** 此設定同樣需要一個字串值。在此處設定 *Azure BLOB storage* 的端點。
- **AppendContainerName** 此設定需要一個布林值。請根據建構 URL 時是否需要將容器名稱附加到 `EndPoint` 後方，將值設定為 **`true`** 或 **`false`**。

## 設定 Cloudflare Images

外掛設定非常簡單，除了主啟用/停用開關外，還包含四個欄位。

![Image](_static/cloudflare-images/cloudflare-images.png)

其中一個關鍵欄位是 **Delivery URL**（傳遞 URL）。它必須按照以下特定格式進行設定：

```bash
https://imagedelivery.net/[account_hash]/<image_id>/<variant_name>
```

此 URL 作為範本使用。外掛將會動態插入所需的 `image_id` 和 `variant_name`，以產生顯示在網站上縮圖的最終 URL。

## 使用方式

一旦設定完成並啟用，該外掛就會在背景自動運作，運作方式與 Azure Blob storage 整合類似。它會流暢地處理以下事項：

- 將新縮圖上傳至 Cloudflare Images 服務。
- 在所有公開頁面上，將本機縮圖 URL 取代為對應的 Cloudflare Images URL。