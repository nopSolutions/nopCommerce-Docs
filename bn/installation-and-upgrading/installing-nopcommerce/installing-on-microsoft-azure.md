---
title: Installing on Microsoft Azure
uid: en/installation-and-upgrading/installing-nopcommerce/installing-on-microsoft-azure
author: git.AndreiMaz
contributors: git.skoshelev, git.DmitriyKulagin, git.exileDev, git.ivkadp, git.mariannk
---

# Installing on Microsoft Azure

There are three ways to deploy nopCommerce on Microsoft Azure:

1. **FTP**. Use this method if you already have a package ready to deploy (no source code). You can publish to a local file system then upload the published files through FTP.
  How to get FTP credentials for Azure? You should go [Azure.com](https://azure.microsoft.com/) → My Account → Management portal → Choose your website → go to Dashboard → quick glance. From here you can find the FTP credentials or you can *Reset your deployment credentials* or *Download the publish profile*.
  For the new azure portal go [portal.azure.com](http://portal.azure.com/) → browse websites → navigate to your website → Properties. From here you can find the FTP credentials or you can *Reset your deployment credentials* or *Download the publish profile*.

1. **Visual Studio** - web deploy. You can also deploy directly to azure from visual studio. Download or get the deployment credentials from azure using the above way and setup web deploy profile in visual studio.

1. **Web platform installer**. nopCommerce is available in Azure Web Sites application gallery. So go to Azure portal, click "*start, new site, from gallery*".  Select nopCommerce from the list of available applications. After you enter your database connection information and click "*OK*", nopCommerce will be ready to launch.

    >[!TIP]
    > If you get error "HTTP Error 500.32 - ANCM Failed to Load dll", it might be that the platform has to be changed to 64bit (through Azure: "App Settings" - "Settings" - "Configuration" - tab "General settings" - "Platform settings" - "Platform").

Once the site is deployed you have to install nopCommerce. Please read more about it [here](xref:en/installation-and-upgrading/installing-nopcommerce/index).

Azure has support for multiple instances since version 3.70. It's great for any application scalability. Now you should not worry whether your site can handle a large number of visitors. So what exactly has been done to support multiple instances in Azure and web farms?

* **BLOB storage account support in Microsoft Azure**. Please learn more about storage accounts in Azure [here](https://azure.microsoft.com/documentation/articles/storage-introduction/). *How to configure:*
  * Once your BLOB storage is set up in Azure, open your `appsettings.json` (or `web.config` in previous versions) file, find **AzureBlobStorage** element and specify your BLOB storage connection string, container, endpoint.
* **Distributed caching and session management support**. [Redis](http://redis.io/) has been chosen as a caching server (already available in Azure, Amazon, other cloud hosting companies). *How to configure:*
  * First, you have to install and setup Redis. Please find more about how to use Redis in Azure [here](https://azure.microsoft.com/documentation/articles/cache-dotnet-how-to-use-azure-redis-cache/).
  * Once it's done you have to configure it in nopCommerce. In order to enable caching in Redis open `appsettings.json` (or `web.config` in previous versions) file. Find **RedisEnabled** and **UseRedisForCaching** (**RedisCaching** in previous versions) config elements. Set them to *true* and then specify **RedisConnectionString** pointing your Redis server (configured in the first step).
  * For versions 3.90 (and below) you also have to enable Redis as our distributed session management. Please open the `web.config` file. Find and uncomment **sessionState** element. Specify its attributes (*host*, *accessKey*, etc) pointing to your Redis server.
* Recommended settings of the `appsettings.json` file to improve stability:
  * **UsePluginsShadowCopy** - set it to *false* to prevent the problem with IIS pool recycle and horizontal scaling.
* Ensure that the nopCommerce schedule tasks are run on one farm node at a time. To configure this (for versions 3.90 and below):
  * In order to enable this functionality open the `web.config` file, find **WebFarms** element, and set its **MultipleInstancesEnabled** attribute to *true*. If you use Microsoft Azure Websites (not cloud services), then also set **RunOnAzureWebsites** attribute to *true*.

## Installation process

The further installation process for nopCommerce it does not differ from the installation process on Windows, you can see the instruction by [this link](xref:en/installation-and-upgrading/installing-nopcommerce/installing-on-windows#install-nopcommerce)

## Web farms

You can also configure load balancing with IIS web farms. Please read more about it [here](xref:en/installation-and-upgrading/installing-nopcommerce/web-farms).
