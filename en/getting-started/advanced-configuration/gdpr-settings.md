---
title: GDPR settings
uid: en/getting-started/advanced-configuration/gdpr-settings
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.ivkadp, git.mariannk
---

# GDPR settings

*GDPR* (General Data Protection Regulation) is a revised European Union's new data privacy law which impacts how all companies collect, use and share personal data of their European customers. The regulation entered into force on May 24, 2016 and applies since May 25, 2018. The regulation is an essential step to strengthen individuals' fundamental rights in the digital age and facilitate business by clarifying rules for companies and public bodies in the digital single market.

For more information (please refer to this source):

[https://ec.europa.eu/info/law/law-topic/data-protection/data-protection-eu_en](https://ec.europa.eu/info/law/law-topic/data-protection/data-protection-eu_en)

## Configure GDPR

To enable GDPR settings on your nopCommerce store go to **Administration → Configuration → Settings → GDPR settings**.

![Configure](_static/gdpr-settings/configure.jpg)

Then tick the **GDPR enabled** checkbox. Additional settings will allow you to capture a log of the following activities:

* **Log "accept privacy policy" consent**.
* **Log "newsletter" consent**.
* **Log user profile changes**.

You can add consents on your nopCommerce site by clicking the **Add consent** button in the *Consents* panel:

![Consents](_static/gdpr-settings/consents.jpg)

To add a new consent you will be redirected to the *Add consent* window:

![Add consent](_static/gdpr-settings/add-consent.jpg)

Define the following consent settings:

* **Message** or question which will be displayed to customers.
* If the consent **Is required**.
* If the consent will be **Displayed during registration**.
* If thes consent will be **Displayed on "customer info" page** in the "My account" section.
* **Display order** is the consent display order. 1 represents the first item in the list.

Here is an example of a consent option on customer info page:

![agreement](_static/gdpr-settings/agreement.png)

If you have enabled the consent log settings then you can see the log activity by going to: **Administration → Customers → GDPR requests (log)**.

![log](_static/gdpr-settings/log.png)

When GDPR setting is enabled, store owner can also perform actions like:

* **Permanent delete** for deletion of customer record.
* **Export data** for exporting customer data.

To do this go to **Administration → Customers → Edit customer** page.

![customerdetails](_static/gdpr-settings/customerdetails.png)

## Tutorials

* [Managing GDPR settings in nopCommerce](https://www.youtube.com/watch?v=6bLc_TDqD18&feature=youtu.be)
