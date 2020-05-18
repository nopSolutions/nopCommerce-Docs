---
title: Updating an existing entity. How to add a new property.
uid: en/developer/tutorials/update-existing-entity
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Updating an existing entity. How to add a new property

This tutorial covers how to add a property to the "Category" entity that ships with the nopCommerce source code.

## The data model

Entities will have two classes that are used to map records to a table. The first class defines the properties, fields, and methods consumed by the web application.

```sh
File System Location: [Project Root]\Libraries\Nop.Core\Domain\Catalog\Category.cs
Assembly: Nop.Core
Solution Location: Nop.Core.Domain.Catalog.Category.cs
```

The second class is used to map the properties defined in the class above to their respective SQL columns. The mapping class is also responsible for mapping relationships between different SQL tables.

```sh
File System Location: [Project Root]\Libraries\Nop.Data\Mapping\Catalog\CategoryMap.cs
Assembly: Nop.Data
Solution Location: Nop.Data.Mapping.Catalog.CategoryMap.cs
```

Add the following property to the Category class.

```csharp
public string SomeNewProperty { get; set; }
```

Add the following code to the constructor of the CategoryMap class.

```csharp
// This code maps a column in the database to the new property we created above
// This creates a nullable nvarchar with a length of 255 characters
// in the Category SQL table
this.Property(m => m.SomeNewProperty).HasMaxLength(255).IsOptional();
```

Because I’m all about results, at this point I would run the code, re-install the database, and verify that the column was created appropriately.

## The presentation model

The presentation model is used to transport information from a controller to the view (read more at asp.net/mvc). Models have another purpose; defining requirements.

We configured our database to only store 255 characters for the SomeNewProperty. If we try and save an SomeNewProperty with 300 characters the application will break (or truncate the text). We want the application to protect users from failures the best we can, and our view models help enforce requirements like string length.

```sh
File System Location: [Project Root]\Presentation\Nop.Web\Areas\Admin\Models\Catalog\CategoryModel.cs
Assembly: Nop.Admin
Solution Location: Nop.Web.Areas.Admin.Models.Catalog.CategoryModel.cs
```

The validator class is used to validate the data stored inside of the model class (e.g. required fields, max length, and required ranges).

```sh
File System Location: [Project Root]\Presentation\Nop.Web\Areas\Admin\Validators\Catalog\CategoryValidator.cs
Assembly: Nop.Web
Solution Location: Nop.Web.Areas.Admin.Validators.Catalog.CategoryValidator.cs
```

Add the property to our view model.

```csharp
// The NopResourceDisplayName provides the "key" used during localization
// Keep an eye out for more about localization in future blogs
[NopResourceDisplayName("Admin.Catalog.Categories.Fields.SomeNewProperty")]
public string SomeNewProperty { get; set; }
```

The requirements code will be added in the constructor of the validator.

```csharp
//I think this code can speak for itself
RuleFor(m => m.SomeNewProperty).Length(0, 255);
```

## The view

```sh
File System Location: [Project Root]\Presentation\Nop.Web\Administration\Views\Category\ _CreateOrUpdate.cshtml
Assembly: Nop.Admin
```

Views contain the html for displaying model data. Place this html under the "active" section.

```csharp
<div class="form-group">
     <div class="col-md-3">
        <nop-label asp-for="" />
     </div>
     <div class="col-md-9">
        <nop-editor asp-for="SomeNewProperty" />
        <span asp-validation-for="SomeNewProperty"></span>
     </div>
 </div>
```

## The controller

In this case the controller is responsible for mapping the domain data model to our view model and vice versa. The reason I choose the category model to update is because of the simplicity. I want this to be an introduction to the nopCommerce platform and I would like to keep it as simple as possible.

```sh
File System Location: [Project Root]\Presentation\Nop.Web\Areas\Admin\Controllers\CategoryController.cs
Assembly: Nop.Admin
Solution Location:
Nop.Web.Areas.Admin.Controllers.CategoryController.cs
```

We're going to make three updates to the CategoryController class.

* Data Model → View Model
* Create View Model → Data Model
* Edit View Model → Data Model

Normally I would write tests for the following code and verify that model mapping is working correctly, but I'll skip unit testing to keep it simple.

In the appropriate methods ("Create", "Edit", or "PrepareSomeModel") add the code to set this property. In most case it's not required because it's automatically handled by AutoMapper in the .ToModel() method.

In the public method to save entity (usually: "Create" or "Edit" methods with [HttpPost] attribute)

```csharp
// Edit View Model → Data Model
category.SomeNewProperty = model.SomeNewProperty;
```

## Database

If you're extending the already installed application (with created database), then you also have to manually add the appropriate column ("SomeNewProperty") to the appropriate table ("Category").

## Troubleshooting

* Recreate the database. Either your own custom SQL script or use the nopCommerce installer.
* Stop the development web server between schema changes.
* Post a detailed comment on [our forums](http://www.nopcommerce.com/boards/).
