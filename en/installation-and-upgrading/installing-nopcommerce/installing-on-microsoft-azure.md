---
title: Installing on Microsoft Azure
uid: en/installation-and-upgrading/installing-nopcommerce/installing-on-microsoft-azure
author: git.AndreiMaz
contributors: git.skoshelev, git.DmitriyKulagin, git.exileDev, git.ivkadp, git.mariannk
---

# Installing on Microsoft Azure

There are three ways to deploy nopCommerce on Microsoft Azure:

1. **FTP**. Use this method if you already have a package ready to deploy (no source code). You can publish to a local file system, then upload the published files through FTP.
  How to get FTP credentials for Azure? You should go to **[Azure.com](https://azure.microsoft.com/) → My Account → Management portal → Choose your website → go to Dashboard → quick glance**. Here, you can find the FTP credentials or *Reset your deployment credentials* or *Download the publish profile*.
  For the new Azure portal, go to [portal.azure.com](http://portal.azure.com/) → browse websites → navigate to your website → Properties. Here, you can find the FTP credentials or *Reset your deployment credentials* or *Download the publish profile*.

1. **Visual Studio** - web deploy. You can also deploy directly to Azure from *Visual Studio*. Download or get the deployment credentials from Azure using the method above and setup a web deploy profile in *Visual Studio*.

1. **Web platform installer**. nopCommerce is available in the Azure Web Sites application gallery. So go to Azure portal, click "*start, new site, from gallery*".  Select nopCommerce from the list of available applications. After you enter your database connection information and click *`OK`*, nopCommerce will be ready to launch.

    >[!TIP]
    >
    > If you get the error "HTTP Error 500.32 - ANCM Failed to Load dll," it might be that the platform has to be changed to 64bit (through Azure: **App Settings - Settings - Configuration - General settings - Platform settings - Platform**).

Once the site is deployed, you have to install nopCommerce. Please read more about it [here](xref:en/installation-and-upgrading/installing-nopcommerce/index).

Azure has support for multiple instances since version 3.70. It's great for any application scalability. Now you should not worry about whether your site can handle many visitors. So what exactly has been done to support multiple instances in Azure and web farms?

* **BLOB storage account support in Microsoft Azure**. Please learn more about storage accounts in Azure [here](https://azure.microsoft.com/documentation/articles/storage-introduction/). *How to configure:*
  * Once your BLOB storage is set up in Azure, open your `appsettings.json` (or `web.config` in previous versions) file, find the **AzureBlobStorage** element and specify your BLOB storage connection string, container, endpoint.
* **Distributed caching and session management support**. Supported options are SQL Server and Redis. To configure SQL Server, see [DistributedCacheConfig](https://docs.nopcommerce.com/en/developer/tutorials/appsettings-json-file.html#distributedcacheconfig) section for more details. Following description assumes that [Redis](http://redis.io/) has been chosen as a caching server (already available in Azure, Amazon, other cloud hosting companies). *How to configure:*
  * First, you have to install and setup Redis. Please find more about how to use Redis in Azure [here](https://azure.microsoft.com/documentation/articles/cache-dotnet-how-to-use-azure-redis-cache/).
  * Once it's done you have to configure it in nopCommerce. In order to enable caching in Redis open `appsettings.json` file. Find the **DistributedCacheConfig** config section. There, set **DistributedCacheType** to Redis and **Enabled** to *`true`* and then specify **ConnectionString** pointing to your Redis server (configured in the first step).
  * For version 3.90 (and below), you also have to enable Redis as our distributed session management. Please open the `web.config` file. Find and uncomment the **sessionState** element. Specify its attributes (such as *host*, *accessKey*, etc.) pointing to your Redis server.
* Recommended settings of the `appsettings.json` file to improve stability:
  * **UsePluginsShadowCopy** - set it to *`false`* to prevent the problem with IIS pool recycle and horizontal scaling.
* Ensure that the nopCommerce schedule tasks are run on one instance at a time. To configure this :
  * For version 3.90 (and below), open the `web.config` file, find the **WebFarms** element, and set its **MultipleInstancesEnabled** attribute to *`true`*. If you use Microsoft Azure Websites (not cloud services), then set the **RunOnAzureWebsites** attribute to *`true`* as well.
  * For newer versions no configuration change is required because the task runner uses the distributed cache to ensure that tasks will run on one instance at a time.

## Installation process

The further installation process for nopCommerce is the same as the installation process on Windows; you can see the instruction following [this link](xref:en/installation-and-upgrading/installing-nopcommerce/installing-on-windows#install-nopcommerce).

## Web farms

You can also configure load balancing with IIS web farms. Please read more about it [here](xref:en/installation-and-upgrading/installing-nopcommerce/web-farms).
