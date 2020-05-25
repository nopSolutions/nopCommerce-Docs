---
title: System requirements for developing
uid: en/developer/tutorials/system-requirements-for-developing
author: nop.sea
contributors: git.RomanovM, git.DmitriyKulagin
---

# System requirements for developing

## Operating System

* Windows

| OS                | Version       |
| ----------------- | ------------- |
| Windows Client    | 7 SP1+, 8.1   |
| Windows 10 Client | Version 1607+ |
| Windows Server    | 2012 R2+      |

* Linux

| OS                           | Version             |
| ---------------------------- | ------------------- |
| Red Hat Enterprise Linux     | 6+                  |
| CentOS, Oracle Linux         | 7+                  |
| Fedora                       | 30+                 |
| Debian                       | 9+                  |
| Ubuntu                       | 18.04, 19.10, 20.04 |
| Linux Mint                   | 18+                 |
| OpenSUSE                     | 15+                 |
| SUSE Enterprise Linux (SLES) | 12 SP2+             |
| Alpine Linux                 | 3.10+               |

* MacOS

| OS       | Version |
| -------- | ------- |
| Mac OS X | 10.13+  |

> [!NOTE]
>
> For more information about browser support please visit [Supported OS versions](https://github.com/dotnet/core/blob/master/release-notes/3.1/3.1-supported-os.md)

## 1. Supported Browsers

* Microsoft Internet Explorer 9 and above (IE6 and IE7 were supported in versions prior 3.60, IE8 was support in versions prior 4.10)
* Mozilla Firefox 2.0 and above
* Google Chrome 1.x
* Apple Safari 2.x

## 2. Tools Required for Development

Since it is based on Microsoft's ASP.NET framework we need to install a few tools before starting developing on top of nopCommerce.

### \.NET Core 3.1 runtime & .NET Core SDK

Since nopCommerce 4.30 is based on .NET Core 3.1 framework. We need to install [.NET Core 3.1 runtime](https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-aspnetcore-3.1.3-windows-hosting-bundle-installer) and [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.201-windows-x64-installer) before we start development on nopCommerce.

### Visual Studio 2019 or Above / Visual Studio Code

As we know nopCommerce is based on 'Microsoft's ASP.NET framework' and Visual Studio IDE is best for developing Dot Net based Applications. Since .NET Core is platform independent so we can develop and deploy .Net based application on any platform but visual studio is not available in other platforms than window. So we can use Visual Studio Code as the alternative of Visual Studio for developing on Windows as well as in other platform.

### Microsoft SQL Server 2012 or Above / MySql Server 5.7 or Above

Started from 4.30 version nopCommerce uses Linq2DB as an ORM Framework. Linq2DB  is an object-relational mapper (ORM) that enables .NET developers to work with a database using .NET objects. It can map .Net objects to various numbers of Database providers. And you may choose between MS SQL Server and MySql server.

### Internet Information Service (IIS) 7.0 or above

For hosting nopCommerce app/project we can use IIS. Which is Microsoft technology used to host Microsoft web based applications on windows. But you are not limited for hosting your nopCommerce in windows only you can host nopCommerce in Linux and MacOS too. As you may know you that IIS is not supported in other platform then windows. So, you can use other tools like Apache or Nginx to host your application on Linux server.
