---
title: Adding CSS and JS resource files into nopCommerce Plugin
uid: en/developer/plugins/resource-files
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Adding CSS and JS resource files into nopCommerce Plugin

To load resource files correctly you need to add its references into your plugin's view files.

You can use `_nopHtmlHelper.AddScriptParts()` or `_nopHtmlHelper.AddCssFileParts()` helper methods by `INopHtmlHelper`.

- `_nopHtmlHelper.AddCssFileParts()`
- `_nopHtmlHelper.AddScriptParts()`

You can check into more details about this methods by going to its definition in your nopCommerce projects.

```csharp
@{
     //Loading CSS file
     _nopHtmlHelper.AddCssFileParts(ResourceLocation.Head, "~/Plugins/{PluginName}/Content/{CSSFileName.Css}", excludeFromBundle = false);

     //Loading js file
     //Third parameter value indicating whether to exclude this script from bundling
     _nopHtmlHelper.AddScriptParts(ResourceLocation.Footer, "~/Plugins/{PluginName}/Scripts/{JSFileName.js}", excludeFromBundle: true);
}
```

If you want to add a resource link in the header then you can use **ResourceLocation.Head** and for footer you can use **ResourceLocation.Footer**.
