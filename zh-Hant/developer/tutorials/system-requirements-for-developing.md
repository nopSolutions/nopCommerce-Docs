---
標題: 開發系統需求
uid: zh-Hant/developer/tutorials/system-requirements-for-developing
作者: nop.sea
貢獻者: git.RomanovM, git.DmitriyKulagin, git.skoshelev
---

# 開發系統需求

## 作業系統

### Windows

| 作業系統 | 版本 |
| ----------------- | ------------- |
| Windows 11        | 25H2, 24H2 (IoT), 24H2 (E), 24H2, 23H2|
| Windows 10 Client | 21H2 (E), 21H2 (IoT), 1809 (E), 1607 (E) |
| Windows Server    | 2025, 23H2, 2022, 2019, 2016 |
| Nano Server       | 2025, 2022, 2019 |

### Linux

| 作業系統 | 版本 |
| ---------------------------- | ------------------- |
| Red Hat Enterprise Linux     | 10, 9, 8            |
| Fedora                       | 42, 41              |
| Debian                       | 13, 12              |
| Ubuntu                       | 25.10, 24.04, 22.04 |
| openSUSE Leap                | 16.0, 15.6          |
| SUSE Enterprise Linux        | 16.0, 15.6          |
| Alpine Linux                 | 3.22, 3.21, 3.20    |

其他發行版本採用「盡力支援」原則，詳情請參閱 [.NET 對 Linux 發行版的支援與相容性](https://github.com/dotnet/core/blob/main/linux.md)。

### Apple

| 作業系統 | 版本 |
| -------- | ---------- |
| macOS    | 26, 15, 14 |

> [!NOTE]
>
> 查詢完整的作業系統支援列表，請點擊 [here](https://github.com/dotnet/core/blob/main/release-notes/9.0/supported-os.md)。
>
> [!IMPORTANT]
>
> 從 .NET 7.0 開始，不再支援 **Windows Client 7 SP1 與 8.1** 作業系統。
>
> 如需更多關於支援 OS 版本的資訊，請造訪 [此頁面](https://github.com/dotnet/core/blob/main/release-notes/7.0/supported-os.md)。

## 支援的瀏覽器

* Microsoft Edge。Microsoft Internet Explorer 9 以上版本（IE6 和 IE7 曾於 3.60 以前的版本支援，IE8 曾於 4.10 以前的版本支援）
* Mozilla Firefox 2.0 以上版本
* Google Chrome 1.x 以上版本
* Apple Safari 12.x 以上版本

## 開發所需工具

由於 nopCommerce 是基於 Microsoft 的 .NET 9 建構，我們需要在開始 nopCommerce 開發前安裝以下幾項工具。

### .NET 9 Runtime 與 .NET 9 SDK

由於 nopCommerce 4.90 是基於 .NET 9 建構，我們在進行 nopCommerce 開發前必須安裝 [.NET 9 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-9.0.0-windows-x64-installer) 和 [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-9.0.100-windows-x64-installer)。

### Visual Studio 2022 或以上版本 / Visual Studio Code

眾所周知，nopCommerce 是基於「Microsoft .NET 9」建構的，而 *Visual Studio IDE* 是開發 .NET 應用程式的最佳選擇。由於 .NET Core 是跨平台的，我們可以在任何平台上開發與部署 .NET 應用程式。不過，我們也可以使用 *Visual Studio Code* 作為 *Visual Studio* 的替代方案，在 Windows 或其他平台上進行開發。

> [!NOTE]
>
> 如果您使用 *Visual Studio Code*，您需要安裝 **C# for Visual Studio Code (powered by OmniSharp)** 擴充套件。

### Microsoft SQL Server 2012 或以上版本 / MySql Server 5.7 或以上版本 / PostgreSQL 9.2 或以上版本

從 4.30 版本開始，nopCommerce 使用 *Linq2DB* 作為 ORM 框架。*Linq2DB* 是一個物件關聯映射 (ORM) 框架，讓 .NET 開發人員能夠使用 .NET 物件來操作資料庫。它能將 .NET 物件映射到多種資料庫提供程序。您可以選擇使用 MS SQL Server、MySql Server 或 PostgreSQL。

> [!NOTE]
>
> 如需更多關於支援資料庫的資訊，請造訪 [此頁面](https://linq2db.github.io/articles/general/databases.html)。

### Internet Information Service (IIS) 7.0 或以上版本

託管 nopCommerce 應用程式/專案的其中一個選項是 IIS，這是一項在 Windows 上託管網頁應用程式的 Microsoft 技術。不過，nopCommerce 也可以在不支援 IIS 的 Linux 和 MacOS 上執行。在這種情況下，您可以使用 Apache 或 Nginx 等替代工具，在 Linux 伺服器上託管您的應用程式。