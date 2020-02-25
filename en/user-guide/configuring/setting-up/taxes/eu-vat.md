---
title: EU VAT configuration guide
uid: en/user-guide/configuring/setting-up/taxes/eu-vat
author: git.AndreiMaz
contributors: git.exileDev
---

# EU VAT configuration guide

To set up NopCommerce VAT support for shops in the EU go to **Configuration → Settings → Tax Settings**.

* Set **Tax Based** On to **Shipping Address**.
* Tick **EU VAT enabled**. This will ensure that tax is charged only for shipments within the EU.
* Select the **Country** your shop is in.
* If applicable, tick *Allow VAT exemption*. This will ensure that your VAT registered customers who are shipping within the EU but outside the country in which the store is located will not be charged VAT.
* If you checked **Allow VAT exemption**, then you might want to tick the "**Use web service**" and "**Notify admin when a new VAT number is submitted**" checkboxes too.

Click **Save** button.

Go to **Configuration → Countries**. Check that all the countries which are in the scope of the VAT have "**Subject to VAT**" set to **true**.

> [!NOTE]
> 
> Jersey, Guernsey, and the other Channel Islands are not a part of the UK and are not within the scope of VAT. If you sell to those places you may need to change that.

Go to **Configuration → Tax Categories**. Set up a tax category for each VAT rate in your country. For example, "Standard Rate", "Zero rate", "Discounted rate". Delete default classes that are already there and are not applicable.

Go to **Configuration → Tax Providers**. Make the Fixed Rate Provider the default one and save.

Click **Configure** to edit tax rates that are charged by the **Fixed Rate Provider**. You'll see your VAT rate categories on the Configuration tab. Click **Edit** beside each category and enter the percentage rates.

Make sure that all products have a tax category assigned to them on their [product pages](xref:en/user-guide/running/product-management/products/adding-products/index).
