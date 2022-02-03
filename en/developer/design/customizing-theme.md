---
title: Customizing nopCommerce Themes
uid: en/developer/design/customizing-theme
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Customizing nopCommerce Themes

## Uploading your store logo

To upload your store logo to a nopCommerce website, there are 2 methods:

### First Method

Upload your logo through the admin area. See how to do this in the [Uploading your logo](xref:en/getting-started/design-your-store/uploading-your-logo) article.

### Second method

1. Go to nopCommerce root folder `/Themes/YOUR THEME/Content/images/`
1. Look for logo.png image file
1. Replace the `logo.png` with your store logo and name it as `logo.png` (with same width:250px and height:50px)

If you wish to make changes in the stylesheet regarding the logo, look for the following code in your `styles.css`:

```css
.header-logo {
    margin: 0 0 20px;
    text-align: center;
}
.header-logo a {
    display: inline-block;
    max-width: 100%;
    line-height: 0; /*firefox line-height bug fix*/
}
.header-logo a img {
    max-width: 100%;
    opacity: 1;
}
```

> [!IMPORTANT]
> You might have to clear the browser cache (or refresh the page using Ctrl+F5 keys) to see the changes (new store logo).

## How to change a layout

1. If you would like to customize/make changes in the base layout (i.e. `_Root.cshtml`) of your nopCommerce website look for this CSS code in your `styles.css` file

    ```css
    .master-wrapper-content {
        position: relative;
        z-index: 0;
        width: 90%;
        margin: 0 auto;
    }
    .master-column-wrapper {
        position: relative;
        z-index: 0;
    }
    .master-column-wrapper:after {
        content: "";
        display: block;
        clear: both;
    }
    ```

1. If you would like to customize/make changes in the layout of `_ColumnOne.cshtml`. Please look for this CSS code in your `style.css`

    ```css
    .center-1 {
        margin: 0 0 100px;
    }
    ```

1. If you would like to customize/make changes in the layout of `_ColumnTwo.cshtml`. Please look for this CSS code in your `style.css`

    ```css
    .center-2, .side-2 {
        margin: 0 0 50px;
    }
    .side-2:after {
        content: "";
        display: block;
        clear: both;
    }
    ```

## How to make changes in the header menu (top menu)

1. If you would like to customize/make changes in the header menu (top menu) of your nopCommerce website, please go to the following location:

    Go to nopCommerce root folder `/Views/Shared/Components/TopMenu/Default.cshtml`
1. Open file `Default.cshtml` - You can add or remove menu items in `<li>` according to your requirements.

## How to make changes in the footer (or footer links)

1. If you would like to customize/make changes in a footer (or footer links) of your nopCommerce website, please go to the following location:

    Go to nopCommerce root folder `/Views/Shared/Components/Footer/Default.cshtml`
1. Open file `Default.cshtml` - You can add or remove links in `<li>` or complete `<ul>` according to your requirements.
