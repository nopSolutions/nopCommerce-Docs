---
title: Customizing nopCommerce Themes
uid: en/developer/design/customizing-theme
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Customizing nopCommerce Themes

## Uploading your store logo

In order to upload your store logo in a nopCommerce website, there are basically 2 methods:

### First Method

1. Go to nopCommerce root folder `/Themes/YOUR THEME/Content/images/`
1. Look for logo.gif image file
1. Replace the `logo.gif` with your store logo and name it as `logo.gif` (with same width:310px and height:60px)

### Second Method

1. Save your store logo in this location : nopCommerce root folder `/Themes/YOUR THEME/Content/images/`
1. Go to nopCommerce root folder `/Views/Shared/Header.cshtml`
1. Open `Header.cshtml` file
1. Look for this code at the top:

    ```csharp
    var logoPath = "~/Themes/" + currentThemeName + "/Content/images/logo.gif";
    ```

    You can mention the path of your custom logo here.

    > [!NOTE]
    > In the above mentioned css code: logo.gif is the name of the store logo image file

1. Change logo.gif with `YourLogo.gif/jpg/png`
1. Save changes to the `Header.cshtml` file

> [!IMPORTANT]
> You might have to refresh the browser or delete history or cookies of your browser in order to see the changes (new store logo).

If you wish to make changes in stylesheet in regard to the logo, look for the following code in your `styles.css`:

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

## How to change a layout

1. If you would like to customize / make changes in the base layout (i.e. `_Root.cshtml`) of your nopCommerce website. Please look for this css code in your `style.css`

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

1. If you would like to customize / make changes in the layout of `_ColumnOne.cshtml`. Please look for this css code in your `style.css`

    ```css
    .center-1 {
        margin: 0 0 100px;
    }
    ```

1. If you would like to customize / make changes in the layout of `_ColumnTwo.cshtml`. Please look for this css code in your `style.css`

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

## How to make changes in header menu (top menu)

1. If you would like to customize/make changes in header menu (top menu) of your nopCommerce website, please go to the following location:

    Go to nopCommerce root folder `/Views/Shared/Components/TopMenu/Default.cshtml`
1. Open file `Default.cshtml` - You can add or remove menu items in `<li>` according to your requirements.

## How to make changes in footer (or footer links)

1. If you would like to customize/make changes in a footer (or footer links) of your nopCommerce website, please go to the following location:

    Go to nopCommerce root folder `/Views/Shared/Components/Footer/Default.cshtml`
1. Open file `Default.cshtml` - You can add or remove links in `<li>` or complete `<ul>` according to your requirements.
