---
title: Tax settings
uid: en/getting-started/configure-taxes/tax-settings
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.mariannk
---

# Tax settings

This section describes your store's tax settings, for example, defining prices including or excluding tax, defining the tax display type, and more.

To manage your tax settings, go to **Configuration → Settings → Tax settings**.

![Tax settings](_static/tax-settings/tax-settings.jpg)

First of all, define **common** tax settings:
* From the **Tax based on** dropdown list, select the required option the tax is based on as follows:
  * **Billing address**. When this option is selected, the tax is based on the customer's billing address. If the billing address is unknown, the default address is used (entered below).
  * **Shipping address**. When this option is selected, the tax is based on the customer's shipping address. If the shipping address is unknown, the default address is used (entered below).
  * **Default address**. When this option is selected, the tax is based on the default address entered below.
* Choose the **Default tax category** for products. It'll be pre-selected on the *Add new product* page.
* The **Tax based on pickup point address** checkbox defines whether the pickup point address should be used for tax calculation when a pickup point is chosen.
* Tick the **Prices include tax** checkbox indicating whether the entered prices include tax.

Then define the **default tax address (used for tax calculation)** as follows:
* Select the **Country**.
* Select the **State/province**.
* Define the **County/region**.
* Define the **City**.
* Define the **Address 1**.
* Enter **Zip/postal code**.

In the **Tax displaying** panel, you can set up how the tax will be displayed for customers:
* Tick the **Allow customers to select tax display type** checkbox to indicate whether customers are allowed to select the tax display type. When unticked, the **Tax display type** dropdown list is displayed:
  * **Excluding tax**: select to enforce excluding tax.
  * **Including tax**: select to enforce including tax.
* Tick the **Display tax suffix** checkbox to display the tax suffix (incl. tax\excl. tax).
* Tick the **Display all applied tax rates** checkbox to display all applied tax rates on a separate line on the shopping cart page.
* Tick the **Hide zero tax** checkbox to hide the zero tax value in the order summary.
* Tick the **Hide tax in order summary** checkbox to hide the tax value in the order summary when prices are shown as tax inclusive.
* Tick the **Force tax exclusion from order subtotal** checkbox to always exclude tax from the order subtotal (irrelevant to the selected tax display type). This checkbox only affects pages with order totals displayed.

In the *Shipping* panel, tick the **Shipping is taxable** checkbox to indicate that the shipping is taxable. The following fields will be displayed:
* **Shipping price includes tax**: select to indicate the shipping price includes tax.
* **Shipping tax category**: select the required tax category used for the shipping tax calculation.

In the *Payment* panel, tick the **Payment method additional fee is taxable** checkbox to indicate the payment method additional fee is taxable. The following options will be displayed:
* **Payment method additional fee includes tax**: select to indicate that the payment method additional fee is taxable.
* **Payment method additional fee tax category**: from the dropdown list, select the required tax category used for the payment method additional fee tax calculation.

Then set up the VAT in the *VAT* panel:
* Tick the **EU VAT enabled** checkbox to indicate that the European Union value added tax is enabled. When this option is selected, customer's *Company VAT number* will be requested during registration or on the customer account details page. This VAT number can be validated automatically through a web service if the **Use web service** checkbox is ticked or manually on the customer details page in the administration area by the store owner.
* **Your shop country**: from the dropdown list, select the country where your store is located.
* **Allow VAT exemption**: tick this checkbox to exempt eligible VAT registered customers from the VAT.
* **Assume VAT always valid**: tick this checkbox to skip VAT validation. Entered VAT numbers will always be valid. It is the client's responsibility to provide the correct VAT number.
* **Use web service**: tick this checkbox to use the web service to validate VAT numbers.
* **Notify admin when a new VAT number is submitted**: tick this checkbox to receive a notification by email whenever a new VAT number is submitted.

> [!NOTE]
> 
> If VAT is enabled, then it charges 0% tax to shipping outside the EU and 0% to those who have supplied a validated and approved VAT number and are shipping within the EU but outside the shop country. Refer to the article for further information on the EU VAT.

> [!TIP]
>
> Read how to set up the EU VAT here: [EU VAT configuration guide](xref:en/getting-started/configure-taxes/index#eu-vat-configuration-guide).

Click **Save**.

## Tutorials

* [Managing tax settings](https://www.youtube.com/watch?v=8iF5nQvIoLs&feature=youtu.be)
