---
標題: 如何為 nopCommerce 編寫外掛
uid: zh-Hant/developer/plugins/how-to-write-plugin-3.90
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.exileDev
---

# 如何為 nopCommerce 3.90 (及更早版本) 編寫外掛

> 在計算領域中，外掛（plugin）是一組能為大型軟體應用程式增加特定功能的軟體元件（維基百科）。

外掛用於擴充 nopCommerce 的功能。nopCommerce 擁有多種型別的外掛。例如：付款方式（如 PayPal）、稅務提供程序、配送方式計算方法（如 UPS、USPS、FedEx）、小部件（如「線上客服」區塊）等等。nopCommerce 本身已內建許多不同的外掛。您也可以在 [nopCommerce 官方網站](https://www.nopcommerce.com/marketplace) 上搜尋各種外掛，看看是否已經有人開發出符合您需求的外掛。如果沒有，本文將指引您完成建立外掛的流程。
## 外掛結構、必要檔案與位置

1. 您首先需要做的是在方案中建立一個新的「類別庫 (Class Library)」專案。將所有外掛放置在方案根目錄的 `\Plugins` 資料夾中是一種良好的實踐（請勿與位於 `\Nop.Web` 目錄下、用於已部署外掛的 `\Plugins` 子目錄混淆）。將所有外掛放置在「Plugins」方案資料夾中也是一種良好的實踐（您可以在[這裡](http://msdn.microsoft.com/library/sx2027y2.aspx)找到更多關於方案資料夾的資訊）。

    建議的外掛專案命名為 "Nop.Plugin.{Group}.{Name}"。{Group} 是您的外掛群組（例如 "Payment" 或 "Shipping"）。{Name} 是您的外掛名稱（例如 "PayPalStandard"）。例如，PayPal Standard 付款外掛的名稱為：Nop.Plugin.Payments.PayPalStandard。但請注意，這並非強制要求，您可以為外掛選擇任何名稱，例如 "MyGreatPlugin"。

    ![p1](_static/how-to-write-plugin-3.90/write_plugin_3.90_4.jpg)

1. 建立外掛專案後，請更新專案的組建輸出路徑。將其設定為 `..\..\Presentation\Nop.Web\Plugins\{Group}.{Name}`。例如，Authorize.NET 付款外掛的輸出路徑為：`..\..\Presentation\Nop.Web\Plugins\Payments.AuthorizeNet`。完成後，相關的外掛 DLL 將會自動複製到 `\Presentation\Nop.Web\Plugins` 目錄中，nopCommerce 核心會在此目錄搜尋有效的外掛。同樣地，這也不是強制性的，您可以為外掛選擇任何輸出目錄名稱。

    ![p1](_static/how-to-write-plugin-3.90/write_plugin_3.90_1.jpg)

    - 在「專案 (Project)」選單中，點選「屬性 (Properties)」。
    - 點選「建置 (Build)」索引標籤。
    - 點選「輸出路徑 (Output path)」欄位旁的「瀏覽 (Browse)」按鈕，並選擇一個新的建置輸出目錄。

    您應該對所有現有的組態（"Debug" 與 "Release"）執行上述步驟。

1. 下一個步驟是為每個外掛建立必要的 `Description.txt` 檔案。此檔案包含描述您外掛的元資訊。只需從任何其他現有的外掛中複製此檔案並根據需求修改即可。例如，PayPal Standard 付款外掛具有以下 `Description.txt` 檔案：

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

    所有欄位皆可望文生義，但以下提供幾點注意事項。**SystemName** 欄位必須是唯一的。**Version** 欄位是您的外掛版本號；您可以將其設為任何您喜歡的值。**SupportedVersions** 欄位可以包含以逗號分隔的支援 nopCommerce 版本清單（請確保當前的 nopCommerce 版本包含在此清單中，否則外掛將無法載入）。**FileName** 欄位格式為 *Nop.Plugin.{Group}.{Name}.dll*（這是您的外掛組件檔名）。請確保將此檔案的「複製到輸出目錄 (Copy to Output Directory)」屬性設定為「有更新時才複製 (Copy if newer)」。

    ![p2](_static/how-to-write-plugin-3.90/write_plugin_3.90_2.jpg)

1. 您也應該建立一個 `web.config` 檔案，並確保它被複製到輸出目錄中。只需從現有的外掛中複製即可。

    > [!IMPORTANT]
    > 往後請確保所有第三方組件參考（包括核心函式庫如 Nop.Services.dll 或 Nop.Web.Framework.dll）的「複製本機 (Copy local)」屬性皆設定為「False」（不複製）。

1. 最後一個必要步驟是建立一個實作 `IPlugin` 介面（位於 `Nop.Core.Plugins` 命名空間）的類別。nopCommerce 提供了 `BasePlugin` 類別，它已經實作了一些 `IPlugin` 的方法，可以讓您避免原始程式碼重複。nopCommerce 也提供了一些衍生自 `IPlugin` 的特定介面。例如，我們有用於建立新付款方式外掛的 "IPaymentMethod" 介面。它包含一些僅針對付款方式的方法，如 `ProcessPayment()` 或 `GetAdditionalHandlingFee()`。目前，nopCommerce 擁有以下特定的外掛介面：

   - **IPaymentMethod**。這些外掛用於處理付款。
   - **IShippingRateComputationMethod**。這些外掛用於擷取可用的配送方式及對應的運費計算。例如 UPS、FedEx 等。
   - **IPickupPointProvider**。這些外掛用於提供取貨點。
   - **ITaxProvider**。稅務提供程序用於取得稅率。
   - **IExchangeRateProvider**。用於取得貨幣匯率。
   - **IDiscountRequirementRule**。允許您建立新的折扣規則，例如「顧客的帳單國家必須是…」
   - **IExternalAuthenticationMethod**。用於建立外部驗證方式，例如 Facebook、Twitter、OpenID 等。
   - **IWidgetPlugin**。允許您建立小部件。小部件會渲染在網站的某些部位。例如，它可能是您網站左欄中的「即時對談」區塊。
   - **IMiscPlugin**。若您的外掛不適用於上述任何介面時使用。
> [!IMPORTANT]
> 每次建置專案後，在進行更改前請先清理方案。部分資源會被快取，這可能會導致開發人員陷入瘋狂。
## 處理請求：控制器、模型與視圖

現在，您可以透過前往 **後台管理 → 設定 → 外掛** 來看到該外掛。但正如您所料，我們的外掛目前什麼功能都沒有。它甚至沒有用於設定的使用者介面。讓我們建立一個頁面來設定此外掛。

我們現在需要做的是建立一個控制器 (Controller)、一個模型 (Model) 和一個視圖 (View)。

- MVC 控制器負責回應對 ASP.NET MVC 網站提出的請求。每個瀏覽器請求都會對應到特定的控制器。
- 視圖包含傳送到瀏覽器的 HTML 標記與內容。在 ASP.NET MVC 應用程式中，視圖相當於一個頁面。
- MVC 模型包含應用程式中所有未包含在視圖或控制器中的應用程式邏輯。

您可以透過 [這裡](http://www.asp.net/mvc/tutorials/older-versions/overview/understanding-models-views-and-controllers-cs) 找到關於 MVC 模式的更多資訊。

那麼讓我們開始吧：

- **建立模型**。在外掛中新增一個 Models 資料夾，然後新增一個符合您需求的模型類別。
- **建立視圖**。在外掛中新增一個 Views 資料夾，接著新增一個 {Name} 資料夾（其中 {Name} 為您的外掛名稱），最後新增一個名為 `Configure.cshtml` 的 cshtml 檔案。重要注意事項：對於 2.00-3.30 版本，視圖應標記為「內嵌資源 (Embedded Resource)」。而從 3.40 版本開始，請確保視圖檔案的「建置動作 (Build Action)」屬性設為「內容 (Content)」，並將「複製到輸出目錄 (Copy to Output Directory)」屬性設為「有更新時複製 (Copy if newer)」。
- **建立控制器**。在外掛中新增一個 Controllers 資料夾，然後新增一個控制器類別。一種良好的做法是將外掛控制器命名為 `{Group}{Name}Controller.cs`。例如，PaymentAuthorizeNetController。當然，這並非強制性要求（僅為建議）。接著，為設定頁面（在後台管理區域）建立適當的動作方法。讓我們將其命名為 "Configure"。準備一個模型類別並將其傳遞給對應的視圖。對於 nopCommerce 2.00-3.30 版本，您應該傳遞內嵌視圖路徑 - "Nop.Plugin.{Group}.{Name}.Views.{Group}{Name}.Configure"。從 nopCommerce 3.40 版本開始，您應該傳遞實體視圖路徑 - `~/Plugins/{PluginOutputDirectory}/Views/{ControllerName}/Configure.cshtml`。例如，請開啟 Authorize.NET 付款外掛並查看其 PaymentAuthorizeNetController 的實作方式。

    > [!TIP]
    >
    > - 完成上述步驟最簡單的方法是開啟任何其他外掛，並將這些檔案複製到您的外掛專案中，然後只需重新命名對應的類別與目錄即可。
    >
    > - 如果您想限制只有管理員（商店擁有者）才能存取控制器的特定動作方法，只需將其標記為 [AdminAuthorize] 屬性即可。

    例如，Authorize.NET 外掛的專案結構如下圖所示：

    ![p3](_static/how-to-write-plugin-3.90/write_plugin_3.90_3.jpg)
## 路由

現在我們需要註冊適當的外掛路由。ASP.NET 路由負責將進入的瀏覽器請求對應到特定的 MVC 控制器動作。您可以在http://www.asp.net/mvc/tutorials/older-versions/controllers-and-routing/asp-net-mvc-routing-overview-cs找到關於路由的更多資訊。請遵循以下步驟：

- 上述提到的部分特定外掛介面以及「IMiscPlugin」介面擁有以下方法：「GetConfigurationRoute」。它應該回傳一個用於外掛設定的控制器動作路由。實作您外掛介面中的「GetConfigurationRoute」方法。此方法會告知 nopCommerce 使用什麼路由來進行外掛設定。如果您的外掛沒有設定頁面，則「GetConfigurationRoute」應回傳 null。例如，請參閱下方的程式碼：

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

- (選用) 如果您需要新增一些自訂路由，請建立 `RouteProvider.cs` 檔案。它會將外掛路由資訊告知 nopCommerce 系統。例如，下方的 RouteProvider 類別新增了一個新路由，可以透過開啟網頁瀏覽器並瀏覽至 `http://www.yourStore.com/Plugins/PaymentPayPalStandard/PDTHandler` 這個 URL 來存取（PayPal 外掛會使用此路由）：

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

    一旦您安裝了外掛並新增了設定方法，您就會在後台管理 → 設定 → 外掛中找到設定您外掛的連結。
## 處理 "Install" 與 "Uninstall" 方法

此步驟為選用。有些外掛在安裝過程中可能需要額外的邏輯。例如，外掛可能需要插入新的在地化資源。因此，請開啟您的 IPlugin 實作（大多數情況下會繼承自 BasePlugin 類別）並覆寫下列方法：

- Install。此方法將在安裝外掛期間被呼叫。您可以在此處初始化任何設定、插入新的在地化資源，或建立新的資料庫資料表（如有必要）。
- Uninstall。此方法將在解除安裝外掛期間被呼叫。

> [!IMPORTANT]
> 如果您覆寫了這些方法之一，請勿隱藏其基礎實作。

例如，Authorize.NET 外掛的專案結構如下圖所示：

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
> 已安裝外掛的清單位於 `\App_Data\InstalledPlugins.txt`。此清單是在安裝過程中建立的。
## 升級 nopCommerce 可能會導致外掛失效

有些外掛可能會因為過時而無法在新版本的 nopCommerce 中運作。如果您在升級至新版本後遇到問題，請先刪除該外掛，並前往 nopCommerce 官方網站查看是否有提供新版本。許多外掛開發者會更新其外掛以適應新版本，但也有部分開發者不會這麼做，導致其外掛隨著 nopCommerce 的改進而變得不再適用。不過在大多數情況下，您只需開啟對應的 `plugin.json` 檔案並更新 **SupportedVersions** 欄位即可。
## 結論

希望這能幫助您順利開始使用 nopCommerce，並為您構建更複雜的外掛做好準備。