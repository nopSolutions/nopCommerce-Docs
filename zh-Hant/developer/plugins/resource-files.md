---
標題: 將 CSS 和 JS 資源檔案新增至 nopCommerce 外掛
uid: zh-Hant/developer/plugins/resource-files
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.exileDev
---

# 將 CSS 和 JS 資源檔案新增至 nopCommerce 外掛

若要正確載入資源檔案，您需要將其參考新增至外掛的檢視（View）檔案中。

您可以透過 `INopHtmlHelper` 使用 `AddScriptParts()` 或 `AddCssFileParts()` 輔助方法。

- `@NopHtml.AddCssFileParts()`
- `@NopHtml.AddScriptParts()`

您可以前往 nopCommerce 專案中的定義，查看關於這些方法的更多詳細資訊。

```js
//Loading CSS file
@NopHtml.AddCssFileParts("~/Plugins/{PluginName}/Content/{CSSFileName.Css}", excludeFromBundle: false);

//Loading js file
//Third parameter value indicating whether to exclude this script from bundling
@NopHtml.AddScriptParts(ResourceLocation.Footer, "~/Plugins/{PluginName}/Scripts/{JSFileName.js}", excludeFromBundle: true);
```

如果您想在頁首（header）新增資源連結，可以使用 **ResourceLocation.Head**；若要放在頁尾（footer），則可以使用 **ResourceLocation.Footer**。

但如果您想將外部指令碼（external scripts）新增至頁面該怎麼辦？在這種情況下，您必須在元素層級使用 Tag Helper 停用字元（"!"）來停用 Tag Helper：

```js
<!script async src="https://www.googletagmanager.com/gtag/js"></!script>
```

> [!NOTE]
>
> 如果出於某種原因，您希望指令碼在新增它的位置直接產生，那麼您也必須在 `script` 屬性中使用 `!`，這樣它才不會被內建的 helper tag 處理。

如果您想將指定的資源放置在頁面的 `head` 標籤中，則需要新增以下程式碼：

```js
@NopHtml.GenerateScripts(ResourceLocation.Head)
```

同樣地，如果您需要將資源移動到 `Footer`：

```js
@NopHtml.GenerateScripts(ResourceLocation.Footer)
```

如果您只需要將 `js` 程式碼新增至頁面，以下是一個範例。請注意，它產生的位置是在 `GenerateScripts` tag helper 方法中指定的。

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