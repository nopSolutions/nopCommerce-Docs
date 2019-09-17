---
title: Installation guide
uid: da/user-guide/installing/installation-guide/index
---

# Installation guide

This chapter describes how to download nopCommerce software, upload it to your server, define the file permissions, and install it on your system. You may also watch the screencast about nopCommerce installation on our [YouTube channel.](https://www.youtube.com/watch?v=L7NGodeB9sQ)

Before you begin the install, ensure that you and your web host have the [minimum requirements to run nopCommerce.](xref:en/user-guide/installing/technology-system-requirements)

> [!TIP] For more information on hosting selection guidelines, visit [this page.](xref:en/user-guide/installing/installation-guide/hosting-provider)

There are several options available when downloading nopCommerce. In order to determine which option to download, you need to decide how you will use it. The following options are available:

1. **Web (no source).** This option is available for users who do not wish/need to develop any custom code. This is a pre-compiled version of nopCommerce that can simply be uploaded to your hosting provider and used immediately. With this option, users can still modify the look and feel or user interface (UI) of their site to suit their needs, but they do not have to worry about development.
2. **Source code.** This option contains a full Visual Studio solution. It is for users who wish to customize the code within nopCommerce. It contains all the source code used to develop nopCommerce and must be opened in Visual Studio. It also includes scripts to build and compile the solution to upload to your hosting provider.
3. **Upgrade script.** The upgrade script option is for users who have a nopCommerce installation already in place. The script will upgrade your current installation to the latest version.

With each of these options, excluding the upgrade script, you can deploy nopCommerce to your development environment and your hosting provider. Choose the option that you would like to [download](https://www.nopcommerce.com/downloads.aspx) and click on the appropriate download link to begin your download. It is recommended that you create a new folder on your desktop to store your downloaded files for easy access.

## Running the site using IIS (package without source code)

To use IIS, copy the contents of the extracted nopCommerce folder to an IIS virtual directory (or site root), and then view the site using a browser.

If you are using nopCommerce 3.90 and below, then configure it to run in integrated mode, and configure the application pool to run the .NET Framework version 4. Please note that it's not required for nopCommerce 4.00 and above

> [!TIP] For more information on IIS, visit [this page.](xref:en/user-guide/installing/installation-guide/installing-IIS)

**Running the site using Visual Studio (package with source code)** This step describes how to launch a site in Visual Studio. To run the site in Visual Studio, extract the full source code archive (.rar) to a local folder. Launch Visual Studio and select **File → Open → Project/Solution**. Navigate to the folder where you extracted the archive and open **NopCommerce.sln** solution file. Run **`Nop.Web`** project.

**Getting the "ready to deploy" package (without source code) from a package with source code** If you're using nopCommerce **3.20 (or above)**, then follow the next steps:

- Open the solution in Visual Studio
- Rebuild the entire solution
- Publish the `Nop.Web` project from Visual Studio. When publishing ensure that configuration is set to "Release"

If you're using **nopCommerce 2.00-3.10**, then please note that publishing these versions of nopCommerce using Visual Studio is unlike publishing a regular web application. Once you’re ready to deploy the site, follow the next steps:

- Run the Prepare.bat file to build the project in release mode and move the plugins to the correct directory.
- Run the Deploy.bat file to perform the same procedure as the Prepare.bat file, but also move all the websites and files to the `\Deployable\Nop_{Version}` directory.
- Select all the files in `\Deployable\Nop_{Version}` directory and upload them to your web server.
- Note for HP (Hewlett Packard) users: HP machines come preloaded with a registry key that will interfere when running the deploy batch file. If you have an HP laptop and get the following error: "the OutputPath property is not set for project `Nop.Web.csproj`", then using `regedit.exe` navigate to `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SessionManager\Environment`
  
    Delete the complete key (Key and value) Platform (your value is MCD). Restart your computer.

## Installation process

nopCommerce requires write permissions for the directories and files described below

- **For nopCommerce versions 4.00 and above:** 
  - `\App_Data`
  - `\bin`
  - `\log`
  - `\Plugins`
  - `\Plugins\bin`
  - `\wwwroot\bin`
  - `\wwwroot\bundles`
  - `\wwwroot\db_backups`
  - `\wwwroot\files\exportimport`
  - `\wwwroot\images`
  - `\wwwroot\images\thumbs`
  - `\wwwroot\images\uploaded`
  - `\App_Data\installedPlugins.json` (after installation)
  - `\App_Data\dataSettings.json` (after installation)
- **For nopCommerce versions 2.00-3.90:** 
  - `\App_Data`
  - `\bin`
  - `\Content`
  - `\Content\Images`
  - `\Content\Images\Thumbs`
  - `\Content\Images\Uploaded`
  - `\Content\files\ExportImport`
  - `\Plugins`
  - `\Plugins\bin`
  - `\Global.asax`
  - `\web.config`

These permissions are validated during the installation process. If you do not have write permissions, a warning message is displayed, requesting you to configure permissions.

Before installing nopCommerce, ensure you have SQL Server 2012 databases installed on your system

You can use any of the following authentication methods to connect to the server:

- **SQL Server Account:** When connecting using this method, logins are created in the SQL Server that is not based on the Windows user accounts. Both the user name and the password are created using the SQL Server and are stored in SQL Server. When using this method you must enter your login and password.
- **Integrated Windows Authentication:** When connecting using this method, the SQL Server validates the account name and password using the Windows principal token in the operating system. This means the user identity is confirmed by Windows. The SQL Server does not request a password and does not perform the identity validation. Windows Authentication is the default authentication mode and is much more secure than SQL Server Authentication. Windows Authentication uses Kerberos security protocol, provides password policy enforcement with regard to complexity validation for strong passwords, provides support for account lockout, and supports password expiration. A connection made using Windows Authentication is sometimes called a trusted connection, because SQL Server trusts the credentials provided by Windows.

Once you open the site for the first time, you’ll be redirected to the installation page, as follows: ![nopCommerce installation](_static/index/installation.jpg)

- **Admin user e-mail**: This is the e-mail address for the first admin of the site.
- **Admin password**: You will need to supply a password for the admin account.
- **Create sample data**: Check this box if you would like sample products to be created. This is recommended so you can start working with your site before adding any of your own products. You can always delete these items later, or unpublish them so they no longer appear on your site.
- **Database Information**: Here you can select either SQL Server Compact or SQL Server. It is recommended to use a full SQL Server product, not the Compact edition.
- **Create database if doesn't exist**: It is recommended that you create your database and database user beforehand to ensure a successful installation. Simply create a database instance and add the database user to it. The installation process will create all the tables, stored procedures, and so on.
- **SQL Server name**: This is the IP, URL, or server name for your database. You will get your SQL Server name from your hosting provider.
- **Database name**: This is the name of the database used by nopCommerce. If you opted to create your database ahead of time, use the name you gave your database here.
- **Use SQL Server account/Use integrated Windows authentication**: If you are installing at a hosting provider, you can use your SQL Server account and supply the credentials you created with your database. If you are using a development environment, you can select Windows authentication. If you are using Windows authentication, the account hosting the application pool in IIS must be a user in the database.>

Specify Custom SQL Server collation: This is an advanced setting and should be left unchecked.

- In the **Store information** area, define the following: 
  - In the Admin user email field, enter a new email that will be used to enter the admin area of your site.
  - In the Admin user password field, enter your new password and confirm it.
  - Check the Create sample data checkbox to include sample data in the database
- In the **Database information** area, define the following: 
  - SQL Compact 4.0 or above: Select the Use built-in data storage (SQL Server Compact) checkbox.
  - SQL Standard 2008 or above: Select the Use an existing SQL Server (or SQL Express) database checkbox and define your SQL server information as follows:
  - Select the **Create database if it doesn't exist** option, if you want to automatically create a database if it doesn’t exist.
  - In the **Database name** field, enter your database name.
  - In the **SQL Server name or IP address** field, enter the required server name or IP address.
  - **Use SQL Server account:** Select this option when your SQL Server uses SQL Server Authentication. When using this option, you must enter your login and password in the relevant fields.
  - **Use Integrated Windows authentication:** Select this option when your SQL Server uses Integrated Windows Authentication.
- Click **Install** to order to start the installation process. When the setup process is complete, your new site's home page is displayed.

> [!NOTE]
> 
> 1. The **Restart installation** button at the bottom of the installation page enables you to restart the installation process in case anything goes wrong.
> 
> 2. Ensure that your application pool is set to Integrated mode
> 
> 3. If you want to completely reset a nopCommerce site to its default settings, you can delete the `Settings.txt` file from App_Data directory. When using IIS you might want to read this article.

## See also

- [Hosting providers](xref:da/user-guide/installing/installation-guide/hosting-provider)
- [Installing Internet Information Services (IIS)](xref:da/user-guide/installing/installation-guide/installing-IIS)
- [Known Issues and Solutions](xref:da/user-guide/installing/installation-guide/known-issues-and-solutions)