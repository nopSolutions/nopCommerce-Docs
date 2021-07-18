---
title: Customer roles
uid: en/running-your-store/customer-management/customer-roles
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.ivkadp, git.mariannk
---

# Customer roles

The customer roles in nopCommerce enable you to form groups of your web store users. You can create various groups such as store admins, shoppers, [vendors](xref:en/running-your-store/vendor-management), and other. You can also grant these groups certain rights such as discounted pricing, and other special statuses (such as tax exemption, free shipping, and more) using the [access control list](xref:en/running-your-store/customer-management/access-control-list).

To manage customer roles go to **Customers → Customer roles**. The *Customer roles* window is displayed, as follows:

![Customer roles](_static/customer-roles/customerroles1.png)

Click **Add new** to add a new customer role. The *Add a new customer role* window is displayed:

![Add a new customer role](_static/customer-roles/customerroles2.png)

Define the following information:
* **Name** of the customer role.
* Tick the **Active** to make this role active.
* Tick the **Free shipping** checkbox, to enable customers with this role to get free shipping on their orders.
* Tick the **Tax exempt** checkbox, to enable customers with this role to make tax-free purchases.
* Tick the **Override default tax display type** and select from **Default tax display type** drop-down list one of the tax types:
  * *Including tax*
  * *Excluding tax*
* Tick the **Enable password lifetime**, to force customers to change their passwords after a specified time.
* **Purchased with product**. Click the **Choose product** button to choose a special product. A customer is added to this customer role once this product is purchased (paid). 
  > [!NOTE]
  >
  > In case of refund or order cancellation you must manually remove a customer from this role.

* **Is system role**. This setting shows whether this role is used in the code. It is predefined and cannot be modified.
* **System name** of the customer role.

Click **Save**.

## Tutorials

* [Overview of customer roles](https://www.youtube.com/watch?v=3vdIDNIYFIQ)
* [Recovering back a deleted admin user](https://www.youtube.com/watch?v=D45WkrbaA38)
