---
title: Adding products
author: AndreiMaz
uid: user-guide/running/product-management/products/adding-products
---
# Adding products

Setting up products in the best way possible is highly important for a store. Make sure to not miss any detail, such displaying different size and color options, giving a thorough product description, adding appealing pictures, etc.

To add a new product, go to **Catalog → Products**. Click **Add new** button in the right top corner.
> [!NOTE] 
> you can import a product from an external file by clicking Import from Excel. Once you have a list of products, you can export it to an external file for backup purposes, by clicking Export to XML(all) or Export to Excel (all). You can export selected products by clicking Export to XML (selected) and Export to Excel (selected). Additionally, it is possible to Download catalog as PDF to print the selected products to a PDF file. To remove products from the list, select the items to be deleted and click the Delete (selected) button.

![](/user-guide/running/_static/products_page.png)

Adding New Product page are available in two modes: **advanced** and **basic** (in Advanced mode by default). You can switch to the Basic mode and select the required fields in Settings.

![](/user-guide/running/_static/add_a_new_product.png)

### General information

Start by filling up General Information on the **Product info panel**:

- Set up your product as **Simple** or **Grouped** (read more about it here)
- Enter your **Product Name**
- Tick **Visible individually** if you want the product to be in catalog or search results, otherwise the product will be hidden in the catalog and accessible only from a grouped product details page.
- Enter the product **Short Description** that will be displayed in the catalog
- Enter the product **Full Description** that will be shown on the product page. Here you can add text, bullet points, links, or additional images. Make sure to write a detailed description as it affects your buyers' decision making
- Enter **SKU**, the product stock keeping unit, used internally for tracking the product. This is your internal unique ID used to track this product.
- Tick **Published**, to make the product visible in your store.
- Enter **Product Tags**, the keywords for product identification. Enter tags separating them by comma. The more products are associated with a particular tag, the larger it will look in the Popular Tags area, displayed in the sidebar of the Catalog page

![](/user-guide/running/_static/popular_tags.png)

- Enter **GTIN** (global trade item number). These identifiers include UPC (in North America), EAN (in Europe), JAN (in Japan), and ISBN (for Books).
- Enter **Manufacturer part number** - a part number provided by a manufacturer for the product.
- **Show on homepage**. If this checkbox is selected, the store owner can also specify the Display Order.
- Tick **Allow customer reviews**, to enable customers to provide reviews of the product.
- Define **Available Start date and/or Available End date** of the product availability.
- Tick **Mark as new**, to mark the product as recently added. This way you can manage a list of products displayed on the "New products" page. You can also specify a period during which this product will be marked as new.
- In the **Admin comment** field enter a comment for information purposes.

### Product class

Define whether the product is a

- [Gift Carduser](xref:user-guide/marketing/promotional/gift-cards)
- [Downloadable Product](xref:user-guide/running/product-management/products/downloadable-products)
- [Recurring Product](xref:user-guide/running/product-management/products/recurring-products)
- [Rental Product](xref:user-guide/running/product-management/products/rental-products)

### Product price

In the **Prices** section define:

- **Price**, in a predefined currency. Note that you can change the store currency in **Configuration → Currencies**.
- **Old price**. If it is larger than zero it becomes visible in the public store and is displayed beside the new price for comparison purposes.
- **Product cost**, the sum of all costs associated with the production of the product or service. This cost is not displayed to customers. 
- To **Disable buy button**. This can be useful for products “upon request”.
- To **Disable wishlist button**.
- **Available for pre-order** if the product is not in the store yet, but you want the customers to be able to order it. The **Pre-ordered** button will be displayed to replace the standard **Add to cart** button. When this option is selected, the **Pre-order availability start date** field is displayed. Enter the availability start date of the product in UTC. The Pre-order button will be changed to Add to cart when this date is reached.
- **Call for Price**, to show **Call for Pricing** or **Call for Quote** instead of the price. This can help you to establish a contact with your customers and provide with additional information about the product they are interested in.
- **Customer enters price**, to indicate that a customer must enter the price. When selected, the following fields are displayed:
- In the **Minimum amount** field, enter the minimum amount for the price.
- In the **Maximum amount** field, enter the maximum amount for the price.
- **PAngV (base price) enabled**, if the product has a base price. This is required according to the German law (PAngV). For example, if you sell 500ml of beer for 1,50 euro, you have to show the base price: 3.00 euro per 1L. When selected, the following fields are displayed:
- **Amount in product** - amount of the product that is being sold.
- **Unit of product** - measure of a previously entered value.
- **Reference amount** - the base amount.
- **Reference unit** - measure of a previously entered value.
- **Discounts**. Learn how to set up discounts here.
- Whether the product is exempted from tax, by ticking **Tax exempt**. Otherwise, from the Tax category dropdown list, select the required tax classification for this product. Tax categories can be configured by the store owner in  the **Configuration → Tax → Tax Categories.**
- The product as **Telecommunications, broadcasting, and electronic services**, to apply special tax rules used in the European Union. Find more info here.
- Set up [tier prices](xref:user-guide/marketing/promotional/tier-prices) if required.

