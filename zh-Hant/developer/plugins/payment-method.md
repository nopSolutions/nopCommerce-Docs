---
標題: 如何編寫自己的付款提供程序
uid: zh-Hant/developer/plugins/payment-method
作者: git.AndreiMaz
貢獻者: git.Sandeep911, git.exileDev, git.DmitriyKulagin
---

# 如何編寫自己的付款提供程序

在 nopCommerce 中，付款方式是透過外掛來實現的。我們建議您在開始編寫新的付款方式程式碼之前，先閱讀 [如何為 nopCommerce 4.90 編寫外掛](xref:zh-Hant/developer/plugins/how-to-write-plugin-4.90)。該文件將說明建立外掛所需的步驟。

實際上，付款方式就是一個實作了 **`IPaymentMethod`** 介面（位於 *Nop.Services.Payments* 命名空間）的普通外掛。如您所料，*IPaymentMethod* 介面是用於建立付款方式外掛的。它包含一些專屬於付款方式的方法，例如 `ProcessPaymentAsync()` 或 `GetAdditionalHandlingFeeAsync()`。因此，請將一個新的付款外掛專案（*類別庫*）新增至解決方案中，讓我們開始吧。

---
## 控制器、檢視、模型

您首先需要做的是建立一個控制器。此控制器負責回應對 ASP.NET MVC 網站提出的請求。

1. 在實作新的付款方式時，此控制器應繼承自特殊的 **BasePaymentController** 抽象類別。

1. 接著實作 **Configure** 動作方法，用於外掛設定（由商店擁有者在後台管理執行）。此方法與適當的檢視將定義商店擁有者如何在後台管理中查看設定選項（*系統 → 設定 → 付款方式*）。

---
uid: zh-Hant/developer/tutorials/how-to-code-a-payment-method
title: 如何編寫付款方式
author: git.AndreiMaz
---

## 公開檢視元件 GetPublicViewComponent

接下來，您必須建立一個檢視元件，以便在前台商店中顯示該外掛。此檢視元件與對應的檢視頁面將定義您的顧客在結帳期間如何查看付款資訊頁面。首先，讓我們建立一個檢視元件類別。它應該放置在 *`/Components`* 資料夾中。請參考 *PayPalCommerce* 外掛的實作方式：

```csharp
public class PaymentInfoViewComponent : NopViewComponent
{
    public IViewComponentResult Invoke(string widgetZone, object additionalData)
    {
        return View("~/Plugins/Payments.PayPalCommerce/Views/Public/PaymentInfo.cshtml");
    }
}
```

**Invoke** 方法會從您外掛的 */Views* 資料夾中回傳適當的 `PaymentInfo` 檢視頁面。請注意，我們是使用自訂的 `NopViewComponent` 類別作為基底類別，而不是現有的內建 `ViewComponent`。

接著，讓我們建立顯示付款資訊的 `PaymentInfo` 檢視頁面。在這裡，我們只是呈現一段文字，告知顧客將被重新導向至付款頁面。但如有需要，也可以建立更複雜的檢視元件。例如，如果您想在付款資訊頁面上收集顧客的資訊，可以參考 `PayPalDirect` 付款外掛中是如何完成此功能的。

## 付款處理

現在您需要建立一個實作 **IPaymentMethod** 介面的類別。這個類別將負責處理與您的付款閘道（payment gateway）通訊的所有實際工作。當有人建立訂單時，系統會呼叫您類別中的 `ProcessPayment` 或 `PostProcessPayment` 方法。以下是 **CheckMoneyOrderPaymentProcessor** 類別的定義方式（以 `CheckMoneyOrder` 付款方式為例）：

```csharp
public class CheckMoneyOrderPaymentProcessor : BasePlugin, IPaymentMethod
```

**IPaymentMethod** 介面有幾個必須實作的方法與屬性。

- **ValidatePaymentFormAsync** 用於在前台網站驗證顧客輸入的資訊。它會回傳一個警告清單（例如：顧客未輸入信用卡姓名）。如果您的付款方式不需要顧客輸入額外資訊，則 `ValidatePaymentFormAsync` 應回傳一個空清單：

    ```csharp
    public Task<IList<string>> ValidatePaymentFormAsync(IFormCollection form)
    {
        return Task.FromResult<IList<string>>(new List<string>());
    }
    ```

- **GetPaymentInfoAsync** 方法用於在前台網站解析顧客輸入的資訊，例如信用卡資料。此方法會回傳一個包含已解析顧客輸入內容（例如信用卡資訊）的 ProcessPaymentRequest 物件。如果您的付款方式不需要顧客輸入額外資訊，則 `GetPaymentInfoAsync` 將回傳一個空的 ProcessPaymentRequest 物件：

    ```csharp
    public Task<ProcessPaymentRequest> GetPaymentInfoAsync(IFormCollection form)
    {
        return Task.FromResult(new ProcessPaymentRequest());
    }
    ```

- **ProcessPaymentAsync**。此方法總是在顧客下訂單前立即被呼叫。當您需要在訂單儲存至資料庫之前處理付款時，請使用此方法。例如：授權或請款信用卡。通常，當顧客不需要重新導向至第三方網站來完成付款，且所有付款都在您的網站上處理時（例如 *PayPalCommerce*），會使用此方法。

    ```csharp
    public Task<ProcessPaymentResult> ProcessPaymentAsync(ProcessPaymentRequest processPaymentRequest)
    {
        return Task.FromResult(new ProcessPaymentResult());
    }
    ```

- **PostProcessPaymentAsync**。此方法在顧客下訂單後立即被呼叫。通常，當您需要將顧客重新導向至第三方網站以完成付款時（例如 PayPal Standard），會使用此方法。
- **HidePaymentMethodAsync**。您可以在此處放置任何邏輯。例如：如果購物車中的所有商品皆為可下載商品，則隱藏此付款方式；或者如果目前顧客來自特定國家/地區，則隱藏此付款方式。
- **GetAdditionalHandlingFeeAsync**。您可以回傳任何將被計入訂單總金額的額外處理費。
- **CaptureAsync**。部分付款閘道允許您在請款前先進行付款授權。這讓商店管理員能在付款完成前審核訂單詳情。在此情況下，您只需在 **ProcessPaymentAsync** 或 **PostProcessPaymentAsync** 方法（如上所述）中進行付款授權，然後再進行請款。此時，後台管理頁面的訂單詳情頁面將會出現一個「請款」（Capture）按鈕。請注意，訂單必須已完成授權，且 **SupportCapture** 屬性必須回傳 **`true`**。
- **RefundAsync**。此方法允許您進行退款。此時，後台管理頁面的訂單詳情頁面將會出現一個「退款」（Refund）按鈕。請注意，訂單必須已完成付款，且 **SupportRefund** 或 **SupportPartiallyRefund** 屬性必須回傳 **`true`**。
- **VoidAsync**。此方法允許您作廢（Void）已授權但尚未請款的交易。此時，後台管理頁面的訂單詳情頁面將會出現一個「作廢」（Void）按鈕。請注意，訂單必須已完成授權，且 **SupportVoid** 屬性必須回傳 **`true`**。
- **ProcessRecurringPaymentAsync**。使用此方法處理定期購商品。
- **CancelRecurringPaymentAsync**。使用此方法取消定期購商品。
- **CanRePostProcessPaymentAsync**。通常，當系統將顧客重新導向至第三方網站以完成付款時會使用此方法。如果第三方付款失敗，此選項將允許顧客在稍後嘗試重新完成該訂單，而無需建立新訂單。**CanRePostProcessPaymentAsync** 必須回傳 **`true`** 才能啟用此功能。
- **GetConfigurationPageUrl**。如同您所記得的，我們在上一步中建立了一個控制器。此方法應回傳該控制器中 `Configure` 方法的 URL。例如：

    ```csharp
    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/PaymentCheckMoneyOrder/Configure";
    }
    ```

- **GetPaymentMethodDescriptionAsync**。此方法取得將顯示在前台網站結帳頁面上的付款方式說明。

    ```csharp
    public async Task<string> GetPaymentMethodDescriptionAsync()
    {
        return await _localizationService.GetResourceAsync("Plugins.Payment.CheckMoneyOrderPaymentMethodDescription");
    }
    ```

- **SupportCapture**、**SupportPartiallyRefund**、**SupportRefund**、**SupportVoid**。這些屬性指出該付款方式是否支援對應的操作方法。
- **RecurringPaymentType**。此屬性指出是否支援定期購付款。
- **PaymentMethodType**。此屬性指出付款方式的類型。目前共有三種類型。**`Standard`** 用於顧客無需重新導向至第三方網站的付款方式。**`Redirection`** 用於顧客需要重新導向至第三方網站的付款方式。而 **`Button`** 則與 **`Redirection`** 付款方式類似，唯一的區別在於它會以按鈕形式顯示在購物車頁面上（例如 *Google Checkout*）。
- **SkipPaymentInfo**。指出我們是否應為此此外掛顯示付款資訊頁面。

---
## 結論

希望這些內容能幫助您開始新增付款提供程序。

---