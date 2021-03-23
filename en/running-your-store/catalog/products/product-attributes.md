---
title: Product attributes
uid: en/running-your-store/catalog/products/product-attributes
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.dunaenko, git.mariannk
---

# Product attributes

Product attributes are variations of a product (such as color, size, etc.).

A user can create different combinations of the attributes. For example, a product can be in different sizes and colors. Thus, a user would create two attributes and their values, e.g. "size" (S, M, L) and "color" (red, blue, white), and then set up groups according to the availability of the products.

In nopCommerce, product attributes are used in **inventory tracking**, and can also cause the **price difference**.

To define product attributes go to **Catalog → Attributes → Product attributes**.

![Product attributes](_static/product-attributes/product_attributes.png)

> [!NOTE]
> 
> By default, there are no product attributes pre-created in nopCommerce.

## Adding product attributes

Click **Add new** to add an attribute.

![Add new](_static/product-attributes/add_a_new_product_attribute.png)

In the *Add a new product attribute* window fill the fields **Name** and **Description**.

Click **Save and continue edit** to proceed to the *Predefined values* editing panel.

> [!TIP]
> 
> [YouTube tutorial: Adding a product with color attributes](https://youtu.be/QihipwQ61YU)

## Adding predefined values

In the *Predefined values* panel click **Add a new value**, and *Add a new value window* will be opened:

![Adding predefined values](_static/product-attributes/add_a_new_value.png)

In the *Add a new value* window define:

- The attribute **Name**.
- The **Price adjustment** applied when choosing this attribute value. For example '10' to add 10 dollars. Or 10% if **Price adjustment. Use percentage** is ticked.
- **Price adjustment. Use percentage** checkbox allows determining price adjustment in percentage instead of an absolute value.
- The **Weight adjustment** applied when choosing this attribute value.
- The attribute value **Cost** is the cost of all the different components which make up this value. This may be either the purchase price if the components are bought from outside suppliers, or the combined cost of materials and manufacturing processes if the component is made in-house.
- Whether the value **Is pre-selected** for a customer.
- The value **Display order** in an attribute list.

After filling the fields, click **Save**.

> [!TIP]
> 
> It's not necessary to create attributes values when adding product attribute, you can create them later when apply certain product attribute to the product. 
> Once the attributes and values are set, they can be grouped and managed in the *Product attributes* panel on the product edit page.

> [!NOTE]
> 
> Some store owners prefer to highlight products differentiated by attributes and create a separate product with each specific attribute (for example, separately listed blue T-shirts and red T-shirts). In this case, we recommend creating a grouped product (shirts, as in the example) in order for all of the variations to be displayed on the same page, once the grouped product is being viewed by a customer. Read more about [grouped products](xref:en/running-your-store/catalog/products/grouped-products-variants).

## Used by products panel

In the *Used by products* panel you can which products use this attribute:

![Used by products](_static/product-attributes/used-by.jpg)

## See also

- [Adding products](xref:en/running-your-store/catalog/products/add-products)
- [Grouped product](xref:en/running-your-store/catalog/products/grouped-products-variants)

## Tutorials

- [Overview of conditional product attributes](https://www.youtube.com/watch?v=eIdHVcEdos8&t=55s)