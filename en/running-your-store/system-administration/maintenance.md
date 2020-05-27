---
title: Maintenance
uid: en/running-your-store/system-administration/maintenance
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.mariannk
---

# Maintenance

From the **System** menu, select **Maintenance**. The **Maintenance** window is displayed, as follows:

![Maintenance](_static/maintenance/maintenance.jpg)

## Delete guest customer records

From the **Deleting guest customers** panel, click the **Delete** button. This option enables you to delete customer records created for guest visitors.

> [!NOTE]
> 
> Only guests without orders or written customer content (such as product reviews or news comments) will be deleted.

## Deleting old exported files

From the **Deleting old exported files** panel, click the **Delete** button. All the exported and generated files (such as, PDF and Excel files for example) will be deleted and removed from the database.

## Delete abandoned shopping carts and wishlists

From the **Deleting abandoned shopping carts** panel, click the **Delete** button. All shopping cart items created before the specified date will be deleted.

## Re-index database tables

From the **Re-index database tables** panel, click the **Re-index** button. This procedure modifies existing tables by rebuilding the index. When you execute re-indexing in a table, only the statistics associated with the indexes are updated. Automatic or manual statistics created in the table (instead of an index) are not updated.

## Database backups 

From the **Database backups** panel, click the **Backup now** button to create a database backup. 

> [!NOTE]
>
> Database backup functionality works only when your nopCommerce application is deployed on the same server as the database. Otherwise you will have to take care of the backup yourself (contact your system administrator).


## Tutorials

[Overview of system maintenance options](https://www.youtube.com/watch?v=CNgTJZoWHTA)