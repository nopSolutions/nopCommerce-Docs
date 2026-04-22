---
標題: 如何為 nopCommerce 編寫外掛
uid: zh-Hant/developer/plugins/how-to-write-plugin-4.50
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin
---

# 如何為 nopCommerce 編寫外掛

外掛用於擴充 nopCommerce 的功能。nopCommerce 擁有幾種類型的外掛。例如，付款提供程序（如 PayPal）、稅務提供程序、配送方式計算方法（如 UPS、USPS、FedEx）、小部件（如「線上客服」區塊）等等。nopCommerce 本身已經內建了許多不同的外掛。您也可以在 [nopCommerce 官方網站](https://www.nopcommerce.com/marketplace) 上搜尋各種外掛，看看是否有人已經開發出符合您需求的外掛。如果沒有，這篇文章將指導您完成建立自己外掛的過程。

## 外掛結構、必要檔案與位置

1. 您首先需要做的是在方案中建立一個新的 *`Class Library`* (類別庫) 專案。將所有外掛放置在方案根目錄的 `\Plugins` 資料夾中是一個好習慣（請勿與 `\Nop.Web` 目錄下用於已部署外掛的 `\Plugins` 子目錄混淆）。將所有外掛放在方案的 `Plugins` 資料夾中是推薦的做法。

    外掛專案的推薦命名方式為 **`Nop.Plugin.{Group}.{Name}`**。**`{Group}`** 是您的外掛群組（例如 *Payment* 或 *Shipping*）。**`{Name}`** 是您的外掛名稱（例如 *PayPalStandard*）。例如，PayPal Standard 付款外掛的名稱為：**`Nop.Plugin.Payments.PayPalStandard`**。但請注意，這並非強制要求。您可以為外掛選擇任何名稱，例如 `MyGreatPlugin`。

    ![p1](_static/how-to-write-plugin-4.50/write_plugin_4.50_1.jpg)

1. 建立外掛專案後，您必須在任何文字編輯器中開啟其 `.csproj` 檔案，並將其內容替換為以下內容：

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        <PropertyGroup>
            <TargetFramework>net6.0</TargetFramework>
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
    > **PLUGIN_OUTPUT_DIRECTORY** 應替換為外掛名稱，例如 `Payments.PayPalStandard`。
    >
    > 我們這樣做是為了能夠使用 .NET Core 引入的添加第三方參考的新方法。但實際上，這並非強制。此外，已參考的程式庫中的參考項目會被自動載入，因此非常方便。

1. 下一個步驟是建立每個外掛所需的 `plugin.json` 檔案。此檔案包含描述您外掛的中繼資訊。只需從任何其他現有的外掛複製此檔案，並根據您的需求進行修改即可。有關 `plugin.json` 檔案的資訊，請參閱 [plugin.json 檔案](xref:zh-Hant/developer/plugins/plugin_json)

1. 最後一個必要的步驟是建立一個實作 **`IPlugin`** 介面（`Nop.Services.Plugins` 命名空間）的類別。nopCommerce 擁有 **`BasePlugin`** 類別，它已經實作了一些 `IPlugin` 方法，讓您能避免原始程式碼重複。nopCommerce 還提供了一些衍生自 `IPlugin` 的特定介面。例如，我們有 `IPaymentMethod` 介面，用於建立新的付款方式外掛。它包含了一些僅適用於付款方式的方法，例如 *`ProcessPaymentAsync()`* 或 *`GetAdditionalHandlingFeeAsync()`*。目前，nopCommerce 擁有以下特定的外掛介面：

   - **IPaymentMethod**：這些外掛用於處理付款。
   - **IShippingRateComputationMethod**：這些外掛用於獲取已接受的配送方式與對應的運費。例如：UPS、FedEx 等。
   - **IPickupPointProvider**：這些外掛用於提供取貨點。
   - **ITaxProvider**：稅務提供程序用於獲取稅率。
   - **IExchangeRateProvider**：用於獲取貨幣匯率。
   - **IDiscountRequirementRule**：允許您建立新的折扣規則，例如「顧客的結帳國家/地區必須為……」。
   - **IExternalAuthenticationMethod**：用於建立外部驗證方法，例如 Facebook、Twitter、OpenID 等。
   - **IMultiFactorAuthenticationMethod**：用於建立多重驗證方法，例如 *GoogleAuthenticator* 等。
     >[!NOTE]
     > 這是一個新介面，自 4.40 版本起，我們為 MFA 整合提供了現成的對應基礎設施。
- **IWidgetPlugin**。它允許您建立小部件。小部件會渲染在您網站的某些部分。例如，它可以是您網站左側欄位中的「即時交談」區塊。
- **IMiscPlugin**。如果您的外掛不適用於上述任何介面，請使用此介面。

> [!IMPORTANT]
> 每次建置專案後，在進行更改前請先清理方案。某些資源會被快取，這可能會導致開發者抓狂。
>
> 新增外掛後，您可能需要重建方案。如果您在 `Nop.Web\Plugins\PLUGIN_OUTPUT_DIRECTORY` 下沒有看到您的外掛 DLL，則需要重建方案。如果您的 DLL 不存在於 `Nop.Web` 的正確資料夾中，nopCommerce 將不會在*在地化外掛*頁面中列出您的外掛。

## 處理請求：控制器、模型與視圖

現在，您可以透過前往 **後台管理 → 設定 → 本地外掛** 來看到您的外掛。但正如您所料，目前我們的外掛什麼功能都沒有，甚至連用於設定的使用者介面都沒有。讓我們建立一個頁面來設定該外掛。

我們現在需要做的是建立一個控制器（Controller）、一個模型（Model）和一個視圖（View）。

1. MVC 控制器負責回應針對 ASP.NET Core MVC 網站提出的請求。每個瀏覽器請求都會對應到特定的控制器。
1. 視圖包含傳送給瀏覽器的 HTML 標記與內容。在 `ASP.NET Core MVC` 應用程式中，視圖相當於一個頁面。
1. MVC 模型包含應用程式中所有不屬於視圖或控制器的邏輯。

您可以找到更多關於 MVC 模式的資訊 [here](https://docs.microsoft.com/aspnet/core/mvc/overview?view=aspnetcore-6.0)。

那麼讓我們開始吧：

- **建立模型**：在新的外掛中新增一個 **Models** 資料夾，然後新增一個符合您需求的模型類別。
- **建立視圖**：在新的外掛中新增一個 **Views** 資料夾，然後新增一個名為 `Configure.cshtml` 的 `*.cshtml` 檔案。將該視圖檔案的 **「建置動作」(Build Action)** 屬性設定為 **「內容」(Content)**，並將 **「複製到輸出目錄」(Copy to Output Directory)** 屬性設定為 **「永遠複製」(Copy always)**。請注意，設定頁面應使用 `_ConfigurePlugin` 版面配置。
- 同時請確保您的 \Views 目錄下擁有 `_ViewImports.cshtml` 檔案。您可以直接從任何現有的其他外掛中複製過來。
- **建立控制器**：在新的外掛中新增一個 **Controllers** 資料夾，然後新增一個控制器類別。一個良好的慣例是將外掛控制器命名為 `{Group}{Name}Controller.cs`。例如：`PaymentPayPalStandardController`。當然，這並非強制性要求（僅為建議）。接著，為設定頁面（在後台管理區）建立適當的動作方法。讓我們將其命名為 *`Configure`*。準備一個模型類別，並使用實體視圖路徑將其傳遞給視圖：`~/Plugins/{PluginOutputDirectory}/Views/Configure.cshtml`。
- 為您的動作方法使用下列屬性：

```csharp
public override string GetConfigurationPageUrl()
{
    return $"{_webHelper.GetStoreLocation()}Admin/{CONTROLLER_NAME}/{ACTION_NAME}";
}
```

其中 **{CONTROLLER_NAME}** 是您的控制器名稱，**{ACTION_NAME}** 是動作名稱（通常為 `Configure`）。

一旦您安裝了外掛並新增了設定方法，您就會在 **後台管理 → 設定 → 本地外掛** 下方找到設定該外掛的連結。

> [!TIP]
> 完成上述步驟最簡單的方法，是打開任何其他外掛，將這些檔案複製到您的外掛專案中，然後重新命名適當的類別與目錄。

例如，*PayPalStandard* 外掛的專案結構如下圖所示：

![p3](_static/how-to-write-plugin-4.50/write_plugin_4.50_3.jpg)

## 處理 "InstallAsync"、"UninstallAsync" 與 "UpdateAsync" 方法

此步驟為選用。某些外掛在安裝過程中可能需要額外的邏輯。例如，外掛可能需要插入新的在地化資源。因此，請開啟您的 `IPlugin` 實作（大多數情況下將繼承自 `BasePlugin` 類別）並覆寫下列方法：

1. **InstallAsync**。此方法將在外掛安裝期間被呼叫。您可以在此初始化任何設定、插入新的在地化資源，或建立新的資料庫資料表（如有需要）。
1. **UninstallAsync**。此方法將在外掛解除安裝期間被呼叫。
1. **UpdateAsync**。此方法將在外掛更新期間（當其版本在 `plugin.json` 檔案中變更時）被呼叫。

> [!IMPORTANT]
> 如果您覆寫了其中一個方法，請勿隱藏其基礎實作。

例如，被覆寫的 `InstallAsync` 方法應包含下列方法呼叫：*`base.Install()`*。*PayPalStandard* 外掛的 `InstallAsync` 方法看起來如下方程式碼所示：

```csharp
public override async Task InstallAsync()
{
    await _settingService.SaveSettingAsync(new PayPalStandardPaymentSettings
    {
        UseSandbox = true
    });
    
    await _localizationService.AddLocaleResourceAsync(new Dictionary<string, string>
    {
        ...
    });
    await base.InstallAsync();
}
```

> [!TIP]
> 已安裝外掛的列表位於 `\App_Data\plugins.json`。該列表會在安裝期間建立。

## 路由

在這裡，我們將了解如何註冊外掛路由。ASP.NET Core 路由負責將傳入的瀏覽器請求映射到特定的 MVC 控制器動作。您可以找到更多關於路由的資訊 [here](https://docs.microsoft.com/aspnet/core/fundamentals/routing)。請遵循以下步驟：

如果您需要新增自定義路由，請建立 `RouteProvider.cs` 檔案。它會將外掛路由通知 nopCommerce 系統。例如，以下的 `RouteProvider` 類別新增了一個新路由，可以透過開啟您的網路瀏覽器並導航至 `http://www.yourStore.com/Plugins/PaymentPayPalStandard/PDTHandler` 網址來存取（由 *PayPal* 外掛使用）：

```csharp
public partial class RouteProvider : IRouteProvider
{
    public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        //PDT
        endpointRouteBuilder.MapControllerRoute("Plugin.Payments.PayPalStandard.PDTHandler", "Plugins/PaymentPayPalStandard/PDTHandler",
                new { controller = "PaymentPayPalStandard", action = "PDTHandler" });
    }
    public int Priority => -1;
}
```

## 升級 nopCommerce 可能會導致外掛失效

有些外掛可能會因為過時而無法在新版本的 nopCommerce 中運作。如果您在升級至新版本後遇到問題，請先刪除該外掛，並前往 nopCommerce 官方網站查看是否有提供新版本。許多外掛開發者會更新其外掛以適應新版本，但也有部分開發者不會這麼做，導致其外掛隨著 nopCommerce 的改進而變得不再適用。但在大多數情況下，您只需開啟對應的 `plugin.json` 檔案，並更新 **SupportedVersions** 欄位即可。

## 結論

希望這篇文章能幫助您開始使用 nopCommerce，並為您開發更複雜的外掛做好準備。

## 外掛範本

您可以使用我們為開發 nopCommerce 新外掛所提供的 Visual Studio 範本。它能為開發人員節省大量時間，因為開發人員現在不需要手動完成所有的初始步驟。例如建立資料夾（Controllers、Views、Models 等）、建立其他必要的檔案（DependencyRegistrar.cs、_ViewImports.cshtml、ObjectContext、plugin.json 等）、進行配置、設定專案參考等等。請點擊此處 [here](https://github.com/nopSolutions/nopCommerce-plugin-template-VS/) 取得範本及安裝說明。