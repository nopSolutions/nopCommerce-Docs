---
title: The settings from the appsettings.json file
uid: en/developer/tutorials/appsettings-json-file
author: git.nopsg
contributors: git.nopsg, git.DmitriyKulagin, git.mariannk
---

# The settings from the appsettings.json file

## Overview

This article contains the description of the `appsettings.json` file. In this article, we will explain what settings are available in this file, what they are used for and how to use these settings to change the functionality/behavior of the nopCommerce project.

> [!NOTE]
>
> You can also edit this file from the **Configuration → Settings → App settings** page.

> [!NOTE]
>
> All settings can be overridden in environment variables.

## The appsettings.json file overview

If you have worked on `ASP.NET Core` project previously or you are familiar wit `ASP.NET Core` then you might have used the `appsettings.json` file and have some understanding of what this file is and what this file is used for.

The `appsettings.json` file is generally used to store the application configuration settings such as database connection strings, any application scope global variables and many other information. Actually in `ASP.NET Core`, the application configuration settings can be stored in different configurations sources such as `appsettings.json` file, `appsettings.{EnvironmentName}.json` file (where the {Environment} is the application's current hosting environments such as Development, Staging or Production), `User Secrets` (where we used to store sensitive information) etc.

## Settings available in the appsettings.json file

### CacheConfig

Cache configuration

* **DefaultCacheTime** This setting determines the lifetime of the cached data. The default is 60 minutes.
* **ShortTermCacheTime** In some cases, it is required to cache data for a shorter period of time than the default. These values apply to caching addresses, generic attributes, customers, etc.
* **BundledFilesCacheTime** This setting is used when the function of combining css and/or js files into one is activated, to control the cache lifetime for them.

### HostingConfig

Hosting contains settings used to configure the hosting behavior. It is a json object and contains some properties settings which can be tweak to change the behavior for hosting.

* **UseHttpClusterHttps** This setting expects a boolean value. Default value is "**false**", you can set the value to "**true**" if your hosting uses a load balancer. It'll be used to determine whether the current request is HTTPS.
* **UseHttpXForwardedProto** This setting expects a boolean value. Default value is "**false**", You might want to set the value to "**true**" if your hosting uses load balancer and the if you are enabling the above **`UseHttpClusterHttps`** setting. This setting is used to add **`X-Forwarded-Proto`** in the `HTTP` header. **`X-Forwarded-Proto`** is an HTTP Header and is part of the HTTP standard. It is set on each HTTP request by a proxy or load balancer and can be used by a server application to determine what protocol the client used to connect.
* **ForwardedHttpHeader** This setting expects a string value. You can use this setting if your hosting doesn't use `"X-FORWARDED-FOR"` header to determine IP address. In some cases server use other HTTP header. You can specify a custom HTTP header here. For example, CF-Connecting-IP, X-FORWARDED-PROTO, etc.

#### DistributedCacheConfig

A distributed cache is a cache shared by multiple app servers, typically maintained as an external service to the app servers that access it. A distributed cache can improve the performance and scalability of an ASP.NET Core app, especially when the app is hosted by a cloud service or a server farm.

* **DistributedCacheType** You can choose one of the implementations:
  * **Memory** This is a framework-provided implementation of `IDistributedCache` that stores items in memory. The Distributed Memory Cache isn't an actual distributed cache. Cached items are stored by the app instance on the server where the app is running.
  * **SQL Server** The Distributed SQL Server Cache implementation allows the distributed cache to use a SQL Server database as its backing store. To create a SQL Server cached item table in a SQL Server instance, you can use the sql-cache tool. The tool creates a table with the name and schema that you specify. It is recommended to use a separate database for this purpose.
  
  ```sh
  dotnet sql-cache create "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DistCache;Integrated Security=True;" dbo nopCache
  ```

  * **Redis** nopCommerce supports *Redis* out of the box. In order to enable the `Redis` in our application we must set appropriate value for the following settings. For more information about [Redis](https://azure.microsoft.com/documentation/articles/cache-dotnet-how-to-use-azure-redis-cache).
* **Enabled** This setting expects a boolean value. Set the value to **"true"** if you want to enable `Distributed cache`. The system uses a In-Memory caching, so this setting is used to indicating whether we should use `Distributed Cache` for caching, instead of default `in-memory caching`. So, use this setting if you want to use for example `Redis` for caching.
* **ConnectionString (optional)** This setting is only used in conjunction with `Redis` or `SQL Server`. This setting expects a string value. Default value for this setting is `127.0.0.1:{PORT},ssl=False`.
* **SchemaName (optional)** This setting is only used in conjunction with `SQL Server`.
* **TableName (optional)** This setting is only used in conjunction with `SQL Server`. SQL Server database name.

### AzureBlobConfig

We can use `Azure Blob Storage` to store blob data. nopCommerce already have feature integrated for that, we just need to set the following information correctly in order to use/enable this feature. Values for these setting can be obtained for Azure while you create the storage account.

* **ConnectionString** This setting expects a string value. Here you need to add your `AzureBlobStorage` connection string
* **ContainerName** Value for this setting is also of type string. In this setting we set the container name for Azure BLOB storage.
* **EndPoint** This setting also expects a string value. Here we need to set the end point for Azure BLOB storage.
* **AppendContainerName** This setting expects a boolean value. Set the value to "**true**" or "**false**" on the basis of whether the Container Name is appended to the `EndPoint` when constructing the url.
* **StoreDataProtectionKeys** This setting  expects a boolean value. Set the value to "**true** if you want to  use the Windows Azure BLOB storage for Data Protection Keys (the  UseRedisToStoreDataProtectionKeys option should be disabled)
* **DataProtectionKeysContainerName** This  setting expects a string value. Here you need to set up a Azure  container name for storing Data Prtection Keys (this container  should be separate from the container used for media and should be  Private)
* **DataProtectionKeysVaultId (optional)** This setting also expects a string value. Set the Azure key vault ID if  you need to encrypt the Data Protection Keys

### InstallationConfig

It contains settings that used to configure the behavior of nopCommerce during the nopCommerce installation.

* **DisableSampleDataDuringInstallation** This setting expects a boolean value. This setting indicating whether a store owner can install sample data during installation. If you don't want store owner to install sample data during installation then just set the value for this setting to "**true**".
* **PluginsIgnoredDuringInstallation:** This setting expects a boolean value. By default the value for this setting is "**false**". You might want to set value for this setting to "**true**" if you don't want to `Install` any `Plugin` during nopCommerce installation
* **InstallRegionalResources** This setting enables the selection of additional language resources during installation. The choice of the country determines the settings that will be applied to the store (exchange rates, taxes, units of measurement, etc. regional features).

#### PluginConfig

* **ClearPluginShadowDirectoryOnStartup** This setting expects a boolean value. You might want to Set the value to "**true**" if you want to clear `/Plugins/bin` directory during application startup.
* **CopyLockedPluginAssembilesToSubdirectoriesOnStartup** This setting expects a boolean value. You might want to set the value to "**true**" if you want to copy "locked" assemblies from `/Plugins/bin` directory to temporary subdirectories during application startup.
* **UsePluginsShadowCopy** This setting expects a boolean value. You might want to set the value to "**true**" if you want to copy plugins library to the `/Plugins/bin` directory on application startup.
* **UseUnsafeLoadAssembly** This setting expects a boolean value. You might want to set the value to "**true**" if you want to load an assembly into the load-from context, bypassing some security checks.

### CommonConfig

*CommonConfig* contains settings used to configure the behavior of nopCommerce itself. It is a json object and contains some properties settings which can be tweak to change the behavior of nopCommerce.

* **DisplayFullErrorStack** This setting expects a boolean value. Default value is **"false"**. You can set the value to "**true**" if you want to see the full error in the production environment. Which is not what we usually suggest. But if you have a good reason to show full error during production environment then you can do it from this setting. For the development environment this setting is ignored and whatever the value you set for this setting full error will be shown. We can say that this setting is always enabled for development environment.
* **UserAgentStringsPath** This setting stores the loction/path for `Browscap.xml` file, `Browscap.xml` is, as the filename might indicate, a browser capabilities database. It's essentially a list of all known browsers and bots, along with their default capabilities and limitations.
    >[!NOTE]
    > In computing, a user agent is software (a software agent) that is acting on behalf of a user, such as a web browser that "retrieves, renders and facilitates end user interaction with Web content". For more information please visit [UserAgent](https://en.wikipedia.org/wiki/User_agent).
* **CrawlerOnlyUserAgentStringsPath** This setting stores the location/path for `browscap.crawlersonly.xml`. It stores user agents only for "CrawlerOnly"
* **UseSessionStateTempDataProvider** This setting expects a boolean value. Default value for this setting is "**false**". You might want to set the value to "**true**" if you want to store `TempData` in the session state. By default the cookie-based `TempData` provider is used to store `TempData` in cookies.
* **MiniProfilerEnabled** This setting activates the *MiniProfiler* performance appraisal tool
* **ScheduleTaskRunTimeout** The length of time, in milliseconds, a running schedule task timeouts. Set null to use a default value.
* **StaticFilesCacheControl** Specify a value of 'Cache - Control' header value for static content (in seconds).

  ```powershell
  public,max-age=31536000
  ```

* **SupportPreviousNopcommerceVersions** Specify a value indicating whether we should support previous nopCommerce versions (it can slightly improve performance). In this case, old URLs (from previous nopCommerce versions) will redirect to new ones. Enable it only if you upgraded from one of the previous nopCommerce versions
* **PluginStaticFileExtensionsBlacklist** Specify the blacklist of static file extension for plugin directories.
