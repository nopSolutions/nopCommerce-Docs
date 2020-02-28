---
title: Languages
author: AndreiMaz
uid: user-guide/configuring/settingup/mainstore/languages
---
# Languages

In nopCommerce, your store can have several languages installed. However, the customers will only see the data that has been defined in their selected language.

> [!TIP]
> **By default**, the English language is installed.

![language](_static/languages/Language.png)

> [!NOTE]
> You can download new language packs from the official [Marketplace](http://www.nopcommerce.com/marketplace).

## Adding new language

Click on **Add new** button, In the Add a new language window, **define the following settings:**

![addlanguage](_static/languages/addlanguage.png)

* **Name** - Define name of the new language.

* **Language culture** - a specific language code (for example, de-AT for Austrian German)

* **Unique SEO code** - a two letter language SEO code used to generate URLs like `http://www.yourstore.com/en/` when you have more than one published language. Note: “SEO friendly URLs with multiple languages” option should be enabled in Configuration → Settings → General Settings → Localization settings panel.

* **Flag image file name** - enter the flag image file name. The image should be saved under the …/images/flags directory. You can also choose an image from a predefined list.

* **Right-to-Left** - Tick **Right-to-Left** if needed (for example, for Arabic, Hebrew, etc). Note: The active theme should support RTL (have an appropriate CSS style file). This option affects only the public store.

* **Default currency** - Define currency for a specific language. If not specified, then the first found one (with the lowest display order) will be used.

* **Limited to stores option** - This will allowing to set this language for a specific store(s). You can choose the store(s) from a pre-created list. Leave this field empty if you don’t use this option.

* **Publish** - to enable this language to be visible and selected by visitors in your store.

* **Display order** - Display order of the language. Lower number represents the top of the list.

* Click on **Save** or **Save and Continue Edit** button.

> [!NOTE]
> After adding a new language, the Import resources from XML and the Export to XML buttons appear enabling you to import and export all resources for a new language.
