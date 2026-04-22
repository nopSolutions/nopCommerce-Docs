---
標題: 註冊新路由
uid: zh-Hant/developer/tutorials/register-new-routes
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.exileDev
---

# 註冊新路由

[ASP.NET Core 路由](https://docs.microsoft.com/aspnet/core/fundamentals/routing) 負責將傳入的瀏覽器請求映射到特定的 MVC 控制器動作（action）。nopCommerce 提供了一個 `IRouteProvider` 介面，用於在應用程式啟動期間進行路由註冊。所有主要的路由皆註冊於 *`Nop.Web`* 專案中的 `RouteProvider` 與 `GenericUrlRouteProvider` 類別內。

您可以視需求建立任意數量的 `RouteProvider` 類別。例如，如果您的外掛程式包含需要註冊的自訂路由，您可以建立一個實作 `IRouteProvider` 介面的新類別，並針對該外掛程式註冊專屬的路由。

```csharp
/// <summary>
/// Represents plugin route provider
/// </summary>
public class RouteProvider : IRouteProvider
{
    /// <summary>
    /// Register routes
    /// </summary>
    /// <param name="endpointRouteBuilder">Route builder</param>
    public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapControllerRoute(PayPalCommerceDefaults.ConfigurationRouteName,
                "Admin/PayPalCommerce/Configure",
                new { controller = "PayPalCommerce", action = "Configure" });
        
    }
    /// <summary>
    /// Gets a priority of route provider
    /// </summary>
    public int Priority => 0;
}
```