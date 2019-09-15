---
title: Upgrading nopCommerce
uid: cs/user-guide/installing/upgrading
---

# Upgrading nopCommerce

This chapter describes how to upgrade nopCommerce to the [latest](https://www.nopcommerce.com/downloads.aspx) version. You might want to do this because you've seen a message at the nopCommerce news section of your dashboard telling you that a new release is available. nopCommerce doesn’t support automatic upgrades, you have to do it manually.

**Follow the next steps:**

1. Make a backup of everything on your site, including the database. This is extremely important so that you can roll back to a running site no matter what happens during migration.
2. Then you have to execute SQL upgrade scripts. You have to execute them stepwise. For example, if your current version is 2.00 and the latest available version is 2.30, then you have to upgrade to 2.10, then to 2.20, and then to 2.30. So download the required upgrade scripts from our [Downloads](https://www.nopcommerce.com/downloads.aspx) page. Once an upgrade script is downloaded, execute it over your database.
3. Remove all files from the previous version (all except `App_Data\Settings.txt` and `App_Data\InstalledPlugins.txt`)
4. Upload the new site files (get the latest version [here](https://www.nopcommerce.com/downloads.aspx)). As you deploy, make sure that the target `App_Data\Settings.txt` and `App_Data\InstalledPlugins.txt` files aren't overwritten, so that the production site continues to point to the production database.
5. Ensure that everything is OK

> [!NOTE] As you deploy, make sure that the target `Settings.txt` and `InstalledPlugins.txt` files aren't overwritten, so that the production site continues to point to the production database.
> 
> If you stored your pictures on the file system, then also backup them (`\wwwroot\Images`) and copy back after the upgrade.
> 
> **(upgrading from 1.X to 2.X):** If you want to upgrade from a version 1.90 to the latest version, you would need to install 2.00 first (over the existing database), run the 1.90 to 2.00 migration SQL script, and then upgrade to 2.10, 2.20, 2.30, etc

## Troubleshooting

If you experience problems after the upgrade, you can always restore your backup and replace the files with ones from your previous version. You can always post a question on our [forums](https://www.nopcommerce.com/boards/).

> [!Note] If when doing advanced search you cannot find what you need, then try a Google search focused into nopCommerce site: [your search words **site:[nopcommerce.com](https://www.nopcommerce.com/ "nopcommerce.com")**]