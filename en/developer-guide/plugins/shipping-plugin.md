---
title: How to code my own shipping rate computation method
uid: en/developer/plugins/shipping-plugin
author: git.AndreiMaz
contributors: git.exileDev
---

# How to code my own shipping rate computation method

If customers have some shippable products, they can choose a shipping option during checkout. These shipping options are returned from shipping rate computation methods (such as UPS, USPS, FedEx, etc). Shipping rate computation methods are implemented as plugins in nopCommerce. We recommend you read [How to write a plugin for nopCommerce 4.20](xref:en/developer/plugins/how-to-write-plugin-4.20) before you start coding a new shipping rate computation method. The article will explain to you the required steps for creating a plugin. So actually a shipping rate computation method is an ordinary plugin which implements an **IShippingRateComputationMethod** interface (Nop.Services.Shipping namespace). So add a new shipping plugin project (class library) to solution and let's get started.

## Controllers, views, models

Add a controller and an appropriate **Configure** action method and a view. These will define how a store owner sees configuration options in admin panel (System → Configuration → Shipping → Shipping providers). This article does not explain how to configure plugins, but you can find more info about it [here](xref:en/user-guide/configuring/setting-up/shipping/providers/index).

![shipping-plugin_1](_static/shipping-plugin/shipping-plugin_1.png)

Once this step is complete, you can start adding required business logic for getting shipping options.

## Getting shipping options

Now you need to create a class which implements **IShippingRateComputationMethod** interface. This is the class that will be doing all the actual work. When nopCommerce calculates shipping totals or needs to get a list of available shipping options, the **GetShippingOptions** or **GetFixedRate** methods of your class will be called. Here is how UPSComputationMethod class is defined (“UPS” method):

```csharp
public class UPSComputationMethod : BasePlugin, IShippingRateComputationMethod
```

**IShippingRateComputationMethod** interface has several methods and properties which are required to implement.

- **ShippingRateComputationMethodType**. This property indicates how your shipping rate computation works. It can be **Offline** or **Realtime**. **Offline** plugins do not use any third-party sites to get the rates (examples of such methods are “Fixed or by weight”). These plugins get all the required information from local databases. **Realtime** plugins use third-party sites to get the rates (for example, UPS).
- **GetShippingOptions**. This method is always invoked when a customer chooses a shipping option during checkout. This method returns **GetShippingOptionResponse** which contains a list of **ShippingOption** objects. Each **ShippingOption** object contains information about certain shipping options such as option name (for example, “By ground”), its rate (for example, 10 USD) and other information. Put all your logic here (get the rates from your database or request them from a third-party site such as UPS).
- **GetFixedRate**. As you already know **GetShippingOptions** is used to get shipping options during checkout (on “Select shipping method” page). But sometimes we need to know a shipping rate before a shipping option is chosen (for example, on the shopping cart page). In this case you can return a fixed rate. For example, your shipping rate computation method provides only one shipping option, and there’s no need to wait until the customer chooses it on “Select shipping method” page. Return will be **null**, if your fixed rates are not supported. In this case customers will see the following message beside “Shipping total” on the shopping cart: “Calculated during checkout”.
- **GetConfigurationPageUrl**. As you remember we created a controller in the previous step. This method should return a url of its Configure method. For example:

```csharp
public override string GetConfigurationPageUrl()
{
    return $"{_webHelper.GetStoreLocation()}Admin/FixedOrByWeight/Configure";
}
```

## Conclusion

Hopefully this will get you started with adding a new shipping rate computation method.
