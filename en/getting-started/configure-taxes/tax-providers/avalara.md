---
title: Avalara
uid: en/getting-started/configure-taxes/tax-providers/avalara
author: git.AndreiMaz
contributors: git.exileDev, git.mariannk, git.RomanovM, git.DmitriyKulagin
---

# Avalara tax provider

## Connect to AvaTax

After installing the Avalara tax provider, you need to configure the integration.

> [!NOTE]
>
> Make sure that the plugin is installed and selected as "Enabled" (**Configuration → Local plugins**). To enable the plugin, click **Edit** and select the **Is enabled** checkbox.

![Local plugins](_static/avalara/local-plugins.png)

To configure the Avalara tax provider, go to **Configuration → Tax providers**.

![Tax Providers](_static/avalara/tax-providers.png)

Click **Mark as primary provider**.

Click **Configure** beside the Avalara tax provider option in the list.

Follow the instructions at the top of the page to create an account.
Then configure the plugin; each field's function will be annotated when "?" is hovered on.

1. Configure your AvaTax credentials:

    ![Configuration Credentials](_static/avalara/avalara-configuration-common.png)

    * **Account ID**: provided during your AvaTax account activation process.
    * **License key**: provided during your AvaTax account activation process.
    * **Company**: company profile identifier in the AvaTax admin console.
    * **Use sandbox** is enabled to commit a test transaction.
    * **Enable logging** enables logging of all requests to the Avalara services.

2. Configure tax calculation settings:

    ![Configuration Tax](_static/avalara/avalara-configuration-tax-calculation.png)

    * **Commit transactions** is enabled to commit transactions right after they are saved.
    * **Validate address** is enabled to validate the address entered.
    * **Tax origin address** is used for tax requests to the Avalara services.
    * **Use tax rate tables to estimate** determines whether to use tax rate tables to estimate. This will be used as a default tax calculation for catalog pages and adjusted and reconciled to the final transaction tax during checkout. Tax rates are looked up by ZIP Code (US only) in a file periodically updated by Avalara (see Schedule tasks).
    * **Tax rates by address only** is shown if the previous setting is clear. Enable it to get tax rates by the address only (reduces the number of API calls but may bias results).

3. Configure tax exemption settings:

    ![Configuration Exemption](_static/avalara/avalara-configuration-exemption.png)

    * **Enable exemption certificates** is enabled to activate tax exemption certificates.
    * **Limited to customer roles** is used to restrict customers who have access to this functionality.
    * **Auto validate certificates** is enabled to automatically validate new uploaded/created certificates.
    * **Allow edit info** is enabled to allow customers to edit their info (such as name, phone, address) when managing certificates.
    * **Display 'No valid certificates' message** is enabled to display a message that there are no valid certificates for the customer on the order confirmation page.

    ![Configuration Exemption None](_static/avalara/avalara-configuration-exemption-none.png)

    > [!TIP]
    >
    > This message text can be edited in the language resources.

4. **Save** and click the **Test connection** button to perform the test connection.
5. To perform the *test tax calculation*, fill the address form at the bottom of the page (please note that the nopCommerce Avalara tax plugin commits transactions to US addresses only) and click **Test tax transaction**.

    ![Configuration Test](_static/avalara/avalara-configuration-test.png)

## Assign AvaTax code

Navigate to **Configuration → Tax categories**.

At the top right of the page, you will see the branded **Avalara tax codes** button. Clicking it will show the following dropdown menu:

> [!IMPORTANT]
> The **Avalara tax codes** button will only be displayed if the *Avalara* plugin is selected as the main tax provider on the *Configuration → Tax providers page*.

![Tax categories](_static/avalara/tax-categories.jpg)

* **Export tax codes to Avalara** – exports all codes from your store to your AvaTax backend.
* **Import Avalara system tax codes** – imports all AvaTax tax codes from Avalara.
    ![Tax categories import](_static/avalara/tax-categories-import.jpg)
* **Delete Avalara system tax codes** – deletes all codes exported from Avalara.

## Assign an AvaTax system tax code to an item

1. Go to **Catalog → Products**.
1. Select a product to open the product details screen and click **Edit**.
1. On the **Product Details** screen, in the **Price** panel, assign the appropriate code from the dropdown list in the **Tax category** field.

    ![Product tax category](_static/avalara/product-tax-category.png)

    > [!IMPORTANT]
    > Ensure that **SKU** is entered for better navigation in the AvaTax backend.
1. Click **Save**.
1. To see a listing of all available AvaTax system tax codes, visit [http://taxcode.avatax.avalara.com](http://taxcode.avatax.avalara.com).

## Validate a customer address

1. Ensure **Validate address** checkbox is on; in that case, the address will be validated automatically.
1. User will see the following screen:

    ![Validate address](_static/avalara/validate-customer-address.png)

## Tax exemption

There are two ways to enable the tax exemption using this plugin:

1. Assign an AvaTax tax exempt category to a specific customer or an entire customer role in the admin area:

    * Click **Customers → Customers → Edit customer**
    * Find the highlighted **Entity use code** field and select the field, select the appropriate customer type code.

        ![Customer details](_static/avalara/customer-entity-use-code.png)
    * Click **Save**

    > [!NOTE]
    >
    > It is not necessary to select **Tax exempt** checkbox: assigning **Entity use code** is enough.

2. Activate the exemption certificates feature:

    > [!IMPORTANT]
    >
    > You will need a [CertCapture account](https://avlr.co/3bA1P1X) for this feature to work correctly.

    * Ensure that the **Enable exemption certificates** checkbox is on; in that case, customers will be able to manage their tax exemption certificates before making a purchase.
    * A new page will be added in the account section

        ![Tax exemption certificates](_static/avalara/exemption-certificates.png)
