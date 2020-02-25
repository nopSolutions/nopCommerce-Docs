---
title: nopCommerce Data Access Layer
uid: en/developer/tutorials/data-access-layer
author: git.AndreiMaz
contributors: git.exileDev
---

# nopCommerce Data Access Layer

The Nop.Data project contains a set of classes and functions for reading from and writing to a database or other data store. The Nop.Data library helps separate data-access logic from your business objects. NopCommerce uses the Entity Framework Core (EF Core) Code-First approach. Code-First allows a developer to define entities in the source code (all core entities are defined in the Nop.Core project), and then use EF Core to generate the database from the C# classes. That's why it's called Code-First. You can then query your objects using LINQ, which translates to SQL behind the scenes and is executed against the database. NopCommerce uses [Fluent API](https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx) to fully customize the persistence mapping. You can find more about Code-First [here](https://weblogs.asp.net/senthil/code-first-ef-core) or [here](https://neelbhatt.com/2018/01/14/code-first-migration-in-net-core2-0-crud-operations/).
