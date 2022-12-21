---
title: System requirements for developing
uid: en/developer/tutorials/system-requirements-for-developing
author: nop.sea
contributors: git.RomanovM, git.DmitriyKulagin, git.skoshelev
---

# System requirements for developing

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
| Red Hat Enterprise Linux     | 7+                  |
| CentOS, Oracle Linux         | 7+                  |
| Fedora                       | 33+                 |
| Debian                       | 10+                 |
| Ubuntu                       | 18.04               |
| Linux Mint                   | 18+                 |
| OpenSUSE                     | 15+                 |
| SUSE Enterprise Linux (SLES) | 12 SP2+             |
| Alpine Linux                 | 3.15+               |

* MacOS

| OS       | Version |
| -------- | ------- |
| Mac OS X | 10.15+  |

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

Since it is based on Microsoft's .NET 6 we need to install a few tools before starting developing on top of nopCommerce.

### .NET 7 runtime & .NET 7 SDK

Since nopCommerce 4.60 is based on .NET 7. We need to install [.NET 7 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-7.0.1-windows-x64-installer) and [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-7.0.101-windows-x64-installer) before we start development on nopCommerce.

### Visual Studio 2022 or Above / Visual Studio Code

As we know nopCommerce is based on 'Microsoft's .NET 7' and Visual Studio IDE is best for developing  .Net based Applications. Since .NET Core is platform independent so we can develop and deploy .Net based application on any platform but visual studio is not available in other platforms than window. So we can use Visual Studio Code as the alternative of Visual Studio for developing on Windows as well as in other platform.

### Microsoft SQL Server 2012 or Above / MySql Server 5.7 or Above / PostgreSQL 9.2 or Above

Started from 4.30 version nopCommerce uses Linq2DB as an ORM Framework. Linq2DB  is an object-relational mapper (ORM) that enables .NET developers to work with a database using .NET objects. It can map .Net objects to various numbers of Database providers. And you may choose between MS SQL Server, MySql server and PostgreSQL.

> [!NOTE]
>
> For more information about supported databases please visit [this page](https://linq2db.github.io/articles/general/databases.html)

### Internet Information Service (IIS) 7.0 or above

For hosting nopCommerce app/project we can use IIS. Which is Microsoft technology used to host Microsoft web based applications on windows. But you are not limited for hosting your nopCommerce in windows only you can host nopCommerce in Linux and MacOS too. As you may know you that IIS is not supported in other platform then windows. So, you can use other tools like Apache or Nginx to host your application on Linux server.
