---
title: How to code my own payment method
uid: en/developer/plugins/payment-method
author: git.AndreiMaz
contributors: git.Sandeep911, git.exileDev, git.DmitriyKulagin
---

# How to code my own payment method

Payment methods are implemented as plugins in nopCommerce. We recommend you read [How to write a plugin for nopCommerce 4.20](xref:en/developer/plugins/how-to-write-plugin-4.20) before you start coding a new payment method. It will explain to you what the required steps are for creating a plugin.

So actually a payment method is an ordinary plugin which implements an IPaymentMethod interface (Nop.Services.Payments namespace). As you already guessed IPaymentMethod interface is used for creating payment method plugins. It contains some methods which are specific only for payment methods such as ProcessPayment() or GetAdditionalHandlingFee(). So add a new payment plugin project (class library) to solution and let's get started.

## Controllers, views, models

First thing you need to do is create a controller. This controller is responsible for responding to requests made against an ASP.NET MVC website.

1. When implementing a new payment method, this controller should be derived from a special **BasePaymentController** abstract class.

1. Then implement **Configure** action methods used for plugin configuration (by a store owner in admin area). This method and an appropriate view will define how a store owner sees configuration options in admin panel (System → Configuration → Payment methods).

## Public view component.GetPublicViewComponent

Then you have to create a view component for displaying plugin in public store. This view component and an appropriate view will define how your customers will see the payment information page during checkout. First let's create a view component class. It should be placed in /Components folder. Look how it's done for PayPalStandard plugin:

```csharp
[ViewComponent(Name = "PaymentPayPalStandard")]
public class PaymentPayPalStandardViewComponent : NopViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("~/Plugins/Payments.PayPalStandard/Views/PaymentInfo.cshtml");
    }
}
```

Invoke method returns an appropriate PaymentInfo view from */Views* folder of your plugin. Note that we use our custom NopViewComponent class as a base class instead of existing built-in ViewComponent.

Than let's create PaymentInfo view which shows payment information. For PayPalStandard plugin this view is pretty simple. There we just render text saying that a customer will be redirected to the payment page. But it's possible to create a more complex view component if need. For example if you want to collect customer's information on the payment information page look how it's already done in PayPalDirect payment plugin.

## Payment processing

Now you need to create a class which implements **IPaymentMethod** interface. This is the class that will be doing all the actual work of communicating with your payment gateway. When someone creates an order either the **ProcessPayment** or **PostProcessPayment** methods of your class will be called. Here is how CheckMoneyOrderPaymentProcessor class is defined ("CheckMoneyOrder" payment method):

```csharp
public class CheckMoneyOrderPaymentProcessor : BasePlugin, IPaymentMethod
```

**IPaymentMethod** interface has several methods and properties which are required to implement.

- **ValidatePaymentForm** is used in the public store to validate customer input. It returns a list of warnings (for example, a customer did not enter his credit card name). If your payment method does not ask the customer to enter additional information, then the ValidatePaymentForm should return an empty list:

    ```csharp
    public IList<string> ValidatePaymentForm(IFormCollection form)
    {
        return new List<string>();
    }
    ```

- **GetPaymentInfo** method is used in the public store to parse customer input, such as credit card information. This method returns a ProcessPaymentRequest object with parsed customer input (for example, credit card information). If your payment method does not ask the customer to enter additional information, then GetPaymentInfo will return an empty ProcessPaymentRequest object:

    ```csharp
    public ProcessPaymentRequest GetPaymentInfo(IFormCollection form)
    {
        return new ProcessPaymentRequest();
    }
    ```

- **ProcessPayment**. This method is always invoked right before a customer places an order. Use it when you need to process a payment before an order is stored into database. For example, capture or authorize credit card. Usually this method is used when a customer is not redirected to third-party site for completing a payment and all payments are handled on your site (for example, PayPal Direct).
- **PostProcessPayment**. This method is invoked right after a customer places an order. Usually this method is used when you need to redirect a customer to a third-party site for completing a payment (for example, PayPal Standard).
- **HidePaymentMethod**. You can put any logic here. For example, hide this payment method if all products in the cart are downloadable. Or hide this payment method if current customer is from certain country
- **GetAdditionalHandlingFee**. You can return any additional handling fees which will be added to an order total.
- **Capture**. Some payment gateways allow you to authorize payments before they're captured. It allows store owners to review order details before the payment is actually done. In this case you just authorize a payment in **ProcessPayment** or **PostProcessPayment** method (described above), and then just capture it. In this case a **Capture** button will be visible on the order details page in admin area. Note that an order should be already authorized and **SupportCapture** property should return **true**.
- **Refund**. This method allows you make a refund. In this case a **Refund** button will be visible on the order details page in admin area. Note that an order should be paid, and **SupportRefund** or **SupportPartiallyRefund** property should return **true**.
- **Void**. This method allows you void an authorized but not captured payment. In this case a **Void** button will be visible on the order details page in admin area. Note that an order should be authorized and **SupportVoid** property should return **true**.
- **ProcessRecurringPayment**. Use this method to process recurring payments.
- **CancelRecurringPayment**. Use this method to cancel recurring payments.
- **CanRePostProcessPayment**. Usually this method is used when it redirects a customer to a third-party site for completing a payment. If the third party payment fails this option will allow customers to attempt the order again later without placing a new order. **CanRePostProcessPayment** should return **true** to enable this feature.
- **GetConfigurationPageUrl**. As you remember we created a controller in the previous step. This method should return a url of its Configure method. For example:

    ```csharp
    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/PaymentCheckMoneyOrder/Configure";
    }
    ```

- **GetPublicViewComponent**. This method should return the name of the view component which used to display public information for customers. We have created an appropriate view component on the previous step. For example:

    ```csharp
    public string GetPublicViewComponent()
    {
        viewComponentName = "CheckMoneyOrder";
    }
    ```

- **SupportCapture, SupportPartiallyRefund, SupportRefund, SupportVoid**. These properties indicate whether appropriate methods of your payment method are supported.
- **RecurringPaymentType**. This property indicates whether recurring payments are supported.
- **PaymentMethodType**. This property indicates payment method type. Currently there are three types. **Standard** used by payment methods when a customer is not redirected to a third-party site. **Redirection** is used when a customer is redirected to a third-party site. And **Button** is similar to **Redirection** payment methods. The only difference is used that it's displayed as a button on shopping cart page (for example, Google Checkout).
- **SkipPaymentInfo**. Indicates whether we should display a payment information page for this plugin.
- **PaymentMethodDescription**. This property gets a payment method description that will be displayed on checkout pages in the public store

## Conclusion

Hopefully this will get you started with adding a new payment method.
