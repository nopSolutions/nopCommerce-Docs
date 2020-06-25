---
title: How do I register new routes?
uid: en/developer/tutorials/register-new-routes
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Register new routes

ASP.NET Core routing is responsible for mapping incoming browser requests to particular MVC controller actions. You can find more information about routing [here](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-3.1). nopCommerce has an IRouteProvider interface which is used for route registration during application startup. All core routes are registered in the RouteProvider class located in the `Nop.Web` project.

```csharp
 public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
{
    var pattern = string.Empty;
    if (DataSettingsManager.DatabaseIsInstalled)
    {
        var localizationSettings = endpointRouteBuilder.ServiceProvider.GetRequiredService<LocalizationSettings>();
        if (localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
        {
            var langservice = endpointRouteBuilder.ServiceProvider.GetRequiredService<ILanguageService>();
            var languages = langservice.GetAllLanguages().ToList();
            pattern = "{language:lang=" + languages.FirstOrDefault().UniqueSeoCode + "}/";
        }
    }

    //home page
    endpointRouteBuilder.MapControllerRoute("Homepage", pattern, new { controller = "Home", action = "Index" });
}
```

You can create as many RouteProvider classes as you need. For example, if your plugin has some custom routes which you want to register, then create a new class implementing the IRouteProvider interface and register the routes specific to your new plugin.
