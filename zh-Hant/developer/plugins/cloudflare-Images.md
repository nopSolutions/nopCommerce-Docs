---
標題: Cloudflare Images
uid: zh-Hant/developer/plugins/cloudflare-images
作者: git.DmitriyKulagin
貢獻者: git.DmitriyKulagin
---

# Cloudflare Images 整合

## 概述

從 4.90 版本開始，縮圖管理機制已進行重構以提供更高的彈性。我們引入了新的介面 `IThumbService` 來處理先前由 `IPictureService` 所負責的所有縮圖相關操作。

以下方法已移至 `IThumbService`：

- `GetThumbLocalPathAsync`
- `GeneratedThumbExistsAsync`
- `SaveThumbAsync`
- `GetThumbLocalPathByFileNameAsync`
- `GetThumbUrlAsync`
- `DeletePictureThumbsAsync`

作為此次變更的一部分，縮圖儲存邏輯已從核心應用程式中解耦，並獨立為外掛。現有的 **Azure Blob Storage** 功能現在已成為獨立的外掛。我們同時開發了一個新的外掛，將 **Cloudflare Images** 整合為縮圖儲存解決方案。

> [!NOTE]
>
> 預設情況下，Azure Blob 和 Cloudflare Images 外掛（`Misc.AzureBlob` 與 `Misc.CloudflareImages`）都會列在 `InstallationConfig.DisabledPlugins` 屬性中。這能確保這些外掛在全新安裝時不會自動啟用，讓商店負責人能依需求選擇並安裝最合適的儲存解決方案。

## 設定 Azure Blob Storage

我們可以使用 *Azure Blob Storage* 來儲存 Blob 資料。nopCommerce 已內建此功能，我們只需要正確設定以下資訊即可使用。這些設定值可以在您於 *Azure* 建立儲存帳戶時取得。

![Image](_static/cloudflare-images/azure-blob.png)

- **ConnectionString** 此設定需要一個字串值。您需在此輸入您的 `AzureBlobStorage` 連接字串。
- **ContainerName** 此設定的值同樣為字串型態。在此設定中，我們指定 *Azure BLOB storage* 的容器名稱。
- **EndPoint** 此設定同樣需要一個字串值。我們需在此設定 *Azure BLOB storage* 的端點。
- **AppendContainerName** 此設定需要一個布林值。請根據在建構 URL 時是否需要將容器名稱附加到 `EndPoint` 後方，將值設為 **`true`** 或 **`false`**。

## 設定 Cloudflare Images

外掛設定非常直觀，除了主開關（啟用/停用）外，包含四個欄位。

![Image](_static/cloudflare-images/cloudflare-images.png)

其中一個關鍵欄位是 **Delivery URL**（傳遞 URL）。它必須按照下列特定格式進行設定：

```bash
https://imagedelivery.net/[account_hash]/<image_id>/<variant_name>
```

此 URL 扮演範本的角色。外掛會動態插入所需的 `image_id` 與 `variant_name`，以產生網站上顯示縮圖所需的最終 URL。

## 使用方式

一旦設定並啟用後，該外掛就會像 Azure Blob Storage 整合一樣，自動在背景運作。它能順暢地處理：

- 將新的縮圖上傳至 Cloudflare Images 服務。
- 在所有前台頁面上，將原始的本地縮圖 URL 取代為對應的 Cloudflare Images URL。