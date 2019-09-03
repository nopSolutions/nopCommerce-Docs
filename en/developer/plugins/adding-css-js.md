---
title: Adding CSS and JS resource files into nopCommerce Plugin
author: AndreiMaz
uid: developer/plugins/adding-css-js
---
# Adding CSS and JS resource files into nopCommerce Plugin

To load resource files correctly you need to add its references into your plugin’s view files.

You can use `Html.AddScriptParts()` or `Html.AddCssFileParts()` helper methods.

You can check into more details about this methods by going to its definition in your nopCommerce projects.

```csharp
@{
     //Loading CSS file
     Html.AddCssFileParts(ResourceLocation.Head, "~/Plugins/{PluginName}/Content/{CSSFileName.Css}");
  
     //Loading js file
     //Third parameter value indicating whether to exclude this script from bundling
     Html.AddScriptParts(ResourceLocation.Footer, "~/Plugins/{PluginName}/Scripts/{JSFileName.js}", true);
}
```

If you want to add resource link in the header then you can use `ResourceLocation.Head` and for footer you can use `ResourceLocation.Footer`.
