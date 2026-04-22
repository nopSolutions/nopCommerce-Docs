---
標題: 在 nopCommerce 外掛中加入 CSS 與 JS 資源檔案
uid: zh-Hant/developer/plugins/resource-files
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.exileDev
---

# 在 nopCommerce 外掛中加入 CSS 與 JS 資源檔案

若要正確載入資源檔案，您需要在外掛的檢視檔案（view files）中加入其參考。

您可以使用 `INopHtmlHelper` 提供的 `AddScriptParts()` 或 `AddCssFileParts()` 輔助方法。

- `@NopHtml.AddCssFileParts()`
- `@NopHtml.AddScriptParts()`

您可以查看 nopCommerce 專案中這些方法的定義，以了解更多詳細資訊。

```js
//Loading CSS file
@NopHtml.AddCssFileParts("~/Plugins/{PluginName}/Content/{CSSFileName.Css}", excludeFromBundle: false);

//Loading js file
//Third parameter value indicating whether to exclude this script from bundling
@NopHtml.AddScriptParts(ResourceLocation.Footer, "~/Plugins/{PluginName}/Scripts/{JSFileName.js}", excludeFromBundle: true);
```

如果您想在頁首（header）加入資源連結，可以使用 **ResourceLocation.Head**；若要加入至頁尾（footer），則可以使用 **ResourceLocation.Footer**。

但如果您想將外部指令碼（external scripts）加入頁面該怎麼辦？在這種情況下，您必須在元素層級使用標籤輔助工具（Tag Helper）的停用字元（"!"）來停用它：

```js
<!script async src="https://www.googletagmanager.com/gtag/js"></!script>
```

> [!NOTE]
>
> 如果因某些原因您希望指令碼在您加入的位置產生，那麼此時您也必須在 `script` 屬性中使用 `!`，這樣它就不會被內建的輔助標籤所處理。

如果您想將指定的資源放置在頁面的 `head` 標籤中，則需要加入以下程式碼：

```js
@NopHtml.GenerateScripts(ResourceLocation.Head)
```

同樣地，如果您需要將資源移至 `Footer`：

```js
@NopHtml.GenerateScripts(ResourceLocation.Footer)
```

如果您只需要將 `js` 程式碼加入頁面，以下是一個範例。請注意，產生程式碼的位置是在 `GenerateScripts` 標籤輔助方法中指定的。

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