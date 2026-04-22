---
標題: 如何為 nopCommerce 編寫外掛
uid: zh-Hant/developer/plugins/how-to-write-plugin-4.70
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.tgondar
---

# 如何為 nopCommerce 編寫外掛

外掛用於擴充 nopCommerce 的功能。nopCommerce 擁有多種類型的外掛。例如，付款方式（如 PayPal）、稅務提供程序、配送方式計算方法（如 UPS、USPS、FedEx）、小部件（如「線上客服」區塊）等等。nopCommerce 發行時已內建許多不同的外掛。您也可以在 [nopCommerce 官方網站](https://www.nopcommerce.com/marketplace) 搜尋各種外掛，看看是否有人已經建立符合您需求的外掛。如果沒有，本文將引導您完成建立外掛的過程。

## 外掛結構、必要檔案與存放位置

1. 您首先需要做的是在解決方案中建立一個新的 *`Class Library`* 專案。將所有外掛放置在解決方案根目錄的 `\Plugins` 資料夾中是一個良好的習慣（請勿與位於 `\Nop.Web` 目錄下、用於已部署外掛的 `\Plugins` 子目錄混淆）。將所有外掛放置在 `Plugins` 解決方案資料夾中也是一種良好的作法。

    建議的外掛專案命名方式為 **`Nop.Plugin.{Group}.{Name}`**。**`{Group}`** 是您的外掛群組（例如 *Payments* 或 *Shipping*）。**`{Name}`** 是您的外掛名稱（例如 *PayPalCommerce*）。例如，PayPal Commerce 付款外掛的名稱為：**`Nop.Plugin.Payments.PayPalCommerce`**。但請注意，這並非強制要求。您可以為外掛選擇任何名稱，例如 `MyGreatPlugin`。

    ![p1](_static/how-to-write-plugin-4.70/write_plugin_4.70_1.jpg)

1. 一旦外掛專案建立完成，您必須使用任何文字編輯器開啟其 `.csproj` 檔案，並將其內容替換為以下內容：

    ```csharp
protected readonly IWebHelper _webHelper;

public PaymentPayPalStandardProvider(IWebHelper webHelper)
{
    _webHelper = webHelper;
}

public override string GetConfigurationPageUrl()
{
    return $"{_webHelper.GetStoreLocation()}Admin/{CONTROLLER_NAME}/{ACTION_NAME}";
}

public override async Task InstallAsync()
{
    await _settingService.SaveSettingAsync(new PayPalStandardPaymentSettings
    {
        UseSandbox = true
    });
    
    await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
    {
        ...
    });
    await base.InstallAsync();
}

public class RouteProvider : IRouteProvider
    {
        /// <summary>
        /// 註冊路由
        /// </summary>
        /// <param name="endpointRouteBuilder">路由建置器</param>
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(PayPalCommerceDefaults.ConfigurationRouteName,
                "Admin/PayPalCommerce/Configure",
                new { controller = "PayPalCommerce", action = "Configure" });

            endpointRouteBuilder.MapControllerRoute(PayPalCommerceDefaults.WebhookRouteName,
                "Plugins/PayPalCommerce/Webhook",
                new { controller = "PayPalCommerceWebhook", action = "WebhookHandler" });
        }

        /// <summary>
        /// 取得路由提供程序的優先順序
        /// </summary>
        public int Priority => 0;
    }
```

## 升級 nopCommerce 可能會導致外掛失效

有些外掛可能會因為過時而無法在新版本的 nopCommerce 中運作。如果您在升級到新版本後遇到問題，請刪除該外掛，並造訪 nopCommerce 官方網站查看是否有可用的新版本。許多外掛開發者會更新他們的外掛以適應新版本，但有些則不會，這些外掛將會隨著 nopCommerce 的改進而變得過時。但在大多數情況下，您只需開啟對應的 `plugin.json` 檔案並更新 **SupportedVersions** 欄位即可。

## 結論

希望這能幫助您開始使用 nopCommerce，並為建立更複雜的外掛做好準備。

## 外掛範本

您可以使用我們為新的 nopCommerce 外掛提供的 Visual Studio 範本。這可以為開發者節省大量時間，因為他們不必手動執行所有初始步驟。例如資料夾建立（Controllers、Views、Models 等）、其他必要檔案（PluginNopStartup.cs、_ViewImports.cshtml、ObjectContext、plugin.json 等）、設定、專案參考等。請點選 [此處](https://github.com/nopSolutions/nopCommerce-plugin-template-VS/) 尋找範本與安裝說明。