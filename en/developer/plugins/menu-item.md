---
title: How to add a menu item into the administration area from a plugin
uid: en/developer/plugins/menu-item
author: git.AndreiMaz
contributors: git.Sandeep911, git.DmitriyKulagin, git.exileDev
---

# How to add a menu item into the administration area from a plugin

In nopCommerce, the administration menu is built from the *sitemap.config* file which is located in *~/Areas/Admin folder*.

To do the same, you can use the following sample code which you need to add in your plugins' `*.cs` file. First, implement the *IAdminMenuPlugin* interface in your plugin main class.

Then, You can also put any security (ACL) logic to this method. For example, validate whether the current customer has "Manage plugins" permission.

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
            IconClass = "far fa-dot-circle",
            Visible = true,
            RouteValues = new RouteValueDictionary() { { "area", AreaNames.Admin } },
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
