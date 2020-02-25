---
title: Uploading favicons
uid: en/user-guide/configuring/setting-up/main-store/uploading-favicons
author: git.RomanovM
contributors: git.rajupaladiya, git.DmitriyKulagin
---

# Uploading favicons

Since the version 4.20 you can automatically upload favicons through the admin area.

> [!NOTE]
> 
> For multistore case you need to repeat this upload procedure for each store.

1. To upload favicons go to **Configuration → Settings → General Settings.** The "Favicon and app icons" block is displayed on the General Settings page: ![settings_block](_static/uploading-favicons/settings_block.png)

1. Click the green button **Upload icons archive**, the file selection dialog will open ![file_selection_dialog](_static/uploading-favicons/file_selection_dialog.png) Here you need to remember or write down the path to your icons (it will vary depending on the store and the virtual directory).

1. There are several options for what to upload, depending on how favicon friendly your site should be for different devices.

   1. The most complete option is the use of one of the favicon generators. In this manual, we will show an example of using a [RealFaviconGenerator](https://realfavicongenerator.net/). Thanks to this service, uploading the full favicon package will be carried out in a few clicks of the buttons.

      1. Go to this generator main page where we are invited to choose a picture for the favicon ![realfavicongenerator](_static/uploading-favicons/realfavicongenerator.png)

      1. After selecting a picture and pressing a button, go to the next page. Here you can adjust the display settings of favicons for specific devices and applications - iOS Web Clip, Android Chrome, Windows Metro, macOS Safari, etc. The service will automatically show display examples. You can customize them to your need or leave the default.

      1. At the bottom of the same page we find an important part of the settings **Favicon Generator Options**. ![favicon_generator_options](_static/uploading-favicons/favicon_generator_options.png)

         1. On this section, you must set certain settings. On the **Path** tab, select the option  `I cannot or I do not want to place favicon files at the root of my web site. Instead I will place them here` and specify the path that was saved from step 2. ![favicon_path](_static/uploading-favicons/favicon_path.png)

         1. On the **Version/Refresh** tab, select the option depending on whether your site is already in production. The setting description will help you with this. ![favicon_version](_static/uploading-favicons/favicon_version.png)

         1. On the tab **Additional fields** it is necessary to check the option to generate an html file in the package. ![favicon_additional_fields](_static/uploading-favicons/favicon_additional_fields.png)

      1. Now all settings are set, click the button to generate. ![generate_button](_static/uploading-favicons/generate_button.png)

      1. Get your favicon package. ![download_package](_static/uploading-favicons/download_package.png)

   1. The simplest option is to use only **favicon.ico** file, that has been successfully used on many sites for a long time, until devices with different screen resolutions appeared.

      1. Find a sample favicon package that is located in **wwwroot/icons/samples/** directory and copy it.

      1. In the new package delete all files except **favicon.ico** and **html_code.html**.

      1. Replace in this package the file **favicon.ico** with your new favicon.

      1. Edit the **html_code.html** file, leave only one line there `<link rel="shortcut icon" href="/icons/icons_0/favicon.ico">` assuming that **/icons/icons_0** it is the path that was saved from step 2.

      1. Save these two files into package. Your favicon package is ready.

   1. An intermediate option is using the full favicon package without a generator.

      1. Find a sample favicon package that is located in **wwwroot/icons/samples/** directory and copy it.

      1. Replace the pictures in the new package with your own ones taking into account the original sizes.

      1. Edit the **html_code.html** file, replace all entries **/icons/icons_0** with the path that was saved from step 2.

      1. Save this package. Your favicon package is ready.

1. Return back to the admin area with a prepared favicon package to upload. Select the desired file and click **Upload icons archive**. ![upload_package](_static/uploading-favicons/file_selection_dialog.png)

1. Ensure your package is successfully uploaded. ![success](_static/uploading-favicons/success.png)

1. To see the new favicon on the site you should clear cache in the admin area and in the browser then reload the page.

> [!TIP]
> 
> To create a favicon package, you can use any generators, third-party services, or do it manually. The only requirement is the existence of the **html_code.html** file with the html code, which will be placed in the `<head>` element of the site pages.
