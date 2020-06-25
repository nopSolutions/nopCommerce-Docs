---
title: nopCommerce Data Access Layer
uid: en/developer/tutorials/data-access-layer
author: git.AndreiMaz
contributors: git.exileDev
---

# nopCommerce Data Access Layer

The Nop.Data project contains a set of classes and functions for reading from and writing to a database or other data store. The Nop.Data library helps separate data-access logic from your business objects. nopCommerce uses the Linq2DB Code-First approach. Code-First allows a developer to define entities in the source code (all core entities are defined in the Nop.Core project), and then use Linq2DB and FluentMigrator to generate the database from the C# classes. That's why it's called Code-First. You can then query your objects using LINQ, which translates to SQL behind the scenes and is executed against the database. NopCommerce uses [Fluent API](https://fluentmigrator.github.io/articles/technical/fluent-api-create.html) to fully customize the persistence mapping.
