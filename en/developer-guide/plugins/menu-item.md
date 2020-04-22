---
title: How to add a menu item into the administration area from a plugin
uid: en/developer/plugins/menu-item
author: git.AndreiMaz
contributors: git.Sandeep911, git.DmitriyKulagin, git.exileDev
---

# How to add a menu item into the administration area from a plugin

In nopCommerce, administration menu is build from the *sitemap.config* file which is located in *~/Areas/Admin folder*.

To do the same, you can use following sample code which you need to add in your plugins’ cs file. First, implement IAdminMenuPlugin interface in your plugin main class. Then,

You can also put any security (ACL) logic to this method. For example, validate whether current customer has "Manage plugins" permission.

```csharp
public void ManageSiteMap(SiteMapNode rootNode)
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
}
```

In version 2.00-3.50 you should do it the following way:

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

In the above code, you can find comments where you need to replace values depending on your requirements. Moreover, the above code also explains how you can add a child menu items inside main menu.
