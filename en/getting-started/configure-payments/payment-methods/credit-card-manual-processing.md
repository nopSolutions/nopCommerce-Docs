---
title: Credit card (manual processing)
uid: en/getting-started/configure-payments/payment-methods/credit-card-manual-processing
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.ivkadp, git.mariannk
---

# Credit card (manual processing)

This is a special payment plugin that allows all orders to be successfully entered on the website, but it does NOT really charge a customer or make calls to any live payment gateway. It is recommended to use this payment method if you want to perform one of the following:

* Process all orders offline
* Process them manually via another back-office system
* Test the site end-to-end before going live

To configure this payment method, go to **Configuration → Payment methods**. Then find the **Credit card (Payments.Manual)** payment method in the payment method list:

![List](_static/credit-card-manual-processing/list.jpg)

## Activate the method, edit its name, and display order

You can edit the payment method name, which will be displayed for customers in the public store, or its display order. To do this, click the **Edit** button in the plugin row on the payment method list page. You will be able to enter **Friendly name** and **Display order**. In this row, you can also activate the plugin or make it inactive using the **Is active** field. Click the **Update** button. Your changes will be saved.

## Configure the payment method

On the **Configuration → Payment methods** page, find the **Credit card (Payments.Manual)** payment method and click the **Configure** button. The *Configure - Credit card* window will be displayed as follows:

![manualprocessing](_static/credit-card-manual-processing/manualprocessing.png)

Set up the payment method as follows:

* In the **After checkout mark payment as** field, specify the transaction mode.
* Define the **Additional fee** for using this method.
* In the **Additional fee. Use percentage** field, define whether to apply an additional percentage fee to the order total. If not enabled, a fixed value is used.

Click **Save**.

## Limit to stores and customer roles

You can limit any payment method to store and customer role. This means that the method will be available to certain stores or customer roles only. You can do this from the *plugin list* page.

1. Go to **Configuration → Local plugins**. Find the plugin you want to limit. In our case, it's **Credit card**. To find it faster, use the *Search* panel at the top of the page and search by **Plugin name** or **Group** using the *Payment methods* option.

   ![Plugins](_static/credit-card-manual-processing/plugin.jpg)

1. Click the **Edit** button, and the *Edit plugin details* window will be displayed as follows:

   ![Plugins](_static/credit-card-manual-processing/edit.jpg)

1. You can set up the following limits:

   * In the **Limited to customer roles** field, choose one or several customer roles, that is, administrators, vendors, guests, who will be able to use this plugin. If you don't need this option, just leave this field empty.

     > [!Important]
     > In order to use this functionality, you have to disable the following setting: **Catalog settings → Ignore ACL rules (sitewide)**. Read more about the access control list [here](xref:en/running-your-store/customer-management/access-control-list).

   * Use the **Limited to stores** option to limit this plugin to a certain store. If you have multiple stores, choose one or several from the list. If you don't use this option, just leave this field empty.

     > [!Important]
     > In order to use this functionality, you have to disable the following setting: **Catalog settings → Ignore "limit per store" rules (sitewide)**. Read more about multi-store functionality [here](xref:en/getting-started/advanced-configuration/multi-store).

 Click **Save**.

## Tutorials

* [Configuring Credit card (manual processing) payment method](https://www.youtube.com/watch?v=dN2q27dKvUU)
