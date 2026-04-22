---
標題: 擴充權限
uid: zh-Hant/developer/tutorials/permissions
作者: git.DmitriyKulagin
貢獻者: git.DmitriyKulagin
---

# 擴充權限

**權限**是應用程式資源安全的重要元素。透過權限，您可以限制對應用程式特定部分的存取，反之亦然，為讀取或變更特定區域提供必要的權利。

所有用於控制範圍的可用權限皆位於*權限控制（ACL）*區段中。

與應用程式中的任何其他變更一樣，建議透過外掛來進行。因此，讓我們來看看如何透過抽象外掛新增權限。本教學旨在向您展示如何管理權限，若要了解如何建立外掛本身，請查看[此頁面](xref:zh-Hant/developer/plugins/index)。

## 在 nopCommerce 4.80 及更高版本中新增自訂權限

首先，讓我們建立一個描述新權限的類別 - 這將是權限提供程序（Permission Provider）。此類別必須繼承自 `IPermissionConfigManager` 介面，這便已足夠。

```csharp
public partial class WebApiBackendPermissionConfigManager : IPermissionConfigManager
{
    public const string ACCESS_WEB_API = "AccessWebApi";

    /// <summary>
    /// Gets all permission configurations
    /// </summary>
    public IList<PermissionConfig> AllConfigs =>
        new List<PermissionConfig>
        {
            new("Access Web API Backend", ACCESS_WEB_API , nameof(StandardPermission.System), NopCustomerDefaults.AdministratorsRoleName)
        };
}
```

指定的記錄將在無需任何額外操作的情況下安裝到資料庫中。

在此範例中，該權限僅針對*管理員角色*設定，但如有必要，可以擴充此列表。

請記得在移除外掛時，也要在 `UninstallAsync()` 方法中移除該權限：

```csharp
//delete permission
var permissionRecord = (await _permissionService.GetAllPermissionRecordsAsync())
    .FirstOrDefault(x => x.SystemName == WebApiBackendPermissionConfigManager.ACCESS_WEB_API);

await _permissionService.DeletePermissionRecordAsync(permissionRecord);
```

現在，在任何需要檢查使用者存取特定資源合法性的地方，我們都需要使用該權限呼叫 `IPermissionService` 服務的 `AuthorizeAsync()` 方法：

```csharp
 //check whether current customer has access to Web API
 if (await _permissionService.AuthorizeAsync(WebApiBackendPermissionConfigManager.ACCESS_WEB_API))
     return;
```

另一種將權限用作控制器動作篩選器（Action Filter）屬性的方法如下：

```csharp
[CheckPermission(WebApiBackendPermissionConfigManager.ACCESS_WEB_API)]
public virtual async Task<IActionResult> Configure()
{
    ...
}
```

基本上就是這樣，安裝外掛後，您就可以在 **權限控制（ACL）** 區段中看到您新增的權限。

> [!NOTE]
>
> 如果您想直接使用預設權限，它們全都列在 `StandardPermission` 類別（`Nop.Services.Security` 命名空間）中。
> 例如：
>
> ```csharp
> [CheckPermission(StandardPermission.Configuration.MANAGE_SETTINGS)]
> ```

## Adding a custom permission in nopCommerce 4.70 and below

First of all, let's create a class that describes the new permission - this will be the permission provider. It is necessary and sufficient that this class be inherited from the `IPermissionProvider` interface.

``````csharp
public partial class WebApiBackendPermissionProvider : IPermissionProvider
{
    public static readonly PermissionRecord AccessWebApiBackend = new ()
    {
        Name = "Access Web API Backend", SystemName = "AccessWebApi", Category= "Standard"
    };
    /// <summary>
    /// Get permissions
    /// </summary>
    /// <returns>Permissions</returns>
    public virtual IEnumerable<PermissionRecord> GetPermissions()
    {
        return new[]
        {
            AccessWebApiBackend
        };
    }
    /// <summary>
    /// Get default permissions
    /// </summary>
    /// <returns>Permissions</returns>
    public virtual HashSet<(string systemRoleName, PermissionRecord[]permissions)> GetDefaultPermissions()
    {
        return new () { (NopCustomerDefaults.AdministratorsRoleName, new [] { AccessWebApiBackend }) };
    }
}
```
```

In this example, the permission is set only for the *administrator role*, but this list can be expanded if necessary.

Now we will add this permission when our plugin is installed. This must be done in the `InstallAsync()` method:

``````csharp
//add permission
await _permissionService.InstallPermissionsAsync(new WebApiBackendPermissionProvider());
```
```

Do not forget to also remove the permission in the `UninstallAsync()` method when removing the plugin:

``````csharp
//delete permission
var permissionRecord = (await _permissionService.GetAllPermissionRecordsAsync())
    .FirstOrDefault(x => x.SystemName == WebApiBackendPermissionProvider.AccessWebApiBackend.SystemName);
var listMappingCustomerRolePermissionRecord = await _permissionService.GetMappingByPermissionRecordIdAsync(permissionRecord.Id);
foreach (var mappingCustomerPermissionRecord in listMappingCustomerRolePermissionRecord)
    await _permissionService.DeletePermissionRecordCustomerRoleMappingAsync(
        mappingCustomerPermissionRecord.PermissionRecordId,
        mappingCustomerPermissionRecord.CustomerRoleId);

await _permissionService.DeletePermissionRecordAsync(permissionRecord);
```
```

Now, in any place where we need to check the legitimacy of the user's access to a specific resource, we need to call the `AuthorizeAsync()` method of the `IPermissionService` service with the permission:

``````csharp
//check whether current customer has access to Web API
if (await _permissionService.AuthorizeAsync(WebApiBackendPermissionProvider.AccessWebApiBackend))
    return;
```

基本上就是這樣，安裝外掛後，您就可以在 **權限控制（ACL）** 區段中看到您新增的權限。

## 參閱

- [權限控制（ACL）](xref:zh-Hant/running-your-store/customer-management/access-control-list)