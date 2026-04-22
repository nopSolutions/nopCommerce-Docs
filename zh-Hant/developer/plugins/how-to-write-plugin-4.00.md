---
標題: 如何為 nopCommerce 編寫外掛
uid: zh-Hant/developer/plugins/how-to-write-plugin-4.00
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.exileDev, git.cromatido
---

# 如何為 nopCommerce 4.00 編寫外掛

> 在計算領域中，外掛（plugin）是一組能為大型軟體應用程式增加特定功能的軟體元件（維基百科）。

外掛用於擴充 nopCommerce 的功能。nopCommerce 擁有幾種類型的外掛。例如，付款提供程序（如 PayPal）、稅務提供程序、配送方式計算方法（如 UPS、USP、FedEx）、小部件（如「即時通訊」區塊）以及其他許多類型。nopCommerce 本身已經隨附了許多不同的外掛。您也可以在 [nopCommerce 官方網站](https://www.nopcommerce.com/marketplace) 上搜尋各種外掛，看看是否有人已經製作出符合您需求的外掛。如果沒有，這篇文章將引導您完成建立自己外掛的過程。

## 外掛架構、必要檔案與位置

1. 首先，您需要在方案中建立一個新的「類別庫 (Class Library)」專案。建議將所有外掛放置在方案根目錄下的 `\Plugins` 目錄中（請勿與 `\Nop.Web` 目錄下用於已部署外掛的 `\Plugins` 子目錄混淆）。建議將所有外掛放置在「Plugins」方案資料夾中（您可以查閱關於方案資料夾的詳細資訊 [here](http://msdn.microsoft.com/library/sx2027y2.aspx)）。

    建議的外掛專案命名格式為 "Nop.Plugin.{群組}.{名稱}"。{群組} 是您的外掛群組（例如 "Payment" 或 "Shipping"）。{名稱} 是您的外掛名稱（例如 "PayPalStandard"）。例如，PayPal Standard 付款外掛的名稱為：Nop.Plugin.Payments.PayPalStandard。但請注意，這並非強制要求。您可以為外掛選擇任何名稱。例如，「MyGreatPlugin」。

    ![p1](_static/how-to-write-plugin-4.00/write_plugin_4.00_1.jpg)

1. 外掛專案建立後，您必須使用任何文字編輯器開啟其 `.csproj` 檔案，並將其內容替換為以下內容：

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
     <PropertyGroup>
       <TargetFramework>net461</TargetFramework>
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

    我們採用這種方式是為了能夠使用 .NET Core 中引入的添加第三方參照的新方法。但這並非必需。此外，已參照程式庫中的參照將會自動載入，因此非常方便。

1. 下一步是為每個外掛建立所需的 `plugin.json` 檔案。此檔案包含描述您外掛的中繼資料（Meta-information）。只需從任何其他現有外掛複製此檔案，並根據您的需求進行修改即可。有關 `plugin.json` 檔案的資訊，請參閱 [plugin.json 檔案](xref:zh-Hant/developer/plugins/plugin_json)。

1. 最後一個必要步驟是建立一個實作 `IPlugin` 介面（位於 Nop.Core.Plugins 命名空間）的類別。nopCommerce 提供了 `BasePlugin` 類別，它已經實作了一些 `IPlugin` 方法，可讓您避免原始程式碼重複。nopCommerce 也提供了一些衍生自 `IPlugin` 的特定介面。例如，我們有 "IPaymentMethod" 介面，用於建立新的付款方式外掛。它包含一些僅針對付款方式特定的方法，例如 `ProcessPayment()` 或 `GetAdditionalHandlingFee()`。目前，nopCommerce 具有以下特定的外掛介面：

   - **IPaymentMethod**。這些外掛用於處理付款。
   - **IShippingRateComputationMethod**。這些外掛用於擷取可接受的配送方式與對應的運費。例如 UPS、FedEx 等。
   - **IPickupPointProvider**。這些外掛用於提供取貨點。
   - **ITaxProvider**。稅務提供程序用於取得稅率。
   - **IExchangeRateProvider**。用於取得貨幣匯率。
   - **IDiscountRequirementRule**。允許您建立新的折扣規則，例如「顧客的帳單國家/地區必須為……」
   - **IExternalAuthenticationMethod**。用於建立外部驗證方法，例如 Facebook、Twitter、OpenID 等。
   - **IWidgetPlugin**。允許您建立小部件。小部件會呈現於您網站的某些部分。例如，網站左欄的「即時對話」區塊。
   - **IMiscPlugin**。如果您的外掛不適用於上述任何介面。

> [!IMPORTANT]
> 每次專案建置後，請在進行更改前清除方案。部分資源會被快取，可能會導致開發人員陷入困擾。

## 處理請求：控制器、模型與檢視

現在，您可以透過前往 **後台管理 → 設定 → 外掛** 來查看該外掛。但如您所料，我們的外掛目前什麼都沒做，甚至沒有用於設定的使用者介面。讓我們建立一個頁面來設定此外掛。

我們現在需要做的是建立一個控制器 (Controller)、一個模型 (Model) 和一個檢視 (View)。

- MVC 控制器負責回應對 ASP.NET MVC 網站發出的請求。每個瀏覽器請求都會對應到特定的控制器。
- 檢視包含傳送到瀏覽器的 HTML 標記與內容。在 ASP.NET MVC 應用程式中，檢視等同於一個頁面。
- MVC 模型包含應用程式中所有未包含在檢視或控制器中的應用程式邏輯。

您可以找到更多關於 MVC 模式的資訊：[here](http://www.asp.net/mvc/tutorials/older-versions/overview/understanding-models-views-and-controllers-cs)。

那麼，讓我們開始吧：

- **建立模型**。在新的外掛中加入一個 `Models` 資料夾，然後加入一個符合您需求的模型類別。
- **建立檢視**。在新的外掛中加入一個 `Views` 資料夾，然後加入一個名為 `Configure.cshtml` 的 cshtml 檔案。將該檢視檔案的「建置動作 (Build Action)」屬性設為「內容 (Content)」，並將「複製到輸出目錄 (Copy to Output Directory)」屬性設為「有更新時才複製 (Copy if newer)」。請注意，設定頁面應使用 `_ConfigurePlugin` 版面配置。此外，請確保您的 `\Views` 目錄中有 `_ViewImports` 檔案。您可以直接從任何其他現有的外掛中複製該檔案。
- **建立控制器**。在新的外掛中加入一個 `Controllers` 資料夾，然後加入一個新的控制器類別。一個良好的做法是將外掛控制器命名為 `{Group}{Name}Controller.cs`。例如：`PaymentPayPalStandardController`。當然，這並非強制規定（僅為建議）。接著，為設定頁面建立適當的動作方法 (Action Method)（位於後台管理區域）。讓我們將其命名為 "Configure"。準備一個模型類別，並使用實體檢視路徑將其傳遞給檢視：`~/Plugins/{PluginOutputDirectory}/Views/Configure.cshtml`。
- 對您的動作方法使用以下屬性：

```csharp
return $"{_webHelper.GetStoreLocation()}Admin/ControllerName/ActionName";
```

其中 `ControllerName` 是您的控制器名稱，而 `ActionName` 是動作名稱（通常是 "Configure"）。

一旦您安裝了外掛並加入了設定方法，您就會在「後台管理 → 設定 → 外掛」下找到一個連結來設定您的外掛。

> [!TIP]
> 完成上述步驟最簡單的方法是開啟任何其他外掛，並將這些檔案複製到您的外掛專案中，然後重新命名適當的類別與目錄即可。

例如，PayPalStandard 外掛的專案結構如下圖所示：

![p3](_static/how-to-write-plugin-4.00/write_plugin_4.00_3.jpg)

## 處理 "Install" 與 "Uninstall" 方法

此步驟為選用。有些外掛在安裝過程中可能需要額外的邏輯。例如，外掛可能需要插入新的在地化資源。因此，請開啟您的 IPlugin 實作（大多數情況下會繼承自 BasePlugin 類別）並覆寫以下方法：

- Install。此方法將在安裝外掛時被呼叫。您可以在此初始化任何設定、插入新的在地化資源，或建立新的資料庫資料表（若有需要）。
- Uninstall。此方法將在解除安裝外掛時被呼叫。

> [!IMPORTANT]
> 如果您覆寫了這些方法之一，請勿隱藏其基礎實作。

例如，覆寫的 "Install" 方法應包含以下方法呼叫：base.Install()。PayPalStandard 外掛的 "Install" 方法看起來如下面的程式碼所示：

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

在這裡，我們將看看如何註冊外掛的路由。ASP.NET Core 路由負責將傳入的瀏覽器請求映射到特定的 MVC 控制器動作（Action）。您可以透過 [here](https://docs.microsoft.com/aspnet/core/fundamentals/routing) 找到更多關於路由的資訊。請按照以下步驟操作：

- 如果您需要新增自訂路由，請建立 `RouteProvider.cs` 檔案。它會通知 nopCommerce 系統有關外掛的路由資訊。例如，下方的 RouteProvider 類別新增了一個新的路由，您可以透過在瀏覽器開啟 `http://www.yourStore.com/Plugins/PaymentPayPalStandard/PDTHandler` 網址（PayPal 外掛所使用）來存取它：

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

部分外掛可能會因為過時，而無法在新版本的 nopCommerce 中運作。如果您在升級到新版本後遇到問題，請刪除該外掛，並前往 nopCommerce 官方網站查看是否有更新的版本可用。許多外掛開發者會更新其外掛以適應新版本，然而，部分開發者可能不會這麼做，導致其外掛隨著 nopCommerce 的改進而變得無法使用。但在大多數情況下，您只需開啟對應的 `plugin.json` 檔案，並更新 **SupportedVersions** 欄位即可。

## 結論

希望這能幫助您開始使用 nopCommerce，並為您構建更複雜的外掛做好準備。