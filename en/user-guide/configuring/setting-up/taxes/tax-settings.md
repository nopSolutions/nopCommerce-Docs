---
title: Tax Settings
uid: en/user-guide/configuring/setting-up/taxes/tax-settings
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Tax Settings

This section describes your store tax settings, for example, defining prices including or excluding tax, defining the tax display type and more.

To manage your tax settings, go to **Configuration → Settings → Tax Settings**.

![Tax Settings](_static/index/tax-settings.png)

Define the following **tax settings**:

* Tick the **Allow customers to select tax display type** checkbox, to indicate whether customers are allowed to selected the tax display type. When unticked the following dropdown list is displayed:
  * **Excluding tax**: Select to enforce excluding tax.
  * **Including tax**: Select to enforce including tax.
* Tick the **Prices include tax** checkbox, to indicate entered prices include tax.
* Tick the **Display tax suffix** checkbox, to display the tax suffix (incl. tax\excl. tax).
* Tick the **Display all applied tax rates** checkbox, to display all applied tax rates on a separate line in the shopping cart page.
* Tick the **Hide zero tax **checkbox, to hide the zero tax value in the order summary.
* Tick the **Hide tax in order summary** checkbox, to hide the tax value in the order summary when prices are shown as tax inclusive.
* Tick the **Force tax exclusion from order subtotal** checkbox, to always exclude tax from the order subtotal (irrelevant to the selected tax display type). This checkbox affects only pages where the order totals are displayed.
* From the **Tax based** on dropdown list, select the required option on which the tax is based, as follows:
  * **Billing Address**. When this option is selected, the tax is based on the customer billing address. If the billing address is unknown, the default address is used (entered below).
  * **Shipping Address**. When this option is selected, the tax is based on customer shipping address. If the shipping address is unknown, the default address is used (entered below).
  * **Default Address**. When this option is selected, the tax is based on the default address that is entered below.

Define the default tax address, as follows:

* Select the **Country**
* Select the **State/Province**
* Enter **Zip / Postal code**

Tick the **Shipping is taxable** checkbox, to indicate the shipping is taxable. The following fields are then displayed:

* **Shipping price includes tax**: Select to indicate the shipping price includes tax.
* **Shipping tax category**: Select the required tax category used for the shipping tax calculation.

Tick the **Payment method additional fee** is taxable checkbox, to indicate the payment method additional fee is taxable. The following options are then displayed

* **Payment method additional fee includes tax**: Select to indicate the Payment method additional fee is taxable.
* **Payment method additional fee tax category**: From the dropdown list, select the required tax category used for the Payment method additional fee tax calculation.

Tick the **EU VAT enabled** checkbox, to indicate European Union Value Added Tax is enabled. When this option is selected, customers will be requested for the Company VAT number during registration or on the customer account details page. This VAT number can be automatically validated through a web service, if the Use web service checkbox is ticked, or manually on the customer details page in the administration area by the store owner.

* **Your shop country**: From the dropdown list, select the country where your store is located.
* **Allow VAT exemption**: Tick this checkbox, to exempt eligible VAT registered customers from VAT.
* **Assume VAT always valid**: Tick this checkbox, to skip VAT validation. Entered VAT numbers will always be valid. It is the client's responsibility to provide the current VAT number.
* **Use web service:** Tick this checkbox, to use the WEB service to validate VAT numbers.
* **Notify admin when a new VAT number is submitted**: Tick this checkbox, to receive a notification by email, when a new VAT number is submitted.

> [!NOTE]
> 
> If VAT is enabled, then it charges 0% tax to shipping outside the EU and 0% to those who have supplied a validated and approved VAT number and are shipping within the EU but outside the shop country. Refer to an article for further information on the EU VAT

Click **Save**.

## Tutorials

* [Managing tax settings](https://www.youtube.com/watch?v=8iF5nQvIoLs&feature=youtu.be)
