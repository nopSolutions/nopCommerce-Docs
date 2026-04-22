---
標題: 具備資料存取功能的外掛
uid: zh-Hant/developer/plugins/plugin-with-data-access-4.20
作者: nop.52152
貢獻者: git.DmitriyKulagin, git.rodolphito, git.exileDev, git.cromatido
---

# 具備資料存取功能的外掛 (4.20 與更早版本)

在本教學中，我將使用 nopCommerce 外掛架構來實作一個商品瀏覽追蹤器。在開始開發之前，請務必先閱讀、理解並順利完成以下列出的教學課程。我將略過先前文章中已涵蓋的部分說明，但您可以使用下方提供的連結進行複習。

- [開發人員教學課程](xref:zh-Hant/developer/tutorials/index)
- [更新現有實體。如何新增屬性。](xref:zh-Hant/developer/tutorials/update-existing-entity)
- [如何為 nopCommerce 4.20 編寫外掛](xref:zh-Hant/developer/plugins/how-to-write-plugin-4.20)

我們將從資料存取層開始編寫程式碼，接著進入服務層，最後以依賴注入作為結尾。

> [!NOTE]
> 這個外掛的實際應用價值值得商榷，但我一時想不出一個 nopCommerce 尚未內建且適合在篇幅適中的文章中介紹的功能。如果您在生產環境中使用此功能，我不提供任何保證。我一直對成功案例很感興趣，若您能告訴我這篇文章不僅僅具有教學價值，我會感到非常高興。

## 入門指南

建立一個新的類別庫專案「Nop.Plugin.Other.ProductViewTracker」。

![plugin-with-data-access_1](_static/plugin-with-data-access/plugin-with-data-access_1.jpg)

新增下列資料夾與 `plugin.json` 檔案。

![plugin-with-data-access_2](_static/plugin-with-data-access/plugin-with-data-access_2.jpg)

關於 `plugin.json` 檔案的資訊，請參閱 [plugin.json 檔案](xref:zh-Hant/developer/plugins/plugin_json)。

然後新增對下列專案的參考：Nop.Core、Nop.Data、Nop.Web.Framework

## 資料存取層（亦即在 nopCommerce 中建立新實體）

在「domain」命名空間內，我們將建立一個名為 `ProductViewTrackerRecord` 的公開類別。此類別繼承自 `BaseEntity`，除此之外它是一個非常簡單的檔案。請記住，所有的屬性都被標記為 `virtual`，這並非為了好玩。由於 Entity Framework 實例化與追蹤類別的方式，資料庫實體必須使用虛擬屬性。另外需要注意的是，我們沒有導覽屬性（關聯屬性），我稍後會再詳細說明。

```csharp
namespace Nop.Plugin.Other.ProductViewTracker.Domain
{
    public class ProductViewTrackerRecord : BaseEntity
    {
        public virtual int ProductId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual int CustomerId { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual bool IsRegistered { get; set; }
    }
}
```

**檔案位置**：若要確認特定檔案應放置的位置，請分析命名空間並據此建立檔案。

下一個要建立的是 Entity Framework 映射類別。在映射類別中，我們會映射欄位、資料表關聯以及資料庫資料表。

```csharp
namespace Nop.Plugin.Other.ProductViewTracker.Data
{
    public class ProductViewTrackerRecordMap : NopEntityTypeConfiguration<ProductViewTrackerRecord>
    {
        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ProductViewTrackerRecord> builder)
        {
            builder.ToTable(nameof(ProductViewTrackerRecord));
            //Map the primary key
            builder.HasKey(record => record.Id);
            //Map the additional properties
            builder.Property(record => record.ProductId);
            //Avoiding truncation/failure
            //so we set the same max length used in the product tame
            builder.Property(record => record.ProductName).HasMaxLength(400);
            builder.Property(record => record.IpAddress);
            builder.Property(record => record.CustomerId);
            builder.Property(record => record.IsRegistered);
        }
    }
}
```

下一個類別是資料存取層中最複雜且最重要的類別。Entity Framework 的 Object Context 是一個傳遞類別，它賦予我們資料庫存取權，並協助追蹤實體狀態（例如新增、更新、刪除）。此 Context 也用於產生資料庫結構描述或更新現有的結構描述。在自訂的 Context 類別中，我們無法參考先前存在的實體，因為這些型別已經與另一個 Object Context 關聯。這也是為什麼在追蹤記錄中沒有複雜導覽屬性的原因。

```csharp
namespace Nop.Plugin.Other.ProductViewTracker.Data
{
    public class ProductViewTrackerRecordObjectContext : DbContext, IDbContext
    {
        public ProductViewTrackerRecordObjectContext(DbContextOptions<ProductViewTrackerRecordObjectContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductViewTrackerRecordMap());
            base.OnModelCreating(modelBuilder);
        }

        public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public virtual string GenerateCreateScript()
        {
            return Database.GenerateCreateScript();
        }

        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql) where TQuery : class
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public virtual int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            using (var transaction = Database.BeginTransaction())
            {
                var result = Database.ExecuteSqlCommand(sql, parameters);
                transaction.Commit();
                return result;
            }
        }

        public void Install()
        {
               //create the table
               this.ExecuteSqlScript(GenerateCreateScript());
        }
        public void Uninstall()
        {
               //drop the table
               this.DropPluginTable(nameof(ProductViewTrackerRecord));
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public virtual void Detach<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public IQueryable<TQuery> QueryFromSql<TQuery>(string sql, params object[] parameters) where TQuery : class
        {
            throw new NotImplementedException();
        }

        public virtual bool ProxyCreationEnabled
        {
            get => ProxyCreationEnabled;
            set => ProxyCreationEnabled = value;
        }

        public virtual bool AutoDetectChangesEnabled
        {
            get => AutoDetectChangesEnabled;
            set => AutoDetectChangesEnabled = value;
        }
    }
}
```

## 應用程式啟動

此部分負責註冊我們在上一步中建立的記錄 Object Context。

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Other.ProductViewTracker.Data;
using Nop.Web.Framework.Infrastructure.Extensions;

namespace Nop.Plugin.Misc.RepCred.Infrastructure
{
    /// <summary>
    /// Represents object for the configuring plugin DB context on application startup
    /// </summary>
    public class PluginDbStartup : INopStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //add object context
            services.AddDbContext<ProductViewTrackerRecordObjectContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServerWithLazyLoading(services);
            });
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 11;
    }
}
```

## 服務層

服務層連接了資料存取層與呈現層。由於在程式碼中共享任何類型的職責是不好的做法，因此每個層級都需要隔離。服務層使用業務邏輯包裝資料層，而呈現層則依賴服務層。由於我們的任務非常簡單，我們的服務層除了與儲存庫溝通之外不做任何事（在 nopCommerce 中，儲存庫扮演 Object Context 的門面角色）。

```csharp
namespace Nop.Plugin.Other.ProductViewTracker.Services
{
    public interface IProductViewTrackerService
    {
        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        void Log(ProductViewTrackerRecord record);
    }
}

namespace Nop.Plugin.Other.ProductViewTracker.Services
{
    public class ProductViewTrackerService : IProductViewTrackerService
    {
        private readonly IRepository<ProductViewTrackerRecord> _productViewTrackerRecordRepository;
        public ViewTrackingService(IRepository<ProductViewTrackingRecord> productViewTrackerRecordRepository)
        {
            _productViewTrackerRecordRepository = productViewTrackerRecordRepository;
        }

        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        public virtual void Log(ProductViewTrackerRecord record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));
            _productViewTrackerRecordRepository.Insert(record);
        }
    }
}
```

## 依賴注入

Martin Fowler 對依賴注入或控制反轉撰寫了很棒的說明。我不會重覆他的內容，您可以在此找到他的文章。依賴注入管理物件的生命週期，並為依賴物件提供可使用的實例。首先，我們需要設定依賴容器，讓它了解將要控制哪些物件，以及建立這些物件時可能適用的規則。

```csharp
namespace Nop.Plugin.Other.ProductViewTracker.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_product_view_tracker";

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<ProductViewTrackerService>().As<IProductViewTrackerService>().InstancePerLifetimeScope();

            //data context
            builder.RegisterPluginDataContext<ProductViewTrackerRecordObjectContext>(CONTEXT_NAME);

            //override required repository with our custom context
            builder.RegisterType<EfRepository<ProductViewTrackerRecord>>()
            .As<IRepository<ProductViewTrackerRecord>>()
            .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
            .InstancePerLifetimeScope();
        }

        public int Order => 1;
    }
}
```

在上面的程式碼中，我們註冊了不同型別的物件，以便稍後將它們注入控制器、服務與儲存庫中。既然我們已經涵蓋了新主題，我將帶回一些舊主題，以便完成這個外掛。

## 檢視元件 (View Component)

讓我們建立一個檢視元件：

```csharp
namespace Nop.Plugin.Other.ProductViewTracker.Components
{
    [ViewComponent(Name = "ProductViewTracker")]
    public class ProductViewTrackerViewComponent : NopViewComponent
    {
        private readonly IProductService _productService;
        private readonly IProductViewTrackerService _productViewTrackerService;
        private readonly IWorkContext _workContext;
        public ProductViewTrackerViewComponent(IWorkContext workContext,
        IProductViewTrackerService productViewTrackerService,
        IProductService productService)
        {
            _workContext = workContext;
            _productViewTrackerService = productViewTrackerService;
            _productService = productService;
        }
        public IViewComponentResult Invoke(int productId)
        {
            //Read from the product service
            Product productById = _productService.GetProductById(productId);
            //If the product exists we will log it
            if (productById != null)
            {
                //Setup the product to save
                var record = new ProductViewTrackerRecord();
                record.ProductId = productId;
                record.ProductName = productById.Name;
                record.CustomerId = _workContext.CurrentCustomer.Id;
                record.IpAddress = _workContext.CurrentCustomer.LastIpAddress;
                record.IsRegistered = _workContext.CurrentCustomer.IsRegistered();
                //Map the values we're interested in to our new entity
                _productViewTrackerService.Log(record);
            }
            return Content("");
        }
    }
}
```

## 外掛安裝程式

```csharp
namespace Nop.Plugin.Other.ProductViewTracker
{
    public class ProductViewTrackerPlugin : BasePlugin
    {
        private readonly ProductViewTrackerRecordObjectContext _context;
        public ProductViewTrackerPlugin(ProductViewTrackerRecordObjectContext context)
        {
            _context = context;
        }
        public override void Install()
        {
            _context.Install();
            base.Install();
        }
        public override void Uninstall()
        {
            _context.Uninstall();
            base.Uninstall();
        }
    }
}
```

## 使用方式

追蹤程式碼應新增至 `ProductTemplate.Simple.cshtml` 與 `ProductTemplate.Grouped.cshtml` 檔案中。這些檔案是商品範本。

```csharp
@await Component.InvokeAsync("ProductViewTrackerIndex", new { productId = Model.Id })
```

附註：您也可以將其實作為小部件。在這種情況下，您就不需要編輯 cshtml 檔案。