---
title: Recurring product
uid: en/running-your-store/catalog/products/recurring-products
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.dunaenko, git.IvanIvanIvanov, git.mariannk
---

# Recurring products

Recurring product type is usually used for subscriptions or products with installment payment plans. In case your product is recurring, select the corresponding checkbox in the *Recurring product* panel.

![Recurring](_static/recurring-products/recurring.png)

Define the following details:

- **Cycle length**. It is a time period in which the recurring order can be repeated.
- **Cycle Period** in *Days*, *Weeks*, *Months* or *Years*. It defines the units of the time period.
- **Total cycles** is the number of times a customer will receive the recurring product.

You can define a recurring cycle for any product in order to enable the system to create repetitive orders automatically. In this case, whenever the payment must be made, the system will use the payment details of the initial order for the subsequent recurring orders. In addition, the original shipping charges will apply to the subsequent orders.

> [!NOTE]
>
> At least one of the active payment modules should support recurring payments.

## See also

- [Payment methods](xref:en/getting-started/configure-payments/payment-methods/index)
