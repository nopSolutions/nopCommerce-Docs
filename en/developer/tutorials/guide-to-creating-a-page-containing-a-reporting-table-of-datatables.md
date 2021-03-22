---
title: Guide to creating a page containing a reporting table of DataTables
uid: en/developer/tutorials/guide-to-creating-a-page-containing-a-reporting-table-of-datatables
author: nop.sea
contributors: git.RomanovM, git.DmitriyKulagin
---

# Guide to creating a page containing a reporting table of DataTables

In this tutorial we will be learning about how to extend the functionality of the nopCommerce with custom functionality for admin panel, and create a page containing a table with some data as a report. So before starting on this tutorial you need to have some prior knowledge and understanding on some of the topics like.

* [nopCommerce architecture](xref:en/developer/tutorials/source-code-organization).
* [nopCommerce Plugin](xref:en/developer/tutorials/guide-to-expanding-the-functionality-of-the-basic-functions-of-nop-commerce-through-a-plugin).
* [nopCommerce routing](xref:en/developer/tutorials/register-new-routes).

If you are not familiar with the above topics, we highly recommend you to learn about those first. However, if you are comfortable or at least have some basic understanding on the above topic then you are good enough to continue on this tutorial.

So in this tutorial we will be creating a plugin with a page containing the table displaying information on the distribution of users by country (based on the billing address). Let's go through the step by step process to create above mentioned functionality.

## Create a nopCommerce plugin project

I am assuming that you already know where and how to create nopCommerce plugin project and configure the project according to nopCommerce standard. If you don't know then you can visit [this page](xref:en/developer/plugins/how-to-write-plugin-4.40) link to know how to create and configure nopCommerce plugin project.

If you have followed the above provided link to create and configure your plugin project then you may end up with the folder structure like this.

![image1](_static/guide-to-creating-a-page-containing-a-reporting-table-of-datatables/image1.png)

And you also know what kinds of files each of these folder/directory holds. Here "DistOfCustByCountryPlugin.cs" file is the one that inherent from BasePlugin class. Here is the basic code we want in this file for the sake of this tutorial.

```cs
public class DistOfCustByCountryPlugin: BasePlugin
    {
        public DistOfCustByCountryPlugin()
        {

        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/DistOfCustBuCountryPlugin/Configure";
        }

        public override async Task InstallAsync()
        {
            //Code you want to run while installing the plugin goes here.
             await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            //Code you want to run while Uninstalling the plugin goes here.
            await base.UninstallAsync();
        }
    }
```

This class has two overridden methods `InstallAsync` and `UninstallAsync` from `BasePlugin` class. If we want to do something before installing and uninstalling our plugin will put that code before calling the install and uninstall method from base class. For example if our plugin may have to create its own table then we will create that table before we call the install method from base class and likewise we may also want to delete our table from database if users want to uninstall our plugin. In this case we may want to run code to delete tables before calling uninstall method from base class.

First let's create a model named **CustomersDistribution** inside **Models** folder.

## #Models/ CustomersDistribution.cs

```cs
public record CustomersDistribution : BaseNopModel
{
    /// <summary>
    /// Country based on the billing address.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Number of customers from specific country.
    /// </summary>
    public int NoOfCustomers { get; set; }
}
```

Also let's add the search model named `CustomersByCountrySearchModel` inside *Models* folder.

```cs
public record CustomersByCountrySearchModel : BaseSearchModel
{
}
```

nopCommerce uses the repository pattern for data access which is ideal for dependency injection mechanism. Now let us create a service that fetch required data from database. For service we will create an Interface and create a service class implementing that interface.

## #Services/ ICustomersByCountry.cs

```cs
public interface ICustomersByCountry
{
    Task<List<CustomersDistribution>> GetCustomersDistributionByCountryAsync()
}
```

Here we have only one method description since for the sake of this plugin we do not need any other methods.

## #Services/ CustomersByCountry.cs

```cs
public class CustomersByCountry : ICustomersByCountry
{
    private readonly IAddressService _addressService;
    private readonly ICountryService _countryService;
    private readonly ICustomerService _customerService;

    public CustomersByCountry(IAddressService addressService, 
        ICountryService countryService,
        ICustomerService customerService)
    {
        _addressService = addressService;
        _countryService = countryService;
        _customerService = customerService;
    }

    public async Task<List<CustomersDistribution>> GetCustomersDistributionByCountryAsync()
    {
        return await _customerService.GetAllCustomersAsync()
            .Where(c => c.ShippingAddressId != null)
            .Select(c => new
            {
                await (_countryService.GetCountryByAddressAsync(_addressService.GetAddressById(c.ShippingAddressId ?? 0))).Name,
                c.Username
            })
            .GroupBy(c => c.Name)
            .Select(cbc => new CustomersDistribution { Country = cbc.Key, NoOfCustomers = cbc.Count() }).ToList();
    }
}
```

Here we are creating a class named **CustomersByCountry** which is inherent from **ICustomersByCountry** interface. Also, we are implementing the method that retrieves data from the database. We used this approach so that we can use dependency injection techniques to inject this service to the controller.

Now let's create a controller class. A good practice to name plugin controllers is like {Group}{Name}Controller.cs. For example, TutorialCustomersByCountryController, here {Tutorial}{CustomersByCountry}Controller. But remember that it is not a requirement to name the controller with {Group}{Name} it is just recommended way by nopCommerce for naming convention but the Controller part in the name is the requirement of .Net MVC.

## #Controllers/CustomersByCountryController.cs

```cs
    [AutoValidateAntiforgeryToken]
    [AuthorizeAdmin] //confirms access to the admin panel
    [Area(AreaNames.Admin)] //specifies the area containing a controller or action
    public class DistOfCustByCountryPluginController : BasePluginController
    {
        private readonly ICustomersByCountry _service;
        public DistOfCustByCountryPluginController(ICustomersByCountry service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Configure()
        {
            CustomersByCountrySearchModel customerSearchModel = new CustomersByCountrySearchModel
            {
                AvailablePageSizes = "10"
            };
            return View("~/Plugins/Tutorial.DistOfCustByCountry/Views/Configure.cshtml", customerSearchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomersCountByCountry()
        {
            try
            {
                return Ok(new DataTablesModel { Data = await _service.GetCustomersDistributionByCountryAsync() });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
```

In the controller we are injecting "ICustomersByCountry" service we created previously to get data from database. Here we have created two Actions one is of type "HttpGet" and another of type "HttpPost". The "Configure" HttpGet action is returning a view named "Configure.cshtml" which we haven't created yet. And GetCustomersCountByCountry HttpPost action which is using an injected service to retrieve data and returning data in the json format. This action is going to be called by the data table which expects a response as DataTablesModel object. However, here we are setting the data property which is actually the data which will be rendered in the table.

Now let's create a view with DataTables where we can display our data which then can be viewed by our users. As well as a _ViewImports.cshtml file which contains code to import all required references for our view files.

## #Views/ Configure.cshtml

```cs
@using Nop.Web.Framework.Models.DataTables
@{
    Layout = "_ConfigurePlugin";
}

@await Html.PartialAsync("Table", new DataTablesModel
{
    Name = "customersDistributionByCountry-grid",
    UrlRead = new DataUrl("GetCustomersCountByCountry", "TutorialCustomersByCountry"),
    Paging = false,
    ColumnCollection = new List<ColumnProperty>
    {
        new ColumnProperty(nameof(CustomersDistribution.Country))
        {
            Title = "Country",
            Width = "300"
        },
        new ColumnProperty(nameof(CustomersDistribution.NoOfCustomers))
        {
            Title = "Number Of Customers",
            Width = "100"
        }
    }
})
```

## #Views/_ViewImports.cshtml

```cs
@inherits Nop.Web.Framework.Mvc.Razor.NopRazorPage<TModel>
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Nop.Web.Framework

@using Microsoft.AspNetCore.Mvc.ViewFeatures
@using Nop.Web.Framework.UI
@using Nop.Web.Framework.Extensions
@using System.Text.Encodings.Web
@using Nop.Plugin.Tutorial.DistOfCustByCountry.Models
@using Nop.Web.Framework.Models.DataTables;
@using Microsoft.AspNetCore.Routing;
```

* In "Configure.cshtml" we are using a partial view named "Table". This is the nopCommerce implementation of JQuery DataTables. We can find this file under `Nop.Web/Areas/Admin/Views/Shared/Table.cshtml`. There you can see the code for implementation of DataTables. This view model takes a DataTablesModel class for configuration of DataTables. Let's explain the property we have set for the DataTablesModel class.
* **Name:** This will be set as an id for DataTables.
* **UrlRead:** this is the URL from where DataTables is going to fetch data to render in table. Here we are setting the URL to "GetCustomersCountByCountry" Action of the "TutorialCustomersByCountry" Controller from where we are getting data for DataTables.
* **Paging:** This property is used to enable or disable pagination for DataTables.
* **ColumnCollection:** This property holds the column configuration property.

There are several other properties which you can play around with to understand what each of the properties are used for.

We are almost done but not complete yet. If you have remembered we previously created a service Interface and a service class inheriting that interface and we have injected that service to our controller. But we haven't yet registered that service to any IOC container. So, lets create a class to register the service for dependency injection.

## #Infrastructure/ DependencyRegistrar.cs

```cs
class DependencyRegistrar : IDependencyRegistrar
{
    public int Order => 1;

    public void Register(IServiceCollection services, ITypeFinder typeFinder, NopConfig config)
    {
        services.AddScoped<ICustomersByCountry, CustomersByCountry>();
    }
}
```

Here we are inheriting from the "IDependencyRegistrar" interface which is provided by nopCommerce. Here we need to implement a "Register" Method and an integer property Order. Inside the Register method we must register all of our services for our plugin as shown in the above code. Under the hood it uses AutoFac to register our services. DependencyRegistrar is just the layer created by nopCommerce which we are using to register our dependencies.

Now the last step is to register our route for the Action "GetCustomersCountByCountry" from Controller "TutorialCustomersByCountry". We do not need to register the route for "Configure" Action because we have already registered that in the `DistOfCustByCountryPlugin` class.

## #Infrastructure/RouteProvider

```cs
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
        //add route for the access token callback
        endpointRouteBuilder.MapControllerRoute("CustomersDistributionByCountry", "Plugins/Tutorial/CustomerDistByCountry/",
            new { controller = "TutorialCustomersByCountry", action = "GetCustomersCountByCountry" });
    }

    /// <summary>
    /// Gets a priority of route provider
    /// </summary>
    public int Priority => 0;
}
```

To learn more about nopCommerce routing please visit [this page](xref:en/developer/tutorials/register-new-routes)

Now just build your project and run. Login as Administrative user and go to the LocalPlugins menu under Configuration, there you will see your newly created plugin. Install that plugin. After installation is complete you will see a configuration button in your plugin. If you have followed correctly through this tutorial then you will see output something like:

![image2](_static/guide-to-creating-a-page-containing-a-reporting-table-of-datatables/image2.png)
