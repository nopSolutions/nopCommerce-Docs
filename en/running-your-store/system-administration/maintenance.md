---
title: Maintenance
uid: en/running-your-store/system-administration/maintenance
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.mariannk
---

# Maintenance

In the **System** menu, select **Maintenance**.

## Delete guest customer records

![Maintenance](_static/maintenance/deleting_guest_customers.png)

In the *Deleting guest customers* panel, click the **Delete** button. This option enables you to delete customer records created for guest visitors.

> [!NOTE]
>
> Only guests without orders or written customer content (such as product reviews or news comments) will be deleted.

## Deleting old exported files

![Maintenance](_static/maintenance/old_export.png)

In the *Deleting old exported files* panel, click the **Delete** button. All the exported and generated files (such as PDF and Excel files) will be deleted and removed from the database.

## Delete abandoned shopping carts and wishlists

![Maintenance](_static/maintenance/deleting_abandoned_sc.png)

In the *Deleting abandoned shopping carts* panel, click the **Delete** button. All shopping cart and wishlist items created before the specified date will be deleted.

## Re-index database tables

![Maintenance](_static/maintenance/re-index.png)

In the *Re-index database tables* panel, click the **Re-index** button. This procedure modifies existing tables by rebuilding the index. When you execute re-indexing in a table, only the statistics associated with the indexes are updated. Automatic or manual statistics created in the table (instead of an index) are not updated.

## Database backups

![Maintenance](_static/maintenance/db_backup.png)

In the *Database backups* panel, click the **Backup now** button to create a database backup.

> [!NOTE]
>
> Database backup functionality only works when your nopCommerce application is deployed on the same server as the database. Otherwise, you will have to take care of the backup yourself (contact your system administrator).

## Delete already sent emails

![Maintenance](_static/maintenance/delete_already_sent_emails.png)

## Delete minification files

![Maintenance](_static/maintenance/delete_min_files.png)

## Shrink database

![Maintenance](_static/maintenance/shrink.png)

## Delete image thumbs

![Maintenance](_static/maintenance/delete_image_thumbs.png)

## Tutorials

[Overview of system maintenance options](https://www.youtube.com/watch?v=CNgTJZoWHTA)
