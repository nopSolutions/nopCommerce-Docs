---
title: Installing on Windows Azure
uid: vi/user-guide/installing/azure
---

# Installing on Windows Azure

## There are three ways to deploy nopCommerce on Windows Azure

1. **FTP.** Use this method if you already have a package ready to deploy (no source code). You can publish to a local File System then upload the published files through ftp. How to get FTP credentials for azure? you go [Azure.com](https://azure.microsoft.com/) → My Account → Management portal → Choose your website → go Dashboard → quick glance. From here you can find the FTP credentials or you can 'Reset your deployment credentials' or 'Download the publish profile'. For new azure portal go [portal.azure.com](http://portal.azure.com/) → browse websites → navigate to your website → Properties. From here you can find the FTP credentials or you can 'Reset your deployment credentials' or 'Download the publish profile'.

2. **Visual Studio** - web deploy. You can also deploy directly to azure from visual studio. Download or get the deployment credentials from azure using the above way and setup web deploy profile in visual studio.

3. **Web platform installer.** nopCommerce is available in Azure Web Sites application gallery. So go to Azure portal, click "start, new site, from gallery". Select nopCommerce from the list of available applications. After you enter your database connection information and click OK, nopCommerce will be ready to launch.

Once the site is deployed you have to install nopCommerce. Please read more about it [here](xref:en/user-guide/installing/installation-guide/index) ("Installation process").

Azure has support for multiple instances since version 3.70. It's great for any application scalability. Now you should not worry whether your site can handle a large number of visitors. So what exactly has been done to support multiple instances in Azure and web farms?

* **BLOB storage account support in Windows Azure.** Please learn more about storage accounts in Azure [here](https://azure.microsoft.com/documentation/articles/storage-introduction/). *How to configure:* 
  * Once your BLOB storage is set up in Azure, open your `appsettings.json` (or web.config in previous versions) file, find "AzureBlobStorage" element and specify your BLOB storage connection string, container, endpoint.
* **Distributed caching and session management support.** [Redis](http://redis.io/) has been chosen as a caching server (already available in Azure, Amazon, other cloud hosting companies). *How to configure:* 
  * So first, you have to install and setup Redis. Please find more about how to use Redis in Azure [here](https://azure.microsoft.com/documentation/articles/cache-dotnet-how-to-use-azure-redis-cache/).
  * Once it's done we have to configure it in nopCommerce. In order to enable caching in Redis open `appsettings.json` (or web.config in previous versions) file. Find "RedisCaching" config element. Set its "Enabled" attribute to "true" and then specify "ConnectionString" pointing your Redis server (configured in the first step).
  * For versions 3.90 (and below) we also have to enable Redis as our distributed session management. Please open web.config file. Find and uncomment "sessionState" element. Specify its attributes (host, accessKey, etc) pointing to your Redis server. Please find more about distributed session support in Azure here.
* **Ensure that our schedule tasks are run on one farm node at a time.** *How to configure (for versions 3.90 and below):* 
  * In order to enable this functionality open web.config file, find "WebFarms" element, and set its "MultipleInstancesEnabled" attribute to "True". If you use Windows Azure Websites (not cloud services), then also set "RunOnAzureWebsites" attribute to "True".