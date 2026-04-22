---
標題: 資料庫遷移
uid: zh-Hant/developer/tutorials/migrations
作者: git.AndreiMaz
貢獻者: git.skoshelev, git.DmitriyKulagin
---
# 資料庫遷移

## 資料庫處理方式變更的簡述

nopCommerce 在 4.30 版本中大幅重構了與資料庫互動的方式。第一個顯而易見的變更是完全捨棄了導覽屬性 (navigation properties)。我們可能會針對此作法的實用性進行討論，但它確實有幾個正面效益：

1. 簡化程式碼的理解與維護。
    > [!NOTE]
    > 在程式碼重構過程中，我們發現並修正了數個影響效能與功能的瑕疵。
1. 對查詢及其執行時機擁有完全的控制權（這對整個解決方案的效能有正面影響）。
1. 能夠簡化遷移至任何資料庫框架的流程（這是最重要的）。

由於 nopCommerce 完全轉向 .Net Core（4.10 版本）並成為跨平台解決方案，支援多種資料庫已變得越來越重要。nopCommerce 團隊進行了深入的研究與分析，決定放棄使用標準的 Entity Framework Core。同時，我們決定不再透過 OOP 方法（這是 C# 開發人員最常用的方式）使用 LINQ 查詢來操作資料庫。最終選擇了 Linq2DB 與 FluentMigrator 的組合。以下我將詳細說明這兩個框架各自的角色。

## Linq2DB

> [!NOTE]
> 從 4.30 版本開始，nopCommerce 使用 Linq2DB 作為 ORM 框架。Linq2DB 是一個物件關聯對映 (ORM) 框架，它讓 .NET 開發人員能夠使用 .NET 物件來操作資料庫。它可以將 .NET 物件對映到各種不同的資料庫提供程序。

在 nopCommerce 中，Linq2DB 被用作資料庫存取層。目前，nopCommerce 支援三種最受歡迎的資料庫：MS SQL Server、MySQL 與 PostgreSQL。如果分析程式碼，可以輕易發現每個資料庫都由實作 INopDataProvider 介面的類別所支援。但如果您不打算建立自己的資料庫存取提供程序，則可以完全忽略這些實作細節。對於大多數開發任務，理解以下幾點就足夠了：

1. 您需要一個對應資料庫中資料表的物件（POCO 類別）。
1. 所有資料表資料的操作皆透過 `IRepository<TEntity>` 介面進行。您甚至不需要擔心其在 IoC 中的放置位置，因為它會透過對適當工廠方法的呼叫來註冊。
1. 您需要控管資料庫中資料表的建立。

為了解決最後一個問題，我們需要使用組合中的第二個框架，即 FluentMigrator。

## FluentMigrator

> [!NOTE]
> Fluent Migrator 是一個 .NET 的遷移框架，與 Ruby on Rails 的 Migrations 非常相似。*遷移 (Migrations)* 是一種結構化的方式來變更您的資料庫結構，它取代了手動執行大量 SQL 指令碼的作法（這些指令碼通常需要每位開發人員手動執行）。遷移解決了針對多個資料庫（例如開發人員的本機資料庫、測試資料庫與生產資料庫）演進資料庫結構的問題。資料庫結構變更在以 C# 編寫的類別中描述，這些類別可以提交到版本控制系統中。

有關新增實體的詳細規劃請參閱以下文章：[具有資料存取權限的外掛](xref:zh-Hant/developer/plugins/how-to-write-plugin-4.70)。因此，我們僅針對一般的理論要點進行說明：

1. 遷移在 nopCommerce 程式碼層級中獲得支援。
1. 您可以建立任何繼承自抽象類別 **MigrationBase** 的遷移。
1. 為了簡化遷移的版本控制，我們在程式碼中新增了繼承自 **MigrationAttribute** 的 **NopMigrationAttribute** 屬性。現在，您只需指定遷移建立的日期與時間，而不需要使用常見的長數字編號。
1. 我們還新增了 **SkipMigrationOnUpdateAttribute** 屬性，該屬性用於指出在更新過程中是否應跳過此遷移。
1. 您可以透過兩種方式在資料庫中建立資料表：
    * 在遷移類別的 **Up** 方法中使用 **Create.Table** 方法，並使用擴充方法指定所有細節。
    * 在遷移類別的 **Up** 方法中使用 **IMigrationManager.BuildTable<T\>** 方法，並在需要時使用 **IEntityBuilder** 與 **INameCompatibility** 介面的實作來指定所有細節（在 nopCommerce 中我們採用此方式）。

> [!IMPORTANT]
>
> 為了執行新的遷移，您必須增加 plugin.json 檔案中的版本號。