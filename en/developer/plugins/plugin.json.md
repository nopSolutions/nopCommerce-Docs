---
title: Description of the structure of the plugin.json file
uid: en/developer/plugins/plugin_json
author: git.cromatido
contributors: git.cromatido
---

# Description of the structure of the `plugin.json` file

 This file contains the Meta information description for a plugin which is used by nopCommerce to determine which group this plugin belongs to,  if the plugin is compatible with the current version of nopCommerce or not, what is the version of the plugin and several other information. Each nopCommerce plugin must have this file.

## File Structure

```json
{
    "Group": "Payment methods",
    "FriendlyName": "PayPal Standard",
    "SystemName": "Payments.PayPalStandard",
    "Version": "1.49",
    "SupportedVersions": [ "4.20" ],
    "Author": "nopCommerce team",
    "DisplayOrder": 1,
    "FileName": "Nop.Plugin.Payments.PayPalStandard.dll",
    "Description": "This plugin allows paying with PayPal Standard",
    "LimitedToStores":"",
    "LimitedToCustomerRoles":"",
    "DependsOnSystemNames":"",
}
```

- **Group**. It is used by nopCommerce to identify or search or filter plugin by its group name in the plugin list under `Admin/Configuration/LocalPlugin` menu. This may be your company name.

- **FriendlyName**. It is the display name for the plugin. It is used to identify our plugin from the plugin list.

- **SystemName**. It is used by nopCommerce to identify the plugin uniquely, so it needs to be unique from all other plugins. We cannot register more than one plugin with the same `SystemName`.

- **Version**. This is the version number of the plugin, you can set this to any value you like. This number is used to identify which version of plugin is it currently installed in the nopCommerce application.

- **SupportedVersions**. It is the array of string. It contains one or more than one versions of nopCommerce that this plugin is supported on or we can say this plugin is target for. During development ensure that the current version of nopCommerce in which you are developing this plugin is included in this list, otherwise, it will not be loaded in the plugin list.

- **Author**. This is the information about the creator of plugin. It may be a person name or a company name or a team who created this plugin.

- **DisplayOrder**. It is used to set the order in which this plugin should be displayed in the plugin list. Its value is of type number.

- **FileName**. It has the following format **Nop.Plugin.{Group}.{Name}.dll** (it is your plugin assembly filename).

- **Description**. It contains a short description about your plugin like what this plugin is all about, what this plugin does. This is shown in the Plugin list under plugin name.
- **LimitedToStores** - The list of store identifiers in which this plugin is available. If empty, then this plugin is available in all stores.
- **LimitedToCustomerRoles** - The list of customer role identifiers for which this plugin is available. If empty, then this plugin is available for all ones.
- **DependsOnSystemNames** - The list of plugins' system name that this plugin depends on

> [!TIP]
> After you edit your **plugin.json** file's content you need to set its `Copy to Output Directory` property value to `Copy if newer`.
> ![image3](_static/plugin.json/plugin_json_0.jpg)
> It is required because we need this file to be copied to compiled directory from where nopCommerce can access this file to display our plugin in the plugin list in admin panel.

## Examples

- The  **FixedOrByCountryStateZip** plugin has the following `plugin.json` file:

  ```json
  {
      "Group": "Tax Providers",
      "FriendlyName": "Manual (Fixed or By Country/State/Zip)",
      "SystemName": "Tax. FixedOrByCountryStateZip",
      "Version": "1.29",
      "SupportedVersions": [ "4.20" ],
      "Author": "nopCommerce team ",
      "DisplayOrder": 1,
      "FileName": "Nop.Plugin.Tax. FixedOrByCountryStateZip.dll",
      "Description": "This plugin allow to configure fix tax rates or tax rates by countries, states and zip codes "
  }
  ```

- The **Google Analytics** widget has the following *plugin.json* file:

  ```json
      {
          "Group": "Widgets",
          "FriendlyName": "Google Analytics ",
          "SystemName": "Widgets.GoogleAnalytics",
          "Version": "1.62",
          "SupportedVersions": [ "4.30" ],
          "Author": "nopCommerce team, Nicolas Muniere",
          "DisplayOrder": 1,
          "FileName": "Nop.Plugin.Widgets.GoogleAnalytics.dll",
          "Description": "This plugin integrates with Google Analytics. It   keeps track of statistics about the visitors and eCommerce conversion   on your website"
      }
  ```
