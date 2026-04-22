---
標題: 如何為 nopCommerce 編寫外掛
uid: zh-Hant/developer/plugins/how-to-write-plugin-4.30
作者: git.AndreiMaz
貢獻者: git.skoshelev, git.cromatido, git.DmitriyKulagin
---

# 如何為 nopCommerce 編寫外掛

外掛用於擴充 nopCommerce 的功能。nopCommerce 擁有多種型別的外掛。例如，付款提供程序（如 PayPal）、稅務提供程序、配送計算方式（如 UPS、USP、FedEx）、小部件（如「即時聊天」區塊）以及其他許多型別。nopCommerce 預設已經內建了許多不同的外掛。您也可以在 [nopCommerce 官方網站](https://www.nopcommerce.com/marketplace) 上搜尋各種外掛，看看是否已經有人建立了符合您需求的外掛。如果沒有，本文將引導您完成建立外掛的流程。

## 外掛結構、必要檔案與位置

1. 首先，您需要在解決方案中建立一個新的「類別庫 (Class Library)」專案。建議將所有外掛放置在解決方案根目錄下的 `\Plugins` 目錄中（請勿與位於 `\Nop.Web` 目錄下用於已部署外掛的 `\Plugins` 子目錄混淆）。此外，將所有外掛放置在 "Plugins" 解決方案資料夾中也是一種良好的做法。

    外掛專案的建議命名方式為 "Nop.Plugin.{Group}.{Name}"。其中 {Group} 是您的外掛群組（例如 "Payment" 或 "Shipping"），{Name} 是您的外掛名稱（例如 "PayPalStandard"）。例如，PayPal Standard 付款外掛的名稱為：Nop.Plugin.Payments.PayPalStandard。但請注意，這並非強制要求，您可以為外掛選擇任何名稱，例如 "MyGreatPlugin"。

    ![p1](_static/how-to-write-plugin-4.30/write_plugin_4.30_1.jpg)

1. 外掛專案建立完成後，您必須使用任何文字編輯器開啟其 `.csproj` 檔案，並將內容替換為以下內容：

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        <PropertyGroup>
            <TargetFramework>netcoreapp3.1</TargetFramework>
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
    > 其中 PLUGIN_OUTPUT_DIRECTORY 應替換為外掛名稱，例如 Payments.PayPalStandard。
    >
    > 我們這樣做是為了能使用 .NET Core 引入的新方法來新增第三方參考。但這並非強制，而且已經參考的程式庫之相關參考項目會自動載入，這非常方便。

1. 下一個步驟是建立每個外掛皆須具備的 `plugin.json` 檔案。此檔案包含描述您外掛的中繼資訊。您只需從任何其他現有的外掛複製此檔案，並根據需要進行修改即可。關於 `plugin.json` 檔案的詳細資訊，請參閱 [plugin.json 檔案](xref:zh-Hant/developer/plugins/plugin_json)。

1. 最後一個必要步驟是建立一個實作 **IPlugin** 介面（位於 Nop.Services.Plugins 命名空間）的類別。nopCommerce 提供了 **BasePlugin** 類別，它已經實作了部分 IPlugin 方法，可讓您避免程式碼重複。nopCommerce 還提供了一些衍生自 IPlugin 的特定介面。例如，我們有 "IPaymentMethod" 介面，用於建立新的付款提供程序外掛。它包含了一些僅針對付款提供程序特有的方法，例如 *ProcessPayment()* 或 *GetAdditionalHandlingFee()*。目前，nopCommerce 擁有以下特定的外掛介面：

   - **IPaymentMethod**：這些外掛用於處理付款。
   - **IShippingRateComputationMethod**：這些外掛用於取得可用的配送方式及相應的運費計算。例如 UPS、FedEx 等。
   - **IPickupPointProvider**：這些外掛用於提供取貨點資訊。
   - **ITaxProvider**：稅務提供程序用於獲取稅率。
   - **IExchangeRateProvider**：用於獲取貨幣匯率。
   - **IDiscountRequirementRule**：允許您建立新的折扣規則，例如「顧客的帳單國家/地區必須為……」。
   - **IExternalAuthenticationMethod**：用於建立外部驗證方法，例如 Facebook、Twitter、OpenID 等。
   - **IWidgetPlugin**：允許您建立小部件。小部件會呈現在網站的特定區域。例如，網站左側欄位的「即時通訊」區塊。
   - **IMiscPlugin**：若您的外掛不符合上述任何介面，則使用此介面。

> [!IMPORTANT]
> 重要提醒：在每次建置專案後，請在進行更動前先清理方案（Clean Solution）。某些資源會被快取，這可能會導致開發人員感到困擾。
>
> 在加入外掛後，您可能需要重新建置您的方案。如果您在 Nop.Web\Plugins\PLUGIN_OUTPUT_DIRECTORY 下沒有看到您的外掛 DLL 檔案，您必須重新建置您的方案。如果您的 DLL 檔案未存在於 Nop.Web 中的正確資料夾內，nopCommerce 將不會在「本地外掛」頁面中列出您的外掛。

## 處理請求：控制器、模型與檢視

現在您可以透過前往 **後台管理 → 設定 → 本地外掛** 來查看該外掛。但如您所料，我們的外掛目前什麼功能都沒有。它甚至沒有用於設定的外掛使用者介面。讓我們建立一個頁面來設定此外掛。

我們現在需要做的是建立一個控制器（Controller）、一個模型（Model）和一個檢視（View）。

1. MVC 控制器負責回應針對 ASP.NET MVC 網站發出的請求。每個瀏覽器請求都會對應到特定的控制器。
1. 檢視包含發送到瀏覽器的 HTML 標記和內容。在開發 ASP.NET MVC 應用程式時，檢視相當於網頁。
1. MVC 模型包含應用程式中所有未包含在檢視或控制器中的邏輯。

您可以透過 [here](https://docs.microsoft.com/aspnet/core/mvc/overview?view=aspnetcore-3.1) 找到更多關於 MVC 模式的資訊。

那麼讓我們開始吧：

- **建立模型**。在外掛中新增一個 **Models** 資料夾，然後新增一個符合您需求的模型類別。
- **建立檢視**。在外掛中新增一個 **Views** 資料夾，然後新增一個名為 `Configure.cshtml` 的 cshtml 檔案。將該檢視檔案的 **「建置動作 (Build Action)」** 屬性設定為 **「內容 (Content)」**，並將 **「複製到輸出目錄 (Copy to Output Directory)」** 屬性設定為 **「永遠複製 (Copy always)」**。請注意，設定頁面應使用 "_ConfigurePlugin" 版面配置。
- 同時請確保您的 \Views 目錄中包含 `_ViewImports.cshtml` 檔案。您可以直接從任何其他現有的外掛中複製它。
- **建立控制器**。在外掛中新增一個 **Controllers** 資料夾，然後新增一個控制器類別。一個好的做法是將外掛控制器命名為 `{Group}{Name}Controller.cs`。例如：PaymentPayPalStandardController。當然，這並非強制命名要求（僅為建議）。接著，為設定頁面（在後台管理區域中）建立一個適當的動作方法。讓我們將其命名為 *"Configure"*。準備一個模型類別，並使用實體檢視路徑將其傳遞給檢視：`~/Plugins/{PluginOutputDirectory}/Views/Configure.cshtml`。
- 為您的動作方法使用下列屬性：

```csharp
public override string GetConfigurationPageUrl()
{
    return $"{_webHelper.GetStoreLocation()}Admin/{CONTROLLER_NAME}/{ACTION_NAME}";
}
```

其中 *{CONTROLLER_NAME}* 是您的控制器名稱，而 *{ACTION_NAME}* 是動作名稱（通常為 "Configure"）。

一旦您安裝了外掛並新增了設定方法，您就能在 **後台管理 → 設定 → 本地外掛** 下方找到設定此外掛的連結。

> [!TIP]
> 完成上述步驟最簡單的方法是打開任何其他外掛，並將這些檔案複製到您的外掛專案中。然後只需重新命名適當的類別和目錄即可。

例如，PayPalStandard 外掛的專案結構如下圖所示：

![p3](_static/how-to-write-plugin-4.30/write_plugin_4.30_3.jpg)

## 處理「安裝 (Install)」、「解除安裝 (Uninstall)」與「更新 (Update)」方法

此步驟為選用。有些外掛在安裝過程中可能需要額外的邏輯。例如，外掛可能需要插入新的在地化資源。請開啟您的 IPlugin 實作（在大多數情況下，它會繼承自 BasePlugin 類別）並覆寫下列方法：

1. **Install**：此方法會在安裝外掛時被呼叫。您可以在此初始化任何設定、插入新的在地化資源，或建立新的資料庫資料表（若有需要）。
2. **Uninstall**：此方法會在解除安裝外掛時被呼叫。
3. **Update**：此方法會在更新外掛時被呼叫（即 `plugin.json` 檔案中的版本號有所變更時）。

> [!IMPORTANT]
> 重要提示：如果您覆寫了這些方法中的其中一個，請勿隱藏其基礎實作。

例如，覆寫後的「Install」方法應該包含下列方法呼叫：*base.Install()*。PayPalStandard 外掛的「Install」方法程式碼如下所示：

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
> 已安裝外掛的列表位於 `\App_Data\plugins.json`。該列表會在安裝過程中建立。

## 路由

在這裡，我們將探討如何註冊外掛路由。ASP.NET Core 路由負責將傳入的瀏覽器請求映射到特定的 MVC 控制器動作（Action）。您可以透過 [here](https://docs.microsoft.com/aspnet/core/fundamentals/routing) 找到更多關於路由的資訊。請按照以下步驟操作：

如果您需要新增自訂路由，請建立 `RouteProvider.cs` 檔案。它會將外掛路由通知給 nopCommerce 系統。例如，以下的 RouteProvider 類別新增了一條新路由，可以透過開啟您的網頁瀏覽器並導向 `http://www.yourStore.com/Plugins/PaymentPayPalStandard/PDTHandler` 這個 URL 來存取（由 PayPal 外掛使用）：

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

部分外掛可能會因為過時，而無法在新版本的 nopCommerce 中運作。如果您在升級至新版本後遇到問題，請刪除該外掛，並前往 nopCommerce 官方網站查看是否有更新的版本。許多外掛開發者會更新其外掛以適應新版本，但也有部分開發者不會這麼做，導致其外掛隨著 nopCommerce 的改進而變得不再適用。不過在大多數情況下，您只需要開啟對應的 `plugin.json` 檔案，並更新 **SupportedVersions** 欄位即可。

## 結論

希望這些內容能協助您開始使用 nopCommerce，並為您構建更複雜的外掛做好準備。

## 外掛模板

您可以使用我們為 nopCommerce 新外掛提供的 Visual Studio 模板。這能為開發者節省大量時間，因為現在您不必手動執行所有初始步驟。例如建立資料夾（Controllers、Views、Models 等）、其他必要檔案（DependencyRegistrar.cs、_ViewImports.cshtml、ObjectContext、plugin.json 等）、設定組態、專案參考等。請參閱 [here](https://github.com/nopSolutions/nopCommerce-plugin-template-VS/) 以取得模板檔案與安裝說明。