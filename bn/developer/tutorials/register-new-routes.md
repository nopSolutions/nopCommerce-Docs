---
title: How do I register new routes?
uid: en/developer/tutorials/register-new-routes
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Register new routes

ASP.NET Core routing is responsible for mapping incoming browser requests to particular MVC controller actions. You can find more information about routing [here](https://docs.microsoft.com/aspnet/core/fundamentals/routing). nopCommerce has an IRouteProvider interface which is used for route registration during application startup. All core routes are registered in the RouteProvider class located in the `Nop.Web` project.

```csharp
 public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
{
    var lang = GetLanguageRoutePattern();

    //home page
    endpointRouteBuilder.MapControllerRoute(name: "Homepage", 
                pattern: $"{lang}",
                defaults: new { controller = "Home", action = "Index" });
}
```

You can create as many `RouteProvider` classes as you need. For example, if your plugin has some custom routes which you want to register, then create a new class implementing the `IRouteProvider` interface and register the routes specific to your new plugin.
