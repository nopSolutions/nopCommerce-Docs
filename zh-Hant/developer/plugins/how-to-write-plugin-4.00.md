---
標題: 如何為 nopCommerce 編寫外掛
uid: zh-Hant/developer/plugins/how-to-write-plugin-4.00
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.exileDev, git.cromatido
---

# 如何為 nopCommerce 4.00 編寫外掛

> 在電腦運算領域中，外掛（plugin）是一組能為大型軟體應用程式添加特定功能的軟體組件（Wikipedia）。

外掛用於擴充 nopCommerce 的功能。nopCommerce 擁有多種不同類型的外掛。例如：付款方式（如 PayPal）、稅務提供程序、配送方式計算方法（如 UPS、USPS、FedEx）、小部件（如「即時聊天」區塊）以及其他許多類型。nopCommerce 本身已經內建了許多不同的外掛。您也可以在 [nopCommerce 官方網站](https://www.nopcommerce.com/marketplace) 上搜尋各種外掛，看看是否已經有人開發了符合您需求的外掛。如果沒有，本文將引導您完成編寫自已外掛的流程。

## 外掛結構、必要檔案與存放位置

1. 您需要做的第一件事是在解決方案中建立一個新的「類別庫」（Class Library）專案。將所有外掛放置在解決方案根目錄的 `\Plugins` 目錄中是一個良好的慣例（請勿與位於 `\Nop.Web` 目錄下的 `\Plugins` 子目錄混淆，後者用於已部署的外掛）。將所有外掛放置在「Plugins」解決方案資料夾中也是個好習慣（您可以在[此處](http://msdn.microsoft.com/library/sx2027y2.aspx)找到關於解決方案資料夾的更多資訊）。

    建議的外掛專案命名格式為「Nop.Plugin.{Group}.{Name}」。其中 {Group} 是您的外掛組別（例如 "Payment" 或 "Shipping"），{Name} 是您的外掛名稱（例如 "PayPalStandard"）。例如，PayPal Standard 付款外掛的名稱為：Nop.Plugin.Payments.PayPalStandard。但請注意，這並非強制要求，您可以為外掛選擇任何名稱，例如 "MyGreatPlugin"。

    ![p1](_static/how-to-write-plugin-4.00/write_plugin_4.00_1.jpg)

1. 建立外掛專案後，您必須在任何文字編輯器中開啟其 `.csproj` 檔案，並將其內容替換為以下內容：

    ```csharp
    return $"{_webHelper.GetStoreLocation()}Admin/ControllerName/ActionName";
    ```
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

在這裡，我們將探討如何註冊外掛路由。ASP.NET Core 路由負責將進入的瀏覽器請求映射到特定的 MVC 控制器動作。您可以在[此處](https://docs.microsoft.com/aspnet/core/fundamentals/routing)找到有關路由的更多資訊。請遵循以下步驟：

- 如果您需要加入自訂路由，請建立 `RouteProvider.cs` 檔案。它會將外掛路由資訊通知給 nopCommerce 系統。例如，以下的 RouteProvider 類別新增了一個路由，透過開啟網頁瀏覽器並導向 `http://www.yourStore.com/Plugins/PaymentPayPalStandard/PDTHandler` 網址即可存取（這是 PayPal 外掛所使用的）：

    ```csharp
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
             routeBuilder.MapRoute("Plugin.Payments.PayPalStandard.PDTHandler", "Plugins/PaymentPayPalStandard/PDTHandler",
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

有些外掛可能會過時，無法再與較新版本的 nopCommerce 搭配運作。如果您在升級到較新版本後遇到問題，請刪除該外掛，並前往 nopCommerce 官方網站查看是否有新版本可用。許多外掛作者會更新其外掛以適應新版本，但有些則不會，這些外掛將隨著 nopCommerce 的改進而逐漸淘汰。但在大多數情況下，您只需開啟對應的 `plugin.json` 檔案並更新 **SupportedVersions** 欄位即可。

## 結論

希望這能協助您開始使用 nopCommerce，並為開發更複雜的外掛做好準備。