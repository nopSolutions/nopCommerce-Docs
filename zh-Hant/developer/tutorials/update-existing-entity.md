---
標題: 更新現有實體（如何新增屬性）
uid: zh-Hant/developer/tutorials/update-existing-entity
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.exileDev
---

# 更新現有實體（如何新增屬性）

本教學課程介紹如何將屬性新增至 nopCommerce 原始碼所內建的「分類」實體。

## 資料模型

實體（Entities）將會有兩個類別，用於將記錄映射至資料表。第一個類別定義了網頁應用程式所消耗的屬性、欄位和方法。

```sh
File System Location: [Project Root]\Libraries\Nop.Core\Domain\Catalog\Category.cs
Assembly: Nop.Core
Solution Location: Nop.Core.Domain.Catalog.Category.cs
```

第二個類別用於將上述類別中定義的屬性映射到各自的 SQL 欄位。映射類別也負責映射不同 SQL 資料表之間的關聯。

```sh
File System Location: [Project Root]\Libraries\Nop.Data\Mapping\Builders\Catalog\CategoryBuilder.cs
Assembly: Nop.Data
Solution Location: Nop.Data.Mapping.Builders.Catalog.CategoryBuilder.cs
```

但我們建議您僅將其用於您的實體類別。在我們的案例中，我們將使用遷移（Migration）機制來取代映射類別。

將以下屬性加入至 `Category` 類別。

```csharp
public string SomeNewProperty { get; set; }
```

加入新的類別 `Nop.Data.Migrations.AddSomeNewProperty` 並包含以下程式碼：

```csharp
using FluentMigrator;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Migrations

[NopSchemaMigration("2024-04-20 00:00:00", "Category. Add some new property")]
public class AddSomeNewProperty: ForwardOnlyMigration
{
    /// <summary>
    /// Collect the UP migration expressions
    /// </summary>
    public override void Up()
    {
        var categoryTableName = nameof(Category);
        if (!Schema.Table(categoryTableName).Column(nameof(Category.SomeNewProperty)).Exists())
            Alter.Table(categoryTableName)
                .AddColumn(nameof(Category.SomeNewProperty)).AsInt32().AsString(255).Nullable();
    }
}

```

> [!NOTE]
> 更新遷移的程序會在資料庫初始化期間執行。
>
>```csharp
>public virtual void InitializeDatabase()
>{
>    var migrationManager = EngineContext.CurrenResolve<IMigrationManager>();
>    migrationManager.ApplyUpMigrations(typeof(NopDbStartup).Assembly);
>}
>```

## 展示模型 (Presentation model)

展示模型用於將資訊從控制器傳輸至檢視（更多資訊請參閱 asp.net/mvc）。模型還有另一個目的：定義需求。

我們將資料庫設定為僅儲存 `SomeNewProperty` 的 255 個字元。如果我們嘗試儲存 300 個字元的 `SomeNewProperty`，應用程式將會損毀（或截斷文字）。我們希望應用程式能盡可能保護使用者免於失敗，而我們的檢視模型有助於強制執行諸如字串長度等需求。

```sh
File System Location: [Project Root]\Presentation\Nop.Web\Areas\Admin\Models\Catalog\CategoryModel.cs
Assembly: Nop.Admin
Solution Location: Nop.Web.Areas.Admin.Models.Catalog.CategoryModel.cs
```

驗證器類別用於驗證儲存在模型類別內部的資料（例如必填欄位、最大長度以及必填範圍）。

```sh
File System Location: [Project Root]\Presentation\Nop.Web\Areas\Admin\Validators\Catalog\CategoryValidator.cs
Assembly: Nop.Web
Solution Location: Nop.Web.Areas.Admin.Validators.Catalog.CategoryValidator.cs
```

將該屬性新增至我們的檢視模型。

```csharp
// The NopResourceDisplayName provides the "key" used during localization
[NopResourceDisplayName("Admin.Catalog.Categories.Fields.SomeNewProperty")]
public string SomeNewProperty { get; set; }
```

需求程式碼將會新增至驗證器的建構函式中。

```csharp
//I think this code can speak for itself
RuleFor(m => m.SomeNewProperty).Length(0, 255);
```

## 檢視 (View)

```sh
File System Location: [Project Root]\Presentation\Nop.Web\Areas\Admin\Views\Category\_CreateOrUpdate.Info.cshtml
Assembly: Nop.Web
```

檢視 (View) 包含了用於顯示模型資料的 HTML。請將此 HTML 放置在 "PictureId" 區段之下。

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

## 控制器

在此情況下，控制器負責將領域資料模型映射至檢視模型（View model），反之亦然。我們選擇更新分類模型的原因是因為它相對簡單。

```sh
File System Location: [Project Root]\Presentation\Nop.Web\Areas\Admin\Controllers\CategoryController.cs
Assembly: Nop.Admin
Solution Location: Nop.Web.Areas.Admin.Controllers.CategoryController.cs
```

我們將對 `CategoryController` 類別進行三項更新。

* 資料模型 → 檢視模型
* 建立檢視模型 → 資料模型
* 編輯檢視模型 → 資料模型

通常我們會針對下列程式碼撰寫測試，以驗證模型映射是否正常運作，但為了保持簡單，我們暫時跳過單元測試。

在適當的方法（"Create"、"Edit" 或 "PrepareSomeModel"）中加入程式碼以設定此屬性。在大多數情況下，這並非必要，因為它會由 `.ToModel()` 方法中的 *AutoMapper* 自動處理。

在儲存實體的公開方法（通常是帶有 `[HttpPost]` 屬性的 "Create" 或 "Edit" 方法）中：

```csharp
// Edit View Model → Data Model
category.SomeNewProperty = model.SomeNewProperty;
```

## 疑難排解

* 重新建立資料庫。您可以執行自訂的 SQL 指令碼，或是使用 nopCommerce 安裝程式。
* 在進行資料庫結構變更時，請先停止開發用的網頁伺服器。
* 在[我們的論壇](http://www.nopcommerce.com/boards/)上發表詳細的評論。