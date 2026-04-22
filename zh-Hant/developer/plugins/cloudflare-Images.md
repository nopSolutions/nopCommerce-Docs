---
標題: Cloudflare Images
uid: zh-Hant/developer/plugins/cloudflare-images
作者: git.DmitriyKulagin
貢獻者: git.DmitriyKulagin
---

# Cloudflare Images 整合

## 概述

從 4.90 版本開始，縮圖管理已進行重構以實現更高的靈活性。我們引入了一個新的介面 `IThumbService`，用於處理先前由 `IPictureService` 管理的所有縮圖相關操作。

以下方法已移至 `IThumbService`：

- `GetThumbLocalPathAsync`
- `GeneratedThumbExistsAsync`
- `SaveThumbAsync`
- `GetThumbLocalPathByFileNameAsync`
- `GetThumbUrlAsync`
- `DeletePictureThumbsAsync`

作為此變更的一部分，縮圖儲存邏輯已從核心應用程式中解耦，改為獨立的外掛。現有的 **Azure Blob Storage** 功能現在已成為獨立的外掛。此外，我們開發了一個新的外掛，將 **Cloudflare Images** 整合為縮圖儲存解決方案。

> [!NOTE]
>
> 預設情況下，Azure Blob 和 Cloudflare Images 外掛（`Misc.AzureBlob` 和 `Misc.CloudflareImages`）都會列在 `InstallationConfig.DisabledPlugins` 屬性中。這能確保在全新安裝時不會自動安裝這些外掛，讓商店管理員可以根據需求選擇並安裝適合的儲存解決方案。

## 設定 Azure Blob Storage

我們可以使用 *Azure Blob Storage* 來儲存 Blob 資料。nopCommerce 已經內建了此功能，只需正確設定以下資訊即可使用。當您建立儲存體帳戶時，可以從 *Azure* 獲得這些設定值。

![Image](_static/cloudflare-images/azure-blob.png)

- **ConnectionString**：此設定需要一個字串值。在此處，您需要填入您的 `AzureBlobStorage` 連接字串。
- **ContainerName**：此設定的值同樣為字串類型。在此設定中，我們指定 *Azure BLOB storage* 的容器名稱。
- **EndPoint**：此設定同樣需要一個字串值。在此處，我們需要設定 *Azure BLOB storage* 的端點（Endpoint）。
- **AppendContainerName**：此設定需要一個布林值。請根據建構 URL 時是否需要將容器名稱附加到 `EndPoint` 後方，將值設定為 **`true`** 或 **`false`**。

## 設定 Cloudflare Images

外掛設定非常直觀，除了主要的啟用/停用開關外，還包含四個欄位。

![Image](_static/cloudflare-images/cloudflare-images.png)

其中一個關鍵欄位是 **Delivery URL**。它必須採用以下特定格式進行設定：

```bash
https://imagedelivery.net/[account_hash]/<image_id>/<variant_name>
```

此 URL 作為範本使用。外掛會動態插入所需的 `image_id` 和 `variant_name`，以產生網站顯示縮圖時所需的最終 URL。

## 使用方式

一旦完成設定並啟用，該外掛就會在背景自動運作，這與 Azure Blob Storage 的整合方式類似。它會自動處理：

- 將新縮圖上傳至 Cloudflare Images 服務。
- 在所有前台頁面上，將原始的本地縮圖 URL 取代為對應的 Cloudflare Images URL。