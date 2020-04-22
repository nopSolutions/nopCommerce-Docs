---
title: External Authentication Method
uid: en/user-guide/configuring/system/external-authentication/index
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# External Authentication Method

External authentication methods allow users to login to a nopCommerce site without entering their credentials: email and password. Users can be authenticated using an external site (such as  Facebook, Google, and so on). nopCommerce has a built-in external authentication through Facebook. You can set up other methods using plugins from the [marketplace](https://www.nopcommerce.com/marketplace).

After an external authentication method is configured and marked as active, users will see a new authentication option on the login page.

> [!NOTE]
> 
> You can enable an auto registration for users logged-in using external methods. To do so, go to **Configuration → Settings → Customer Settings**. In the **External authentication settings** section of the Customer Settings tab tick the **Auto register enabled** checkbox.

## Manage the external authentication methods

Go to **Configuration → External Authentication Methods**. The External Authentication Methods window is displayed:

![External auth](_static/index/external-authentication.png)

Click **Edit** beside an authentication method and tick **Is active** to activate the method. Also, define the method display order.

Click **Configure** for the method configuration.

## See also

* [Facebook authentication](xref:en/user-guide/configuring/system/external-authentication/facebook)
