---
title: System Requirements for Development
uid: en/developer/tutorials/system-requirements-for-developing
author: nop.sea
contributors: git.RomanovM, git.DmitriyKulagin, git.skoshelev
---

# System Requirements for Development

## Operating System

* Windows

| OS                | Version       |
| ----------------- | ------------- |
| Windows 11        | Version 22000+|
| Windows 10 Client | Version 1607+ |
| Windows Server    | 2012 R2+      |
| Nano Server       | Version 1809+ |

* Linux

| OS                           | Version             |
| ---------------------------- | ------------------- |
| Red Hat Enterprise Linux     | 8+                  |
| Fedora                       | 38+                 |
| Debian                       | 11+                 |
| Ubuntu                       | 20.04+               |
| OpenSUSE                     | 15+                 |
| SUSE Enterprise Linux (SLES) | 12 SP5+             |
| Alpine Linux                 | 3.16+               |

Other distributions are supported at best effort, per [.NET Support and Compatibility for Linux Distributions](https://github.com/dotnet/core/blob/main/linux-support.md).

* MacOS

| OS       | Version |
| -------- | ------- |
| Mac OS X | 12.0+  |

> [!NOTE]
>
> The **Windows Client 7 SP1, 8.1** operating systems are no longer supported, starting with .NET 7.0.
>
> For more information about supported OS versions please visit [this page](https://github.com/dotnet/core/blob/main/release-notes/7.0/supported-os.md)

## 1. Supported Browsers

* Microsoft Internet Explorer 9 and above (IE6 and IE7 were supported in versions prior 3.60, IE8 was support in versions prior 4.10)
* Mozilla Firefox 2.0 and above
* Google Chrome 1.x
* Apple Safari 2.x

## 2. Tools Required for Development

Since it is based on Microsoft's .NET 8 we need to install a few tools before starting development on top of nopCommerce.

### .NET 8 runtime & .NET 8 SDK

Since nopCommerce 4.70 is based on .NET 8. We need to install [.NET 8 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-8.0.4-windows-x64-installer) and [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.204-windows-x64-installer) before we start development on nopCommerce.

### Visual Studio 2022 or Above / Visual Studio Code

As we know, nopCommerce is based on "Microsoft .NET 8" and *Visual Studio IDE* is best for developing .Net based applications. Since .NET Core is platform independent, we can develop and deploy a .Net based application on any platform. But we can use *Visual Studio Code* as an alternative to *Visual Studio* for developing on Windows as well as other platforms.

> [!NOTE]
>
> If you are using *Visual Studio Code*, you will need to install the **C# for Visual Studio Code (powered by OmniSharp)** extension.

### Microsoft SQL Server 2012 or Above / MySql Server 5.7 or Above / PostgreSQL 9.2 or Above

Starting from the 4.30 version, nopCommerce uses Linq2DB as an ORM Framework. Linq2DB is an object-relational mapper (ORM) that enables .NET developers to work with a database using .NET objects. It can map .Net objects to various numbers of Database providers. You may choose between MS SQL Server, MySql server, and PostgreSQL.

> [!NOTE]
>
> For more information about supported databases please visit [this page](https://linq2db.github.io/articles/general/databases.html)

### Internet Information Service (IIS) 7.0 or above

One option for hosting a nopCommerce app/project is IIS, a Microsoft technology that hosts web-based applications on Windows. However, nopCommerce can also run on Linux and MacOS, which do not support IIS. In that case, you can use alternative tools like Apache or Nginx to host your application on a Linux server.
