---
title: Configure taxes
uid: en/getting-started/configure-taxes/index
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.mariannk
---

# Configure taxes

This chapter covers the settings of the nopCommerce tax tools.

> [!NOTE]
>
> This chapter includes nopCommerce in-built tax instruments, not third-party tax services.

nopCommerce supports external services as well, but they require plugins from the [marketplace](http://www.nopcommerce.com/marketplace) to be installed. The installation process of such modules is described in the [Plugins](xref:en/developer/plugins/index) chapter.

## EU VAT configuration guide

To set up nopCommerce VAT support for shops in the EU, go to **Configuration → Settings → Tax settings**.

In the *Common* panel:

* Set **Tax based on** to **Shipping address**.

In the *VAT* panel:

* Select **EU VAT enabled**. This will ensure that tax is charged only for shipments within the EU.
* Select the **Country** your shop is in.
* If applicable, select *Allow VAT exemption*. This will ensure that your VAT registered customers who are shipping within the EU but outside the country in which the store is located will not be charged VAT.
* Select the **Assume VAT always valid** checkbox to skip VAT validation. Entered VAT numbers will always be valid. It will be a client's responsibility to provide the correct VAT number.
* If you checked **Allow VAT exemption**, then you might want to select the "**Use web service**" and "**Notify admin when a new VAT number is submitted**" checkboxes too.

Click the **Save** button.

Go to **Configuration → Countries**. Make sure that all the countries in the scope of the VAT have **Subject to VAT** set to *true*.

![Countries](_static/index/countries.jpg)

> [!NOTE]
>
> Jersey, Guernsey, and the other Channel Islands are neither a part of the UK nor within the scope of the VAT. If you sell to those places, you might need to change that.

Go to **Configuration → Tax categories**.

![Tax categories](_static/index/tax-categories.jpg)

Set up a tax category for each VAT rate in your country. For example, "Standard Rate," "Zero rate," "Discounted rate." Delete default classes that are already there but not applicable.

Go to **Configuration → Tax providers**. Select the **Manual (fixed or by country/state/zip)** as the default one using the **Mark as primary provider** button.

![Tax providers](_static/index/tax-providers.jpg)

Click **Configure** in the **Manual (fixed or by country/state/zip)** provider line to edit tax rates. At the top of the page, you will see the switch. Choose **Fixed rate** there.

![Configure](_static/index/configure.jpg)

On this page, you can see your VAT rate categories. Click **Edit** beside each category and enter the percentage rates. Then click the **Update** button.

Make sure that all products have a tax category assigned to them on their [product pages](xref:en/running-your-store/catalog/products/add-products).

![Product](_static/index/product.jpg)

## See also

* [Tax settings](xref:en/getting-started/configure-taxes/tax-settings)
* [Tax providers](xref:en/getting-started/configure-taxes/tax-providers/index)
