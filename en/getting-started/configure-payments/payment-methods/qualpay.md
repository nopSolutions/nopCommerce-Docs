---
title: Qualpay
uid: en/user-guide/configuring/setting-up/payments/methods/qualpay
author: git.AndreiMaz
contributors: git.exileDev, git.ivkadp
---

# Qualpay

To configure **Qualpay** plugin as a payment method follow these steps:

1. If you don't have a **Qualpay** account, use the links below to create one.

    * [Sign up for a Qualpay sandbox account](https://app-test.qualpay.com/login/signup).

        ![qualpay1](_static/qualpay/Qualpay1.png)
    * [Apply for a Qualpay production account](https://www.qualpay.com/get-started/nopcommerce).
1. Log into either the [Qualpay sandbox](http://app-test.qualpay.com/) or [Qualpay production](http://app.qualpay.com/) environment to retrieve your security keys.
1. On the main menu to the left, select **"Administration"** then **"API Security Keys".**

    ![qualpay2](_static/qualpay/Qualpay2.png)

1. Click **Create Security Key** and specify the API profile label
1. In **"Manage Access"** enable the following API's

    ![qualpay3](_static/qualpay/Qualpay3.png)

    * Payment Gateway API
    * Embedded Fields API
    * Customer Vault API
    * Recurring Billing API
    * Webhook API
1. Click **"Generate".**
1. Save the configuration.
1. In the **"Key Detail"** section, locate your **Merchant ID** and click the clipboard icon to copy your merchant ID. Click on the clipboard icon to copy your Security Key.
1. To configure plugin in the nopCommerce admin panel go to **Configuration → Payment methods** → click **Configure** for **Payments.Qualpay.** Paste your **Merchant ID** below in the Merchant ID field.
1. Paste your **Security Key** below.

    ![qualpay4](_static/qualpay/Qualpay4.png)

**Save** the configuration.

> [!NOTE]
> 
> For this plugin to work properly ensure that you have set your primary store currency to USD.
> 
> If you’d like to display Qualpay Customer Vault details on a customer details page, make sure that the Qualpay Widget is activated on your Widgets page.

Qualpay Sandbox (test) or Production (live) are unique.  For example, you may not use a Sandbox (test) Merchant ID and Security Key to run Production (live) transactions.
