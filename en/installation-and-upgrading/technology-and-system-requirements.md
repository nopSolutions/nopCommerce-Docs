---
title: Technology & system requirements
uid: en/installation-and-upgrading/technology-and-system-requirements
author: git.AndreiMaz
contributors: git.IvanIvanIvanov, git.rajupaladiya, git.exileDev, git.DmitriyKulagin, git.skoshelev
---

# Technology & system requirements

In order to run nopCommerce, you will need the following installed on your server/computer:

## Supported operation systems

* Windows
  * Windows 10 Client (1607 or above)
  * Windows 11 (22000 or above)
  * Windows Server 2012 R2 or above

* Linux
  * Red Hat 7 / CentOS 7 or above
  * Fedora 33 or above or
  * Debian 10 or above / Ubuntu 18.04 or above / Linux Mint 18 or above
  * OpenSUSE 15 or above / SUSE Enterprise 12 SP2 or above
  * Alpine Linux 3.15 or above

* macOS
  * macOS X 10.15 or above

## Supported web servers

* Internet Information Service (IIS) 7.0 or above
* For nopCommerce 4.60: Install .NET 7 runtime ([download](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-7.0.1-windows-x64-installer)).
* For nopCommerce 4.50: Install .NET 6 runtime ([download](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-6.0.1-windows-hosting-bundle-installer)).
* For nopCommerce 4.40: Install .NET 5 runtime ([download](https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-aspnetcore-5.0.3-windows-hosting-bundle-installer)).
* For nopCommerce 4.30: Install .NET Core 3.1 runtime ([download](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-aspnetcore-3.1.3-windows-hosting-bundle-installer)).
* For nopCommerce 4.20: Install .NET Core 2.2 runtime ([download](https://dotnet.microsoft.com/download)).
* For nopCommerce 4.10: Install .NET Core 2.1 runtime ([download](https://dotnet.microsoft.com/download)).
* For nopCommerce 4.00: Install .NET Core Window Server hosting runtime ([download](https://dotnet.microsoft.com/download))
* For nopCommerce 3.90 or below: ASP.NET 4.5 (MVC 5.0) and Microsoft .NET Framework 4.5.1 or above

## Supported databases

* MS SQL Server 2012 or above
* MySql Server 5.7 or above (starting from nopCommerce 4.30)
* PostgreSQL 9.5 or above (starting from nopCommerce 4.40)

## Supported browsers

* Microsoft Edge. Microsoft Internet Explorer 9 and above (IE6 and IE7 were supported in versions prior to 3.60, IE8 was supported in versions prior to 4.10)
* Mozilla Firefox 2.0 and above
* Google Chrome 1.x
* Apple Safari 2.x

**For nopCommerce 4.60 or above: MS Visual Studio 2022 (version 17.4 or above). And don't forget to install .NET 7 SDK ([download](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-7.0.101-windows-x64-installer)). Required for developers who want to edit the source code.**

**For nopCommerce 4.506: MS Visual Studio 2022 (version 17.0 or above). And don't forget to install .NET 6 SDK ([download](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.101-windows-x64-installer)). Required for developers who want to edit the source code.**

**For nopCommerce 4.40 or above: MS Visual Studio 2019 (version 16.9 or above). And don't forget to install .NET 5 SDK ([download](https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.200-windows-x64-installer)). Required for developers who want to edit the source code.**

**For nopCommerce 4.30: MS Visual Studio 2019 (version 16.3 or above). And don't forget to install .NET Core SDK ([download](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.201-windows-x64-installer)). Required for developers who want to edit the source code.**

**For nopCommerce 4.20 or above: MS Visual Studio 2017 (version 15.9 or above). And don't forget to install .NET Core SDK ([download](https://dotnet.microsoft.com/download)). Required for developers who want to edit the source code.**

**For nopCommerce 4.10: MS Visual Studio 2017 (version 15.7 or above). And don't forget to install .NET Core SDK ([download](https://dotnet.microsoft.com/download)). Required for developers who want to edit the source code.**

**For nopCommerce 4.00 or below: MS Visual Studio 2017 (version 15.3 or above). And don't forget to install .NET Core SDK ([download](https://dotnet.microsoft.com/download)). Required for developers who want to edit the source code.**

> [!NOTE]
> If you're installing nopCommerce on Windows and going to use the multi-store feature with SSL, then Windows Server 2012 with IIS 8 is required because it supports SNI (Server Name Indication).
