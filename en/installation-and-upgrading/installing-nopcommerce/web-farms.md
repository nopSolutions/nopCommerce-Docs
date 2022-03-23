---
title: Load balancing and web farms in nopCommerce
uid: en/installation-and-upgrading/installing-nopcommerce/web-farms
author: git.exileDev
contributors: git.mariannk, git.AndreiMaz
---

# Load balancing and web farms in nopCommerce

Load balancing is the distribution of a workload across many nodes. It is typically used for balancing HTTP traffic over multiple servers acting together as a web front end.

There are several ways to configure load balancing in nopCommerce:

1. Use cloud-based autoscaling appliances like Microsoft's Azure Web Apps. Please find more about it [here](xref:en/installation-and-upgrading/installing-nopcommerce/installing-on-microsoft-azure).
1. Configure load balancing with IIS web farms. This approach is described below.

We highly recommend you to read [this tutorial](https://docs.microsoft.com/en-us/iis/web-hosting/scenario-build-a-web-farm-with-iis-servers/overview-build-a-web-farm-with-iis-servers) by Microsoft before you start configuring a web farm for nopCommerce. So Microsoft suggests two ways to build a web farm with IIS servers:

1. Local Content Infrastructure
1. Shared Network Content Infrastructure

nopCommerce supports both ways and handles content replication by using Distributed File System Replication (DFSR) if you use **Local Content Infrastructure** or uses a central location to manage the content with **Shared Network Content Infrastructure**.

## nopCommerce configuration

First of all, you have to configure the initial settings of your web farm in IIS and add the first instance of your nopCommerce store there. Then, you have to configure a few settings in the nopCommerce admin area to allow nopCommerce to work with web farms:

1. Go to **Configuration → Settings → All settings (advanced)**. Find the **mediasettings.useabsoluteimagepath** setting and change its value to *false*

1. Go to **Configuration → Settings → App settings** and find the *Distributed cache configuration* tab. Select the **Use distributed cache** checkbox and choose the option you prefer:

   - *Redis*. In this case, you just need to enter the **Connection string** to your Redis server below
   - *SQL Server*. In this case, you need to prepare a new table in your database using the "sql-cache create" command first. Read more about it in Microsoft docs [here](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-5.0#distributed-sql-server-cache). Then fill in the **Connection string**, **Schema name**, and **Table name** fields

1. Since our web farm utilizes Application Request Routing (ARR) to control traffic using a proxy server, select the **Use proxy servers** checkbox
1. Click the **Save** button. The nopCommerce application will be restarted.

## Web farm configuration

### Admin area redirection rule

Since a web farm hosts multiple instances of an application, you need to choose one of the nopCommerce instances as a primary one to avoid file locking. This primary node will process requests from the admin panel.

The rule should look as follows:

```xml
<rule name="Admin Area" enabled="true" patternSyntax="ECMAScript" stopProcessing="true">
    <match url="^(admin(/.*)?)$|^(lib_npm/.+)$" />
    <conditions logicalGrouping="MatchAll" trackAllCaptures="false">
        <add input="{HTTP_HOST}" pattern="^admin\.wf\.local$" negate="true" />
    </conditions>
    <action type="Rewrite" url="http://admin.wf.local/{R:0}" />
</rule>
```

Where `admin.wf.local` is the address of your primary instance.

### Load balancing rules

After you set up the web farm, you need to configure a load balancing rule in the **Application Request Routing** section. Add the condition that prevents handling requests intended for the primary node (admin area requests in our case):

```xml
<rule name="ARR_wf-local_loadbalance" enabled="true" patternSyntax="ECMAScript" stopProcessing="true">
    <match url=".*" ignoreCase="false" />
    <conditions logicalGrouping="MatchAll" trackAllCaptures="false">
        <add input="{PATH_INFO}" pattern="^(/admin(/.*)?)$|^(/lib_npm/.+)$" negate="true" />
    </conditions>
    <action type="Rewrite" url="http://wf-local/{R:0}" />
</rule>
```

Where `wf-local` is the web farm name.

You can add the rules we described above in two ways:

1. Include them in the `applicationHost.config` file (**system.webServer/rewrite/globalRules** section)
1. Use the **URL Rewrite script** section in IIS Manager

> [!NOTE]
>
> In some cases, ARR has issues handling URLs of javascript files containing more than one “.” symbol. For example, it can affect minified js-files ending with `.min.js`. To avoid errors when processing such files, we recommend directly routing these requests to one of the nopCommerce instances. As you can see in the examples above, we do this for the whole `lib_npm` directory by routing to the primary instance.

When the configuration is finished, add new instances to your web farm.

### File replication

When you start the configuration of file replication, please make sure that the following folders of the primary instance are set up to support replication to all other nodes (instances):

- /App_Data
- /Plugins
- /Themes
- /wwwroot

> [!NOTE]
>
> All actions that assume restarting the nopCommerce application (for example, plugin installation, updating of the app settings), require manual restarting of all application pools related to the web farm.
