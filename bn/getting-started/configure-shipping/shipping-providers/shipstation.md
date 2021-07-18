---
title: ShipStation
uid: en/getting-started/configure-shipping/shipping-providers/shipstation
author: git.mariannk, git.skoshelev
---

# ShipStation

To use **ShipStation** integration plugin please follow these steps:

1. Register or login on the [ShipStation](https://www.shipstation.com/?ref=partner-nopcommerce&utm_campaign=partner-referrals&utm_source=nopcommerce&utm_medium=partner-referral) site.
1. In the nopCommerce admin area go to **Configuration → Shipping → Shipping providers**. 
![Shipping rate methods](_static/shipstation/shipping-rate-methods.jpg)
    * Enable this method, as follows:
        * In the ShipStation row, click the **Edit** button.
        * In the **Is active** column, check the checkbox.
        * Click the **Update** button. The false option becomes **true**.
    * Click the **Configure** button beside the ShipStation option in the list. The *Configure - ShipStation* window is displayed, as follows: ![Configure page](_static/shipstation/shipstation-configure.jpg)
1. Enter the following information obtained from the ShipStation provider:
    * The **API Key** and the **API Secret**: this data is used to get a list of available carriers. You may get them on the *Settings - API Settings* page on the ShipStation site.
    > [!Note]
    > If you do not plan to use the automatic determination of the shipping cost, then entering this data is not necessary.
    > But in this case, the plugin will stop working as a provider of shipping methods and you will need to make sure that there is another active plugin the same type.

    * Create a **Username** and **Password**, enter them into the settings form (the ShipStation form and the nopCommerce form). This data is necessary for the safe transfer of data between your server and the ShipStation server. Always keep them secret.

    > [!Important] 
    > Do not use the ShipStation or nopCommerce user credentials for this fields.

    * Check the **Pass dimensions** checkbox if sending dimensions to the ShipStation server is needed. When this parameter is activated, the additional parameter **Packing type** appears. This parameter is responsible for the type of data sent. ![Packing type](_static/shipstation/packing-type.jpg)