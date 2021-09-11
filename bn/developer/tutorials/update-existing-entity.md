---
title: একটি বিদ্যমান এন্টিটি আপডেট করা হয়। কিভাবে একটি নতুন সম্পত্তি যুক্ত করবেন।
uid: bn/developer/tutorials/update-existing-entity
author: git.AndreiMaz
contributors: git.AfiaKhanom
---

# একটি বিদ্যমান এন্টিটি আপডেট করা হয়। কিভাবে একটি নতুন সম্পত্তি যুক্ত করবেন

এই টিউটোরিয়ালে নপকমার্স সোর্স কোড দিয়ে পাঠানো "Category" এন্টিটিতে কিভাবে একটি প্রপার্টি যোগ করতে হয় তা বর্ণনা করা হয়েছে।

## ডেটা মডেল

এন্টিটির দুটি ক্লাস থাকবে যা একটি টেবিলে রেকর্ড ম্যাপ করার জন্য ব্যবহৃত হয়। প্রথম ক্লাস ওয়েব অ্যাপ্লিকেশন দ্বারা ব্যবহৃত প্রপার্টিস, ফিল্ডস এবং মেথদ গুলি সংজ্ঞায়িত করে।

```sh
File System Location: [Project Root]\Libraries\Nop.Core\Domain\Catalog\Category.cs
Assembly: Nop.Core
Solution Location: Nop.Core.Domain.Catalog.Category.cs
```

দ্বিতীয় ক্লাসটি তাদের নিজ নিজ এসকিউএল কলামগুলিতে উপরের ক্লাসতে সংজ্ঞায়িত প্রপার্টিগুলি ম্যাপ করতে ব্যবহৃত হয়। বিভিন্ন এসকিউএল টেবিলের মধ্যে সম্পর্ক ম্যাপ করার জন্য ম্যাপিং ক্লাসই দায়ী।

```sh
File System Location: [Project Root]\Libraries\Nop.Data\Mapping\Builders\Catalog\CategoryBuilder.cs
Assembly: Nop.Data
Solution Location: Nop.Data.Mapping.Builders.Catalog.CategoryBuilder.cs
```

কিন্তু আমি আপনাকে শুধুমাত্র আপনার নিজের সত্তা ক্লাসের জন্য এটি ব্যবহার করার পরামর্শ দিচ্ছি। আমাদের ফিল্ডে, আমরা ম্যাপিং ক্লাসের পরিবর্তে মাইগ্রেশন মেকানিজম ব্যবহার করব।

ক্যাটাগরি ক্লাসে নিম্নলিখিত সম্পত্তি যুক্ত করুন।

```csharp
public string SomeNewProperty { get; set; }
```

নিম্নলিখিত কোড সহ নতুন ক্লাস (Nop.Data.Migrations.AddSomeNewProperty) যোগ করুন:

```csharp
using FluentMigrator;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Migrations
{
    [NopMigration("2020/05/25 11:24:16:2551770", "Category. Add some new property")]
    public class AddSomeNewProperty: AutoReversingMigration
    {
        /// <summary>Collect the UP migration expressions</summary>
        public override void Up()
        {
            Create.Column(nameof(Category.SomeNewProperty))
            .OnTable(nameof(Category))
            .AsString(255)
            .Nullable();
        }
    }
}
```

> [!NOTE]
> মাইগ্রেশন আপডেট করার পদ্ধতি ডাটাবেসের প্রারম্ভিকতার সময় সঞ্চালিত হয়।

```csharp
var migrationManager = EngineContext.Current.Resolve<IMigrationManager>();
migrationManager.ApplyUpMigrations(typeof(NopDbStartup).Assembly);
```

## প্রেসেন্টেশন মডেল

প্রেসেন্টেশন মডেলটি কন্ট্রোলার থেকে ভিউতে তথ্য পরিবহনের জন্য ব্যবহৃত হয় (asp.net/mvc এ আরও পড়ুন)। মডেলের আরেকটি উদ্দেশ্য আছে; প্রয়োজনীয়তা নির্ধারণ।

 আমরা SomeNewProperty- এর জন্য ২৫৫ টি অক্ষর সংরক্ষণ করতে আমাদের ডাটাবেস কনফিগার করেছি। যদি আমরা ৩০০ অক্ষর দিয়ে SomeNewProperty- কে সংরক্ষণ করার চেষ্টা করি তাহলে অ্যাপ্লিকেশনটি ভেঙে যাবে (অথবা পাঠ্যটি ছাঁটাই করবে)। আমরা ব্যবহারকারীদের যতটা সম্ভব ব্যর্থতা থেকে রক্ষা করতে চাই এবং আমাদের ভিউ মডেলগুলি স্ট্রিং দৈর্ঘ্যের মতো প্রয়োজনীয়তা প্রয়োগ করতে সহায়তা করে।

```sh
File System Location: [Project Root]\Presentation\Nop.Web\Areas\Admin\Models\Catalog\CategoryModel.cs
Assembly: Nop.Admin
Solution Location: Nop.Web.Areas.Admin.Models.Catalog.CategoryModel.cs
```

ভ্যালিডেটর ক্লাস মডেল ক্লাসের ভিতরে সংরক্ষিত ডেটা যাচাই করতে ব্যবহৃত হয় (উদা required প্রয়োজনীয় ফিল্ড, সর্বোচ্চ দৈর্ঘ্য এবং প্রয়োজনীয় রেঞ্জ)।

```sh
File System Location: [Project Root]\Presentation\Nop.Web\Areas\Admin\Validators\Catalog\CategoryValidator.cs
Assembly: Nop.Web
Solution Location: Nop.Web.Areas.Admin.Validators.Catalog.CategoryValidator.cs
```

আমাদের ভিউ মডেলে প্রপার্টি যুক্ত করুন।

```csharp
// The NopResourceDisplayName provides the "key" used during localization
// Keep an eye out for more about localization in future blogs
[NopResourceDisplayName("Admin.Catalog.Categories.Fields.SomeNewProperty")]
public string SomeNewProperty { get; set; }
```

যাচাইকারীর কনস্ট্রাক্টারে প্রয়োজনীয় কোড যুক্ত করা হবে।

```csharp
//I think this code can speak for itself
RuleFor(m => m.SomeNewProperty).Length(0, 255);
```

## ভিউ

```sh
File System Location: [Project Root]\Presentation\Nop.Web\Areas\Admin\Views\Category\ _CreateOrUpdate.Info.cshtml
Assembly: Nop.Web
```

ভিউতে মডেল ডেটা প্রদর্শনের জন্য এইচটিএমএলটি থাকে। এই এইচটিএমএলটিকে "PictureId" বিভাগের অধীনে রাখুন।

```csharp
<div class="form-group">
     <div class="col-md-3">
        <nop-label asp-for="SomeNewProperty" />
     </div>
     <div class="col-md-9">
        <nop-editor asp-for="SomeNewProperty" />
        <span asp-validation-for="SomeNewProperty"></span>
     </div>
 </div>
```

## কন্ট্রোলার

এই ক্ষেত্রে নিয়ামক আমাদের ভিউ মডেলে ডোমেন ডেটা মডেল এবং উলটাভাবে ম্যাপ করার জন্য দায়ী। আপডেট করার জন্য আমি ক্যাটাগরি মডেলটি বেছে নেওয়ার কারণ হল সরলতা। আমি এটি নপকমার্স প্ল্যাটফর্মের একটি সূচনা হতে চাই এবং আমি এটি যতটা সম্ভব সহজ রাখতে চাই।

```sh
File System Location: [Project Root]\Presentation\Nop.Web\Areas\Admin\Controllers\CategoryController.cs
Assembly: Nop.Admin
Solution Location:
Nop.Web.Areas.Admin.Controllers.CategoryController.cs
```

আমরা ক্যাটাগরি কন্ট্রোলার ক্লাসে তিনটি আপডেট করতে যাচ্ছি।

* Data Model → View Model
* Create View Model → Data Model
* Edit View Model → Data Model

সাধারণত আমি নিম্নলিখিত কোডের জন্য পরীক্ষা লিখব এবং যাচাই করব যে মডেল ম্যাপিং সঠিকভাবে কাজ করছে, কিন্তু আমি এটি সহজ রাখার জন্য ইউনিট টেস্ট এড়িয়ে যাব।

যথাযথ মেথড দিয়ে ("Create", "Edit", বা "PrepareSomeModel") এই প্রপার্টি সেট করার জন্য কোড যোগ করুন। বেশিরভাগ ক্ষেত্রে এটি প্রয়োজন হয় না কারণ এটি স্বয়ংক্রিয়ভাবে .ToModel () মেথডে অটোমেপার দ্বারা পরিচালিত হয়।

এন্টিটি সংরক্ষণের পাবলিক মেথড (সাধারণত: [HttpPost] অ্যাট্রিবিউট সহ "Create" বা "Edit" মেথড)

```csharp
// Edit View Model → Data Model
category.SomeNewProperty = model.SomeNewProperty;
```

## সমস্যা সমাধান

* ডাটাবেস পুনরায় তৈরি করুন। হয় আপনার নিজস্ব কাস্টম এসকিউএল স্ক্রিপ্ট অথবা নপকমার্স ইনস্টলার ব্যবহার করুন।
* স্কিমা পরিবর্তনের মধ্যে ডেভেলপমেন্ট ওয়েব সার্ভার বন্ধ করুন।
* একটি বিস্তারিত মন্তব্য পোস্ট করুন [আমাদের ফোরামে](http://www.nopcommerce.com/boards/).
