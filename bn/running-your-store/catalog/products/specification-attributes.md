---
title: Specification attributes
uid: en/running-your-store/catalog/products/specification-attributes
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.dunaenko, git.mariannk
---

# Specification attributes

Specification attributes are similar to [product attributes](xref:en/running-your-store/catalog/products/product-attributes), however, they are only used for information purposes (visible on the product details page) and for filtering products on the category details page. They don't define a product price as well as can't be used for the inventory tracking.

## Example

Let's say you are running an online computer store. What helps a customer to make a decision?

- Provide a customer full and descriptive information about your products. Despite you fill the short and full description of the certain computer, allow the customer to see the product's characteristic where reflected it's most important details:

  ![Specification attributes](_static/specification-attributes/specs.jpg)

  This table can be displayed on the product details page if you select the **Show on product page** field when [adding a specification attribute to a product](xref:en/running-your-store/catalog/products/add-products#specification-attributes).

- Allow your customers to search computers using the filter functionality. Let's say we can search in your store by CPU type and memory. Then the category page will look the following way:

  ![Filtering](_static/specification-attributes/filter.jpg)

  Select the **Allow filtering** field when [adding a specification attribute to a product](xref:en/running-your-store/catalog/products/add-products#specification-attributes) to allow filtering by this attribute for the certain product.

- Include the "Compare products" functionality in your store. This feature is also uses specification attributes. For your computer store the "Compare products" page will look the following way:
  
  ![Compare products](_static/specification-attributes/compare.jpg)

  To enable the "Compare products" functionality go to **Configuration → Settings → Catalog settings**. In the *Compare products* panel tick the **'Compare products' enabled** checkbox.

The next section describes how to create specification attributes. Note that after creating a list of specification attributes you will need to add the specification attributes to the products one by one. Learn how add specification attributes to products in the [Add products - Specification attributes](xref:en/running-your-store/catalog/products/add-products#specification-attributes) section.

## Create a specification attribute group

> [!NOTE]
>
> All specification attributes which don't belong to any group are under the *Default group (non-grouped specification attributes)*.

To see and edit a list of specification attributes and their groups, go to **Catalog → Attributes → Specification attributes**.

![Specification attributes](_static/specification-attributes/specification_attributes.jpg)

Click **Add group** to add a new group. The *Add a new specification attribute group* window is displayed, as follows:

![Add group](_static/specification-attributes/specification_group.jpg)

In the *Attribute group info* panel, enter:

- The **Name** of the specification attribute group.
- The **Display order** number.

Then save the changes.

## Create a specification attribute

> [!NOTE]
>
> By default, there are no specification attributes pre-created in nopCommerce.

To see and edit a list of specification attributes, go to **Catalog → Attributes → Specification attributes**.

![Specification attributes](_static/specification-attributes/specification_attributes.jpg)

On this page you can delete specification attributes by selecting them and then clicking the **Delete(selected)** button.

Click **Add attribute** to add a new attribute. The *Add a new specification attribute* window is displayed, as follows:

![Add new](_static/specification-attributes/new-attribute.jpg)

In the *Attribute info* panel, enter:

- The **Name** of the specification attribute.
- The **Display order** number.

Click **Save and continue edit** to proceed to the *Options* editing panel.

### Add a new option

Click the **Add a new option** button in the *Options* panel to create a new specification attribute option. The *Add a new option* window will be displayed, as follows:

![Add a new option](_static/specification-attributes/add_a_new_option.jpg)

Define the following option settings:

- The **Name** of the specification attribute option.
- Tick the **Specify color** checkbox to choose color to be used instead of an option text name (it'll be displayed as a "color square").
  - Choose the **RGB color** which will be displayed to customers.
- The **Display order** number.

Click **Save** to save the option details.

The following screenshot shows the already added options:
![Options](_static/specification-attributes/options.jpg)

### Used by products

If you applied the specification attribute to products you can see the list of these products in the *Used by products* panel:

![Used by products](_static/specification-attributes/used-by.jpg)

## See also

- [Adding products](xref:en/running-your-store/catalog/products/add-products)
- [Product attributes](xref:en/running-your-store/catalog/products/product-attributes)
- [YouTube tutorial: Managing Specification Attributes](https://www.youtube.com/watch?v=YmD_vHqWzQw&index=11&list=PLnL_aDfmRHwsbhj621A-RFb1KnzeFxYz4)
