---
title: কিভাবে নতুন রুট নিবন্ধন করব?
uid: bn/developer/tutorials/register-new-routes
author: git.AndreiMaz
contributors: git.AfiaKhanom
---

# নতুন রুট নিবন্ধন

ASP.NET Core রাউটিং নির্দিষ্ট MVC কন্ট্রোলার ক্রিয়ায় ইনকামিং ব্রাউজার অনুরোধ ম্যাপ করার জন্য দায়ী। আপনি রাউটিং সম্পর্কে আরও তথ্য পেতে পারেন [এখানে](https://docs.microsoft.com/aspnet/core/fundamentals/routing)। নপকমার্সের একটি IRouteProvider ইন্টারফেস রয়েছে যা অ্যাপ্লিকেশন শুরুর সময় রুট নিবন্ধনের জন্য ব্যবহৃত হয়। সমস্ত মূল রুট `Nop.Web` প্রকল্পে অবস্থিত RouteProvider ক্লাসে নিবন্ধিত।

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

আপনি আপনার প্রয়োজন হিসাবে অনেক `RouteProvider` ক্লাস তৈরি করতে পারেন। উদাহরণস্বরূপ, যদি আপনার প্লাগিনে কিছু কাস্টম রুট থাকে যা আপনি নিবন্ধন করতে চান, তাহলে `IRouteProvider` ইন্টারফেস বাস্তবায়ন করে একটি নতুন শ্রেণী তৈরি করুন এবং আপনার নতুন প্লাগইনটির জন্য নির্দিষ্ট রুটগুলি নিবন্ধন করুন।
