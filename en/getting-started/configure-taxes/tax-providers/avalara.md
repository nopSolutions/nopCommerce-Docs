---
title: Avalara tax provider
uid: en/user-guide/configuring/setting-up/taxes/tax/methods/avalara
author: git.AndreiMaz
contributors: git.exileDev
---

# Avalara tax provider

## Connect to AvaTax

After installing the AvaTax integration you need to configure the integration

> [!NOTE]
> 
> Be sure that the plugin is installed in Local Plugins and is checked as Active (Configuration → Plugins). To enable the plugin click Edit  → and check “Is Enabled” Checkbox

![Local plugins](_static/avalara/local-plugins.png)

To configure Avalara tax provider go to **Configuration → Tax Providers**.

![Tax Providers](_static/avalara/tax-providers.png)

Click **Mark as Primary Provider**.

Click **Configure** beside the Avalara tax provider option in the list.

Follow the instructions on top of the page, each field’s function is annotated when “?” is hovered on.

![Configuration](_static/avalara/avalara-configuration.png)

1. Configure your Avalara Tax Credentials:
    * **Account ID**: Provided during your AvaTax account activation process.
    * **License Key**: Provided during your AvaTax account activation process
    * **Company**: Company profile identifier in the AvaTax Admin Console
    * **Use Sandbox** Is enabled to commit test transaction
    * **Commit transactions** Is enabled to commit transactions right after they are saved
    * **Validate address** is enabled to validate address entered
1. **Save** and click the **Test Connection** button to perform test connection
1. To perform the Test Tax Calculation fill the form on the bottom of the page (Please note, that nopCommerce Avalara tax plugin commits transactions to US addresses only) and click **Test Tax Transaction**

## Assign Avalara AvaTax Code

Navigate to **Configuration → Tax Categories**

At the top left right of the page you will see the branded Avalara Tax Codes button. Clicking it the drop-down menu will show the following menu

![Tax categoties](_static/avalara/tax-categories.png)

* **Export tax codes to Avalara** – Exports all codes from your store to your Avalara backend
* **Import Avalara system tax codes** – Imports all Avalara tax codes from Avalara
* **Delete Avalara System tax codes** – Deletes all codes exported from Avalara

## Assign an Avalara tax exempt category to a customer

1. Click **Customers → Customer Details**
1. Find the highlighted **Entity use code** field and select the field, select the appropriate customer type code.

    ![Customer details](_static/avalara/customer-entity-use-code.png)
1. Click **Save**

> [!NOTE]
> 
> It is not necessary to check Tax Exempt check box: Assigning Entity use code is enough

## Assign an AvaTax System Tax Code to an item

1. Go to **Catalog → Products**
1. Select a Product to open the Product details screen.
1. Click **Edit**
1. On the Product details screen, in Prices group assign appropriate code from drop-down list in **Tax Category** field.

    ![Product tax category](_static/avalara/product-tax-category.png)
1. **IMPORTANT:** Ensure that SKU is entered, for better navigation in Avalara backend
1. Click **Save**
1. To see a listing of all available AvaTax System tax codes, click ([http://taxcode.avatax.avalara.com](http://taxcode.avatax.avalara.com)).

## Validate a customer address

1. Ensure Validate Address checkbox is on, in that case the address will be validated automatically
1. User will be show the following screen:

    ![Validate address](_static/avalara/validate-customer-address.png)
