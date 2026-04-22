---
標題: Cloudflare Images
uid: zh-Hant/developer/plugins/cloudflare-images
作者: git.DmitriyKulagin
貢獻者: git.DmitriyKulagin
---

# Cloudflare Images 整合

## 概述

從 4.90 版本開始，縮圖管理機制已進行重構以提供更高的靈活性。我們引入了新的介面 `IThumbService`，用於處理先前由 `IPictureService` 管理的所有縮圖相關操作。

以下方法已移至 `IThumbService`：

- `GetThumbLocalPathAsync`
- `GeneratedThumbExistsAsync`
- `SaveThumbAsync`
- `GetThumbLocalPathByFileNameAsync`
- `GetThumbUrlAsync`
- `DeletePictureThumbsAsync`

作為此次變更的一部分，縮圖儲存邏輯已從核心應用程式中解耦，並改為獨立的外掛。現有的 **Azure Blob Storage** 功能現在已成為獨立的外掛。此外，我們開發了一個新的外掛來整合 **Cloudflare Images**，作為縮圖儲存的解決方案。

> [!NOTE]
>
> 預設情況下，Azure Blob 和 Cloudflare Images 外掛（`Misc.AzureBlob` 和 `Misc.CloudflareImages`）都會列在 `InstallationConfig.DisabledPlugins` 屬性中。這確保了這些外掛不會在全新安裝期間自動安裝，讓商店擁有者可以根據需求選擇並安裝最適合的儲存解決方案。

## 設定 Azure Blob Storage

我們可以使用 *Azure Blob Storage* 來儲存 Blob 資料。nopCommerce 已經整合了此功能，我們只需正確設定以下資訊即可使用。當您建立儲存體帳戶時，即可從 *Azure* 取得這些設定值。

![Image](_static/cloudflare-images/azure-blob.png)

- **ConnectionString** 此設定需要一個字串值。您需要在此處填入您的 `AzureBlobStorage` 連接字串。
- **ContainerName** 此設定的值同樣為字串類型。在此設定中，我們指定 *Azure BLOB storage* 的容器名稱。
- **EndPoint** 此設定同樣需要一個字串值。我們需要在此處設定 *Azure BLOB storage* 的端點（Endpoint）。
- **AppendContainerName** 此設定需要一個布林值。請根據建構 URL 時是否需要將容器名稱附加到 `EndPoint` 後方，將值設為 **`true`** 或 **`false`**。

## 設定 Cloudflare Images

外掛設定非常直觀，除了主要的啟用/停用開關外，還包含四個欄位。

![Image](_static/cloudflare-images/cloudflare-images.png)

其中一個關鍵欄位是 **Delivery URL**（傳遞 URL）。它必須依照下列特定格式進行設定：

```bash
https://imagedelivery.net/[account_hash]/<image_id>/<variant_name>
```

此 URL 作為範本使用。外掛將會動態插入所需的 `image_id` 與 `variant_name`，以產生顯示於網站上的最終縮圖網址。

## 使用方式

一旦完成設定並啟用，該外掛就會像 Azure Blob Storage 整合一樣，自動在背景運作。它能順暢地處理以下事項：

- 將新的縮圖上傳至 Cloudflare Images 服務。
- 在所有前台頁面上，將原始的本地縮圖網址自動替換為對應的 Cloudflare Images 網址。