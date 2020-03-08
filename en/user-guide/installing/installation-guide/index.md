---
title: Installation guide
uid: en/user-guide/installing/installation-guide/index
author: git.AndreiMaz
contributors: git.rajupaladiya, git.exileDev, git.DmitriyKulagin, git.IvanIvanIvanov
---

## Installation process

nopCommerce requires write permissions for the directories and files described below

- **For nopCommerce versions 4.20 and above:**
  - `\App_Data\`
  - `\bin\`
  - `\log\`
  - `\Plugins\`
  - `\Plugins\bin\`
  - `\wwwroot\bin\`
  - `\wwwroot\bundles\`
  - `\wwwroot\db_backups\`
  - `\wwwroot\files\exportimport\`
  - `\wwwroot\images\`
  - `\wwwroot\images\thumbs\`
  - `\wwwroot\images\uploaded`
  - `\App_Data\Plugins.json` (after installation)
  - `\App_Data\dataSettings.json` (after installation)

- **For nopCommerce versions 4.00 and 4.10:**
  - `\App_Data\`
  - `\bin\`
  - `\log\`
  - `\Plugins\`
  - `\Plugins\bin\`
  - `\wwwroot\bin\`
  - `\wwwroot\bundles\`
  - `\wwwroot\db_backups\`
  - `\wwwroot\files\exportimport\`
  - `\wwwroot\images\`
  - `\wwwroot\images\thumbs\`
  - `\wwwroot\images\uploaded`
  - `\App_Data\installedPlugins.json` (after installation)
  - `\App_Data\dataSettings.json` (after installation)

- **For nopCommerce versions 2.00-3.90:**
  - `\App_Data\`
  - `\bin\`
  - `\Content\`
  - `\Content\Images\`
  - `\Content\Images\Thumbs\`
  - `\Content\Images\Uploaded\`
  - `\Content\files\ExportImport\`
  - `\Plugins\`
  - `\Plugins\bin\`
  - `\Global.asax`
  - `\web.config`

These permissions are validated during the installation process. If you do not have write permissions, a warning message is displayed, requesting you to configure permissions.

Before installing nopCommerce, ensure you have SQL Server 2012  databases installed on your system

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
> 3. If you want to completely reset a nopCommerce site to its default settings, you can delete the `dataSettings.json` (`Settings.txt` for nopCommerce 3.90 or below version) file from App_Data directory. When using IIS you might want to read this article.

## See also

- [Hosting providers](xref:en/user-guide/installing/installation-guide/hosting-providers)
- [Installing Internet Information Services (IIS)](xref:en/user-guide/installing/installation-guide/installing-IIS)
- [Known Issues and Solutions](xref:en/user-guide/installing/installation-guide/known-issues-and-solutions)
