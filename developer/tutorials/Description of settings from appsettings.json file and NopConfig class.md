# Description settings from appsettings.json file and NopConfig and HostingConfig class

## Overview

This article contains description of `appsettings.json` file and `NopConfig class`. In this article we will be explaining what are the different settings available in these files what each settings does and when to use this settings to change the functionality/behavior of nopCommerce project.

## appsettings.json file overview

If you have worked on `ASP.NET core` project previously or you are familiar wit `ASP.NET core` then you might have used `appsettings.json` file and have some understanding of what this file is and what this file is used for.

`appsettings.json` file is generally used to store the application configuration settings such as database connection strings, any application scope global variables and many other information. Actually in `ASP.NET Core`, the application configuration settings can be stored in different configurations sources such as `appsettings.json` file, `appsettings.{EnvironmentName}.json` file (where the {Environment} is the application's current hosting environments such as Development, Staging or Production), `User Secrets` (where we used to store sensitive information) etc.

## Settings available in appsettings.json file

### Hosting

Hosting contains settings used to configure the hosting behavior. It is a json object and contains some properties settings which can be tweak to change the behavior for hosting.

#### UseHttpClusterHttps

This setting expects a boolean value. Default value is "false", you can set the value to "true" if your hosting uses a load balancer. It'll be used to determine whether the current request is HTTPS.

#### UseHttpXForwardedProto

This setting expects a boolean value. Default value is "false", You might want to set the value to "true" if your hosting uses load balancer and the if you are enabling the above `UseHttpClusterHttps` setting. This setting is used to add `X-Forwarded-Proto` in the `HTTP` header. `X-Forwarded-Proto` is an HTTP Header and is part of the HTTP standard. It is set on each HTTP request by a proxy or load balancer and can be used by a server application to determine what protocol the client used to connect.

#### ForwardedHttpHeader

This setting expects a string value. You can use this setting if your hosting doesn't use "X-FORWARDED-FOR" header to determine IP address. In some cases server use other HTTP header. You can specify a custom HTTP header here. For example, CF-Connecting-IP, X-FORWARDED-PROTO, etc.

### Nop

Nop contains settings used to configure the behavior of nopCommerce itself. It is a json object and contains some properties settings which can be tweak to change the behavior of nopCommerce.

#### DisplayFullErrorStack

This setting expects a boolean value. Default value is "false". You can set the value to "true" if you want to see the full error in the production environment. Which is not what we usually suggest. But if you have a good reason to show full error during production environment then you can do it from this setting. For the development environment this setting is ignored and whatever the value you set for this setting full error will be shown. We can say that this setting is always enabled for development environment.

#### Azure Blob Storage

We can use `Azure Blob Storage` to store blob data. nopCommerce already have feature integrated for that, we just need to set the following information correctly in order to use/enable this feature. Values for these setting can be obtained for Azure while you create the storage account.

* **AzureBlobStorageConnectionString:** This setting expects a string value. Here you need to add your `AzureBlobStorage` connection string
* **AzureBlobStorageContainerName:** Value for this setting is also of type string. In this setting we set the container name for Azure BLOB storage.
* **AzureBlobStorageEndPoint:** This setting also expects a string value. Here we need to set the end point for Azure BLOB storage.
* **AzureBlobStorageAppendContainerName:** This setting expects a boolean value. Set the value to "true" or "false" on the basis of whether the Container Name is appended to the AzureBlobStorageEndPoint when constructing the url.

#### Redis

nopCommerce supports `Redis` out of the box. In order to enable the `Redis` in our application we must set appropriate value for the following settings. for more information about [Redis](xref:https://azure.microsoft.com/en-us/documentation/articles/cache-dotnet-how-to-use-azure-redis-cache)

* **RedisEnabled:** This setting expects a boolean value. Set the value to true if you want to enable `Redis`.
* **RedisDatabaseId:** Redis database id, If you need to use a specific redis database, just set its number here. Set empty if should use the different database for each data type (used by default). set -1 if you want to use the default database.
* **RedisConnectionString:** This setting expects a string value. Default value for this setting is `127.0.0.1:{PORT},ssl=False`. Here we need to set the "Connection String" for `Redis`. It is used when Redis is enabled.
* **UseRedisToStoreDataProtectionKeys:** This setting expects a boolean value. This setting indicating whether the data protection system should be configured to persist keys in the Redis database.
* **UseRedisForCaching:** This setting expects a boolean value. The system uses a In-Memory caching, so this setting is used to indicating whether we should use `Redis` server for caching, instead of default `in-memory caching`. So, use this setting if you want to use `Redis` for caching.
* **UseRedisToStorePluginsInfo:** This setting expects a boolean value. nopCommerce uses plugin.json file to store plugin info. So, This setting indicating whether we should use Redis server to store the plugins info (instead of default plugin.json file).

#### User Agent

* **UserAgentStringsPath:**
* **CrawlerOnlyUserAgentStringsPath:**

#### Installation

It contains settings that used to configure the behavior of nopCommerce during the nopCommerce installation.

* **DisableSampleDataDuringInstallation:** This setting expects a boolean value. This setting indicating whether a store owner can install sample data during installation. If you don't want store owner to install sample data during installation then just set the value for this setting to "true".
* **UseFastInstallationService:** This setting expects a boolean value. By default the value for this setting is "false". this setting indicating whether to use the fast installation or not.
* **PluginsIgnoredDuringInstallation:** This setting expects a boolean value. By default the value for this setting is "false". You might want to set value for this setting to "true" if you don't want to `Install` any `Plugin` during nopCommerce installation

#### Plugins

* **ClearPluginShadowDirectoryOnStartup:** This setting expects a boolean value. You might want to Set the value to "true" if you want to clear `/Plugins/bin` directory during application startup.
* **CopyLockedPluginAssembilesToSubdirectoriesOnStartup:** This setting expects a boolean value. You might want to set the value to "true" if you want to copy "locked" assemblies from `/Plugins/bin` directory to temporary subdirectories during application startup.
* **UsePluginsShadowCopy:** This setting expects a boolean value. You might want to set the value to "true" if you want to copy plugins library to the `/Plugins/bin` directory on application startup.
* **UseUnsafeLoadAssembly:** This setting expects a boolean value. You might want to set the value to "true" if you want to load an assembly into the load-from context, bypassing some security checks.

#### UseRowNumberForPaging

This setting expects a boolean value. Default value for this setting is "false". You may want to set the value to "true" if you want to configure your nopCommerce application for backwards compatibility with `SQL Server 2008` and `SQL Server 2008R2`.

#### UseSessionStateTempDataProvider

This setting expects a boolean value. Default value for this setting is "false". You might want to set the value to "true" if you want to store TempData in the session state. By default the cookie-based TempData provider is used to store TempData in cookies.

### NopConfig and HostingConfig Class

As we can see `appsettings.json` file has lots of settings defined in it, which we can use in our application so that our application behaves according to the setting defined in this file. But we don't want to read these settings from `appsettings.json` file each time we need to access these settings. So, this is where `NopConfig` and `HostingConfig` class comes into play. We can find these class in `Libraries/Nop.Core/Configuration`. If you open these class then you can see that the properties of these classes are same as properties in `appsettings.json` file.
if you look at the properties from `HostingConfig` class and `Host` setting in `appsetting.json` and `NopConfig` class and `Nop` setting in `appsettings.json` file, they have same properties. This is because we load settings from `appsettings.json` to these files and inject to controllers using dependency injection mechanism.