---
title: System Requirements for Development
uid: en/developer/tutorials/system-requirements-for-developing
author: nop.sea
contributors: git.RomanovM, git.DmitriyKulagin, git.skoshelev
---

# System Requirements for Development

## Operating System

### Windows

| OS                | Version       |
| ----------------- | ------------- |
| Windows 11        | 24H2 (IoT), 24H2 (E), 24H2, 23H2, 22H2 (E) |
| Windows 10 Client | 22H2, 21H2 (E), 21H2 (IoT), 1809 (E), 1607 (E) |
| Windows Server    | 23H2, 2022, 2019, 2016, 2012-R2, 2012 |
| Nano Server       | 2022, 2019 |

### Linux

| OS                           | Version             |
| ---------------------------- | ------------------- |
| Red Hat Enterprise Linux     | 9, 8                |
| Fedora                       | 40                  |
| Debian                       | 12                  |
| Ubuntu                       | 24.10, 24.04, 22.04 |
| openSUSE Leap                | 15.6, 15.5          |
| SUSE Enterprise Linux        | 15.6, 15.5          |
| Alpine Linux                 | 3.20, 3.19          |

Other distributions are supported at best effort, per [.NET Support and Compatibility for Linux Distributions](https://github.com/dotnet/core/blob/main/linux.md).

### Apple

| OS       | Version    |
| -------- | ---------- |
| macOS    | 15, 14, 13 |

> [!NOTE]
>
> Find a complete list of supported operation systems [here](https://github.com/dotnet/core/blob/main/release-notes/9.0/supported-os.md).
>
> [!IMPORTANT]
>
> The **Windows Client 7 SP1, 8.1** operating systems are no longer supported, starting with .NET 7.0.
>
> For more information about supported OS versions please visit [this page](https://github.com/dotnet/core/blob/main/release-notes/7.0/supported-os.md).

## Supported Browsers

* Microsoft Edge. Microsoft Internet Explorer 9 and above (IE6 and IE7 were supported in versions prior 3.60, IE8 was support in versions prior 4.10)
* Mozilla Firefox 2.0 and above
* Google Chrome 1.x and above
* Apple Safari 12.x and above

## Tools Required for Development

Since it is based on Microsoft's .NET 9 we need to install a few tools before starting development on top of nopCommerce.

### .NET 9 runtime & .NET 9 SDK

Since nopCommerce 4.90 is based on .NET 9. We need to install [.NET 9 runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-9.0.0-windows-x64-installer) and [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-9.0.100-windows-x64-installer) before we start development on nopCommerce.

### Visual Studio 2022 or Above / Visual Studio Code

As we know, nopCommerce is based on "Microsoft .NET 9" and *Visual Studio IDE* is best for developing .Net based applications. Since .NET Core is platform independent, we can develop and deploy a .Net based application on any platform. But we can use *Visual Studio Code* as an alternative to *Visual Studio* for developing on Windows as well as other platforms.

> [!NOTE]
>
> If you are using *Visual Studio Code*, you will need to install the **C# for Visual Studio Code (powered by OmniSharp)** extension.

### Microsoft SQL Server 2012 or Above / MySql Server 5.7 or Above / PostgreSQL 9.2 or Above

Starting from the 4.30 version, nopCommerce uses *Linq2DB* as an ORM Framework. *Linq2DB* is an object-relational mapper (ORM) that enables .NET developers to work with a database using .NET objects. It can map .Net objects to various numbers of Database providers. You may choose between MS SQL Server, MySql Server, and PostgreSQL.

> [!NOTE]
>
> For more information about supported databases please visit [this page](https://linq2db.github.io/articles/general/databases.html)

### Internet Information Service (IIS) 7.0 or above

One option for hosting a nopCommerce app/project is IIS, a Microsoft technology that hosts web-based applications on Windows. However, nopCommerce can also run on Linux and MacOS, which do not support IIS. In that case, you can use alternative tools like Apache or Nginx to host your application on a Linux server.
