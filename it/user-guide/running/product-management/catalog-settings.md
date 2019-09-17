---
title: Catalog settings
uid: it/user-guide/running/product-management/catalog-settings
---

# Catalog settings

Catalog settings include enabling options for **product sorting, changing view modes, comparing products** and more.

To define catalog settings, go to **Configuration → Settings → Catalog Settings**. The Catalog Settings page are available in advanced and basic modes (in the advanced mode **by default**).

The top block on the page sets up **Search**:

- Tick the **Search autocomplete enabled** checkbox, to display the autocomplete search box in the public store, as follows:

![search](_static/catalog-settings/search.png)

When this option is enabled the following additional fields are displayed:

- **Number of autocomplete products** to display: set up the number of results that will be visible in the autocomplete dropdown list of the search box in the public store.
- **Show product images** in autocomplete box: tick this checkbox, to enable displaying product images in the autocomplete search box.
- **Search term minimum length** - the minimum number of chars required for the search.
- Tick the **Search page.Allow customers to select page size** checkbox, if you want to allow customers to select the page size from the predefined list of options.
- In the **Search page.Page size options** (comma separated) field enter the list of page size options for customers, or enter the number of products that you want to display on the search page in the Search page.Products per page field, in case you have chosen to not allow customers to select the page size.

The **second block** sets up **Product reviews**. Define the following:

- **Product reviews must be approved**, to enforce product reviews to be approved by a store administrator prior to being published
- **Allow anonymous users to write product reviews**
- **Product review possible only after product purchasing**, to allow reviewing the product only by customers who have already ordered it
- **Notify about new product reviews**, to notify the store owner about new public reviews
- **Notify customer about product review reply**, to notify customer about product review reply
- **Reviews per store**, allows displaying only reviews of the current store (on the product details page). Untick this checkbox if you want your customers to see the product’s reviews written in all of your stores.
- **Show product reviews tab **on 'My account' page to allow customers see all their reviews on ‘My account’ page.
- **Product reviews page size** - the amount of reviews per page.
- **Sort by ascending**, to sort product reviews by creation date as ascending

The next block sets up **Review types**. You can configure a list of review types if you think that a basic review is not enough.

![review_types](_static/catalog-settings/review_types.png)

Click **Add new button** for create new review type.

![quality_rating](_static/catalog-settings/quality_rating.PNG)

**Define the following:**

- Enter your review type **Name**.
- Enter the review type **Description**.
- **Display order**.
- **Required** - when required, customers have to choose an appropriate rating value before they can continue.
- **Visible to all customers** - Sets visibility of the review type for all customers. Click **Save** button,to add a new review type.

Now in the public store will be able to fill additional ratings on the product review page.

![product_reviews](_static/catalog-settings/product_reviews.PNG)

Also on this page you can see the left feedback of all customers (if this setting is active).In the customer personal account page there is also an opportunity to see all the reviews left on the products.

![my_account](_static/catalog-settings/my_account.PNG)

The next block sets up **Performance**. Having the following settings enabled can significantly improve the store performance:

- **Ignore ACL rules** (sitewide) turns off the [ACL rules](xref:en/user-guide/configuring/setting-up/customers/acl) configured for entities.
- **Ignore limit per store** (sitewide), to ignore limit per stores rules configured for entities (sitewide). It is recommended to enable this setting if you have only one store or do not have any store-specific limitations.
- **Ignore discounts **(sitewide)
- **Ignore featured products** (sitewide)
- **Cache Product Prices**. You should not enable it if you use some complex discounts, discount requirement rules, or coupon codes.

**Sharing options** allow to set up an opportunity for shoppers to share the products across their social media network. The options will appear as small icons on product pages. To set up the sharing options:

- Tick the **Show a share button**, to display a share button on the product details page. When this field is selected the Share button code field is displayed.
- The **Share button code** field displays the pages button code.

> [!TIP] By default, AddThis service is used (<http://www.addthis.com/>).

![Share](_static/catalog-settings/zzz.png)

- Tick the 'Email a friend' enabled, to allow customers to use the 'Email a friend' option.
- Allow anonymous users to email a friend

**Compare products option** enables consumers to compare different offers based on their characteristics and price so they can make the best shopping decisions. Set up the block, as follows:

- Tick the '**Compare Products**' enabled checkbox, to make customers be able to compare product options in your public store. The button Add to Compare list will appear on product pages.
- Tick the **Include short description in compare products** checkbox, to display short product descriptions on the compare products page.
- Tick the **Include full description in compare products** checkbox, to display full product descriptions on the compare products page.

![compare_product](_static/catalog-settings/compare_product.png)

In general **catalog settings** you can define:

- To **Show best sellers on home page**.
- When the Products also purchased option is enabled, the **Number of also purchased products to display** field appears, in which the store owner can set the number of products to be displayed.
- **"Products also purchased” enabled** checkbox, to enable customers to view a list of products purchased by others who also purchased the above.
- “**Email a friend**” to enable registered customers to recommend specific products to friends by email.
- Tick the **Allow anonymous users to email a friend** checkbox, to enable anonymous users to email friends.
- To **Show SKU** in the public store.
- To **Show GTIN** in the public store.
- To **Show manufacturer part number** in the public store.
- “**Recently viewed products**” **enabled** checkbox, to enable customers to see products recently viewed in your store.
- In the **Number of “Recently viewed products”** field, enter the number of recently viewed products to be displayed when the Recently viewed products checkbox is enabled.
- **Recently added products” enabled** checkbox, to enable customers to see products recently added in your store.
- **In the Number of “Recently added products”** field, enter the number of recently added products to display when the recently added products checkbox is enabled.
- To **Show free shipping icon** for products with this option enabled.
- To **Allow view mode changing** on the Category and Manufacturers pages.
- To **Allow viewing of unpublished product details page**. In this case, SEO will not be affected, and search crawlers will index the page, even though a product is temporary unpublished and invisible for the customers. Note that a store owner always has access to unpublished products.
- Tick the **Discontinued message for unpublished products** checkbox, to display "a product has been discontinued" message when customers try to access the product details page.
- **To Include products from subcategories** when viewing a category details page.
- To **Show number of distinct products beside each category** in the category navigation area located in the left column in the public store.
- **Category breadcrumb enabled.**
- **New products” page enabled** in the store.
- Modify the **Number of products** on **New products” page** to set the number of products on the “New products page”
- **Number of product tags (cloud)** - the number of tags that appear in a tag cloud.
- **To Allow customers to select 'Products by tag' page size** on the product tag page from a predefined list of options defined by the store owner. **The Page size options** field becomes visible in this case in the administration area. When disabled, customers will not be able to select a page size and the store owner enters it. The **“Products by tag” page**. Products per page field becomes visible in this case, in which the store owner enters the number of products.
- **Number of manufacturers to display** in the manufacturer navigation block.

Certain **tax/shipping info** options specific to Germany are available in the next block:

- Display tax/shipping info (footer)
- Display tax/shipping info (product details page)
- Display tax/shipping info (product boxes)
- Display tax/shipping info (wishlist)
- Display tax/shipping info (order details page)

Tick the **Export/Import products with attributes** checkbox if you need product attributes to be exported/imported whenever you export/import the product.

Tick the **Allow product sorting** checkbox, to enable a product sorting option on the categories and manufacturers pages. You can activate/deactivate sorting by Position, Name, Price and Date of Creation.

![product_sorting](_static/catalog-settings/product_sorting.png)