---
標題: plugin.json 檔案結構說明
uid: zh-Hant/developer/plugins/plugin_json
作者: git.cromatido
貢獻者: git.cromatido
---

# plugin.json 檔案結構說明

此檔案包含外掛的元資訊（Meta information）描述，nopCommerce 使用這些資訊來判斷此檔案所屬的群組、該外掛是否與當前的 nopCommerce 版本相容、外掛的版本號，以及其他相關資訊。每個 nopCommerce 外掛都必須包含此檔案。

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

- **Group**：nopCommerce 用於在後台管理系統 `Admin/Configuration/LocalPlugin` 選單的外掛列表中，識別、搜尋或篩選外掛所屬的群組。這可以是您的公司名稱。

- **FriendlyName**：這是外掛的顯示名稱，用於從外掛列表中識別您的外掛。

- **SystemName**：nopCommerce 用於唯一識別外掛，因此它必須與所有其他外掛區隔開來。我們不能註冊多個具有相同 `SystemName` 的外掛。

- **Version**：這是外掛的版本號，您可以將其設定為任何您喜歡的值。此數字用於識別當前 nopCommerce 應用程式中安裝的是哪個版本的外掛。不過，我們建議採用以下格式 - `"{nopCommerce_version_number}.{build_number}"`。

- **SupportedVersions**：這是一個字串陣列。它包含一個或多個支援此插件的 nopCommerce 版本，或者可以說是此插件的目標版本。在開發過程中，請確保此清單中包含您正在開發此插件的當前 nopCommerce 版本，否則它將不會載入到外掛列表中。

- **Author**：這是關於外掛創作者的資訊。它可以是個人姓名、公司名稱或建立此插件的團隊名稱。

- **DisplayOrder**：用於設定此插件在外掛列表中的顯示順序。其值為數字類型。

- **FileName**：它具有以下格式 **Nop.Plugin.{Group}.{Name}.dll**（這是您的外掛組件檔案名稱）。

- **Description**：包含您外掛的簡短描述，例如此插件的主要用途、功能為何。這會顯示在後台外掛列表的外掛名稱下方。
- **LimitedToStores** - 可使用此插件的商店識別碼清單。若為空，則此插件可用於所有商店。
- **LimitedToCustomerRoles** - 可使用此插件的顧客角色識別碼清單。若為空，則此插件適用於所有顧客角色。
- **DependsOnSystemNames** - 此插件所依賴的外掛系統名稱清單。

> [!TIP]
> 當您編輯完 **plugin.json** 檔案內容後，您需要將其 `Copy to Output Directory`（複製到輸出目錄）屬性值設定為 `Copy if newer`（有更新時才複製）。
> ![image3](_static/plugin.json/plugin_json_0.jpg)
> 這是必要的步驟，因為我們需要將此檔案複製到編譯後的目錄中，nopCommerce 才能存取此檔案，以便在後台管理系統的外掛列表中顯示您的外掛。

## 範例

- **FixedOrByCountryStateZip** 外掛具有以下的 `plugin.json` 檔案：

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

- **Google Analytics** 小部件具有以下的 *plugin.json* 檔案：

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