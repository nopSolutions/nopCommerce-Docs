---
標題: 控制反轉與依賴注入
uid: zh-Hant/developer/tutorials/inversion-of-control
作者: git.AndreiMaz
貢獻者: git.exileDev, git.DmitriyKulagin
---

# 控制反轉與依賴注入

控制反轉（Inversion of Control, IoC）與依賴注入（Dependency Injection, DI）是兩種用於拆解應用程式中依賴關係的相關方法。[控制反轉 (IoC)](https://en.wikipedia.org/wiki/Inversion_of_control) 意味著物件不再負責建立其工作所依賴的其他物件，而是從外部來源取得所需的物件。[依賴注入 (DI)](http://en.wikipedia.org/wiki/Dependency_injection) 則是指在沒有物件介入的情況下完成此過程，通常是由框架元件透過建構函式參數傳遞與設定屬性來達成。Martin Fowler 對依賴注入或*控制反轉*有非常精闢的描述。我們在此不再贅述，您可以點擊[此處](https://martinfowler.com/articles/injection.html)閱讀他的文章。nopCommerce 使用 ASP.NET Core 內建的 DI 容器，即 `IServiceProvider` 介面。該容器負責將依賴項映射到特定型別，並將這些依賴項注入到各類物件中。一旦編寫好服務及其實現的對應介面，您就應該在任何實作 `INopStartup` 介面（位於 `Nop.Core.Infrastructure` 命名空間）的類別中註冊它們。`ConfigureServices` 方法負責在應用程式中安裝服務。服務是透過 `IServiceCollection` 參數新增至專案中的。

```csharp
    public class NopStartup : INopStartup
    {
        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
                services.AddScoped<IWebHelper, WebHelper>();
            ...
        }

        public void Configure(IApplicationBuilder application)
        {
        }

        public int Order => 2000;
    }
```

您可以根據需要建立任意數量的依賴註冊類別。請注意，每個實作 **INopStartup** 介面的類別都有一個 **Order** 屬性。它允許您替換現有的依賴項。若要覆寫 nopCommerce 的依賴項，請將 `Order` 屬性設定為大於 0 的數值。nopCommerce 會對這些依賴類別進行排序，並按遞增順序執行。數值越大，您的物件註冊的時間就越晚。

透過這種方式，您可以同時註冊內建的 ASP.NET Core 服務與您自訂的服務；同樣的註冊機制也適用於外掛中的服務註冊。