---
標題: 如何為 nopCommerce 編寫外掛
uid: zh-Hant/developer/plugins/how-to-write-plugin-4.10
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.exileDev, git.cromatido
---

# 如何為 nopCommerce 4.10 編寫外掛

> 在電腦運算中，外掛（plugin）是一組能為大型軟體應用程式增加特定功能的軟體元件（維基百科）。

外掛用於擴充 nopCommerce 的功能。nopCommerce 擁有多種不同類型的外掛。例如：付款方式（如 PayPal）、稅務提供程序、配送方式計算方法（如 UPS、USPS、FedEx）、小部件（如「即時對談」區塊）等等。nopCommerce 本身已經隨附了許多不同的外掛。您也可以在 [nopCommerce 官方網站](https://www.nopcommerce.com/marketplace) 上搜尋各種外掛，看看是否有人已經開發出符合您需求的外掛。如果沒有，本文將引導您完成建立外掛的過程。

## 外掛結構、必要檔案與位置

1. 您首先需要做的是在方案中建立一個新的「類別庫 (Class Library)」專案。將所有外掛放置在方案根目錄的 `\Plugins` 目錄中是一個良好的習慣（請勿與位於 `\Nop.Web` 目錄下的 `\Plugins` 子目錄混淆，後者用於已經部署的外掛）。將所有外掛放置在「Plugins」方案資料夾中也是一個良好的習慣（關於方案資料夾的更多資訊，請參閱 [here](http://msdn.microsoft.com/library/sx2027y2.aspx)）。

    外掛專案的建議命名方式為「Nop.Plugin.{Group}.{Name}」。{Group} 是您的外掛群組（例如，「Payment」或「Shipping」）。{Name} 是您的外掛名稱（例如，「PayPalStandard」）。例如，PayPal Standard 付款外掛的名稱如下：Nop.Plugin.Payments.PayPalStandard。但請注意，這並非強制要求，您可以為外掛選擇任何名稱，例如「MyGreatPlugin」。

    ![p1](_static/how-to-write-plugin-4.10/write_plugin_4.10_1.jpg)

1. 一旦建立了外掛專案，您必須使用任何文字編輯器開啟其 `.csproj` 檔案，並將其內容取代為以下內容：

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        <PropertyGroup>
        <TargetFramework>netcoreapp2.1</TargetFramework>
        </PropertyGroup>
        <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
        <OutputPath>..\..\Presentation\Nop.Web\Plugins\PLUGIN_OUTPUT_DIRECTORY</OutputPath>
        <OutDir>$(OutputPath)</OutDir>
        </PropertyGroup>
        <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
        <OutputPath>..\..\Presentation\Nop.Web\Plugins\PLUGIN_OUTPUT_DIRECTORY</OutputPath>
        <OutDir>$(OutputPath)</OutDir>
        </PropertyGroup>
        <!-- This target execute after "Build" target -->
        <Target Name="NopTarget" AfterTargets="Build">
        <!-- Delete unnecessary libraries from plugins path -->
        <MSBuild Projects="$(MSBuildProjectDirectory)\..\..\Build\ClearPluginAssemblies.proj"    Properties="PluginPath=$(MSBuildProjectDirectory)\$(OutDir)" Targets="NopClear" />
        </Target>
    </Project>
    ```

    其中 PLUGIN_OUTPUT_DIRECTORY 應替換為外掛名稱，例如 Payments.PayPalStandard。

    我們採用這種方式是為了能夠使用 .NET Core 中引入的新方法來加入第三方參考，但這並非必要。此外，來自已參考函式庫的參考將會自動載入，因此非常方便。

1. 下一個步驟是建立每個外掛都需要的 `plugin.json` 檔案。此檔案包含描述您外掛的中繼資訊。只需從任何其他現有的外掛中複製此檔案，並根據您的需求進行修改即可。關於 `plugin.json` 檔案的資訊，請參閱 [plugin.json 檔案](xref:zh-Hant/developer/plugins/plugin_json)。

1. 最後一個必要步驟是建立一個實作 `IPlugin` 介面（Nop.Core.Plugins 命名空間）的類別。nopCommerce 提供了 `BasePlugin` 類別，它已經實作了一些 IPlugin 方法，讓您可以避免原始程式碼重複。nopCommerce 也提供了一些衍生自 `IPlugin` 的特定介面。例如，我們有「IPaymentMethod」介面，用於建立新的付款方式外掛。它包含一些僅針對付款方式的特定方法，例如 ProcessPayment() 或 GetAdditionalHandlingFee()。目前，nopCommerce 擁有以下特定的外掛介面：

   - **IPaymentMethod**。這些外掛用於付款處理。
   - **IShippingRateComputationMethod**。這些外掛用於擷取可用的配送方式與對應的運費。例如 UPS、FedEx 等。
   - **IPickupPointProvider**。這些外掛用於提供取貨點。
   - **ITaxProvider**。稅務提供程序用於獲取稅率。
   - **IExchangeRateProvider**。用於獲取貨幣匯率。
   - **IDiscountRequirementRule**。允許您建立新的折扣規則，例如「顧客的帳單國家/地區必須是……」。
   - **IExternalAuthenticationMethod**。用於建立外部驗證方法，例如 Facebook、Twitter、OpenID 等。
   - **IWidgetPlugin**。允許您建立小部件。小部件會渲染在您網站的某些部分。例如，它可以是您網站左欄中的「線上客服」區塊。
   - **IMiscPlugin**。如果您的外掛不適用於上述任何介面。

> [!IMPORTANT]
> 在每次專案建置後，請在進行更改前清理方案。某些資源會被快取，這可能會導致開發者感到困擾。

## 處理請求：控制器、模型與檢視

現在您可以透過前往 **後台管理 → 設定 → 外掛** 來看到該外掛。但正如您所料，我們的外掛目前什麼也沒做。它甚至沒有用於設定的使用者介面。讓我們建立一個頁面來設定外掛。

我們現在需要做的是建立一個控制器 (Controller)、一個模型 (Model) 和一個檢視 (View)。

- MVC 控制器負責回應對 ASP.NET MVC 網站發出的請求。每個瀏覽器請求都會對應到特定的控制器。
- 檢視包含傳送到瀏覽器的 HTML 標記和內容。在開發 ASP.NET MVC 應用程式時，檢視相當於一個頁面。
- MVC 模型包含應用程式中不屬於檢視或控制器的所有邏輯。

您可以找到更多關於 MVC 模式的資訊 [here](http://www.asp.net/mvc/tutorials/older-versions/overview/understanding-models-views-and-controllers-cs)。

讓我們開始吧：

- **建立模型**：在新的外掛中新增一個 Models 資料夾，然後新增一個符合您需求的模型類別。
- **建立檢視**：在新的外掛中新增一個 Views 資料夾，然後新增一個名為 `Configure.cshtml` 的 cshtml 檔案。將該檢視檔案的 "Build Action"（建置動作）屬性設定為 "Content"（內容），並將 "Copy to Output Directory"（複製到輸出目錄）屬性設定為 "Copy if newer"（有更新時複製）。請注意，設定頁面應使用 `_ConfigurePlugin` 版面配置。此外，請確保您的 \Views 目錄中有 `_ViewImports` 檔案。您可以直接從任何現有的其他外掛中複製它。
- **建立控制器**：在新的外掛中新增一個 Controllers 資料夾，然後新增一個控制器類別。一個良好的實作方式是將外掛控制器命名為 `{Group}{Name}Controller.cs`。例如：PaymentPayPalStandardController。當然，這並非強制性要求（僅為建議）。接著，為設定頁面（在後台管理區域中）建立適當的動作方法 (Action method)。讓我們將其命名為 "Configure"。準備一個模型類別，並使用實體檢視路徑將其傳遞給對應的檢視：`~/Plugins/{PluginOutputDirectory}/Views/Configure.cshtml`。
- 為您的動作方法使用下列屬性：

    ```csharp
    [AuthorizeAdmin] //confirms access to the admin panel
    [Area(AreaNames.Admin)] //specifies the area containing a controller or action
    ```

    For example, open PayPalStandard payment plugin and look at its implementation of PaymentPayPalStandardController.

Then for each plugin that has a configuration page, you should specify a configuration URL. A base class named `BasePlugin` has `GetConfigurationPageUrl` method which returns a configuration URL:

```csharp
return $"{_webHelper.GetStoreLocation()}Admin/ControllerName/ActionName";
```

其中 ControllerName 是您的控制器名稱，ActionName 是動作的名稱（通常為 "Configure"）。

一旦安裝了外掛並新增了設定方法，您就會在 **後台管理 → 設定 → 外掛** 下方找到一個用來設定外掛的連結。

> [!TIP]
> 完成上述步驟最簡單的方法是開啟任何其他外掛，並將這些檔案複製到您的外掛專案中。然後只需重新命名適當的類別和目錄即可。

例如，PayPalStandard 外掛的專案結構如下圖所示：

![p3](_static/how-to-write-plugin-4.10/write_plugin_4.10_3.jpg)

## 處理 "Install" 與 "Uninstall" 方法

此步驟為選填。某些外掛在安裝過程中可能需要額外的邏輯。例如，外掛可能需要插入新的在地化資源。因此，請開啟您的 IPlugin 實作（大多數情況下會繼承自 BasePlugin 類別）並覆寫以下方法：

- Install。此方法將在安裝外掛時被呼叫。您可以在此初始化任何設定、插入新的在地化資源，或建立新的資料庫資料表（如果需要）。
- Uninstall。此方法將在解除安裝外掛時被呼叫。

> [!IMPORTANT]
> 如果您覆寫了這些方法中的其中一個，請不要隱藏其基礎實作。

例如，覆寫的 "Install" 方法應該包含以下的方法呼叫：base.Install()。PayPalStandard 外掛的 "Install" 方法程式碼如下所示：

```csharp
public override void Install()
{
    var settings = new PayPalStandardPaymentSettings()
    {
        UseSandbox = true
    };
    _settingService.SaveSetting(settings);
    base.Install();
}
```

> [!TIP]
> 已安裝外掛的清單位於 `\App_Data\installedPlugins.json`。該清單是在安裝過程中建立的。

## 路由

我們將在此介紹如何註冊外掛路由。ASP.NET Core 路由負責將傳入的瀏覽器請求對應到特定的 MVC 控制器動作。您可以參閱 [here](https://docs.microsoft.com/aspnet/core/fundamentals/routing) 以取得更多關於路由的資訊。請依照下列步驟進行：

- 如果您需要新增自訂路由，請建立 `RouteProvider.cs` 檔案。它會將外掛路由資訊通知給 nopCommerce 系統。例如，下方的 RouteProvider 類別新增了一個路由，您可以透過瀏覽器開啟 `http://www.yourStore.com/Plugins/PaymentPayPalStandard/PDTHandler` 這個 URL 來存取（由 PayPal 外掛使用）：

    ```csharp
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
             routeBuilder.MapRoute("Plugin.Payments.PayPalStandard.PDTHandler", "Plugins/   PaymentPayPalStandard/PDTHandler",
             new { controller = "PaymentPayPalStandard", action = "PDTHandler" });
        }
        public int Priority
        {
            get
            {
                return -1;
            }
        }
    }
    ```

## 升級 nopCommerce 可能會導致外掛失效

部分外掛可能會因為過時而無法在新版本的 nopCommerce 中運作。如果您在升級至新版本後遇到問題，請刪除該外掛，並前往 nopCommerce 官方網站查看是否有提供更新版本。許多外掛開發者會為了適應新版本而升級其外掛，但也有部分開發者不會這麼做，導致其外掛隨著 nopCommerce 的改良而遭到淘汰。不過在大多數情況下，您只需開啟對應的 `plugin.json` 檔案並更新 **SupportedVersions** 欄位即可。

## 結論

希望這些內容能協助您開始使用 nopCommerce，並為您開發更複雜的外掛做好準備。