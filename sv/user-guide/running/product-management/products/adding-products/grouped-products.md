---
title: Grouped product
uid: sv/user-guide/running/product-management/products/adding-products/grouped-products
---

# Grouped product

**Grouped products**, or products **with variants**, is a convenient tool to sell a product that has different supplementary items or different attribute sets. Different combinations of such product **can be sold as separate products**, and the price may vary.

In nopCommerce, grouped products look like a single product details page displaying all possible options. It is a convenient and SEO-friendly tool to sell complex products.

> [!TIP] **For example**, a base product, such as a camera body, can be grouped with different sets of lens. Another use case of a grouped product is selling one type of a product with different attribute sets. For example, chocolate with different flavours. In this case, a customer can easily see the main product and all its options on the same page.

![grouped](_static/grouped-products/grouped.png)

## Adding new grouped product

To create a grouped product, go to **Catalog → Products**. There are several **steps** **to follow**:

1. Create several products with a Simple product type and the corresponding simple product template. These are the variants of the main product. Define whether you want them to be visible separately in catalog and search results, or be shown only on a product page of the main product.
    
    ![visible](_static/grouped-products/vvv.png)

2. Create a **Grouped (product with variants) product**, with the corresponding product template, and assigns these Simple products in the **Associated products (variants) section.**
    
    ![variants](_static/grouped-products/variants.png)

> [!NOTE]
> 
> - In the public store, a customer sees a separate **Add to cart** button for each associated product on the **Grouped product** details page.
> - A **Simple** product can be associated with only one **Grouped** product.
> - Grouped products are **not orderable directly**. However, associated with them Simple products are. For example, a customer cannot order the **Creative Sound Card** product directly. The customer must order an **OEM** or **Retail version** of the Creative Sound Card. In this case, the Grouped product is a Creative Sound Card, and there are two associated Simple products for this Grouped product: OEM and Retail, each with potentially different prices.