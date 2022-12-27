---
title: Discounts
uid: en/running-your-store/promotional-tools/discounts
author: git.AndreiMaz
contributors: git.exileDev, git.DmitriyKulagin, git.IvanIvanIvanov, git.mariannk
---

# Discounts

In nopCommerce, you can use discounts to enable access to special offers. They can be applied to certain categories, products, the total amount, and more. You can use various requirements available out of the box and via plugins from the [nopCommerce marketplace](http://www.nopcommerce.com/marketplace) for your discounts.

Products in nopCommerce can have any number of discounts attached. In the case of several discounts applied, nopCommerce will automatically calculate the best possible price for the customer based on all the available discounts and group memberships.

The most common way of using discounts is a coupon code. A coupon code is entered by your customer on the shopping cart page just before checking out.

To see the list of discounts, go to the **Promotions → Discounts** page:

![Discounts](_static/discounts/list.jpg)

The top area of the page enables you to search for discounts by various search criteria:

- **Start date** and **End date** for discounts created between these dates.
- **Discount type**.
- **Coupon code**.
- **Discount name** is a full discount name or its fragment.
- **Is Active**.

> [!NOTE]
>
> By default, there are no discounts available in nopCommerce, so you can create them from scratch and follow your own marketing strategy.

## Adding a new discount

To add a new discount, go to **Promotions → Discounts** and click **Add new**.

![Add a new discount](_static/discounts/add-new.jpg)

This page is available in two modes: advanced and basic. Stay in the basic mode, which only displays the main fields, or switch to the advanced mode displaying all the available fields.

- Select the **Is active** checkbox if you want to set active this discount.
- Enter the **Name** of the discount.
- From the **Discount type** dropdown list, assign the discount to the required option as follows:
  - *Assigned to order total*: these discounts are applied to the entire customer order (order total).
  - *Assigned to products*: after this discount is created, the store owner can assign it to a product on the product details page or add products in the *Applied to products* panel, which will be displayed below after you save the new discount.
  - *Assigned to categories*: after this discount is created, the store owner can assign it to a category on the category edit page or add categories in the *Applied to categories* panel, which will be displayed below after you save the new discount. This enables applying the discount to all products in this category.
    - If selected, the **Apply to subcategories** field will be displayed to allow applying this discount to subcategories.
  - *Assigned to manufacturers*: after this discount is created, the store owner can assign it to a manufacturer on the manufacturer edit page or add the manufacturer in the *Applied to manufacturer* panel, which will be displayed below after you save the new discount. This enables applying the discount to all products by this manufacturer.
  - *Assigned to shipping*: these discounts are applied to the shipping fee.
  - *Assigned to order subtotal*: these discounts are applied to order subtotal values.

- Select the **Use percentage** checkbox if you want to apply a percentage discount.
  - If the previous checkbox is selected, the **Discount percentage** field will be displayed.
  - You can also set the **Maximum discount amount**. Leave this field empty to allow any discount amount. If you're using *Assigned to products* discount type, then it will be applied to each product separately.

- You can apply **Discount amount** to the order or SKU if the **Use percentage** checkbox is clear.
- Select the **Requires coupon code** checkbox to enable a customer to apply a coupon code to get the discount.
  - After selecting the checkbox, the **Coupon code** option appears. You can enter the required coupon code in this field. This enables customers to enter the coupon code during checkout to apply the discount.
    > [!NOTE]
    >
    > A customer can apply an unlimited number of coupon codes to an order if they fit it.

- If you want to specify the **Start date** and **End date** for your discount, select them in the calendar field in Coordinated Universal Time (UTC).
- The **Cumulative with other discounts** option allows customers to use several discounts at the same time. If selected, this discount can be used simultaneously with other ones.
  > [!NOTE]
  >
  > This feature only works for discounts of the same type. Right now, discounts of distinct types are already cumulative.

- You can also limit the number of times the discount will be used. From the **Discount limitation** dropdown list, select the required limitation regarding the discount:
  - *Unlimited*.
  - *N times only*: select this option and enter the number of times this discount will be available.
  - *N times per customer*: select this option and enter the number of times this discount will be available for one customer.

- In the **Maximum discounted quantity** field (only visible when the **Discount type** is set to *Assigned to products*, *categories* or *manufacturers*), specify the maximum product quantity that can be discounted. It can be used for scenarios like "buy 2 and get 1 for free."
- Enter the **Admin comment** if needed. It won't be visible to customers.

Click **Save** to save the changes or **Save and continue edit** to proceed to editing other panels.

## Adding discount requirements

After creating the discount, it's allowed to add discount requirements if you want some specific rules to be applied to the discount.
Set up requirements if you want to limit the discount to certain user categories depending on a customer role, the amount spent or others. You can use a single requirement type or group several types and apply them simultaneously.

To add discount requirements, go to the *Requirements* panel:

![Requirements](_static/discounts/requirements.jpg)

To add a new requirement, select the **Discount requirement type** from the dropdown list.

- There is one requirement type available in nopCommerce out of the box: *Must be assigned to a customer role*. This allows you to configure discounts for certain customer groups (roles). Other requirements are available as plugins on our [marketplace](https://www.nopcommerce.com/en/extensions?searchterm=discount+requirement&category=discounts-promotions).

- Also, you can create a group of requirements to deal with complex requirements with multiple rules. Requirement group is a useful feature for creating discount requirement templates. You can create a requirement group just once and then use it whenever you want this limitation to be applied. You can include one requirement group into another if needed.
  The requirements are set using Boolean logic. For instance, if you want the discount to be assigned to a particular customer role or in case a customer had spent a certain amount. Such requirements and more are available as plugins on our [marketplace](https://www.nopcommerce.com/en/extensions?searchterm=discount+requirement&category=discounts-promotions).

![Group requirements](_static/discounts/discount-requirenents-group.png)

You can set an unlimited number of requirement groups, one inside another. For example, more complex case, when you want your customers to get a discount if they are vendors and have spent a certain amount or if they are forum moderators and they put a certain product to the cart at the same time.

When customers apply a discount while proceeding to the checkout, it will be displayed as follows:

![discount-in-shopping-cart](_static/discounts/discount-in-shopping-cart.png)

![coupone-code](_static/discounts/coupone-code.png)

## Common type of discounts

### Black Friday sale

Black Friday is always the day after Thanksgiving. It's a quite common discount campaign; almost every online store has a Black Friday sale.

![Black Friday](_static/discounts/Black-friday.png)

- **Name** — you can enter any name; it's internal only.
- **Discount type** — here, it's *Assigned to subtotal* type when a discount is applied to the entire order amount before all the fees are added (like shipping fee and taxes). It's relevant here because we want all the products in the cart to be discounted.
- We can apply the discount in % or just enter the amount in chosen currency. We have 10% here.
- The **Maximum discount amount** can be limited as well, so even if the total cost of products in the cart is $300, a customer will only have a $10 discount anyway.
- This discount will require a **Coupon code**. You can apply discounts without entering coupon codes, but it's not recommended considering marketing purposes. Coupon codes allow you to check campaigns' results.
- Discounts are often time-based. Here, we entered Black Friday weekend dates in the **Start date** and **End date** fields.
- **Cumulative with other discounts** option allows customers to use several discounts at the same time.
- The last setting is for **Discount limitation** usage. For instance, this discount can be applied once per customer.

### Buy one item and get 50% off the second

Often, you need to sell more items of a certain product. In this case, to encourage your customers to buy several items, you can offer them a discount. Let's see how to use the discount "Buy one item and get 50% off the second" in your nopCommerce store.

![Edit discount details](_static/discounts/buy_1.png)

- The **Discount type** is *Assigned to products*. In the *Applied to products* panel, you can add them; here, it will be an "Oversized T-shirt."
- We want our customers to get a 50% discount on the 2nd T-shirt.
- This discount can be used once per customer, so the **Maximum discounted quantity** is 1.
- You can set up requirements for the product quantity in the *Requirements* panel. Add a requirement type *Customer has all of these products* and add a T-shirt with a quantity 2. This requirement type can be downloaded as a plugin [here](https://www.nopcommerce.com/en/has-all-products-discount-requirement-rule).     Read about how to install plugins in the [Plugins](xref:en/getting-started/advanced-configuration/plugins-in-nopcommerce) section.

![Buy one item and get 50% on the second](_static/discounts/buy_2.png)

You can use this scenario to set up another popular discount, "Buy one and get the second for free," if you set up a 100% discount.

## See also

- More plugins on discount types and discount requirement types on the [nopCommerce marketplace](http://www.nopcommerce.com/marketplace)
- How to [install plugins](xref:en/getting-started/advanced-configuration/plugins-in-nopcommerce)

## Tutorials

- [Using discounts in nopCommerce](https://www.youtube.com/watch?v=cAXxnV79dzw&index=7&list=PLnL_aDfmRHwsbhj621A-RFb1KnzeFxYz4)
- [Configuring discounts with boolean logic](https://www.youtube.com/watch?v=gBtZG3OcjnQ)
