---
title: SEO
uid: en/user-guide/marketing/content/seo
---

# SEO

SEO stands for search engine optimization, it is a process of getting traffic from “free,” “organic,” “editorial” or “natural” search results on search engines. All major search engines have primary search results, where web pages and other content such as videos or local listings are shown and ranked based on what a search engine considers most relevant to users.

nopCommerce supports **SEO techniques for different types of pages** in your store, find a list of them in **See also.**

## SEO settings

There are some general SEO settings in nopCommerce, that can be applied to the entire store.

To edit SEO settings, go to **Configuration → Settings → General Settings.**

![p1](_static/seo/seo1.png)

- In the **Default title** field, enter the default title for the pages in your store
- From the **Page Title SEO adjustment** field, select the required page title SEO adjustment, as follows:

    A page name comes after the store name in the title

		`YOURSTORE.COM` | PAGENAME

    The store name comes after a page name in the title:

		PAGENAME | `YOURSTORE.COM`

- Specify the **Page Title separator**.
- Enter the **Default meta keywords description** for the pages in your store. This can be overridden for individual **categories, manufacturers, and products**.
- Enter the **Default meta description** for the pages in your store. This can be overridden for individual **categories, manufacturers, and products**.
- Select the **Generate product META description**, to automatically generate the product META descriptions (if not specified on the product details page) based on the **product's short description**.
- Select the **Convert non-western chars** checkbox, to remove the accent in SEO names. For example, convert é to e.
- Select the **Enable canonical URL's** checkbox, to transform an URL into a canonical URL to enable determining whether two syntactically different URL’s may lead to a page with the equivalent content.

## SEO tabs

There are several types of pages in nopCommerce for which you can set up individual SEO settings, including Meta keywords, Meta description, Meta title and Search engine friendly page names. This is done on SEO tabs of the corresponding Administration Area sections.

![p2](_static/seo/seo2.png)

- Enter the required **Meta keywords**, which are a brief and concise list of keyword and phrases for the page. The meta keywords tag takes the following format:

    ```html
    <meta name="keywords" content="keywords, keyword, keyword phrase, etc." >
    ```

- In the **Meta description** field, enter a description of the page. The meta description tag is a brief and concise summary of your page's content. The meta description tag is in the following format:

    ```html
    <meta name="description" content="Brief description of the contents of your page." >
    ```

- In the **Meta title** field, enter the required title. The title tag specifies the title of your Web page. It is a code which is inserted into the header of your web page and is in the following format:

    ```html
    <head>
      <title> Creating Title Tags for Search Engine Optimization & Web Usability </title>
    </head>
    ```

- In the **Search engine friendly page name** field, enter the name of the page used by search engines. If you enter nothing then the web page URL is formed using the page name. If you enter custom-seo-page-name, then the following custom the URL will be used: `http://www.yourStore.com/custom-seo-page-name`

## See also

- [Adding products](xref:en/user-guide/running/product-management/products/adding-products/index)
- [Product categories](xref:en/user-guide/running/product-management/categories)
- [Manufacturers](xref:en/user-guide/running/product-management/manufacturers)
- [Vendors](xref:en/user-guide/configuring/setting-up/customers/vendors/index)
- [Topics (pages)](xref:en/user-guide/marketing/content/topics)
- [News](xref:en/user-guide/marketing/content/news/index)
- [Blog](xref:en/user-guide/marketing/content/blog/index)

## Tutorials

- [Understanding SEO settings in nopCommerce](https://youtu.be/UxqM_nJyv1Q)
