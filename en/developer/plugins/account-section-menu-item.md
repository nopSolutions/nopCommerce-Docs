---
title: How to add a menu item into the my account area from a plugin
uid: en/developer/plugins/account-section-menu-item
author: git.khairuzzaman
contributors: 
---

# How to add a menu item into the my account area from a plugin

In nopCommerce, menu of my account page is prepared by the method *PrepareCustomerNavigationModelAsync* which is in the class `CustomerModelFactory.cs` which is located in `Presentation\Nop.Web\Factories` folder.

To add a new menu to the customer navigation we need to override the *PrepareCustomerNavigationModelAsync* method of `CustomerModelFactory.cs`. First, add a new class `ExtendedCustomerModelFactory.cs` to your plugin's `Factories` folder, however you can name your class as you want. You can also use the following sample code as a reference.

```csharp
 public partial class ExtendedCustomerModelFactory : CustomerModelFactory
    {
        private readonly ILocalizationService _localizationService;

        #region Ctor
        // constructor code omitted for brevity
        #endregion

        #region Methods
        public override async Task<CustomerNavigationModel> PrepareCustomerNavigationModelAsync(int selectedTabId = 0)
        {
            var model = await base.PrepareCustomerNavigationModelAsync(selectedTabId);

            var navigationItems = model.CustomerNavigationItems;
            // Find after which menu you want to place the new menu. In this example it is after order menu.
            var newItemIndex = navigationItems.IndexOf(navigationItems.SingleOrDefault(x => x.Tab == (int)CustomerNavigationEnum.Orders));

            navigationItems.Insert(newItemIndex + 1,
                new CustomerNavigationItemModel
                {
                    RouteName = "Plugin.Widgets.CustomMenu.MyCustomMenu",
                    Title = _localizationService
                        .GetLocaleStringResourceByName("Nop.Plugin.Widgets.CustomMenu.Title")
                    Tab = (int)ExtendedCustomerNavigationEnum.ReceiveGifts,
                    ItemClass = "custom-menu"
                });

            return model;
        }

        public enum ExtendedCustomerNavigationEnum
        {
            // must not conflict with possible values of CustomerNavigationEnum
            CustomMenu = 200
        }
        #endregion
    }
```

Now we need to configure the dependency for `ExtendedCustomerModelFactory.cs` class. To do so, add a new class `NopStartup.cs` to your plugin's `Infrastructure` folder if it is not already added. Below sample code is for a reference.

```csharp
public class NopStartup : INopStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICustomerModelFactory, ExtendedCustomerModelFactory>();
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 3000;
    }
```

Finally we need to register the route that we have used in the override method *PrepareCustomerNavigationModelAsync* of `ExtendedCustomerModelFactory.cs`. Add a new class `RouteProvider.cs` to your plugin's `Infrastructure` folder if it is not already added. Below sample code is for a reference.

```csharp
public partial class RouteProvider : IRouteProvider
    {
        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="endpointRouteBuilder">Route builder</param>
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute("Plugin.Widgets.CustomMenu.MyCustomMenu", "/my-custom-menu/details",
                 new { controller = "MyCustomMenu", action = "RenderCustomMenu" });
        }

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority => -1;
    }
```

Add required method in the controller and add the view that you want to render for the menu. Now you can see the menu in my account page.