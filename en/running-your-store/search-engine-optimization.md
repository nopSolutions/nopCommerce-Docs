---
title: SEO
uid: en/running-your-store/search-engine-optimization
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# SEO

SEO stands for search engine optimization, it is a process of getting traffic from "free", "organic," "editorial" or "natural" search results on search engines. All major search engines have primary search results, where web pages and other content such as videos or local listings are shown and ranked based on what a search engine considers most relevant to users.

nopCommerce supports **SEO techniques for different types** of pages in your store, find a list of them in **See also** section.

## SEO settings

There are some general SEO settings in nopCommerce, that can be applied to the entire store.

To edit SEO settings, go to **Configuration → Settings → General settings**.

![p1](_static/search-engine-optimization/seo1.png)

* In the **Default title** field, enter the default title for the pages in your store.
* From the **Page title SEO adjustment** field, select the required page title SEO adjustment, as follows:

  * A page name comes after a store name in the title:
  `YOURSTORE.COM` | PAGENAME

  * A store name comes after a page name in the title:
  PAGENAME | `YOURSTORE.COM`

* Specify the **Page title separator**.
* Enter the **Default meta keywords** for the pages in your store. This can be overridden for individual **categories, manufacturers, products** and some other pages.
* Enter the **Default meta description** for the pages in your store. This can be overridden for individual **categories, manufacturers, and products** and some other pages.
* Select the **Generate product META description**, to automatically generate the product META descriptions (if not specified on the product details page) based on the **product's short description**.
* Choose the **WWW prefix requirement**. For example, `http://yourStore.com/` could be automatically redirected to `http://www.yourStore.com/`. Select one of the following options:
    * Doesn't matter
    * Pages should have WWW prefix
    * Pages should not have WWW prefix
* Select the **Convert non-western chars** checkbox, to remove the accent in SEO names. For example, convert é to e.
* Select the **Enable canonical URLs** checkbox, to transform an URL into a canonical URL to enable determining whether two syntactically different URLs may lead to a page with the equivalent content.
* Tick the **Twitter META tags** checkbox to generate Twitter META tags on the product details page.
* Tick the **Open Graph META tags** checkbox to generate Open Graph META tags on the product details page.
* Tick the **Microdata tags** checkbox to generate Microdata tags on the product details page.
* Enter the **Custom &#60;head&#62; tag**. For example, some custom &#60;meta&#62; tag. Or leave empty if ignore this setting.

## SEO panels

There are several types of pages in nopCommerce for which you can set up individual SEO settings, including meta keywords, meta description, meta title and search engine friendly page names. This is done on SEO panels of the corresponding admin area sections.

![p2](_static/search-engine-optimization/seo-panel.jpg)

* Enter the required **Meta keywords**, which are a brief and concise list of keyword and phrases for the page. The meta keywords tag takes the following format:

    ```html
    <meta name="keywords" content="keywords, keyword, keyword phrase, etc.">
    ```

* In the **Meta description** field, enter a description of the page. The meta description tag is a brief and concise summary of your page's content. The meta description tag is in the following format:

    ```html
    <meta name="description" content="Brief description of the contents of your page.">
    ```

* In the **Meta title** field, enter the required title. The title tag specifies the title of your web page. It is a code which is inserted into the header of your web page and is in the following format:

    ```html
    <head>
      <title>Creating title tags for search engine optimization & web usability</title>
    </head>
    ```

* In the **Search engine friendly page name** field, enter the name of the page used by search engines. If you enter nothing then the web page URL is formed using the page name. If you enter custom-seo-page-name, then the following custom the URL will be used: `http://www.yourStore.com/custom-seo-page-name`.

## Search engine friendly page names list

To see all search engine friendly page names used in store, go to **System → Search engine friendly page names**.
![p1](_static/search-engine-optimization/seo-page-names-list.jpg)

Here you can filter search engine friendly page names by **Name**, **Language** or **Is active** property. You can also delete one or multiple selected filter search engine friendly page names using **Delete selected** button. In the **Edit page** column you can see the button used to navigate to the appropriate page.

## See also

* [Adding products](xref:en/running-your-store/catalog/products/add-product-for-beginners)
* [Product categories](xref:en/running-your-store/catalog/categories)
* [Manufacturers](xref:en/running-your-store/catalog/manufacturers)
* [Vendors](xref:en/running-your-store/vendor-management)
* [Topics (pages)](xref:en/running-your-store/content-management/topics-pages)
* [News](xref:en/running-your-store/content-management/news)
* [Blog](xref:en/running-your-store/content-management/blog)

## Tutorials

* [Understanding SEO settings in nopCommerce](https://youtu.be/UxqM_nJyv1Q)
