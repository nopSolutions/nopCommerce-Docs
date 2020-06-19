---
title: Discounts
uid: en/running-your-store/promotional-tools/discounts
author: git.AndreiMaz
contributors: git.exileDev, git.DmitriyKulagin, git.IvanIvanIvanov
---

# Discounts

In nopCommerce you can use discounts to enable access to special offers. They can be applied to certain categories, products, to the total amount and so on. You can use different requirements available out of the box and via plugins from [nopCommerce marketplace](http://www.nopcommerce.com/marketplace) for applying to your discounts.

Products in nopCommerce can have any number of discounts attached. In case of several discounts applied, nopCommerce will automatically calculate the best possible price for the customer based on all the available discounts and group memberships.

The most common way of using the discounts is a coupon code. The coupon code is entered by your customer on the order page just before checking out.

The top area of the page enables you to search for a discounts by different search criteria:

- **Discount name**  full discount  name (or a part discount  name)
- **Coupon code**
- **Discount type**
- **Start date** and **End date** for discounts created between these dates

> [!NOTE]
> 
> By default, there are no discounts available in nopCommerce, so you can create them from scratch and follow your own marketing strategy.

![discounts](_static/discounts/discounts.png)

## Adding new discount

To add new discount go to **Promotions → Discounts** and click **Add new**.

![discount-info](_static/discounts/discount-info.png)

Enter the **Name** of the discount.

From the **Discount type** dropdown list, assign the discount to the required option, as follows:

- **Assigned to order total**: These discounts are applied to the entire customer order (order total)

    ![discount-assigned-to-products](_static/discounts/discount-applied-to-product.png)
- **Assigned to products**: After this discount is created, the store owner has to assign this discount to a product (on the product details page)
- **Assigned to categories**: After this discount is created, the store owner has to assign this discount to a category appearing in the Discount applied to the category tab (category details page). This enables the discount to be applied to all products in this category
- **Assigned to manufacturers**: After this discount is created, the store owner has to assign this discount to a manufacturer appearing in the Discount applied to the manufacturer tab (manufacturer details page). This enables the discount to be applied to all products in this manufacturer
- **Assigned to shipping**: These discounts are applied to the shipping fee
- **Assigned to order total**: These discounts are applied to the order subtotal value.

If you want to apply a **Percentage discount** to the order select the Use percentage checkbox. Also here you can add the maximum discount to limit the discount amount.

Or you can apply **Discount amount** to the order or SKU.

If you want to specify the **Start date** and the **End date** for your discount, select them in the calendar field in Coordinated Universal Time (UTC).

Select the **Requires coupon code** checkbox, to enable a customer to supply a coupon code for the discount to be applied.

After ticking the checkbox the **Coupon code** option appears. You can enter the **required coupon code** in this field. This enables customers to enter this coupon code during checkout to apply the discount.

> [!TIP]
> 
> Customers can apply an unlimited number of **coupon codes** to one order if they fit it.

You can also limit the number of times the discount will be used.  From the Discount limitation dropdown list, select the required limitation regarding the discount:

- **Unlimited**
- **N Times only**: Select this option and enter the number of times this discount will be available.
- **N Times per Customer**: Select this option and enter the number of times this discount will be available for one customer.

> [!TIP]
> 
> In **Maximum discounted quantity** (only applies when Discount type is Assigned to Products or Categories) specify the maximum product quantity which could be discounted. It can be used for scenarios like "buy 2 get 1 free". Click **Save**.

## Adding discount requirements

After creating the discount it's allowed to add **discount requirements** if you want some specific rules to be applied to the discount. Select **Requirements tab**.

![requirements](_static/discounts/Requirements.png)

There are several requirement types available in nopCommerce **out of the box**:

- Must be assigned to a customer role
- Customer had spent a certain amount
- Customer has all of these products in the cart
- Customer has one of these products in the cart

Also, you can create a **group of requirements** to deal with complex requirements with multiple rules. The requirements are set using boolean logic. For instance, if you want the discount to be assigned to a particular customer role or in case a customer had spent a certain amount.

![group-requirements](_static/discounts/discount-requirenents-group.png)

You can set an unlimited number of requirement groups, **one inside another**. For example, more complex case, when you want you customers get a discount if they are vendors and had spent a certain amount or if they are forum moderators and they put a certain product to the cart at the same time.

When customers apply a discount while proceeding to the checkout, it is displayed, as follows:

![discount-in-shopping-cart](_static/discounts/discount-in-shopping-cart.png)

![coupone-code](_static/discounts/coupone-code.png)

## See also

- More plugins on discount types and discount requirement types on [nopCommerce marketplace](http://www.nopcommerce.com/marketplace)
- How to [install plugins](xref:en/getting-started/advanced-configuration/plugins-in-nopcommerce)
- [Common types of discounts](xref:en/running-your-store/promotional-tools/discounts)

## Tutorials

- [Using discounts in nopCommerce](https://www.youtube.com/watch?v=cAXxnV79dzw&index=7&list=PLnL_aDfmRHwsbhj621A-RFb1KnzeFxYz4)
- [Configuring discounts with boolean logic](https://www.youtube.com/watch?v=gBtZG3OcjnQ)


# Common type of discounts

## Black Friday sale

Black Friday is always the day after Thanksgiving. It's quite common discount campaign, almost every online store has a Black Friday sale.

![black-friday](_static/discounts/Black-friday.png)

**Name** - you can enter any name, it's only internal

**Discount type** - here it's Assigned to subtotal type when a discount is applied to the whole order amount before all the fees are added (like shipping fee and taxes). It's relevant here because we want all the products in the cart to be discounted.

Besides Assigned to subtotal there are several other discount types. There can be Assigned to order total - when a discount is applied to the whole order amount after all the fees are added (like shipping fee and taxes). Also, we can apply discounts to certain categories, manufacturers, or shipping.

We can apply the discount in % or just enter the amount in chosen currency. We have 10% here.

The **maximum amount** can be limited as well, so even if the total amount of products in the cart will be 300$ a customer will only have 10$ discount anyway.

This discount will require a coupon code. You can apply discounts without entering coupon codes, but it's not recommended considering marketing purposes. Coupon codes allow you to check campaigns results.

Frequently discounts are time-based. Here we entered Black Friday weekend dates.

**Cumulative with other discounts** option allows customers use several discounts at the same time.

The last setting is for limitation discount usage. For instance, this discount can be applied once per customer.

Often you need to sell more items of the certain product. In this case, to encourage your customers to buy several items of that, you can offer them a discount.  Let's consider how to use the discount - Buy one item and get 50% on the second in your nopCommerce store.

![edit-discount-details](_static/discounts/buy_1.png)

The type of the discount is Assigned to products. In Applied to products tab you can add them, here will be an oversized T-shirt. We want our customers to get 50% discount on the 2nd t-shirt. This discount can be used once per customer, so the maximum quantity is 1.

You can set up requirements to the product quantity in Requirements tab. Add requirements type Customer has all of these products and add T-shirt with a quantity 2.

![buy-1-get-discount](_static/discounts/buy_2.png)

You can use this scenario to set up other popular discount buy one and get the second for free if you set up a 100% discount.
