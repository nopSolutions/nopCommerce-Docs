---
標題: ITypeFinder 介面
uid: zh-Hant/developer/tutorials/type-finder
作者: git.skoshelev
貢獻者: git.mariannk, git.DmitriyKulagin
---

# ITypeFinder 介面

## ITypeFinder

這是一個非常簡單的介面。它僅包含兩個方法（儘管其中一個有兩個多載版本）。您可以在下方看到該介面的定義：

  ```csharp
/// <summary>
/// Classes implementing this interface provide information about types 
/// to various services in the Nop engine.
/// </summary>
public interface ITypeFinder
{
    /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
    IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

    /// <param name="assignTypeFrom">Assign type from</param>
    /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
    IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

    /// <summary>
    /// Gets the assemblies related to the current implementation.
    /// </summary>
    IList<Assembly> GetAssemblies();
}
  ```

實作此介面的類別，其主要任務是在組件（assemblies）中搜尋指定類別或介面的型別。我們將透過 nopCommerce 原始程式碼中的範例，來探討這在哪些情況下會派上用場。但首先，讓我們看看 ``GetAssemblies`` 方法。此方法的任務很單純，就是回傳進行搜尋的組件列表。該列表具體如何形成，取決於該介面的特定實作。

## ITypeFinder 的預設實作

此介面的主要預設實作是 ``WebAppTypeFinder`` 類別。而 ``WebAppTypeFinder`` 僅稍微擴充了 ``AppDomainTypeFinder`` 類別，後者本質上完成了所有型別搜尋的工作。但我們使用這個衍生類別，是因為它將型別搜尋的範圍擴展到了 **\Bin** 目錄下的所有組件，而主類別僅處理當前應用程式網域（application domain）中的組件。

我們不深入探討 ``FindClassesOfType`` 方法的實作細節（因為它們最終都歸結為一個非常簡單的函式，其程式碼可在 [this link](https://github.com/nopSolutions/nopCommerce/blob/develop/src/Libraries/Nop.Core/Infrastructure/AppDomainTypeFinder.cs#L184) 處取得），讓我們繼續探討此介面最重要的部分。

## 那麼為什麼我們需要 ITypeFinder 介面？

此介面被用於 nopCommerce 運作中幾個非常重要的方面：

1. 搜尋組件以配置遷移機制（[遷移是如何運作的？](xref:zh-Hant/developer/tutorials/migrations)）
1. 搜尋正確啟動網站所需特定介面的類別，例如：
    * ``IStartupTask`` - 模組與外掛的初步初始化
    * ``INopStartup`` - 在應用程式啟動時配置服務與中介軟體
    * ``IOrderedMapperProfile`` - 建立 **AutoMapper** 配置
    * ``IEntityBuilder``、``INameCompatibility`` - 為 **Linq2Db** 配置資料庫實體建構器，以保持資料表命名的向後相容性（[nopCommerce 資料存取層](xref:zh-Hant/developer/tutorials/source-code-organization#librariesnopdata)）
    * ``IRouteProvider`` - 註冊路由
    * ``IConsumer<T>`` - 為內部事件（如資料庫實體變更）註冊處理常式
    * ``IExternalAuthenticationRegistrar`` - 註冊並配置外部驗證方法
1. 即時搜尋合適的配送追蹤器

## 結論

如您所見，這個介面雖然小，但非常有價值。如果沒有使用透過 `ITypeFinder` 介面方法所實作的這種方法，要在 nopCommerce 中實作如此靈活的模組化結構將會非常困難。