---
title: Adding CSS and JS resource files into nopCommerce Plugin
uid: en/developer/plugins/resource-files
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Adding CSS and JS resource files into nopCommerce Plugin

To load resource files correctly you need to add its references into your plugin's view files.

You can use `AddScriptParts()` or `AddCssFileParts()` helper methods by `INopHtmlHelper`.

- `@NopHtml.AddCssFileParts()`
- `@NopHtml.AddScriptParts()`

You can check into more details about this methods by going to its definition in your nopCommerce projects.

```js
//Loading CSS file
@NopHtml.AddCssFileParts("~/Plugins/{PluginName}/Content/{CSSFileName.Css}", excludeFromBundle: false);

//Loading js file
//Third parameter value indicating whether to exclude this script from bundling
@NopHtml.AddScriptParts(ResourceLocation.Footer, "~/Plugins/{PluginName}/Scripts/{JSFileName.js}", excludeFromBundle: true);
```

If you want to add a resource link in the header then you can use **ResourceLocation.Head** and for footer you can use **ResourceLocation.Footer**.

But what if you want to add external scripts to your page? In this case, you must disable the Tag Helper at the element level with the Tag Helper opt-out character ("!"):

```js
<!script async src="https://www.googletagmanager.com/gtag/js"></!script>
```

> [!NOTE]
>
> If for some reason you want your script to be generated at the place where you added it, then in this case you must also use the `!` in the `script` attribute so that it is not processed by the built-in helper tag.

If you want to place the specified resources in the `head` tag of the page, then you need to add the following code:

```js
@NopHtml.GenerateScripts(ResourceLocation.Head)
```

Similarly, if you need to move resources to `Footer`:

```js
@NopHtml.GenerateScripts(ResourceLocation.Footer)
```

If you just need to add your `js` code to the page, then here is an example of how to do it. Note that the location where it will be generated is specified in the `GenerateScripts` tag helper method.

```js
<script>
     $(document).ready(function () {
          //enable "back top" arrow
          $('#backTop').backTop();

          //enable tooltips
          $('[data-toggle="tooltip"]').tooltip({ placement: 'bottom' });
     });
</script>

@NopHtml.GenerateScripts(ResourceLocation.Footer)
```
