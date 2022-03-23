---
title: The settings from the appsettings.json file
uid: en/developer/tutorials/appsettings-json-file
author: git.nopsg
contributors: git.nopsg, git.DmitriyKulagin, git.mariannk
---

# The settings from the appsettings.json file

## Overview

This article contains the description of the *appsettings.json* file. In this article, we will explain what settings are available in this file, what they are used for, and how to use these settings to change the functionality/behavior of the nopCommerce project.

>[!IMPORTANT]
>All settings can be overridden in environment variables.

## The appsettings.json file overview

If you have worked on the *ASP.NET Core* project previously or you are familiar with `ASP.NET Core` then you might have used the *appsettings.json* file and have some understanding of what this file is and what this file is used for.

>[!NOTE]
>You can also edit this file from the **Configuration → Settings → App settings** page.

The *appsettings.json* file is generally used to store the application configuration settings such as database connection strings, any application scope global variables, and much other information. Actually, in *ASP.NET Core*, the application configuration settings can be stored in different configurations sources such as *appsettings.json* file, **`appsettings.{EnvironmentName}.json`** file (where the `{Environment}` is the application's current hosting environments such as Development, Staging or Production), `User Secrets` (where we used to store sensitive information), etc.

## Settings available in the appsettings.json file

### ConnectionStrings

The connection to the database is configured through this section.

* **ConnectionString** This setting expects a string value.
  >[!NOTE]
  >The connection string will have a format specific to the selected database provider.
  > For example connection string for **SqlServer** look like this:
  >
  >```powershell
  >Data Source={localhost};Initial Catalog={your_data_base_name}};Integrated >Security=False;Persist Security Info=False;User ID={your_user_id}};Password={your_password}};>Trust Server Certificate=True
  >```

* **DataProvider** You can specify one of the supported data providers:
  * [**SqlServer**](https://www.microsoft.com/sql-server)
  * [**MySql**](https://www.mysql.com/)
  * [**PostgreSQL**](https://www.postgresql.org/)
* **SQLCommandTimeout** Sets the wait time (in seconds) before terminating the attempt to execute a command and generating an error. By default, timeout isn't set and a default value for the current provider is used. Set **`0`** to use an infinite timeout.

### AzureBlobConfig

We can use *Azure Blob Storage* to store blob data. nopCommerce already has a feature integrated for that, we just need to set the following information correctly to use/enable this feature. Values for these settings can be obtained for *Azure* while you create the storage account.

* **ConnectionString** This setting expects a string value. Here you need to add your `AzureBlobStorage` connection string
* **ContainerName** Value for this setting is also of type string. In this setting, we set the container name for *Azure BLOB storage*.
* **EndPoint** This setting also expects a string value. Here we need to set the endpoint for *Azure BLOB storage*.
* **AppendContainerName** This setting expects a boolean value. Set the value to **`true`** or **`false`** based on whether the Container Name is appended to the `EndPoint` when constructing the URL.
* **StoreDataProtectionKeys** This setting  expects a boolean value. Set the value to **`true`** if you want to use the *Windows Azure BLOB storage* for *Data Protection Keys*.
* **DataProtectionKeysContainerName** This  setting expects a string value. Here you need to set up an Azure  container name for storing *Data Protection Keys* (this container  should be separate from the container used for media and should be  Private)
* **DataProtectionKeysVaultId (optional)** This setting also expects a string value. Set the `Azure key vault ID` if you need to encrypt the *Data Protection Keys*

### CacheConfig

Cache configuration.

* **DefaultCacheTime** This setting determines the lifetime of the cached data. The default is **`60`** minutes.
* **ShortTermCacheTime** In some cases, it is required to cache data for a shorter period than the default. These values apply to caching addresses, generic attributes, customers, etc. The default is **`3`** minutes.
* **BundledFilesCacheTime** This setting is used when the function of combining CSS and/or js files into one is activated, to control the cache lifetime for them. The default is **`120`** minutes.

### CommonConfig

*CommonConfig* contains settings used to configure the behavior of nopCommerce itself. It is a JSON object and contains some properties settings which can be tweaked to change the behavior of nopCommerce.

* **DisplayFullErrorStack** This setting expects a boolean value. The default value is **`false`**. You can set the value to **`true`** if you want to see the full error in the production environment. Which is not what we usually suggest. But if you have a good reason to show full error during the production environment then you can do it from this setting. For the development environment this setting is ignored and whatever the value you set for this setting full error will be shown. We can say that this setting is always enabled for a development environment.
* **UserAgentStringsPath** This setting stores the location/path for the `Browscap.xml` file, `Browscap.xml` is, as the filename might indicate, a browser capabilities database. It's essentially a list of all known browsers and bots, along with their default capabilities and limitations.
  
  ```powershell
  ~/App_Data/browscap.xml
  ```

    >[!NOTE]
    > In computing, a user agent is software (a software agent) that is acting on behalf of a user, such as a web browser that "retrieves, renders and facilitates end-user interaction with Web content". For more information please visit [UserAgent](https://en.wikipedia.org/wiki/User_agent).
* **CrawlerOnlyUserAgentStringsPath** This setting stores the location/path for `browscap.crawlersonly.xml`. It stores user agents only for "CrawlerOnly".
  
  ```powershell
  ~/App_Data/browscap.crawlersonly.xml
  ```

* **UseSessionStateTempDataProvider** This setting expects a boolean value. The default value for this setting is **`false`**. You might want to set the value to **`true`** if you want to store `TempData` in the session state. By default, the cookie-based `TempData` provider is used to store `TempData` in cookies.
* **MiniProfilerEnabled** This setting activates the *MiniProfiler* performance appraisal tool.
* **ScheduleTaskRunTimeout** allows you to set up a running schedule task timeout in milliseconds. Set **`null`** to use a default value.
* **StaticFilesCacheControl** specifies the value of the 'Cache-Control' header for static content (in seconds).

  ```powershell
  public,max-age=31536000
  ```

* **SupportPreviousNopcommerceVersions** setting specifies the value indicating whether we should support previous nopCommerce versions. In this case, old URLs, from previous nopCommerce versions, will redirect to new ones. Enable it only if you upgraded from one of the previous nopCommerce versions. The default value is **`true`**.
* **PluginStaticFileExtensionsBlacklist** Specify the blacklist of the static file extensions for plugin directories.
* **ServeUnknownFileTypes** setting specifies the value indicating whether to serve files that don't have a recognized content type. The default value is **`false`**.

### DistributedCacheConfig

A distributed cache is a cache shared by multiple app servers, typically maintained as an external service to the app servers that access it. A distributed cache can improve the performance and scalability of an ASP.NET Core app, especially when the app is hosted by a cloud service or a server farm.

* **DistributedCacheType** You can choose one of the implementations:
  * **Memory** This is a framework-provided implementation of `IDistributedCache` that stores items in memory. The Distributed Memory Cache isn't a distributed cache. Cached items are stored by the app instance on the server where the app is running.
  * **SQL Server** The Distributed SQL Server Cache implementation allows the distributed cache to use a SQL Server database as its backing store. To create a SQL Server cached item table in a SQL Server instance, you can use the SQL-cache tool. The tool creates a table with the name and schema that you specify. It is recommended to use a separate database for this purpose.
  
  ```sh
  dotnet sql-cache create "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DistCache;Integrated Security=True;" dbo nopCache
  ```

  * **Redis** nopCommerce supports *Redis* out of the box. To enable the *Redis* in our application we must set the appropriate value for the following settings. For more information about [Redis](https://azure.microsoft.com/documentation/articles/cache-dotnet-how-to-use-azure-redis-cache).
* **Enabled** This setting expects a boolean value. Set the value to **`true`** if you want to enable `Distributed cache`. The system uses In-Memory caching, so this setting is used to indicate whether we should use `Distributed Cache` for caching, instead of the default `in-memory caching`. So, use this setting if you want to use for example *Redis* for caching.
* **ConnectionString (optional)** This setting is only used in conjunction with *Redis* or *SQL Server*. This setting expects a string value. The default value for this setting is

  ```powershell
  127.0.0.1:{PORT},ssl=False
  ```

* **SchemaName (optional)** This setting is only used in conjunction with `SQL Server`.
* **TableName (optional)** This setting is only used in conjunction with `SQL Server`. SQL Server database name.

### HostingConfig

Hosting contains settings used to configure the hosting behavior. It is a JSON object and contains some properties settings which can be tweaked to change the behavior for hosting.

* **UseProxy** This setting expects a boolean value. Enable this setting to apply forwarded headers to their matching fields on the current HTTP request.
* **ForwardedProtoHeaderName** This setting expects a string value. Specify a custom HTTP header name to determine the originating IP address (e.g., **`CF-Connecting-IP`**, **`X-ProxyUser-Ip`**).
* **ForwardedForHeaderName** This setting expects a string value. Specify a custom HTTP header name for identifying the protocol (HTTP or HTTPS) that a client used to connect to your proxy or load balancer.
* **KnownProxies** This setting expects a string value. Specify a list of IP addresses (comma separated) to accept forwarded headers.

### InstallationConfig

It contains settings that are used to configure the behavior of nopCommerce during the nopCommerce installation.

* **DisableSampleData** This setting expects a boolean value. This setting indicates whether a store owner can install sample data during installation. If you don't want the store owner to install sample data during installation then just set the value for this setting to **`true`**.
* **DisabledPlugins** This setting expects a string value. Specify a list of plugins (comma separated) ignored during installation.
* **InstallRegionalResources** This setting expects a boolean value. This setting enables the selection of additional language resources during installation. The choice of the country determines the settings that will be applied to the store (exchange rates, taxes, units of measurement, etc. regional features).

### PluginConfig

* **ClearPluginShadowDirectoryOnStartup** This setting expects a boolean value. Set it to **`true`** if you want to clear the `/Plugins/bin` directory during application startup.
* **CopyLockedPluginAssembilesToSubdirectoriesOnStartup** This setting expects a boolean value. You might want to set the value to **`true`** if you want to copy "locked" assemblies from `/Plugins/bin` directory to temporary subdirectories during application startup.
* **UseUnsafeLoadAssembly** This setting expects a boolean value. You might want to set the value to **`true`** if you want to load an assembly into the load-from context, bypassing some security checks.
* **UsePluginsShadowCopy** This setting expects a boolean value. You might want to set the value to **`true`** if you want to copy the plugins library to the `/Plugins/bin` directory on application startup.

### WebOptimizer

We use the [WebOptimizer](https://github.com/ligershark/WebOptimizer) tool for minification and bundling of *CSS* and *JavaScript* code, which is an *ASP.NET Core* middleware. Optimization is performed at runtime with server-side and client-side caching for high performance.

* **EnableJavaScriptBundling** This setting expects a boolean value. You can set it to **`true`** if you want JS file bundling and minification to be enabled.
* **EnableCssBundling** This setting expects a boolean value. You can set it to **`true`** if you want CSS file bundling and minification to be enabled.
* **JavaScriptBundleSuffix** This setting expects a string value. You can set a suffix for the js-file name of generated bundles (by default **`.scripts`**).
* **CssBundleSuffix** This setting expects a string value. You can set a suffix for the CSS-file name of generated bundles (by default **`.styles`**).
* **EnableCaching** This setting expects a boolean value. You can set a value indicating whether server-side caching is enabled (by default **`true`**).
* **EnableMemoryCache** This setting expects a boolean value. You can set a value indicating whether *Microsoft.Extensions.Caching.Memory.IMemoryCache*
 based caching is enabled (by default **`true`**).
* **EnableDiskCache** This setting expects a boolean value. Determines if the pipeline assets are cached to disk. This can speed up application restarts by loading pipeline assets from the disk instead of re-executing the pipeline. Can be helpful to disable while in development mode.
* **EnableTagHelperBundling** This setting expects a boolean value. You can set whether bundling is enabled (by default **`false`**).
* **CdnUrl** This setting expects a string value. You can set the CDN URL used for TagHelpers (by default **`null`**).
* **CacheDirectory** This setting expects a string value. Sets the directory where assets will be stored if **EnableDiskCache** is **`true`** (by default **`{ContentRootPath}\\wwwroot\\bundles`**).
* **AllowEmptyBundle** This setting expects a boolean value. You can set whether the empty bundle is allowed to generate instead of throwing an exception (by default **`true`**).
* **HttpsCompression** This setting expects a integer value. You can set a value Indicating if files should be compressed for HTTPS requests when the Response Compression middleware is available. The default value is **`2`**. You can choose one of the implementations:
  * **1** - Opts out of compression over HTTPS.
  * **2** - Opts into compression over HTTPS.
    >[!NOTE]
    > Enabling compression on HTTPS requests for remotely manipulable content may expose security problems.
