---
標題: 使用 DataTables
uid: zh-Hant/developer/tutorials/guide-to-creating-a-page-containing-a-reporting-table-of-datatables
作者: nop.sea
貢獻者: git.RomanovM, git.DmitriyKulagin
---

# 使用 DataTables

在本教學中，我們將學習如何透過自訂功能來擴充 nopCommerce 的後台管理功能，並建立一個包含資料表格作為報表的頁面。在開始本教學之前，您需要具備一些相關主題的基礎知識與理解。

* [nopCommerce 架構](xref:zh-Hant/developer/tutorials/source-code-organization)。
* [nopCommerce 外掛](xref:zh-Hant/developer/tutorials/guide-to-expanding-the-functionality-of-the-basic-functions-of-nop-commerce-through-a-plugin)。
* [nopCommerce 路由](xref:zh-Hant/developer/tutorials/register-new-routes)。

如果您不熟悉上述主題，我們強烈建議您先進行了解。

本教學將示範如何建立一個表格，以顯示使用者按國家/地區的分佈情況（根據帳單地址）。讓我們依照步驟逐步完成上述功能的建立過程。

## 建立 nopCommerce 外掛專案

我們假設您已經知道在何處以及如何建立 nopCommerce 外掛專案，並根據 nopCommerce 標準進行專案設定。如果您不知道，可以造訪[此頁面](xref:zh-Hant/developer/plugins/how-to-write-plugin-4.70)來學習如何建立與設定 nopCommerce 外掛專案。

如果您已依照上述連結建立並設定好您的外掛專案，您最終應該會得到類似這樣的資料夾結構。

![image1](_static/guide-to-creating-a-page-containing-a-reporting-table-of-datatables/image1.png)

### Models/CustomersDistribution.cs

首先，讓我們在 **Models** 資料夾內建立一個名為 **CustomersDistribution** 的模型。

```cs
public record CustomersDistribution : BaseNopModel
{
    /// <summary>
    /// Country based on the billing address.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Number of customers from a specific country.
    /// </summary>
    public int NoOfCustomers { get; set; }
}
```

接著，讓我們在 *Models* 資料夾內新增名為 `CustomersByCountrySearchModel` 的搜尋模型。

```cs
public record CustomersByCountrySearchModel : BaseSearchModel
{
}
```

nopCommerce 使用儲存庫模式（Repository pattern）進行資料存取，這非常適合依賴注入（Dependency Injection）機制。

### Services/ICustomersByCountry.cs

現在讓我們建立一個從資料庫提取所需資料的服務。對於服務，我們將建立一個介面，並編寫一個實作該介面的服務類別。

```cs
public interface ICustomersByCountry
{
    Task<List<CustomersDistribution>> GetCustomersDistributionByCountryAsync()
}
```

### Services/CustomersByCountry.cs

在此我們建立一個名為 **CustomersByCountry** 的類別，它實作了 **ICustomersByCountry** 介面。

```cs
public class CustomersByCountry : ICustomersByCountry
{
    private readonly IAddressService _addressService;
    private readonly ICountryService _countryService;
    private readonly ICustomerService _customerService;

    public CustomersByCountry(IAddressService addressService, 
        ICountryService countryService,
        ICustomerService customerService)
    {
        _addressService = addressService;
        _countryService = countryService;
        _customerService = customerService;
    }

    public async Task<List<CustomersDistribution>> GetCustomersDistributionByCountryAsync()
    {
        return await _customerService.GetAllCustomersAsync()
            .Where(c => c.ShippingAddressId != null)
            .Select(c => new
            {
                await (_countryService.GetCountryByAddressAsync(_addressService.GetAddressById(c.ShippingAddressId ?? 0))).Name,
                c.Username
            })
            .GroupBy(c => c.Name)
            .Select(cbc => new CustomersDistribution { Country = cbc.Key, NoOfCustomers = cbc.Count() }).ToList();
    }
}
```

我們實作了介面中用於從資料庫檢索資料的方法。採用這種方法是為了能夠使用依賴注入技術，將此服務注入到控制器中。

### Infrastructure/PluginNopStartup.cs

為了將我們的介面及其實作註冊到依賴注入（DI）容器中，我們需要建立一個實作 `INopStartup` 介面的類別。此介面包含兩個方法和一個屬性。我們的主要重點是實作 `ConfigureServices` 方法，我們將在其中向 DI 容器註冊我們的介面。

```cs
/// <summary>
/// Represents object for the configuring services on application startup
/// </summary>
public class PluginNopStartup : INopStartup
{
    /// <summary>
    /// Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //register custom services
        services.AddScoped<ICustomersByCountry, CustomersByCountry>();
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
    public int Order => 3000;
}
```

### Controllers/CustomersByCountryController.cs

現在讓我們建立一個控制器類別。為外掛控制器命名的良好做法是使用 *{Group}{Name}Controller.cs* 的格式。例如：`TutorialCustomersByCountryController`，這裡就是 *{Tutorial}{CustomersByCountry}Controller*。但請記住，不需要強制要求使用 *{Group}{Name}* 命名，這只是 nopCommerce 建議的命名方式，而名稱中的 `Controller` 部分則是 .Net MVC 的必要要求。

```cs
    [AutoValidateAntiforgeryToken]
    [AuthorizeAdmin] //confirms access to the admin panel
    [Area(AreaNames.Admin)] //specifies the area containing a controller or action
    public class DistOfCustByCountryPluginController : BasePluginController
    {
        private readonly ICustomersByCountry _service;
        public DistOfCustByCountryPluginController(ICustomersByCountry service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Configure()
        {
            CustomersByCountrySearchModel customerSearchModel = new CustomersByCountrySearchModel
            {
                AvailablePageSizes = "10"
            };
            return View("~/Plugins/Tutorial.DistOfCustByCountry/Views/Configure.cshtml", customerSearchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomersCountByCountry()
        {
            try
            {
                return Ok(new DataTablesModel { Data = await _service.GetCustomersDistributionByCountryAsync() });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
```

在控制器中，我們注入了先前建立的 **ICustomersByCountry** 服務，以便從資料庫取得資料。這裡我們建立了兩個 Action，一個是 `HttpGet` 類型，另一個是 `HttpPost` 類型。`Configure` Action 會回傳一個名為 "Configure.cshtml" 的檢視，我們稍後再建立它。而 `GetCustomersCountByCountry` Action 則使用注入的服務來檢索資料，並以 JSON 格式回傳。此 Action 將由資料表格呼叫，表格期望的回應為 `DataTablesModel` 物件。因此，我們在此設定了 `data` 屬性，這就是將在表格中呈現的資料。

### Views/Configure.cshtml

現在讓我們建立一個包含 *DataTables* 的檢視，以便顯示資料供使用者檢視。

```cs
@using Nop.Web.Framework.Models.DataTables
@{
    Layout = "_ConfigurePlugin";
}

@await Html.PartialAsync("Table", new DataTablesModel
{
    Name = "customersDistributionByCountry-grid",
    UrlRead = new DataUrl("GetCustomersCountByCountry", "TutorialCustomersByCountry"),
    Paging = false,
    ColumnCollection = new List<ColumnProperty>
    {
        new ColumnProperty(nameof(CustomersDistribution.Country))
        {
            Title = "Country",
            Width = "300"
        },
        new ColumnProperty(nameof(CustomersDistribution.NoOfCustomers))
        {
            Title = "Number Of Customers",
            Width = "100"
        }
    }
})
```

### Views/_ViewImports.cshtml

*_ViewImports.cshtml* 檔案包含匯入檢視檔案所需所有參照的程式碼。

```cs
@inherits Nop.Web.Framework.Mvc.Razor.NopRazorPage<TModel>
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, Nop.Web.Framework

@using Microsoft.AspNetCore.Mvc.ViewFeatures
@using Nop.Web.Framework.UI
@using Nop.Web.Framework.Extensions
@using System.Text.Encodings.Web
@using Nop.Plugin.Tutorial.DistOfCustByCountry.Models
@using Nop.Web.Framework.Models.DataTables;
@using Microsoft.AspNetCore.Routing;
```

* 在 `Configure.cshtml` 中，我們使用了一個名為 **`Table`** 的部分檢視（Partial View）。這是 nopCommerce 對 *JQuery DataTables* 的實作。您可以在 `Nop.Web/Areas/Admin/Views/Shared/Table.cshtml` 下找到該檔案。您可以在那裡看到 *DataTables* 的實作程式碼。此檢視模型採用 `DataTablesModel` 類別來設定 *DataTables*。讓我們解釋一下我們為 `DataTablesModel` 類別設定的屬性：
* **Name:** 這將被設定為 *DataTables* 的 ID。
* **UrlRead:** 這是 *DataTables* 用於提取表格呈現資料的 URL。在此我們將其設為 `TutorialCustomersByCountry` 控制器的 **`GetCustomersCountByCountry`** Action，我們正是從該處獲取 *DataTables* 所需的資料。
* **Paging:** 此屬性用於啟用或停用 DataTables 的分頁功能。
* **ColumnCollection:** 此屬性保存了欄位設定屬性。

還有許多其他屬性，您可以嘗試操作以了解每個屬性的用途。

### Infrastructure/RouteProvider

最後一步是為 `TutorialCustomersByCountry` 控制器的 "GetCustomersCountByCountry" Action 註冊路由。我們不需要為 "Configure" Action 註冊路由，因為我們已經在 `DistOfCustByCountryPlugin` 類別中註冊過了。

```cs
/// <summary>
/// Represents plugin route provider
/// </summary>
public class RouteProvider : IRouteProvider
{
    /// <summary>
    /// Register routes
    /// </summary>
    /// <param name="endpointRouteBuilder">Route builder</param>
    public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        //add route for the access token callback
        endpointRouteBuilder.MapControllerRoute("CustomersDistributionByCountry", "Plugins/Tutorial/CustomerDistByCountry/",
            new { controller = "TutorialCustomersByCountry", action = "GetCustomersCountByCountry" });
    }

    /// <summary>
    /// Gets a priority of route provider
    /// </summary>
    public int Priority => 0;
}
```

>[!NOTE]
> 欲了解更多關於 nopCommerce 路由的資訊，請造訪[此頁面](xref:zh-Hant/developer/tutorials/register-new-routes)

現在只需建置您的專案並執行即可。以管理員身分登入，前往「設定」（Configuration）下的「本機外掛」（LocalPlugins）選單，您會看到剛建立的外掛。安裝該外掛。安裝完成後，您的外掛中會出現一個「設定」按鈕。如果您已正確遵循本教學，您將會看到類似下圖的輸出結果：

![image2](_static/guide-to-creating-a-page-containing-a-reporting-table-of-datatables/image2.png)