---
標題: 如何為 nopCommerce 編寫外掛
uid: zh-Hant/developer/plugins/how-to-write-plugin-3.90
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.exileDev
---

# 如何為 nopCommerce 3.90（及更早版本）編寫外掛

> 在運算領域中，外掛（plugin）是一組能為大型軟體應用程式增加特定功能的軟體元件（維基百科）。

外掛用於擴充 nopCommerce 的功能。nopCommerce 擁有多種類型的外掛。例如，付款提供程序（如 PayPal）、稅務提供程序、配送方式計算方法（如 UPS、USPS、FedEx）、小部件（如「線上客服」區塊）等等。nopCommerce 本身已預先安裝了許多不同的外掛。您也可以在 [nopCommerce 官方網站](https://www.nopcommerce.com/marketplace) 上搜尋，看看是否有人已經開發了符合您需求的外掛。如果沒有，本文將引導您完成建立外掛的過程。

## 外掛結構、必要檔案與存放位置

1. 首先，您需要在解決方案中建立一個新的「類別庫 (Class Library)」專案。建議將所有外掛放置在解決方案根目錄下的 `\Plugins` 資料夾中（請勿與位於 `\Nop.Web` 目錄下用於已部署外掛的 `\Plugins` 子目錄混淆）。建議將所有外掛放置在「Plugins」解決方案資料夾中（您可以在[此處](http://msdn.microsoft.com/library/sx2027y2.aspx)找到關於解決方案資料夾的更多資訊）。

    建議的外掛專案名稱為 "Nop.Plugin.{Group}.{Name}"。{Group} 是您的外掛類別（例如 "Payment" 或 "Shipping"）。{Name} 是您的外掛名稱（例如 "PayPalStandard"）。例如，PayPal Standard 付款外掛的名稱為：Nop.Plugin.Payments.PayPalStandard。但請注意，這並非強制要求，您可以為外掛選擇任何名稱，例如 "MyGreatPlugin"。

    ![p1](_static/how-to-write-plugin-3.90/write_plugin_3.90_4.jpg)

1. 建立外掛專案後，請更新專案的組建輸出路徑。將其設定為 `..\..\Presentation\Nop.Web\Plugins\{Group}.{Name}`。例如，Authorize.NET 付款外掛的輸出路徑為：`..\..\Presentation\Nop.Web\Plugins\Payments.AuthorizeNet`。完成後，相關的外掛 DLL 檔案會自動複製到 `\Presentation\Nop.Web\Plugins` 目錄中，nopCommerce 核心會在此目錄中搜尋有效的外掛。同樣地，這也非強制要求，您可以為外掛選擇任何輸出目錄名稱。

    ![p1](_static/how-to-write-plugin-3.90/write_plugin_3.90_1.jpg)

    - 在「專案 (Project)」選單中，點選「屬性 (Properties)」。
    - 點選「建置 (Build)」索引標籤。
    - 點選輸出路徑欄位旁邊的「瀏覽 (Browse)」按鈕，選擇新的組建輸出目錄。

    您必須針對所有現有的組態（"Debug" 與 "Release"）執行上述步驟。

1. 下一步是為每個外掛建立必要的 `Description.txt` 檔案。此檔案包含描述您外掛的 meta 資訊。您可以直接複製任何現有外掛的此檔案並根據需求修改。例如，PayPal Standard 付款外掛具有以下 `Description.txt` 檔案：

    ```txt
    Group: Payment methods
    FriendlyName: PayPal Standard
    SystemName: Payments.PayPalStandard
    Version: 1.28
    SupportedVersions: 3.90
    Author: nopCommerce team
    DisplayOrder: 1
    FileName: Nop.Plugin.Payments.PayPalStandard.dll
    Description: This plugin allows paying with PayPal Standard
    ```

    All fields are self-descriptive, but here are some notes. **SystemName** field should be unique. **Version** field is a version number of your plugin; you can set it to any value you like. **SupportedVersions** field can contain a list of supported nopCommerce versions separated by commas (ensure that the current version of nopCommerce is included in this list, otherwise, it will not be loaded). **FileName** field has the following format *Nop.Plugin.{Group}.{Name}.dll* (it is your plugin assembly filename). Ensure that the "Copy to Output Directory" property of this file is set to "Copy if newer".

    ![p2](_static/how-to-write-plugin-3.90/write_plugin_3.90_2.jpg)

1. You should also created a web.config file and ensure that it's copied to the output. Just copy it from any existing plugin.

    > [!IMPORTANT]
    > Going forward make sure "Copy local" properties of all third-party assembly references (including core libraries such as Nop.Services.dll or Nop.Web.Framework.dll) are set to "False" (do not copy)

1. The last required step is to create a class that implements the IPlugin interface (Nop.Core.Plugins namespace). nopCommerce has BasePlugin class which already implements some IPlugin methods and allows you to avoid source code duplication. nopCommerce also provides you with some specific interfaces derived from IPlugin. For example, we have the "IPaymentMethod" interface which is used for creating new payment method plugins. It contains some methods which are specific only for payment methods such as ProcessPayment() or GetAdditionalHandlingFee(). Currently, nopCommerce has the following specific plugin interfaces:

   - **IPaymentMethod**. These plugins are used for payment processing.
   - **IShippingRateComputationMethod**. These plugins are used for retrieving accepted delivery methods and appropriate shipping rates. For example, UPS, UPS, FedEx, etc.
   - **IPickupPointProvider**. These plugins are used for providing pickup points.
   - **ITaxProvider**. Tax providers are used for getting tax rates.
   - **IExchangeRateProvider**. Used for getting currency exchange rate.
   - **IDiscountRequirementRule**. Allows you to create new discount rules such as "Billing country of a customer should be…"
   - **IExternalAuthenticationMethod**. Used for creating external authentication methods such as Facebook, Twitter, OpenID, etc.
   - **IWidgetPlugin**. It allows you to create widgets. Widgets are rendered on some parts of your site. For example, it can be a "Live chat" block on your site's left column.
   - **IMiscPlugin**. If your plugin doesn't fit any of the interfaces above.

> [!IMPORTANT]
> After each project build, clean the solution before making changes. Some resources will be cached and can lead to developer insanity.

## Handling requests. Controllers, models, and views

Now you can see the plugin by going to **Admin area → Configuration → Plugins**. But as you guessed our plugin does nothing. It does not even have a user interface for its configuration. Let's create a page to configure the plugin.

What we need to do now is create a controller, a model, and a view.

- MVC controllers are responsible for responding to requests made against an ASP.NET MVC website. Each browser request is mapped to a particular controller.
- A view contains the HTML markup and content that is sent to the browser. A view is the equivalent of a page when working with an ASP.NET MVC application.
- An MVC model contains all of your application logic that is not contained in a view or a controller.

You can find more information about the MVC pattern [here](http://www.asp.net/mvc/tutorials/older-versions/overview/understanding-models-views-and-controllers-cs).

So let's start:

- **Create the model**. Add a Models folder in the new plugin, and then add a new model class that fits your need.
- **Create the view**. Add a Views folder in the new plugin, then add a {Name} folder (where {Name} is your plugin name), and finally add a cshtml file named `Configure.cshtml`. Important note: for versions 2.00-3.30 the view should be marked as an embedded resource. And starting version 3.40 views, ensure that the "Build Action" property of the view file is set to "Content", and the "Copy to Output Directory" property is set to "Copy if newer".
- **Create the controller**. Add a Controllers folder in the new plugin, and then add a new controller class. A good practice is to name plugin controllers `{Group}{Name}Controller.cs`. For example, PaymentAuthorizeNetController. Of course, it's not a requirement to name controllers this way (but just a recommendation). Then create an appropriate action method for the configuration page (in the admin area). Let's name it "Configure". Prepare a model class and pass it to the following view. For nopCommerce versions 2.00-3.30 you should pass embedded view path - "Nop.Plugin.{Group}.{Name}.Views. {Group}{Name}.Configure". And starting nopCommerce version 3.40 you should pass physical view path - `~/Plugins/{PluginOutputDirectory}/Views/{ControllerName}/Configure.cshtml`. For example, open the Authorize.NET payment plugin and look at its implementation of PaymentAuthorizeNetController.

    > [!TIP]
    >
    > - The easiest way to complete the steps described above is by opening any other plugin and copying these files into your plugin project. Then just rename appropriate classes and directories.
    >
    > - If you want to limit access to a certain action method of the controller to administrators (store owners), then just mark it with the [AdminAuthorize] attribute.

    For example, the project structure of the Authorize.NET plugin looks like the image below

    ![p3](_static/how-to-write-plugin-3.90/write_plugin_3.90_3.jpg)

## Routes

Now we need to register appropriate plugin routes. ASP.NET routing is responsible for mapping incoming browser requests to particular MVC controller actions. You can find more information about routing [here](http://www.asp.net/mvc/tutorials/older-versions/controllers-and-routing/asp-net-mvc-routing-overview-cs). So follow the next steps:

- Some of the specific plugin interfaces (described above) and the "IMiscPlugin" interface have the following method: "GetConfigurationRoute". It should return a route to a controller action that is used for plugin configuration. Implement the "GetConfigurationRoute" method of your plugin interface. This method informs nopCommerce about what route is used for plugin configuration. If your plugin doesn't have a configuration page, then "GetConfigurationRoute" should return null. For example, see the code below:

    ```csharp
    public void GetConfigurationRoute(out string actionName,
                out string controllerName,
                out RouteValueDictionary routeValues)
    {
        actionName = "Configure";
        controllerName = "PaymentAuthorizeNet";
        routeValues = new RouteValueDictionary()
        {
            { "Namespaces", "Nop.Plugin.Payments.AuthorizeNet.Controllers" },
            { "area", null }
        };
    }
    ```

- (optional) If you need to add some custom route, then create the `RouteProvider.cs` file. It informs the nopCommerce system about plugin routes. For example, the following RouteProvider class adds a new route which can be accessed by opening your web browser and navigating to `http://www.yourStore.com/Plugins/PaymentPayPalStandard/PDTHandler` URL (used by PayPal plugin):

    ```csharp
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
             routes.MapRoute("Plugin.Payments.PayPalStandard.PDTHandler",
                 "Plugins/PaymentPayPalStandard/PDTHandler",
                 new { controller = "PaymentPayPalStandard", action = "PDTHandler" },
                 new[] { "Nop.Plugin.Payments.PayPalStandard.Controllers"  }
            );
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
    ```

    Once you have installed your plugin and added the configuration method you will find a link to configure your plugin under Admin → Configuration → Plugins.

## Handling "Install" and "Uninstall" methods

This step is optional. Some plugins can require additional logic during plugin installation. For example, a plugin can insert new locale resources. So open your IPlugin implementation (in most cases it'll be derived from BasePlugin class) and override the following methods:

- Install. This method will be invoked during plugin installation. You can initialize any settings here, insert new locale resources, or create some new database tables (if required).
- Uninstall. This method will be invoked during plugin uninstallation.

> [!IMPORTANT]
> If you override one of these methods, do not hide its base implementation.

For example, the project structure of the Authorize.NET plugin looks like the image below

```csharp
public override void Install()
{
    var settings = new AuthorizeNetPaymentSettings()
    {
        UseSandbox = true,
        TransactMode = TransactMode.Authorize,
        TransactionKey = "123",
        LoginId = "456"
    };
    _settingService.SaveSetting(settings);
    base.Install();
}
```

> [!TIP]
> 已安裝外掛的清單位於 `\App_Data\InstalledPlugins.txt`。此清單會在安裝過程中建立。

## 升級 nopCommerce 可能會導致外掛失效

有些外掛可能會過時，無法再與較新版本的 nopCommerce 相容。如果您在升級到新版本後遇到問題，請刪除該外掛，並前往 nopCommerce 官方網站查看是否有更新的版本可用。許多外掛開發者會更新其外掛以適應新版本，但有些則不會，隨著 nopCommerce 的改進，這些外掛將會被淘汰。但在大多數情況下，您只需開啟對應的 `plugin.json` 檔案並更新 **SupportedVersions** 欄位即可。

## 結論

希望這能幫助您開始使用 nopCommerce，並為您建立更複雜的外掛做好準備。