---
title: Import/Export
uid: en/user-guide/running/product-management/products/import-export
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.dunaenko
---

# Import/Export

nopCommerce supports **importing** and **exporting** products in **XML** or **Excel** formats.

 You can find these options in **Catalog → Products** at the main page top left.

![Products](_static/import-export/products3.png)

## Exporting product

You can download products from your catalog in pdf (**Download catalog as PDF** button), export in xml or excel formats (**Export** button). If you don’t need to download all the products you can use **search** to find the required products or/and use checkboxes to select the needed products. Choose to **export all** or only **selected** from the Export dropdown list. A table with products you have chosen will be downloaded. The table consists of all the products characteristics from product editing page tabs (Product info, SEO, Pictures and so on).

> [!NOTE]
> 
> if you use product attributes, an exported table will be grouped by rows, to view attribute details click + next to your product in the table.

![simple product](_static/import-export/simple_product.png)

![exporting product](_static/import-export/exporting_product.png)

### Importing products

If you do not want to add all the products for your catalog manually you can use import option.

> [!NOTE]
> 
> before you choose import on a product page you should download a table template for import in excel format as it was described in Exporting products.  For accurate and correct import of your products it’s crucial to name all the columns in the table properly (exactly as in the downloaded table).

It is not mandatory to fill all the table fields. The product will be created based on the filled fields.

Imported products are distinguished by SKU. If the SKU already exists, then its corresponding product will be updated.

Import requires a lot of memory resources. That's why it's not recommended to import more than 500 - 1,000 records at once. If you have more records, it's better to split them into multiple Excel files and import separately.

> [!NOTE]
> 
> For example, we want to add Dancing shoes to our catalog. Create a new row in the table.

![product table](_static/import-export/product_table.png)

Then click **import**, upload the table and check, we have a new product in the catalog.

![product catalog](_static/import-export/product_catalog.png)

## See also

* [Adding products](xref:en/user-guide/running/product-management/products/adding-products/index)
