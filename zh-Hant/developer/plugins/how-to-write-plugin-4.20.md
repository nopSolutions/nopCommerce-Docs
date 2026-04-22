---
標題: 如何為 nopCommerce 編寫外掛
uid: zh-Hant/developer/plugins/how-to-write-plugin-4.20
作者: git.AndreiMaz
貢獻者: git.Kevat, git.exileDev, git.DmitriyKulagin, git.cromatido
---

# 如何為 nopCommerce 4.20 編寫外掛

外掛（Plugins）用於擴充 nopCommerce 的功能。nopCommerce 擁有數種類型的外掛。例如，付款方式（如 PayPal）、稅務提供程序、配送方式計算方法（如 UPS、USPS、FedEx）、小部件（如「即時聊天」區塊）等等。nopCommerce 本身已預先安裝了許多不同的外掛。您也可以在 [nopCommerce 官方網站](https://www.nopcommerce.com/marketplace) 上搜尋各種外掛，看看是否已有開發者建立了符合您需求的外掛。如果沒有，本文將引導您完成建立自訂外掛的過程。

## 外掛結構、所需檔案與位置

1. 首先，您需要在解決方案中建立一個新的「類別庫（Class Library）」專案。將所有外掛放置在解決方案根目錄下的 `\Plugins` 資料夾中是一種良好的實踐（請勿與位於 `\Nop.Web` 目錄下的 `\Plugins` 子目錄混淆，該目錄用於存放已部署的外掛）。將所有外掛放入「Plugins」解決方案資料夾中也是一種良好的做法（您可以[此處](http://msdn.microsoft.com/library/sx2027y2.aspx)找到更多關於解決方案資料夾的資訊）。

    建議的外掛專案命名格式為「Nop.Plugin.{Group}.{Name}」。{Group} 是您的外掛群組（例如，「Payments」或「Shipping」）。{Name} 是您的外掛名稱（例如，「PayPalStandard」）。例如，PayPal Standard 付款外掛的名稱為：Nop.Plugin.Payments.PayPalStandard。但請注意，這並非強制性要求，您可以為外掛選擇任何名稱，例如「MyGreatPlugin」。

    ![p1](_static/how-to-write-plugin-4.20/write_plugin_4.20_1.jpg)

1. 外掛專案建立完成後，您必須使用任何文字編輯器開啟其 `.csproj` 檔案，並將其內容替換為以下內容：

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        <PropertyGroup>
            <TargetFramework>netcoreapp2.2</TargetFramework>
            <Copyright>SOME_COPYRIGHT</Copyright>
            <Company>YOUR_COMPANY</Company>
            <Authors>SOME_AUTHORS</Authors>
            <PackageLicenseUrl>PACKAGE_LICENSE_URL</PackageLicenseUrl>
            <PackageProjectUrl>PACKAGE_PROJECT_URL</PackageProjectUrl>
            <RepositoryUrl>REPOSITORY_URL</RepositoryUrl>
            <RepositoryType>Git</RepositoryType>
            <OutputPath>..\..\Presentation\Nop.Web\Plugins\PLUGIN_OUTPUT_DIRECTORY</OutputPath>
            <OutDir>$(OutputPath)</OutDir>
            <!--Set this parameter to true to get the dlls copied from the NuGet cache to the output of your    project. You need to set this parameter to true if your plugin has a nuget package to ensure that   the dlls copied from the NuGet cache to the output of your project-->
            <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
        </PropertyGroup>
        <ItemGroup>
            <ProjectReference Include="..\..\Presentation\Nop.Web.Framework\Nop.Web.Framework.csproj" />
            <ClearPluginAssemblies Include="$(MSBuildProjectDirectory)\..\..\Build\ClearPluginAssemblies.proj" />
        </ItemGroup>
        <!-- This target execute after "Build" target -->
        <Target Name="NopTarget" AfterTargets="Build">
            <!-- Delete unnecessary libraries from plugins path -->
            <MSBuild Projects="@(ClearPluginAssemblies)" Properties="PluginPath=$(MSBuildProjectDirectory)\$(OutDir)" Targets="NopClear" />
        </Target>
    </Project>
    ```

    > [!TIP]
    > Where PLUGIN_OUTPUT_DIRECTORY should be replaced with the plugin name, for example, Payments.PayPalStandard.
    >
    > We do it this way to be able to use a new approach to add third-party references which were introduced in .NET Core. But it's not required. Moreover, references from already referenced libraries will be loaded automatically. So it is very convenient.

1. The next step is creating a `plugin.json` file required for each plugin. This file contains meta-information describing your plugin. Just copy this file from any other existing plugin and modify it for your needs. For information about the `plugin.json` file, please see [plugin.json file](xref:zh-Hant/developer/plugins/plugin_json)

1. The last required step is to create a class that implements the **IPlugin** interface (Nop.Services.Plugins namespace). nopCommerce has **BasePlugin** class which already implements some IPlugin methods and allows you to avoid source code duplication. nopCommerce also provides you with some specific interfaces derived from IPlugin. For example, we have the "IPaymentMethod" interface which is used for creating new payment method plugins. It contains some methods which are specific only for payment methods such as *ProcessPayment()* or *GetAdditionalHandlingFee()*. Currently, nopCommerce has the following specific plugin interfaces:

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
>
> You may need to rebuild your solution after adding your plugin. If you do not see DLLs for your plugin under Nop.Web\Plugins\PLUGIN_OUTPUT_DIRECTORY, you need to rebuild your solution. nopCommerce will not list your plugin in the Local Plugins page if your DLLs do not exist in the correct folder in Nop.Web.

## Handling requests. Controllers, models, and views

Now you can see the plugin by going to **Admin area → Configuration → Local Plugins**. But as you guessed our plugin does nothing. It does not even have a user interface for its configuration. Let's create a page to configure the plugin.

What we need to do now is create a controller, a model, and a view.

1. MVC controllers are responsible for responding to requests made against an ASP.NET MVC website. Each browser request is mapped to a particular controller.
1. A view contains the HTML markup and content that is sent to the browser. A view is the equivalent of a page when working with an ASP.NET MVC application.
1. An MVC model contains all of your application logic that is not contained in a view or a controller.

You can find more information about the MVC pattern [here](http://www.asp.net/mvc/tutorials/older-versions/overview/understanding-models-views-and-controllers-cs).

So let's start:

- **Create the model**. Add a **Models** folder in the new plugin, and then add a new model class that fits your need.
- **Create the view**. Add a **Views** folder in the new plugin, and then add a cshtml file named `Configure.cshtml`. Set **"Build Action"** property of the view file is set to **"Content"**, and the **"Copy to Output Directory"** property is set to **"Copy always"**. Note that the configuration page should use the "_ConfigurePlugin" layout.
- Also make sure that you have the `_ViewImports.cshtml` file into your \Views directory. You can just copy it from any other existing plugin.
- **Create the controller**. Add a **Controllers** folder in the new plugin, and then add a new controller class. A good practice is to name plugin controllers `{Group}{Name}Controller.cs`. For example, PaymentPayPalStandardController. Of course, it's not a requirement to name controllers this way (but just a recommendation). Then create an appropriate action method for the configuration page (in the admin area). Let's name it *"Configure"*. Prepare a model class and pass it to the following view using a physical view path: `~/Plugins/{PluginOutputDirectory}/Views/Configure.cshtml`.
- Use the following attributes for your action method:

    ```csharp
    [AuthorizeAdmin] //confirms access to the admin panel
    [Area(AreaNames.Admin)] //specifies the area containing a controller or action
    ```

    For example, open PayPalStandard payment plugin and look at its implementation of PaymentPayPalStandardController.

Then for each plugin that has a configuration page, you should specify a configuration URL. A base class named `BasePlugin` has `GetConfigurationPageUrl` method which returns a configuration URL:

```csharp
public override string GetConfigurationPageUrl()
{
    return $"{_webHelper.GetStoreLocation()}Admin/{CONTROLLER_NAME}/{ACTION_NAME}";
}
```

Where *{CONTROLLER_NAME}* is the name of your controller and *{ACTION_NAME}* is the name of the action (usually it's "Configure").

Once you have installed your plugin and added the configuration method you will find a link to configure your plugin under **Admin → Configuration → Local Plugins**.

> [!TIP]
> The easiest way to complete the steps described above is by opening any other plugin and copying these files into your plugin project. Then just rename appropriate classes and directories.

For example, the project structure of the PayPalStandard plugin looks like the image below:

![p3](_static/how-to-write-plugin-4.20/write_plugin_4.20_3.jpg)

## Handling "Install" and "Uninstall" methods

This step is optional. Some plugins can require additional logic during plugin installation. For example, a plugin can insert new locale resources. So open your IPlugin implementation (in most cases it'll be derived from BasePlugin class) and override the following methods:

1. **Install**. This method will be invoked during plugin installation. You can initialize any settings here, insert new locale resources, or create some new database tables (if required).
1. **Uninstall**. This method will be invoked during plugin uninstallation.

> [!IMPORTANT]
> If you override one of these methods, do not hide its base implementation.

For example, overridden "Install" method should include the following method call: *base.Install()*. The "Install" method of the PayPalStandard plugin looks like the code below

```csharp
public override void Install()
{
    var settings = new PayPalStandardPaymentSettings()
    {
        UseSandbox = true
    };
    _settingService.SaveSetting(settings);
    ...
    base.Install();
}
```

> [!TIP]
> The list of installed plugins is located in `\App_Data\Plugins.json`. The list is created during installation.

## Routes

Here we will have a look at how to register plugin routes. ASP.NET Core routing is responsible for mapping incoming browser requests to particular MVC controller actions. You can find more information about routing [here](https://docs.microsoft.com/aspnet/core/fundamentals/routing). So follow the next steps:

1. If you need to add some custom route, then create the `RouteProvider.cs` file. It informs the nopCommerce system about plugin routes. For example, the following RouteProvider class adds a new route which can be accessed by opening your web browser and navigating to `http://www.yourStore.com/Plugins/PaymentPayPalStandard/PDTHandler` URL (used by PayPal plugin):

```csharp
public partial class RouteProvider : IRouteProvider
{
    public void RegisterRoutes(IRouteBuilder routeBuilder)
    {
         routeBuilder.MapRoute("Plugin.Payments.PayPalStandard.PDTHandler", "Plugins/PaymentPayPalStandard/PDTHandler",
            new { controller = "PaymentPayPalStandard", action = "PDTHandler" });
    }
    public int Priority => -1;
}
```

## 升級 nopCommerce 可能導致外掛失效

有些外掛可能會過時，無法再與新版本的 nopCommerce 搭配運作。如果您在升級到新版本後遇到問題，請刪除該外掛，並前往 nopCommerce 官方網站查看是否有更新的版本可用。許多外掛開發者會升級其外掛以適應新版本，但有些則不會，因此隨著 nopCommerce 的改進，這些外掛可能會被淘汰。但在大多數情況下，您可以直接開啟對應的 `plugin.json` 檔案，並更新 **SupportedVersions** 欄位。

## 結論

希望本文能協助您開始接觸 nopCommerce，並為您開發更複雜的外掛做好準備。

## 外掛模板

您可以使用我們提供的 Visual Studio 模板來建立新的 nopCommerce 外掛。這可以為開發者節省大量時間，因為他們不需要再手動執行所有初始步驟，例如建立資料夾（Controllers、Views、Models 等）、建立其他必要檔案（DependencyRegistrar.cs、_ViewImports.cshtml、ObjectContext、plugin.json 等）、進行設定以及添加專案參考等。請點擊[此處](https://github.com/nopSolutions/nopCommerce-plugin-template-VS/)查看模板內容與安裝說明。