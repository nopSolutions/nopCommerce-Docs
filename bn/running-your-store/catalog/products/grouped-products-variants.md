---
title: Grouped products (variants)
uid: en/running-your-store/catalog/products/grouped-products-variants
author: git.AndreiMaz
contributors: git.exileDev, git.dunaenko, git.IvanIvanIvanov, git.mariannk
---

# Grouped products (variants)

Grouped products, or products with variants, is a convenient tool to sell a product that has different supplementary items or different attribute sets. Different combinations of such product can be sold as separate products, and the price may vary.

In nopCommerce, grouped products look like a single product details page displaying all possible options. It is a convenient and SEO-friendly tool to sell complex products.

> [!TIP]
>
> For example, a base product, such as a camera body, can be grouped with different sets of lens. Another use case of a grouped product is selling one type of a product with different attribute sets. For example, chocolate with different flavours. In this case, a customer can easily see the main product and all its options on the same page.

![Grouped](_static/grouped-products-variants/grouped.jpg)

## Adding a new grouped product

To create a grouped product, go to **Catalog → Products**. There are several steps to follow:

  > [!TIP]
  >
  > Learn how to fill up the product fields [here](xref:en/running-your-store/catalog/products/add-products).

1. Create several products with a *simple* product type. These are the variants of the main product. Define whether you want them to be visible separately in catalog and search results, or be shown only on a product page of the main product using the **Visible individually** checkbox.
1. Create a **Grouped (product with variants)** product and assign these simple products you created in the previous step on the **Associated products (variants)** panel:

    ![variants](_static/grouped-products-variants/variants.png)

> [!NOTE]
> 
> - In the public store, a customer sees a separate **Add to cart** button for each associated product on the *grouped* product details page.
> - A *simple* product can be associated with the only one *grouped* product.
> - *Grouped* products are **not orderable directly**. However, associated with them *simple* products are. For example, a customer cannot order the Creative Sound Card product directly. The customer must order an OEM or Retail version of the Creative Sound Card instead. In this case, the *grouped* product is a Creative Sound Card, and there are two associated *simple* products for this *grouped* product: OEM and Retail, each with potentially different prices.

## Tutorials

- [Understanding grouped products in nopCommerce](https://www.youtube.com/watch?v=B1UdxXf_jmE)
- [Creating Bundled products in nopCommerce](https://www.youtube.com/watch?v=sf9jP6KFcko)
