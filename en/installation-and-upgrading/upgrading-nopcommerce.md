---
title: Upgrading nopCommerce
uid: en/installation-and-upgrading/upgrading-nopcommerce
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.rajupaladiya, git.exileDev, git.dunaenko
---

# Upgrading nopCommerce

This chapter describes how to upgrade nopCommerce to the [latest](https://www.nopcommerce.com/download-nopcommerce) version. You might want to do this because you've seen a message at the nopCommerce news section of your dashboard telling you that a new release is available. nopCommerce doesn't support automatic upgrades, you have to do it manually.

> [!IMPORTANT]
>
> Since version 4.40 we don't use SQL upgrade scripts anymore. The upgrade is performed automatically with migrations (during the first application start). So when you upgrade from 4.30 to 4.40 you need to skip step 2 from the list below!

Follow the next steps:

1. Make a backup of everything on your site, including the database. This is extremely important so that you can roll back to a running site no matter what happens during migration.
1. **[To upgrade to nopCommerce 4.30 and below]** Then you have to execute SQL upgrade scripts. You have to execute them stepwise. For example, if your current version is 3.90 and the latest available version is 4.20, then you have to upgrade to 4.00, then to 4.10, and then to 4.20. So download the required upgrade scripts from the [download nopCommerce](https://www.nopcommerce.com/download-nopcommerce) page. Once an upgrade script is downloaded, execute it over your database.

  > [!NOTE]
  >
  > Don't forget to read the `Readme.txt` file provided with the upgrade script. Sometimes it contains important notes regarding upgrading to the newest version.

1. Remove all files from the previous version except JSON files in the 'App_Data' directory such as `dataSettings.json` (Settings.txt in oldest versions), `plugins.json` (InstalledPlugins.txt in oldest versions), `appsettings.json`, move them to the safe place.
1. Upload the new site files (get the latest version [here](https://www.nopcommerce.com/download-nopcommerce)).
1. Ensure that everything is OK.

> [!NOTE]
>
> As you deploy, make sure that the target `dataSettings.json` (or `Settings.txt`) and `plugins.json` (or `InstalledPlugins.txt`) files are updated as per latest nopCommerce version, so that the production site continues to point to the production database.

> [!NOTE]
> If you stored your pictures on the file system, then also backup them (`\wwwroot\Images\`) and copy back after the upgrade.

> [!NOTE]
> **(upgrading from 3.X to 4.X)**: If you want to upgrade from a version 3.90 to the latest version, you would need to install 4.00 first (over the existing database), run the 3.90 to 4.00 migration SQL script, and then upgrade to 4.10, 4.20 etc.

## Troubleshooting

If you experience problems after the upgrade, you can always restore your backup and replace the files with ones from your previous version. You can always post a question on our [forums](https://www.nopcommerce.com/boards/).

> [!Note]
>
> If when doing advanced search on our forums you cannot find what you need, then try a Google search focused into nopCommerce site: [your search words **site:[nopcommerce.com](https://www.nopcommerce.com/ "nopcommerce.com")**].
