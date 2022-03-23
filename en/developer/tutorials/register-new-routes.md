---
title: How do I register new routes?
uid: en/developer/tutorials/register-new-routes
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Register new routes

[ASP.NET Core routing](https://docs.microsoft.com/aspnet/core/fundamentals/routing) is responsible for mapping incoming browser requests to particular MVC controller actions. nopCommerce has an `IRouteProvider` interface which is used for route registration during application startup. All main routes are registered in the `RouteProvider` and `GenericUrlRouteProvider` classes located in the *`Nop.Web`* project.

You can create as many `RouteProvider` classes as you need. For example, if your plugin has some custom routes which you want to register, then create a new class implementing the `IRouteProvider` interface and register the routes specific to your new plugin.

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
