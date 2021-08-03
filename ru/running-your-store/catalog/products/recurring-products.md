---
title: Recurring product
uid: en/running-your-store/catalog/products/recurring-products
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.dunaenko, git.IvanIvanIvanov, git.mariannk
---

# Recurring products

Recurring product type is usually used for subscriptions or products with installment payment plans. In case your product is recurring, tick the corresponding checkbox in the *Recurring product* panel.

![Recurring](_static/recurring-products/recurring.png)

Define the following details:

- **Cycle length**. It is a time period recurring order can be repeated.
- **Cycle Period** in *Days*, *Weeks*, *Months* or *Years*. It defines units time period will be measured in.
- **Total cycles** is a number of times customer will receive the recurring product.

You can define a recurring cycle to any product in order to enable the system to automatically create repetitive orders. In this case, any time when the payment must be done, the system will use the payment details of the initial order for subsequent recurring orders. In addition, the original shipping charges will apply to subsequent orders.

> [!NOTE]
> 
> At least one of the active payment modules should support recurring payments.

## See also

- [Payment methods](xref:en/getting-started/configure-payments/payment-methods/index)
