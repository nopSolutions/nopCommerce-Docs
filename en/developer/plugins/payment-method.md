---
title: How to code my own payment method
uid: en/developer/plugins/payment-method
author: git.AndreiMaz
contributors: git.Sandeep911, git.exileDev, git.DmitriyKulagin
---

# How to code my payment method

Payment methods are implemented as plugins in nopCommerce. We recommend you read [How to write a plugin for nopCommerce 4.60](xref:en/developer/plugins/how-to-write-plugin-4.60) before you start coding a new payment method. It will explain to you what the required steps are for creating a plugin.

So actually a payment method is an ordinary plugin that implements an **`IPaymentMethod`** interface (*Nop.Services.Payments namespace*). As you already guessed *IPaymentMethod* interface is used for creating payment method plugins. It contains some methods which are specific only for payment methods such as `ProcessPaymentAsync()` or `GetAdditionalHandlingFeeAsync()`. So, add a new payment plugin project (*class library*) to the solution, and let's get started.

## Controllers, views, models

The first thing you need to do is create a controller. This controller is responsible for responding to requests made against an ASP.NET MVC website.

1. When implementing a new payment method, this controller should be derived from a special **BasePaymentController** abstract class.

1. Then implement **Configure** action methods used for plugin configuration (by a store owner in the admin area). This method and an appropriate view will define how a store owner sees configuration options in the admin panel (*System → Configuration → Payment methods*).

## Public view component.GetPublicViewComponent

Then you have to create a view component for displaying the plugin in the public store. This view component and an appropriate view will define how your customers will see the payment information page during checkout. First, let's create a view component class. It should be placed in the *`/Components`* folder. Look how it's done for *PayPalCommerce* plugin:

```csharp
public class PaymentInfoViewComponent : NopViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        ...
        return View("~/Plugins/Payments.PayPalCommerce/Views/PaymentInfo.cshtml", model);
    }
}
```

**Invoke** method returns an appropriate `PaymentInfo` view from the */Views* folder of your plugin. Note that we use our custom NopViewComponent class as a base class instead of the existing built-in ViewComponent.

Then let's create the `PaymentInfo` view which shows payment information. There we just render text saying that a customer will be redirected to the payment page. But it's possible to create a more complex view component if needed. For example, if you want to collect customer's information on the payment information page look how it's already done in the `PayPalDirect` payment plugin.

## Payment processing

Now you need to create a class that implements the **IPaymentMethod** interface. This is the class that will be doing all the actual work of communicating with your payment gateway. When someone creates an order either the `ProcessPayment` or `PostProcessPayment` methods of your class will be called. Here is how **CheckMoneyOrderPaymentProcessor** class is defined (`CheckMoneyOrder` payment method):

```csharp
public class CheckMoneyOrderPaymentProcessor : BasePlugin, IPaymentMethod
```

**IPaymentMethod** interface has several methods and properties which are required to implement.

- **ValidatePaymentFormAsync** is used in the public store to validate customer input. It returns a list of warnings (for example, a customer did not enter his credit card name). If your payment method does not ask the customer to enter additional information, then the `ValidatePaymentFormAsync` should return an empty list:

    ```csharp
    public Task<IList<string>> ValidatePaymentFormAsync(IFormCollection form)
    {
        return Task.FromResult<IList<string>>(new List<string>());
    }
    ```

- **GetPaymentInfoAsync** method is used in the public store to parse customer input, such as credit card information. This method returns a ProcessPaymentRequest object with parsed customer input (for example, credit card information). If your payment method does not ask the customer to enter additional information, then `GetPaymentInfoAsync` will return an empty ProcessPaymentRequest object:

    ```csharp
    public Task<ProcessPaymentRequest> GetPaymentInfoAsync(IFormCollection form)
    {
        return Task.FromResult(new ProcessPaymentRequest());
    }
    ```

- **ProcessPaymentAsync**. This method is always invoked right before a customer places an order. Use it when you need to process payment before an order is stored in a database. For example, capture or authorize credit card. Usually, this method is used when a customer is not redirected to a third-party site for completing payment and all payments are handled on your site (for example, *PayPal Direct*).

    ```csharp
    public Task<ProcessPaymentResult> ProcessPaymentAsync(ProcessPaymentRequest processPaymentRequest)
    {
        return Task.FromResult(new ProcessPaymentResult());
    }
    ```

- **PostProcessPaymentAsync**. This method is invoked right after a customer places an order. Usually, this method is used when you need to redirect a customer to a third-party site for completing a payment (for example, PayPal Standard).
- **HidePaymentMethodAsync**. You can put any logic here. For example, hide this payment method if all products in the cart are downloadable. Or hide this payment method if the current customer is from a certain country.
- **GetAdditionalHandlingFeeAsync**. You can return any additional handling fees which will be added to an order total.
- **CaptureAsync**. Some payment gateways allow you to authorize payments before they're captured. It allows store owners to review order details before the payment is done. In this case, you just authorize a payment in **ProcessPaymentAsync** or **PostProcessPaymentAsync** method (described above), and then just capture it. In this case, a *Capture* button will be visible on the order details page in the admin area. Note that an order should be already authorized and **SupportCapture** property should return **`true`**.
- **RefundAsync**. This method allows you to make a refund. In this case, a *Refund* button will be visible on the order details page in the admin area. Note that an order should be paid, and **SupportRefund** or **SupportPartiallyRefund** property should return **`true`**.
- **VoidAsync**. This method allows you to void an authorized but not captured payment. In this case, a *Void* button will be visible on the order details page in the admin area. Note that an order should be authorized and **SupportVoid** property should return **`true`**.
- **ProcessRecurringPaymentAsync**. Use this method to process recurring payments.
- **CancelRecurringPaymentAsync**. Use this method to cancel recurring payments.
- **CanRePostProcessPaymentAsync**. Usually, this method is used when it redirects a customer to a third-party site for completing a payment. If the third-party payment fails this option will allow customers to attempt the order again later without placing a new order. **CanRePostProcessPaymentAsync** should return **`true`** to enable this feature.
- **GetConfigurationPageUrl**. As you remember we created a controller in the previous step. This method should return a URL of its `Configure` method. For example:

    ```csharp
    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/PaymentCheckMoneyOrder/Configure";
    }
    ```

- **GetPaymentMethodDescriptionAsync**. This method gets a payment method description that will be displayed on checkout pages in the public store.

    ```csharp
    public async Task<string> GetPaymentMethodDescriptionAsync()
    {
        return await _localizationService.GetResourceAsync("Plugins.Payment.CheckMoneyOrderPaymentMethodDescription");
    }
    ```

- **SupportCapture, SupportPartiallyRefund, SupportRefund, SupportVoid**. These properties indicate whether appropriate methods of payment method are supported.
- **RecurringPaymentType**. This property indicates whether recurring payments are supported.
- **PaymentMethodType**. This property indicates the payment method type. Currently, there are three types. **`Standard`** is used by payment methods when a customer is not redirected to a third-party site. **`Redirection`** is used when a customer is redirected to a third-party site. And **`Button`** is similar to **`Redirection`** payment methods. The only difference is used that it's displayed as a button on the shopping cart page (for example, *Google Checkout*).
- **SkipPaymentInfo**. Indicates whether we should display a payment information page for this plugin.

## Conclusion

Hopefully, this will get you started with adding a new payment method.
