---
title: Description of permission
uid: en/developer/tutorials/permissions
author: git.DmitriyKulagin
contributors: git.DmitriyKulagin
---

# Description of permission

**Permissions** are an important element of application resource security. With their help, you can restrict access to a particular part of the application, or vice versa, provide the necessary rights to read/change a specific area.

All available permissions to control the scope are in the *Access Control List (ACL)* section.

Like any other changes in the application, it is recommended to make through plugins. So let's look at adding permissions via an abstract plugin. This tutorial aims to show you how to manage permissions, but creating the plugin itself is out of the scope of this article.

## Adding a custom permission

First of all, let's create a class that describes the new permission - this will be the permission provider. It is necessary and sufficient that this class be inherited from the `IPermissionProvider` interface.

```csharp
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

In this example, the permission is set only for the *administrator role*, but this list can be expanded if necessary.

Now we will add this permission when our plugin is installed. This must be done in the `InstallAsync()` method:

```csharp
//add permission
await _permissionService.InstallPermissionsAsync(new WebApiBackendPermissionProvider());
```

Do not forget to also remove the permission in the `UninstallAsync()` method when removing the plugin:

```csharp
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

Now, in any place where we need to check the legitimacy of the user's access to a specific resource, we need to call the `AuthorizeAsync()` method of the `IPermissionService` service with the permission:

```csharp
//check whether current customer has access to Web API
if (await _permissionService.AuthorizeAsync(WebApiBackendPermissionProvider.AccessWebApiBackend))
    return;
```

That's essentially all, it remains only to add that after installing the plugin, you can see the permission you added in the **ACL** section.

## See also

- [Access control list](xref:en/running-your-store/customer-management/access-control-list)
