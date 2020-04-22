---
title: Manual (Fixed or By Weight and By Total)
uid: en/getting-started/configure-shipping/shipping-providers
author: git.AndreiMaz
contributors: git.rajupaladiya, git.DmitriyKulagin, git.exileDev
---

# Manual (Fixed or By Weight and By Total)

Manual (Fixed or By Weight and By Total) shipping allows setting fixed fees or calculating fees by weight and by total to all predefined shipping methods.

## Define Manual (Fixed or By Weight and By Total) shipping

Go to **Configuration → Shipping → Shipping Providers**. The Shipping providers window is displayed:

![manual shipping methods](_static/index/manual-shipping-rate-methods.png)

Enable manual shipping rate computation method, as follows:

* In the **Manual (Fixed or By Weight and By Total)** row, click the Edit button
* In the **Is active** column, check the checkmark
* Click **Update**. The false option becomes true

Click **Configure** beside the Manual (Fixed or By Weight and By Total) option in the list.

You can switch Fixed rate shipping fee calculation to Shipping by weight and by total calculation by clicking button at the top of the page.

### Fixed rate

![Manual configure](_static/index/manual-shipping-confugure.png)

Click **Edit** beside a shipping method and enter the fixed rate for it.

Click **Update**.

> [!NOTE]
> 
> you can add/remove shipping methods in the Shipping methods window, accessed by clicking ![button](_static/index/manual-shipping-manage-button.png) and restrict some methods for chosen countries by clicking ![restrictions](_static/index/manual-shipping-restrictions.png)

### By weight/Total

![by weight](_static/index/manual-shipping-by-weight-total.png)

The **shipping by weight and by total** option allows setting different shipping fees based on a shipment weight and total. The ability to charge different fees depending on the weight and total of the shipment helps to keep the company's shipping costs down when heavy items are shipped, yet offer reasonable shipping costs to customers who purchase light products.

Use formula **[additional fixed cost] + ([order total weight] - [lower weight limit]) &times; [rate per weight unit] + [order subtotal] &times; [charge percentage]** to calculate the fees, where:

* **additional fixed cost** - is the cost of shipment in case the weight is under a certain level (lower weight limit)
* **rate per weight unit** - is the cost of each weight unit above the lower weight limit
* **order subtotal and charge percentage** - are parameters for calculating the extra cost based on the order subtotal

For example, if you have the following shipping conditions:

* from weight 0 to 1 pounds and order subtotal from 1$ and order subtotal to 10$ the cost is 10$. You should create the **following shipping rules**:
  * Order weight from: **0**
  * Order weight to: **1**
  * Order subtotal from: **1**
  * Order subtotal to: **10**
  * Additional fixed cost: **10**
  * Lower weight limit: **0**
  * Rate per weight unit: **0**
* from weight 1.1 pounds to 2 pounds and order subtotal from 11$ and order subtotal to 1000000$ the cost is 15$. You should create the **following shipping rules**:
  * Order weight from: **1.000**
  * Order weight to: **2**
  * Order subtotal from: **11**
  * Order subtotal to: **1000000**
  * Additional fixed cost: **15**
  * Lower weight limit: **0**
  * Rate per weight unit: **0**
* over 2 pounds the cost is 3$ per each additional 0.5 pounds.You should create the **following shipping rules**:
  * If your fixed cost is $15 and $6 per pounds over 2 pounds.
  * Order weight from: **2.0001**
  * Order weight to: **99999**
  * Additional fixed cost: **15**
  * Lower weight limit: **2**
  * Rate per weight unit: **3**
  
	> [!NOTE] 
	> 
	> it will charge proportionally for additional weight; 
	> example for 2.1 pounds it will charge $15 + (0.1 * 6)= $15.6

To add a new shipping rule, click **Add record**. The Add new record window is displayed:

![Add rule](_static/index/manual-shipping-add-new.png)

**Define the following information**:

* **Store** in which the calculated fees will be applied. Choose * to apply the rules to all stores.
* **Warehouse** from which the shipping will be done. Choose * to apply the rules to all warehouses.
* **Country, State/Province, ZIP** of a shipment destination.
* Select a **Shipping Method** from the list of precreated options. Learn here how to create shipping methods.
* Create your weight configuration by filling **Order weight** from and Order weight to first. If the customer’s shipment weight falls into this range, the additional cost will be fixed and will be calculated according to this record.
* Configure the pricing rules for this record using fields **Order subtotal from, Order subtotal to, Additional fixed cost, Lower weight limit, Rate per weight unit, Charge percentage of subtotal**.

> [!NOTE]
> 
> make sure that you settings "**Configuration → Settings → Shipping Settings → Consider associated products dimensions and weigh**" is true.

Click **Save**.

> [!NOTE]
> 
> you can add/remove shipping methods in the Shipping methods window, accessed by clicking ![button](_static/index/manual-shipping-manage-button.png) and restrict some methods for chosen countries by clicking ![restrictions](_static/index/manual-shipping-restrictions.png)
> 
> If you wish to limit your customers only to methods configured on that screen tick this checkbox on the bottom of the page.

## See also

* [Shipping Methods (Shipping by weight and by total)](xref:en/user-guide/configuring/setting-up/shipping/providers/manual/methods)
* [Shipping Method Restrictions (shipping by weight and by total)](xref:en/user-guide/configuring/setting-up/shipping/providers/manual/restrictions)
* [Shipping Methods (Fixed rate)](xref:en/user-guide/configuring/setting-up/shipping/providers/manual/fixed-rate-methods)
* [Shipping Method Restrictions (Fixed rate)](xref:en/user-guide/configuring/setting-up/shipping/providers/manual/fixed-rate-restrictions)

## Tutorials

* [Configuring manual shipping method](https://www.youtube.com/watch?v=1nYj0NqVUWw&t=8s)
