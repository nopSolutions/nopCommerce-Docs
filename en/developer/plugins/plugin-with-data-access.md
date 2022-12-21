---
title: Plugin with data access
uid: en/developer/plugins/plugin-with-data-access
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.skoshelev, git.cromatido
---

# Plugin with data access

In this tutorial, I'll be using the nopCommerce plugin architecture to implement a product view tracker. Before we begin with the development you must read, understood, and completed the tutorials listed below. I'll be skipping over some explanations covered in the previous articles, but you can recap using the links provided.

- [Developer tutorials](xref:en/developer/tutorials/index)
- [Updating an existing entity. How to add a new property.](xref:en/developer/tutorials/update-existing-entity)
- [How to write a plugin for nopCommerce 4.60](xref:en/developer/plugins/how-to-write-plugin-4.60)

We will start coding with the data access layer, move on to the service layer, and finally end on dependency injection.

## Getting started

Create a new class library project "Nop.Plugin.Misc.ProductViewTracker".

Add the  `plugin.json` file.

>[!TIP]
>For information about the `plugin.json` file, please see [plugin.json file](xref:en/developer/plugins/plugin_json).

Then add references to the **Nop.Web.Framework** projects. This will be enough for us, as other dependencies, such as **Nop.Core** and **Nop.Data**will be connected automatically

## The Data Access Layer (A.K.A. Creating new entities in nopCommerce)

Inside of the "*domain*" namespace we're going to create a public class named **`ProductViewTrackerRecord`**. This class extends **`BaseEntity`**, but it is otherwise a very simple file. Something to remember is that we do not have navigation properties (relational properties), because the *Linq2DB* framework, which we use to work with databases does not support the navigation properties.

```csharp
namespace Nop.Plugin.Misc.ProductViewTracker.Domain
{
    public class ProductViewTrackerRecord : BaseEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CustomerId { get; set; }
        public string IpAddress { get; set; }
        public bool IsRegistered { get; set; }
    }
}
```

The next class to create is the *FluentMigrator* entity builder class. Inside the mapping class, we map the columns, table relationships, and the database table.

```csharp
using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Other.ProductViewTracker.Domains;
using Nop.Data.Extensions;
using System.Data;

namespace Nop.Plugin.Other.ProductViewTracker.Mapping.Builders
{
    public class ProductViewTrackerRecordBuilder : NopEntityBuilder<ProductViewTrackerRecord>
    {
        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            //map the primary key (not necessary if it is Id field)
            table.WithColumn(nameof(ProductViewTrackerRecord.Id)).AsInt32().PrimaryKey()
            //map the additional properties as foreign keys
            .WithColumn(nameof(ProductViewTrackerRecord.ProductId)).AsInt32().ForeignKey<Product>(onDelete: Rule.Cascade)
            .WithColumn(nameof(ProductViewTrackerRecord.CustomerId)).AsInt32().ForeignKey<Customer>(onDelete: Rule.Cascade)
            //avoiding truncation/failure
            //so we set the same max length used in the product name
            .WithColumn(nameof(ProductViewTrackerRecord.ProductName)).AsString(400)
            //not necessary if we don't specify any rules
            .WithColumn(nameof(ProductViewTrackerRecord.IpAddress)).AsString()
            .WithColumn(nameof(ProductViewTrackerRecord.IsRegistered)).AsInt32();
        }
    }
}
```

The next important class for us will be the migration class, which creates our table directly in the database. You can create as many migrations as you like in your plugin, the only thing you need to keep track of is the version of your migration. We specially created our **NopMigration** attribute to make it easier for you. By indicating here the most complete and accurate file creation date, you practically guarantee the uniqueness of your migration number.

```csharp
using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Other.ProductViewTracker.Domains;

namespace Nop.Plugin.Other.ProductViewTracker.Migrations
{
    [NopMigration("2020/05/27 08:40:55:1687541", "Other.ProductViewTracker base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<ProductViewTrackerRecord>();            
        }
    }
}
```

## Service layer

The service layer connects the data access layer and the presentation layer. Since it is bad form to share any type of responsibility in code each layer needs to be isolated. The service layer wraps the data layer with business logic and the presentation layer depends on the service layer. Because our task is very small our service layer does nothing but communicate with the repository (the repository in nopCommerce acts as a facade to the object context).

```csharp
using Nop.Data;
using Nop.Plugin.Other.ProductViewTracker.Domains;
using System;

namespace Nop.Plugin.Other.ProductViewTracker.Services
{
    public interface IProductViewTrackerService
    {
        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        void Log(ProductViewTrackerRecord record);
    }
}

namespace Nop.Plugin.Misc.ProductViewTracker.Services
{
    public class ProductViewTrackerService : IProductViewTrackerService
    {
        private readonly IRepository<ProductViewTrackerRecord> _productViewTrackerRecordRepository;
        public ProductViewTrackerService(IRepository<ProductViewTrackerRecord> productViewTrackerRecordRepository)
        {
            _productViewTrackerRecordRepository = productViewTrackerRecordRepository;
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        public virtual void Log(ProductViewTrackerRecord record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));
            _productViewTrackerRecordRepository.Insert(record);
        }
    }
}
```

## Dependency Injection

Martin Fowler has written a great description of dependency injection or Inversion of Control. I'm not going to duplicate his work, and you can find his article [here](https://martinfowler.com/articles/injection.html). Dependency injection manages the life cycle of objects and provides instances for dependent objects to use. First, we need to configure the dependency container so it understands which objects it will control and what rules might apply to the creation of those objects.

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Other.ProductViewTracker.Services;

namespace Nop.Plugin.Other.ProductViewTracker.Infrastructure
{
    /// <summary>
    /// Represents object for the configuring services on application startup
    /// </summary>
    public class NopStartup : INopStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductViewTrackerService, ProductViewTrackerService>();
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
}
```

In the code above we register different types of objects so they can later be injected into controllers, services, and repositories. Now that we've covered the new topics I'll bring back some of the older ones so we can finish the plugin.

## The view component

Let's create a view component:

```csharp
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Other.ProductViewTracker.Domains;
using Nop.Plugin.Other.ProductViewTracker.Services;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Other.ProductViewTracker.Components
{
    public class ProductViewTrackerViewComponent : NopViewComponent
    {
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IProductViewTrackerService _productViewTrackerService;
        private readonly IWorkContext _workContext;

        public ProductViewTrackerViewComponent(ICustomerService customerService,
            IProductService productService,
            IProductViewTrackerService productViewTrackerService,
            IWorkContext workContext)
        {
            _customerService = customerService;
            _productService = productService;
            _productViewTrackerService = productViewTrackerService;
            _workContext = workContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            if (!(additionalData is ProductDetailsModel model))
                return Content("");

            //Read from the product service
            var productById = await _productService.GetProductByIdAsync(model.Id);
            //If the product exists we will log it
            if (productById != null)
            {
                var currentCustomer = await _workContext.CurrentCustomerAsync();
                //Setup the product to save
                var record = new ProductViewTrackerRecord
                {
                    ProductId = model.Id,
                    ProductName = productById.Name,
                    CustomerId = currentCustomer.Id,
                    IpAddress = currentCustomer.LastIpAddress,
                    IsRegistered = await _customerService.Async(currentCustomer)
                };
                //Map the values we're interested in to our new entity
                _productViewTrackerService.Log(record);
            }

            return Content("");
        }
    }
}
```

## The main plugin class

> [!IMPORTANT]
>
>We implement our plugin as a widget. In this case, we won't need to edit a `cshtml` file.

```csharp
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;

namespace Nop.Plugin.Other.ProductViewTracker
{
    public class ProductViewTrackerPlugin : BasePlugin, IWidgetPlugin
    {
        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => true;

        /// <summary>
        /// Gets a type of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component type</returns>
        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(ProductViewTrackerViewComponent);
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.ProductDetailsTop });
        }
    }
}
```
