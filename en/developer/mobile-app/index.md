---
title: Mobile app documentation
uid: en/developer/mobile-app/index
author: git.DmitriyKulagin
contributors: git.DmitriyKulagin
---

# Mobile app documentation

## Introduction

nopCommerce team provides a [mobile application for iOS and Android](https://www.nopcommerce.com/ecommerce-mobile-app). It will bring enormous value to your business. The mobile app is completely ready-to-go, so you can start selling your products and services straight away. Like the nopCommerce platform, the mobile app comes with the source code and offers unlimited customization options. What’s more, it can seamlessly adapt to your online store design and functionality. No coding or designing skills are required to integrate the app with your nopCommerce store, configure its content and features, publish it on the Google Play Store and App Store, and manage its workflow.

Here are some other significant features that will ensure efficient mobile eCommerce launching, customization, and maintenance:

- Build with latest version of Flutter and Dart

- Compatible with Android and iOS

- Used Riverpod (state management) features

- Easy to use UI and beautiful Material design 3

- Token-Based Authentication

- Internationalization support

- Dark and Light theme support

- Free icons

## Setup

With the "nopCommerce mobile app" plugin, in addition to the "Web API Frontend" plugin, it is possible to manage some application settings.

![image](./_static/index/app_plugin.png)

The following functions are available:

1. It is possible to transfer certain settings to the mobile application. This is done on purpose so as not to give access to absolutely all application settings. If you need additional settings, you must ensure that they are supported in the mobile application.
1. Slider control on the main screen. You can also specify which products users will go to if they click on each slider image.

## Visual Studio Code

The recommended development environment for Flutter is [Android Studio](https://developer.android.com/studio). A convenient alternative is the VS Studio Code editor, below are the basic settings that help you comfortably work with the code, as well as a set of extensions necessary for development and debugging.

### Editor settings

```json
{
    "[dart]": {
        "editor.codeActionsOnSave": {
            "source.fixAll": true
        },
        "editor.selectionHighlight": false,
        "editor.suggest.snippetsPreventQuickSuggestions": false,
        "editor.suggestSelection": "first",
        "editor.tabCompletion": "onlySnippets",
        "editor.wordBasedSuggestions": false,
    },
    "dart.warnWhenEditingFilesOutsideWorkspace": false,
    "dart.renameFilesWithClasses": "prompt",
    "editor.bracketPairColorization.enabled": true,
    "editor.inlineSuggest.enabled": true,
    "editor.formatOnSave": true,
    "explorer.compactFolders": false,
    "dart.debugExternalPackageLibraries": false,
    "dart.debugSdkLibraries": false,
    "editor.minimap.enabled": false
}
```

### Extensions

For development, you will need to install the following extensions:

Name: **Flutter**\
Id: Dart-Code.flutter\
Description: Flutter support and debugger for Visual Studio Code.\
Publisher: Dart Code\
VS Marketplace Link: <https://marketplace.visualstudio.com/items?itemName=Dart-Code.flutter>

Name: **Dart**\
Id: Dart-Code.dart-code\
Description: Dart language support and debugger for Visual Studio Code.\
Publisher: Dart Code\
VS Marketplace Link: <https://marketplace.visualstudio.com/items?itemName=Dart-Code.dart-code>

Name: **Adobe XD**\
Id: adobe.xd\
Description: The new Adobe XD Extension for Visual Studio Code allows developers to visually map design sources, created in XD and available in Creative Cloud Libraries, to platform-specific code using design tokens. DesignOps teams will be able to create shareable Design System Packages (DSPs) that contain all the information developers need to consume while coding, including code snippets and documentation. \
Publisher: Adobe\
VS Marketplace Link: <https://marketplace.visualstudio.com/items?itemName=Adobe.xd>

## Start development and customization

Open the source code from the archive that you will receive after purchasing the application in the Visual Studio Code editor.

Immediately after opening the project in the code editor, you will be prompted to download the included libraries. You can also do it yourself by running the following command in the terminal:

```bash
flutter pub get
```

Now you need to specify an endpoint to connect to the server to access the API. This setting is in the `lib\constants\app_constants.dart` file:

```dart
class AppConstants {
  static const String storeUrl = 'https://yourstore.com';
}
```

Now everything is ready to run the application:

```bash
flutter run
```

## Project structure

A mobile application developed on Flutter carries the functions of interacting with a public store using the Web API (Frontend). All the main processes of user interaction with the application are presented in the diagram below:

![image](./_static/index/app_flow.png)

The approach that focuses on the fact that the functions of the application are root folders will be applied. And inside this folder, we describe the architectural layers inherent in this particular function in the form of subfolders. Thus, everything related to the functionality of interest to us is located in one folder, which greatly simplifies the understanding of the code.

![image](./_static/index/app_structure.png)

## Technology stack

Frameworks & Libraries used:

- Flutter SDK 3.24.5
- Dart SDK 3.5.4
- flutter_riverpod: 2.5.1
- go_router: 14.6.2
- flutter_secure_storage: 9.2.2
- dio: 5.7.0

> [!IMPORTANT]
>
> For correct operation of the application, it is recommended to use the package versions specified in the configuration.

## App architecture

The application architecture is built according to generally accepted standards, described in the [Android documentation](https://developer.android.com/topic/architecture). Since the project uses the Riverpod state management system, the architecture has been extended and looks like this:

![image](./_static/index/app_arch.png)

## Navigation - go_router

For navigation in the application, the [go_router](https://pub.dev/packages/go_router) library is used. It is a declarative routing system built on top of the [Flutter Router API](https://api.flutter.dev/flutter/widgets/Router-class.html).

The navigation map in the app looks like this:

```bash
Full paths for routes:
    splash => /splash
    home => /home
    catalog => /catalog
    category => /catalog/category/:id
    manufacturer => /catalog/manufacturer/:id
    vendor => /catalog/vendor/:id
    productsbytag => /catalog/productsByTag/:id
    productsearch => /catalog/productSearch/:q
    product => /catalog/product/:id
    review => /catalog/product/:id/review
    addreview => /catalog/product/:id/addReview
    cart => /cart
    checkout => /cart/checkout
    account => /account
    logincheckout => /account/logincheckout
    login => /account/login
    forgotpassword => /account/login/forgotPassword
    register => /account/register
    settings => /account/settings
    contactus => /account/contactUs
    wishlist => /account/wishlist
    accountinfo => /account/accountInfo
    accountaddresses => /account/accountAddresses
    createupdateaddress => /account/accountAddresses/createUpdateAddress/:id
    accountorders => /account/accountOrders
    orderdetails => /account/accountOrders/orderDetails/:id
    accountdownloadableproducts => /account/accountDownloadableProducts
    accountbackinstock => /account/accountBackInStock
    accountrewardpoints => /account/accountRewardPoints
    accountchangepassword => /account/accountChangePassword
    accountproductreviews => /account/accountProductReviews
    accountreturnrequests => /account/accountReturnRequests
    returnrequest => /account/returnRequest/:id
```

## Web API client generation

Istall the [OpenAPI Generator](https://openapi-generator.tech/) (Requires [Node.js](https://nodejs.org/en/download/))

To update the version of OpenAPI Generator, use the following command and select the latest stable version from the list provided.

```bash
openapi-generator-cli version-manager list
```

Create a file *openapitools.json*.
Use the [dart-dio](https://openapi-generator.tech/docs/generators/dart-dio) generator.

```json
{
  "$schema": "node_modules/@openapitools/openapi-generator-cli/config.schema.json",
  "spaces": 2,
  "generator-cli": {
    "version": "7.10.0",
    "generators": {
        "frontend": {
            "input-spec": "swagger.json",
            "generator-name": "dart-dio",
            "output": "frontend_api",
            "additionalProperties": {
                "pubName": "frontend_api"
            }
        }
    }
  }
}
```

> [!NOTE]
>
> To update the generator version, run the following command:
>
> ```bash
> openapi-generator-cli version-manager list
>```

It is necessary that the OpenAPI schema *swagger.json* be in the directory with the generator installed.

1. Call the following command to generate the client:

    ```bash
    openapi-generator-cli generate
    ```

1. The standard openapi-generator will only generate base code that uses libraries that rely on Dart's own code generation. Therefore, after the completion of the base generation, it is necessary to start the Dart generator:

    ```bash
    cd frontend_api
    flutter pub get  
    flutter pub run build_runner build -d
    ```

    As a result, we get a ready-made package, which will be located where you specified in  the configuration file or console command. It remains to include it in *pubspec.yaml*:

    ```yaml
    frontend_api: # <- nopCommerce generated api library
        path: lib/frontend_api
    ```

## Localization

To localize the interface of the application itself, you need to place a file with locales in the `\lib\localization` folder. It is important that the file name is in the following format:

`intl_`**{your_language_code_from_server}**`.arb`

As a basis, you can take the *intl_en.arb* file already in the kit and translate its resources into the target language.

Run the following command in a terminal to generate the required resource files for localization.

```bash
flutter gen-l10n
```

You can make sure that all the necessary resources are generated correctly in the `.dart_tool\flutter_gen\gen_l10n` path.

![image](./_static/index/localization.png)

> [!WARNING]
>
> Do not make any changes to this file as this part of the code is auto-generated. All localization changes should be made strictly in the `intl_`**{your_language_code_from_server}**`.arb` file located along the path `\lib\localization`.

Now, when switching the language in the application settings, the application interface will also be localized.

> [!IMPORTANT]
>
> The language code that will be embedded in the name of the localization file must match the one added on your server. Otherwise, the interface will be localized by the default locales (i.e. `en`).

## Designing the user interface

To maintain a unified and consistent look across devices, we harnessed the potential of design tokens provided by Material 3. These tokens allow for storing styles, fonts, and animation values, enabling us to use the same style values in both design files and code.

Using tokens provides several advantages:

- Consistency: Tokens ensure consistent design across screens and components.
- Theming: Easily switch between light/dark/dynamic themes by swapping the ColorScheme or ThemeData.
- Maintainability: Change the look of the entire app from one place (no need to search and replace color codes).
- Design/Dev collaboration: Tokens serve as a bridge between Figma and Flutter.

### Integrating Tokens from Design Tools

[Material Theme Builder](https://m3.material.io/theme-builder) allows designers to create custom themes and export tokens in multiple formats, including Flutter code.

You don’t need an existing project - the theme will be generated from scratch by the plugin. So, download the plugin and launch it in Figma:

![image](./_static/index/material_theme_builder.png)

Click **Create Theme**:
![image](./_static/index/create_theme.png)

A theme with default colors will be generated. Go to the **Custom** tab:

![image](./_static/index/custom_colors.png)

Here you need to select the base colors for the entire scheme. You can enter them manually, for example, if your brand colors are already defined. On the **Dynamic** tab, you can upload an image and extract colors from it. It is recommended to use the service [https://coolors.co/](https://coolors.co/) to generate harmonious colors.

Сlick **Start the generator**:

![image](./_static/index/make_colors.png)

You can delete the extra blocks until only 3 remain, and then press the spacebar to generate variations.

Replace the colors in the palette and click **Export**:

![image](./_static/index/export_colors.png)

> [!IMPORTANT]
>
> Be aware that you can’t edit the created palette later. If you close and reopen the plugin, your colors will reset to default.

Then select **Export Material Tokens (DSP):**

![image](./_static/index/export_material_tokens.png)

You’ll get a folder with the following contents:

- `data` folder
- `dsp.json`

In the `Themes > nopCommerce` folder, copy only the `data` folder. Leave the `dsp.json` file unchanged, as it’s includes custom `build_params` that refer to a custom configuration file `custom_config.js`. This custom config file is used to generate `font-size` in the desired format.

![image](./_static/index/nop_theme_struct.png)

You'll also notice that the theme folder includes `assets`, `dist`, and `fonts` subfolders. The `assets` and `dist` folders are generated automatically by the Adobe XD library (only the `custom_config.js` file was manually added there). The `fonts` folder contains the necessary fonts.

Now we need to regenerate the `style_dictionary.dart` file from the `dist` folder. This file contains constants linked to the palette colors and fonts. As noted in the file itself—**do not edit it manually**.

![image](./_static/index/style_dictionary.png)

Next, you'll need the **Adobe XD extension for VS Code**. This extension generates a Dart file using the [Style Dictionary library](https://amzn.github.io/style-dictionary/#/README). Technically, you can do without Adobe XD, but it's more convenient to do it through the extension interface.

![image](./_static/index/adobe_xd.png)

Once installed, you can open the extension by clicking the **XD** button at the bottom of the VS Code window:

![image](./_static/index/open_adobe_xd.png)

To load a new DSP package, click the **box icon** at the top:

![image](./_static/index/load_dsp.png)

In the window that appears, choose **Edit package**:

![image](./_static/index/edit_package.png)

Then click **Select folder**:

![image](./_static/index/select_folder.png)

Choose the folder with the theme `nopCommerce` and click **Load package**:

![image](./_static/index/load_package.png)

You should now see the tokens and, if everything went well, the colors you selected in Figma.

Next, we need to regenerate the `StyleDictionary` class, which is used to create the color scheme. To do this, click **Start editing**:

![image](./_static/index/start_editing.png)

Without changing anything, click **Finish editing**:

![image](./_static/index/finish_editing.png)

If you see the following message afterward, everything went correctly:

![image](./_static/index/tokens_compiled.png)

That’s it — restart the application and enjoy your new color scheme.

> [!NOTE]
>
> By the way, keep an eye on the fonts. Even though you see **Roboto** in Figma, they might magically download as **Google Sans**.
