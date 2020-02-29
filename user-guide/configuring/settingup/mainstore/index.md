---
title: Setting up Main Store
author: AndreiMaz
uid: user-guide/configuring/settingup/mainstore/index
---
# Setting up Main Store

By default nopCommerce installation, only one store is created and needs to be configured, as described below.

To set up the default store, go to **Configuration → Stores.**

![mainstore](_static/index/mainstore.png)

Click **Edit** beside a default store to configure it.

![editstore](_static/index/Store-Edit.png)

Configure your **main store details**, as follows:

* Define the **Store name.**
* Enter the **Store URL** of your store.
* Select the **SSL enabled** checkbox if your store is SSL secured. SSL (Secure Sockets Layer) is the standard security technology for establishing an encrypted link between a web server and a browser. This link ensures that all data passed between the web server and browsers remain private and integral. SSL is an industry standard and is used by millions of websites in the protection of their online transactions with their customers. Tick this option only after you have installed the SSL certificate on your server. Otherwise, you won’t be able to access your site and will have to manually edit the appropriate record in your database ([Store] table).
* The **HOST values** field is a list of possible HTTP_HOST values of your store (for example, `yourstore.com`, `www.yourstore.com`). Filling this field is only required when you have a [multi-store solution](xref:user-guide/configuring/settingup/mainstore/multiple-store)
* Choose **Default language** of your store. You may also leave it unselected. In this case, the first one (with the lowest display order) will be used.
* Define **Company name.**
* Define **Company address.**
* Set your **Company phone number.**
* In the **Company VAT field**, enter VAT of your company (used in EU)

## See also

* [Setting up Multiple-Store](xref:user-guide/configuring/settingup/mainstore/multiple-store)
* [Countries](xref:user-guide/configuring/settingup/mainstore/countries)
* [Languages](xref:user-guide/configuring/settingup/mainstore/languages)
* [Security Settings](xref:user-guide/configuring/settingup/mainstore/security-settings)
* [PDF settings](xref:user-guide/configuring/settingup/mainstore/pdf-settings)
* [GDPR settings](xref:user-guide/configuring/settingup/mainstore/gdpr-settings)
