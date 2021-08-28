---
title: ডেটা অ্যাক্সেস সহ প্লাগইন
uid: bn/developer/plugins/plugin-with-data-access
author: git.AndreiMaz
contributors: git.AfiaKhanom
---

# ডেটা অ্যাক্সেস সহ প্লাগইন

এই টিউটোরিয়ালে আমি একটি প্রোডাক্ট ভিউ ট্র্যাকার বাস্তবায়নের জন্য নপকমার্স প্লাগইন আর্কিটেকচার ব্যবহার করব। আমরা ডেভেলপমেন্ট শুরু করার আগে এটি খুবই গুরুত্বপূর্ণ যে আপনি নীচে তালিকাভুক্ত টিউটোরিয়ালগুলি পড়েছেন, বুঝতে পেরেছেন এবং সফলভাবে সম্পন্ন করেছেন। আমি পূর্ববর্তী নিবন্ধগুলিতে আচ্ছাদিত কিছু ব্যাখ্যা এড়িয়ে যাচ্ছি, তবে আপনি প্রদত্ত লিঙ্কগুলি ব্যবহার করে পুনরায় সংক্ষেপ করতে পারেন।

- [ডেভেলপার টিউটোরিয়াল](xref:bn/developer/tutorials/index)
- [একটি বিদ্যমান এন্টিটি আপডেট করা। কীভাবে একটি নতুন প্রপারটি যুক্ত করবেন।](xref:bn/developer/tutorials/update-existing-entity)
- [কিভাবে নপকমার্স এর জন্য প্লাগইন লিখব](xref:bn/developer/plugins/how-to-write-plugin-4.40)

আমরা ডেটা অ্যাক্সেস স্তর দিয়ে কোডিং শুরু করব, পরিষেবা স্তরে এগিয়ে যাব এবং অবশেষে নির্ভরতা ইনজেকশনের উপর শেষ করব।

> [!NOTE]
> এই প্লাগইনটির ব্যবহারিক প্রয়োগ প্রশ্নবিদ্ধ, কিন্তু আমি এমন একটি বৈশিষ্ট্য সম্পর্কে ভাবতে পারিনি যা নপকমার্স এর সাথে আসে নি এবং যুক্তিসঙ্গত আকারের পোস্টে ফিট হবে। আপনি যদি এই প্লাগইনটি উত্পাদন পরিবেশে ব্যবহার করেন তবে আমি কোনও ওয়ারেন্টি অফার করি না। আমি সবসময় সাফল্যের গল্পে আগ্রহী এবং আমি এটা শুনে খুশি হব যে পোস্টটি কেবল একটি শিক্ষাগত মূল্যের চেয়ে বেশি প্রদান করেছে।

## শুরু করা

একটি নতুন ক্লাস লাইব্রেরি প্রকল্প তৈরি করুন "Nop.Plugin.Misc.ProductViewTracker"।

`Plugin.json` ফাইল যোগ করুন।

>[!TIP]
>`Plugin.json` ফাইল সম্পর্কে তথ্যের জন্য, দয়া করে দেখুন [plugin.json file](xref:bn/developer/plugins/plugin_json).

তারপর **Nop.Web.Framework** প্রকল্পের রেফারেন্স যোগ করুন। এটি আমাদের জন্য যথেষ্ট হবে, যেমন অন্যান্য নির্ভরতা, যেমন **Nop.Core** এবং **Nop.Data**, স্বয়ংক্রিয়ভাবে সংযুক্ত হবে

## ডেটা অ্যাক্সেস লেয়ার (A.K.A. নপকমার্স এ নতুন এন্টিটি তৈরি করা)

"ডোমেন" নেমস্পেসের ভিতরে আমরা ProductViewTrackerRecord নামে একটি পাবলিক ক্লাস তৈরি করতে যাচ্ছি। এই ক্লাস BaseEntity প্রসারিত করে, কিন্তু এটি অন্যথায় একটি খুব বিরক্তিকর ফাইল। মনে রাখার মতো কিছু আমাদের ন্যাভিগেশন প্রপার্টি (রিলেশনাল প্রপার্টি) নেই, কারণ Linq2DB ফ্রেমওয়ার্ক, যা আমরা ডাটাবেসের সাথে কাজ করার জন্য ব্যবহার করি তা ন্যাভিগেশন প্রপার্টি সমর্থন করে না।

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

**ফাইলের অবস্থান**: নির্দিষ্ট কিছু ফাইল কোথায় থাকা উচিত তা বের করতে নামস্থান বিশ্লেষণ করুন এবং সেই অনুযায়ী ফাইল তৈরি করুন।

পরবর্তী শ্রেণী তৈরি করতে হবে *FluentMigrator* এন্টিটি নির্মাতা শ্রেণী। ম্যাপিং ক্লাসের ভিতরে আমরা কলাম, টেবিল সম্পর্ক এবং ডাটাবেস টেবিল ম্যাপ করি।

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

আমাদের জন্য পরবর্তী গুরুত্বপূর্ণ শ্রেণী হবে মাইগ্রেশন ক্লাস, যা সরাসরি আমাদের ডাটাবেসে টেবিল তৈরি করে। আপনি আপনার প্লাগিনে যত খুশি মাইগ্রেশন তৈরি করতে পারেন, আপনার মাইগ্রেশনের সংস্করণটিই আপনার নজর রাখতে হবে। আপনার জন্য এটি সহজ করার জন্য আমরা বিশেষভাবে আমাদের **NopMigration** বৈশিষ্ট্য তৈরি করেছি। এখানে সবচেয়ে সম্পূর্ণ এবং সঠিক ফাইল তৈরির তারিখ নির্দেশ করে, আপনি কার্যত আপনার মাইগ্রেশন নম্বরের স্বতন্ত্রতার নিশ্চয়তা দেন।

```csharp
using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Other.ProductViewTracker.Domains;

namespace Nop.Plugin.Other.ProductViewTracker.Migrations
{
    [SkipMigrationOnUpdate]
    [NopMigration("2020/05/27 08:40:55:1687541", "Other.ProductViewTracker base schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        protected IMigrationManager _migrationManager;

        public SchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        public override void Up()
        {
            _migrationManager.BuildTable<ProductViewTrackerRecord>(Create);
        }
    }
}
```

>[!NOTE]
>**SkipMigrationOnUpdate** বৈশিষ্ট্যের দিকে মনোযোগ দিন, এর উদ্দেশ্য নাম দ্বারা বর্ণিত হয়েছে। এই বৈশিষ্ট্যটি আপনাকে প্লাগইন আপডেট পদ্ধতি সম্পাদন করার সময় মাইগ্রেশন এড়িয়ে যেতে দেয়।

## সার্ভিস স্তর

সার্ভিস স্তরটি ডেটা অ্যাক্সেস স্তর এবং উপস্থাপনা স্তরকে সংযুক্ত করে। যেহেতু কোডে যেকোনো ধরনের দায়িত্ব ভাগ করা খারাপ ফর্ম তাই প্রতিটি স্তরকে বিচ্ছিন্ন করতে হবে। সার্ভিস স্তরটি ব্যবসায়িক যুক্তি দিয়ে ডেটা স্তরকে আবৃত করে এবং উপস্থাপনা স্তরটি সার্ভিস স্তরের উপর নির্ভর করে। কারণ আমাদের কাজটি খুবই ছোট আমাদের সার্ভিস লেয়ারটি রিপোজিটরির সাথে যোগাযোগ করা ছাড়া আর কিছুই করে না (নপকমার্স- এর রিপোজিটরি বস্তুর প্রসঙ্গের সম্মুখভাগ হিসেবে কাজ করে)।

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

## ডেপেনডেনসি ইনজেকশন

মার্টিন ফাউলার ডেপেনডেনসি ইনজেকশন বা ইনভার্সন অব কন্ট্রোল এর একটি দুর্দান্ত বর্ণনা লিখেছেন। আমি তার কাজের নকল করতে যাচ্ছি না, এবং আপনি তার নিবন্ধটি [এখানে](https://martinfowler.com/articles/injection.html) খুঁজে পেতে পারেন । ডেপেনডেনসি ইনজেকশন বস্তুর জীবনচক্র পরিচালনা করে এবং নির্ভরশীল বস্তুর ব্যবহারের দৃষ্টান্ত প্রদান করে। প্রথমে আমাদের নির্ভরতা ধারকটি কনফিগার করতে হবে যাতে এটি বুঝতে পারে যে কোন বস্তুগুলি এটি নিয়ন্ত্রণ করবে এবং সেই বস্তুগুলি তৈরির ক্ষেত্রে কোন নিয়মগুলি প্রযোজ্য হতে পারে।

```csharp
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Other.ProductViewTracker.Services;

namespace Nop.Plugin.Other.ProductViewTracker.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="appSettings">App settings</param>
        public virtual void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings)
        {
            services.AddScoped<IProductViewTrackerService, ProductViewTrackerService>();
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order => 1;
    }
}
```

উপরের কোডটিতে আমরা বিভিন্ন ধরণের অবজেক্ট নিবন্ধন করি যাতে সেগুলি পরে কন্টল্লার, সার্ভিস এবং সংগ্রহস্থলে প্রবেশ করা যায়। এখন যেহেতু আমরা নতুন বিষয়গুলি কভার করেছি আমি পুরোনো কিছু ফিরিয়ে আনব যাতে আমরা প্লাগইনটি শেষ করতে পারি।

## ভিউ উপাদান

আসুন একটি ভিউ কম্পোনেন্ট তৈরি করি:

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
    [ViewComponent(Name = "ProductViewTracker")]
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

## প্রধান প্লাগইন ক্লাস

> [!IMPORTANT]
>
> আমরা একটি উইজেট হিসাবে আমাদের প্লাগইন বাস্তবায়ন করি। এই ক্ষেত্রে আমাদের একটি cshtml ফাইল সম্পাদনা করতে হবে না।

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
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "ProductViewTracker";
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
