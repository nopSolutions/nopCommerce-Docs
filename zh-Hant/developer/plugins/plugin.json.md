---
標題: plugin.json 檔案結構說明
uid: zh-Hant/developer/plugins/plugin_json
作者: git.cromatido
貢獻者: git.cromatido
---

# plugin.json 檔案結構說明

此檔案包含外掛的元資訊（Meta information）描述，供 nopCommerce 使用，以判斷該外掛屬於哪個群組、是否與當前 nopCommerce 版本相容、外掛版本號以及其他相關資訊。每個 nopCommerce 外掛都必須包含此檔案。

## 檔案結構

```json
{
  "Group": "Payment methods",
  "FriendlyName": "PayPal Commerce",
  "SystemName": "Payments.PayPalCommerce",
  "Version": "4.70.1",
  "SupportedVersions": [ "4.70" ],
  "Author": "nopCommerce team",
  "DisplayOrder": -1,
  "FileName": "Nop.Plugin.Payments.PayPalCommerce.dll",
  "Description": ""
}
```

- **Group**：nopCommerce 用於在「後台管理/設定/在地化外掛」選單的列表頁中，按群組名稱識別、搜尋或篩選外掛。這可以是您的公司名稱。

- **FriendlyName**：這是外掛的顯示名稱。用於在外掛列表中識別您的外掛。

- **SystemName**：nopCommerce 用於唯一識別此外掛，因此它必須與所有其他外掛不同。我們無法註冊超過一個具有相同 `SystemName` 的外掛。

- **Version**：這是外掛的版本號，您可以將其設定為任何您喜歡的值。此數字用於識別目前安裝在 nopCommerce 應用程式中的外掛版本。不過，我們建議遵循以下格式：`"{nopCommerce_version_number}.{build_number}"`。

- **SupportedVersions**：這是一個字串陣列。它包含一個或多個此插件支援的 nopCommerce 版本。在開發過程中，請確保此清單包含您開發此插件時所使用的當前 nopCommerce 版本，否則它將不會被載入到外掛列表中。

- **Author**：這是關於外掛創作者的資訊。它可以是個人姓名、公司名稱或創建此插件的團隊名稱。

- **DisplayOrder**：用於設定此插件在外掛列表中顯示的順序。其值為數字型態。

- **FileName**：具有以下格式：**Nop.Plugin.{Group}.{Name}.dll**（這是您的外掛組件檔名）。

- **Description**：包含對您的外掛的簡短描述，例如此插件的主要用途、功能等。這會顯示在後台外掛列表中的名稱下方。
- **LimitedToStores** - 此插件適用的商店識別碼清單。若為空，則此插件適用於所有商店。
- **LimitedToCustomerRoles** - 此插件適用的顧客角色識別碼清單。若為空，則此插件適用於所有角色。
- **DependsOnSystemNames** - 此插件所依賴的外掛系統名稱清單。

> [!TIP]
> 編輯完 **plugin.json** 檔案內容後，您需要將其 `複製到輸出目錄` (Copy to Output Directory) 屬性值設定為 `有更新時才複製` (Copy if newer)。
> ![image3](_static/plugin.json/plugin_json_0.jpg)
> 這是必要的，因為我們需要將此檔案複製到編譯目錄中，nopCommerce 才能從該處讀取檔案，並在後台管理系統的外掛列表中顯示我們的外掛。

## 範例

- **FixedOrByCountryStateZip** 外掛具有以下 `plugin.json` 檔案：

  ```json
    {
        "Group": "Tax providers",
        "FriendlyName": "Manual (Fixed or By Country/State/Zip)",
        "SystemName": "Tax.FixedOrByCountryStateZip",
        "Version": "4.70.1",
        "SupportedVersions": [ "4.70" ],
        "Author": "nopCommerce team",
        "DisplayOrder": 1,
        "FileName": "Nop.Plugin.Tax.FixedOrByCountryStateZip.dll",
        "Description": "This plugin allow to configure fix tax rates or tax rates by countries, states and zip codes"
    }
  ```

- **Google Analytics** 小部件具有以下 *plugin.json* 檔案：

  ```json
    {
        "Group": "Widgets",
        "FriendlyName": "Google Analytics",
        "SystemName": "Widgets.GoogleAnalytics",
        "Version": "4.70.1",
        "SupportedVersions": [ "4.70" ],
        "Author": "nopCommerce team, Nicolas Muniere",
        "DisplayOrder": 1,
        "FileName": "Nop.Plugin.Widgets.GoogleAnalytics.dll",
        "Description": "This plugin integrates with Google Analytics. It keeps track of statistics about    the visitors and ecommerce conversion on your website"
    }
  ```