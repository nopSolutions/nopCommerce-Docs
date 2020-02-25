---
title: System requirements for developing
uid: en/developer/tutorials/system-requirements-for-developing
author: nop.sea
contributors: git.RomanovM
---

# System requirements for developing
## Operating System
* Windows

| OS                | Version       |
| ----------------- | ------------- |
| Windows Client    | 7 SP1+, 8.1   |
| Windows 10 Client | Version 1607+ |
| Windows Server    | 2008 R2 SP1+  |

* Linux

| OS                           | Version             |
| ---------------------------- | ------------------- |
| Red Hat Enterprise Linux     | 6                   |
| CentOS, Oracle Linux         | 7                   |
| Fedora                       | 29, 30              |
| Debian                       | 9                   |
| Ubuntu                       | 16.04, 18.04, 18.10 |
| Linux Mint                   | 17, 18              |
| OpenSUSE                     | 15+                 |
| SUSE Enterprise Linux (SLES) | 12 SP2+             |
| Alpine Linux                 | 3.7+                |

* MacOS

| OS       | Version |
| -------- | ------- |
| Mac OS X | 10.12+  |

> [!NOTE]
> 
> For more information about browser support please visit [Supported OS versions](https://github.com/dotnet/core/blob/master/release-notes/2.2/2.2-supported-os.md)

## 1. Supported Browsers
* Microsoft Internet Explorer 9 and above (IE6 and IE7 were supported in versions prior 3.60, IE8 was support in versions prior 4.10)
* Mozilla Firefox 2.0 and above
* Google Chrome 1.x
* Apple Safari 2.x
## 2. Tools Required for Development.
Since it is based on Microsoft’s Asp.Net framework we need to install a few tools before starting developing on top of nopCommerce.
### 2.1 \.Net Core 2.2 runtime & .Net Core SDK
Since nopCommerce 4.2 is based on .Net Core 2.2 framework. We need to install .Net Core 2.2 runtime and .Net Core SDK before we start development on nopCommerce.
### 2.2 Visual Studio 2017 or Above / Visual Studio Code
As we know nopCommerce is based on ‘Microsoft’s Asp.Net framework’ and Visual Studio IDE is best for developing Dot Net based Applications. Since .Net Core is platform independent so we can develop and deploy .Net based application on any platform but visual studio is not available in other platforms than window. So we can use Visual Studio Code as the alternative of Visual Studio for developing on Windows as well as in other platform.

### 2.3 Microsoft SQL Server 2012 or Above
nopCommerce uses Entity Framework as a ORM Framework. Entity Framework is an object-relational mapper (O/RM) that enables .NET developers to work with a database using .NET objects. It can map .Net objects to various numbers of Database providers. But unfortunately out of the box nopCommerce doesn’t support other database than MSSQL.

### 2.4 Internet Information Service (IIS) 7.0 or above
For hosting nopCommerce app/project we can use IIS. Which is Microsoft technology used to host Microsoft web based applications on windows. But you are not limited for hosting your nopCommerce in windows only you can host nopCommerce in Linux and MacOS too. As you may know you that IIS is not supported in other platform then windows. So, you can use other tools like Apache or Nginx to host your application on Linux server.