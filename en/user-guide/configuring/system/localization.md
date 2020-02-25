---
title: Localization
uid: en/user-guide/configuring/system/localization
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Localization

## Import language pack

If you wish to add a new language to your store, you should:

1. Visit nopCommerce [translations](https://www.nopcommerce.com/translations) page.
1. Choose nopCommerce version and download the desired language pack.
1. Go to **Configuration → Languages** and press **AddNew** button.

    ![LanguageAddNew](_static/localization/language-add-new.png)

1. Fill in the required fields and click **Save and Continue Edit**.

    ![LanguageSave](_static/localization/language-save.png)

1. Click **Import resources**. And specify the path to the language pack file (*.xml) that you downloaded.

    ![LanguageImport](_static/localization/language-import.png)

If you found a mistake in translation or wish to have custom naming, you can edit the string resources.

## Manage string resources

Go to **Configuration → Languages**. The Languages window is displayed:

![Languages](_static/localization/languages.png)

Click Edit, beside the language. In the **Edit Language Details window**, select the **String resources panel**.

> [!TIP]
> 
> For example, you want to change the name of a panel on top of the page from “*Administration*” (on the picture below) to “*Control panel*”.
> 
> ![Example 1](_static/localization/lang-example-before-change.jpeg)
> 
> In the Resource name field, enter administration. The required string resource if found. Click Edit beside it.
> 
> Enter the new name in the **Value** field and click **Update**.
> 
> ![Example 2](_static/localization/lang-resource-edit.png)
> 
> The changes are implemented
> 
> ![Example 3](_static/localization/lang-example-after-change.jpeg)

Click the **Add new record** to add a new string resource. The window is expanded enabling you to add a new resource record to the grid, as follows:

![Add new record](_static/localization/lang-add-resource.png)

- In the Resource name field, enter the resource string identifier
- In the Resource value field, enter a value for this resource string identifier

Click **Save**.

## Localization settings

![Localization settings](_static/localization/lang-localization-settings.png)

To configure localization settings go to **Configuration  → Settings  → General settings**

- Tick the checkbox **Load all local resources**  to load all local resources on application startup
- Tick the checkbox **Load all localized properties on startup** to load all localized properties on application startup
- Tick the checkbox **Load all search engine friendly names on startup**  to load all search engine friendly names on application startup
- Tick the checkbox **Use images for language selection** to use images instead of language names
- Tick the checkbox **SEO friendly URLs with multiple languages enabled** to allow SEO friendly URLs for all languages
- Tick the checkbox **Automatically detect language** for detecting language based on customer browser settings.
