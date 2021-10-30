---
title: কিভাবে প্লাগইন থেকে প্রশাসন এলাকায় একটি মেনু আইটেম যোগ করা যায়
uid: bn/developer/plugins/menu-item
author: git.AndreiMaz
contributors: git.AfiaKhanom
---

# কিভাবে প্লাগইন থেকে প্রশাসন এলাকায় একটি মেনু আইটেম যোগ করা যায়

নপকমার্স এ, প্রশাসন মেনু *sitemap.config* ফাইল থেকে তৈরি করা হয় যা *~/Areas/Admin folder* এ অবস্থিত।

একই কাজ করার জন্য, আপনি নিম্নলিখিত নমুনা কোড ব্যবহার করতে পারেন যা আপনার প্লাগইন এর সিএস ফাইলে যোগ করতে হবে। প্রথমে, আপনার প্লাগইন প্রধান শ্রেণীতে IAdminMenuPlugin ইন্টারফেস প্রয়োগ করুন।

তারপরে, আপনি এই পদ্ধতিতে কোনও সুরক্ষা (এসিএল) যুক্তি রাখতে পারেন। উদাহরণস্বরূপ, বর্তমান গ্রাহকের "ম্যানেজ প্লাগইন" অনুমতি আছে কিনা তা যাচাই করুন।

```csharp
 public class CustomPlugin : BasePlugin, IAdminMenuPlugin
 {

    public Task ManageSiteMapAsync(SiteMapNode rootNode)
    {
        var menuItem = new SiteMapNode()
        {
            SystemName = "YourCustomSystemName",
            Title = "Plugin Title",
            ControllerName = "ControllerName",
            ActionName = "List",
            Visible = true,
            RouteValues = new RouteValueDictionary() { { "area", null } },
        };
        var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
        if(pluginNode != null)
            pluginNode.ChildNodes.Add(menuItem);
        else
            rootNode.ChildNodes.Add(menuItem);

        return Task.CompletedTask;
    }

}

```

সংস্করণ ২.০০-৩.৫০ এ আপনার নিম্নলিখিত উপায়ে এটি করা উচিত:

```csharp
public bool Authenticate()
{
    return true;
}

public  SiteMapNode BuildMenuItem() // SiteMapNode is Class in Nop.Web.Framework.Menu
{
   var menuItemBuilder = new SiteMapNode()
   {
       Title = "Title For Menu item",   // Title for your Custom Menu Item
       Url = "Path of action link", // Path of the action link
       Visible = true,
       RouteValues = new RouteValueDictionary() { {"Area", "Admin"} }
   };
    var SubMenuItem = new SiteMapNode()   // add child Custom menu
   {
       Title =  "Title For Menu Child menu item", //   Title for your Sub Menu item
       ControllerName = "Your Controller Name", // Your controller Name
       ActionName = "Configure", // Action Name
       Visible = true,
       RouteValues = new RouteValueDictionary() { {"Area", "Admin"} },
   };
   menuItemBuilder.ChildNodes.Add(SubMenuItem);

   return menuItemBuilder;
}
```

উপরের কোডে, আপনি মন্তব্যগুলি খুঁজে পেতে পারেন যেখানে আপনাকে আপনার প্রয়োজনীয়তার উপর নির্ভর করে মানগুলি প্রতিস্থাপন করতে হবে। তাছাড়া, উপরের কোডটিও ব্যাখ্যা করে যে কিভাবে আপনি প্রধান মেনুতে একটি শাখা মেনু আইটেম যোগ করতে পারেন।
