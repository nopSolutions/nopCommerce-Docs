---
title: UPS
uid: en/getting-started/configure-shipping/shipping-providers/ups
author: git.AndreiMaz
contributors: git.exileDev, git.DmitriyKulagin
---

# UPS

To access your account at UPS, use a username, a password, and an **XML license number**, which you will be provided with after the registration process.

## Define the UPS Real Time Shipping Calculations

1. Create a UPS account by going to [https://www.ups.com/upsdeveloperkit?loc=en_US](https://www.ups.com/upsdeveloperkit?loc=en_US) to receive the following:
    * Username ID
    * Password
    * XML access license number

1. In the nopCommerce admin area, go to **Configuration → Shipping → Shipping providers**.
 ![Shipping rate methods](_static/ups/shipping-rate-methods.jpg)
1. Enable this method as follows:
    * In the UPS (United Postal Service) row, click the **Edit** button.
    * In the Is **active** column, select the check mark.
    * Click **Update**. The *false* option will becomes *true*.

1. Click **Configure** beside the UPS (United Parcel Service) option in the list.
    The *Configure – UPS (United Parcel Service)* window will be displayed as follows: ![Configure page](_static/ups/ups-configure.jpg)

1. Enter the following information obtained from the UPS provider:
    * Select the **Use sandbox** checkbox to use the testing environment.
    * Enter the **Account number** of the UPS provider.
    * Enter the **Access Key** obtained from the provider.
    * Enter your **Username** obtained from the provider.
    * Enter the **Password** obtained from the provider
    * Select your required **UPS Customer Classification** as follows:
        * Rates Associated With Shipper Number
        * Daily Rates
        * Retail Rates
        * Regional Rates
        * General List Rates
        * Standard List Rates
    * Select the required **UPS Pickup Type** as follows:
        * Daily Pickup
        * Customer Counter
        * One Time Pickup
        * On Call Air
        * Letter Center
        * Air Service Center
    * Select the required **UPS Packaging Type** as follows:
        * Unknown
        * Letter
        * Customer Supplied Package
        * Tube
        * P A K
        * Express Box
        * 10 kg Box
        * 25 kg Box
        * Pallet
        * Small Express Box
        * Medium Express Box
        * Large Express Box
    * Select the **Insure package** checkbox, to indicate the package will be insured.
    * Enter **Additional handling charge**. It is an additional fee to charge your customers.
    * Select the **Carrier Services** you want to offer to your customers.
    * Select to get rates for **Saturday Delivery enabled**.
    * Select the **Packing type**, as follows:
        * Pack by dimensions
        * Pack by one item per package
        * Pack by volume
    * Tick the **Pass dimensions** checkbox to pass package dimensions when requesting for rates.
    * Select the **Weight type** – pounds or kilograms.
    * Select the **Dimensions type** – inches or centimeters.
    * Tick the **Tracing** checkbox to record system tracing in the system log. The entire request and response XML will be logged (including AccessKey/Username, Password). Do not leave this enabled in a production environment.

    Click **Save**.
