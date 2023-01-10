---
title: How do migrations work?
uid: en/developer/tutorials/migrations
author: git.AndreiMaz
contributors: git.skoshelev, git.DmitriyKulagin
---
# How do migrations work?

## Short description of the changes made in the approach to working with the database

The work with the database was significantly reworked in the nopCommerce version 4.30. The first change that could be noticed is a complete rejection of navigation properties. We may think and argue about the usefulness of this approach but it has a couple of positive points:

1. Simplify the understanding and maintenance of the code.
    > [!NOTE]
    > During the code refactoring, we found and corrected several inaccuracies affecting both performance and functionality.
1. Full control over the queries and the moment of their execution (which positively affects the performance of the entire solution).
1. Possibility to simplify the migration process to any database framework (most importantly).

Since nopCommerce completely switched to .Net Core (version 4.10) and became a cross-platform solution, supporting several databases becomes a more and more important issue. The nopCommerce team has conducted considerable research and analysis and decided to abandon the use of the standard Entity Framework Core. At the same time, we decided not to work with the database through LINQ queries using the OOP approach (what is the most common approach used by C#-developers). The final choice fell on a bunch of Linq2DB and FluentMigrator. Below, I will describe the role of each of these frameworks in detail.

## Linq2DB

> [!NOTE]
> Started from 4.30 version nopCommerce uses Linq2DB as an ORM Framework. Linq2DB is an object-relational mapper (ORM) that enables .NET developers to work with a database using .NET objects. It can map .Net objects to various numbers of Database providers.

In nopCommerce, Linq2DB is used as a database-access level. Currently, nopCommerce supports two of the most popular databases: MS SQL Server and MySQL Server. If we analyze the code, we can easily see that each database is supported by its class that implements the INopDataProvider interface. But if you do not plan to create your database access provider, you can ignore the implementation details at all. For most development tasks, understanding just a few points will be sufficient:

1. You need an object corresponding to the table in the database (POCO class).
1. All work with table data is carried out through the IRepository `<TEntity>` interface. You do not even need to take care of its placement into the IoC, since it is registered through a call to the appropriate factory method.
1. You need to control the creation of the table in the database.

And to solve the last problem, we need to deal with the second framework from the bundle, namely with FluentMigrator.

## FluentMigrator

> [!NOTE]
> Fluent Migrator is a migration framework for .NET much like Ruby on Rails Migrations. *Migrations* are a structured way to alter your database schema and are an alternative to creating lots of SQL scripts that have to be run manually by every developer involved. Migrations solve the problem of evolving a database schema for multiple databases (for example, the developer's local database, test database, and production database). Database schema changes are described in classes written in C#. These classes can be checked into a version control system.

The detailed plan of adding your entities is described in the following article: [Plugin with data access](xref:en/developer/plugins/how-to-write-plugin-4.60). Therefore, we will remain only on general theoretical points:

1. Migrations are supported at the level of the nopCommerce code itself.
1. You can create any migrations inherited from the abstract class **MigrationBase**.
1. To simplify version control for migrations we added the **NopMigrationAttribute** attribute inherited from **MigrationAttribute** to the code. Now you can simply specify the date and time when the migration was created instead of the usual long number.
1. We also added the **SkipMigrationOnUpdateAttribute** attribute that indicates if migration should be skipped during the update process.
1. You can create a table in the database in two ways:
    * Use **Create.Table** method in the **Up** method of your migration class and specify all details using the extension methods.
    * Use **IMigrationManager.BuildTable\<T\>** method in the **Up** method of your migration class and specify all details, if needed, using the implementation of the **IEntityBuilder** and **INameCompatibility** interfaces (in nopCommerce we use this approach).

> [!IMPORTANT]
>
> In order to run new migrations you have to increase the version in your plugin.json file.
