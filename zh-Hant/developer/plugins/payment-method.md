---
標題: 如何撰寫自己的付款提供程序
uid: zh-Hant/developer/plugins/payment-method
作者: git.AndreiMaz
貢獻者: git.Sandeep911, git.exileDev, git.DmitriyKulagin
---

# 如何撰寫自己的付款提供程序

在 nopCommerce 中，付款提供程序是以「外掛」的形式實作。建議您在開始編寫新的付款提供程序程式碼之前，先閱讀 [如何為 nopCommerce 4.90 編寫外掛](xref:zh-Hant/developer/plugins/how-to-write-plugin-4.90)。該文件將為您說明建立外掛所需的步驟。

實際上，付款提供程序是一個實作了 **`IPaymentMethod`** 介面（位於 *Nop.Services.Payments* 命名空間）的一般外掛。正如您所料，*IPaymentMethod* 介面是用於建立付款提供程序外掛。它包含一些專屬於付款提供程序的方法，例如 `ProcessPaymentAsync()` 或 `GetAdditionalHandlingFeeAsync()`。因此，請將一個新的付款外掛專案（*類別庫*）加入至解決方案中，讓我們開始吧。

## 控制器 (Controllers)、檢視 (Views)、模型 (Models)

您需要做的第一件事是建立一個控制器。此控制器負責回應對 ASP.NET MVC 網站發出的請求。

1. 在實作新的付款提供程序時，此控制器應繼承自特殊的 **BasePaymentController** 抽象類別。

1. 然後實作用於外掛設定的 **Configure** 動作方法（由商店擁有者在後台管理中進行設定）。此方法以及對應的檢視將定義商店擁有者如何在後台管理中看到設定選項（*系統 → 設定 → 付款提供程序*）。

## 公開檢視元件 (Public view component.GetPublicViewComponent)

接著，您必須建立一個檢視元件，以便在前台商店中顯示此外掛。此檢視元件以及對應的檢視將定義顧客在結帳期間如何看到付款資訊頁面。首先，讓我們建立一個檢視元件類別。它應放置在 *`/Components`* 資料夾中。請參考 *PayPalCommerce* 外掛的實作方式：

```csharp
public class PaymentInfoViewComponent : NopViewComponent
{
    public IViewComponentResult Invoke(string widgetZone, object additionalData)
    {
        return View("~/Plugins/Payments.PayPalCommerce/Views/Public/PaymentInfo.cshtml");
    }
}
```

**Invoke** 方法會從外掛的 */Views* 資料夾返回對應的 `PaymentInfo` 檢視。請注意，我們使用的是自訂的 `NopViewComponent` 類別作為基底類別，而不是現有的內建 `ViewComponent`。

接下來，讓我們建立顯示付款資訊的 `PaymentInfo` 檢視。在該處，我們僅渲染一段文字，告知顧客將被重新導向至付款頁面。但如有需要，也可以建立更複雜的檢視元件。例如，如果您想在付款資訊頁面上收集顧客資訊，可以參考 `PayPalDirect` 付款外掛的實作方式。

## 付款處理 (Payment processing)

現在，您需要建立一個實作 **IPaymentMethod** 介面的類別。這就是將執行與付款閘道進行通訊之所有實際工作的類別。當有人建立訂單時，您類別中的 `ProcessPayment` 或 `PostProcessPayment` 方法將會被呼叫。以下是 **CheckMoneyOrderPaymentProcessor** 類別的定義方式（`CheckMoneyOrder` 付款提供程序）：

```csharp
public class CheckMoneyOrderPaymentProcessor : BasePlugin, IPaymentMethod
```

**IPaymentMethod** 介面有幾個必須實作的方法和屬性。

- **ValidatePaymentFormAsync**：用於前台商店以驗證顧客輸入。它會返回一個警告清單（例如，顧客未輸入信用卡名稱）。如果您的付款提供程序不需要顧客輸入額外資訊，則 `ValidatePaymentFormAsync` 應返回一個空清單：

    ```csharp
    public Task<IList<string>> ValidatePaymentFormAsync(IFormCollection form)
    {
        return Task.FromResult<IList<string>>(new List<string>());
    }
    ```

- **GetPaymentInfoAsync** 方法：用於前台商店以解析顧客輸入（例如信用卡資訊）。此方法會返回一個 `ProcessPaymentRequest` 物件，其中包含解析後的顧客輸入內容。如果您的付款提供程序不需要顧客輸入額外資訊，則 `GetPaymentInfoAsync` 將返回一個空的 `ProcessPaymentRequest` 物件：

    ```csharp
    public Task<ProcessPaymentRequest> GetPaymentInfoAsync(IFormCollection form)
    {
        return Task.FromResult(new ProcessPaymentRequest());
    }
    ```

- **ProcessPaymentAsync**：此方法總是在顧客下訂單前被立即呼叫。當您需要在訂單儲存到資料庫之前處理付款時，請使用此方法。例如，請款或授權信用卡。通常，當顧客不需要被重新導向至第三方網站以完成付款，且所有付款都在您的網站上處理時（例如 *PayPalCommerce*），會使用此方法。

    ```csharp
    public Task<ProcessPaymentResult> ProcessPaymentAsync(ProcessPaymentRequest processPaymentRequest)
    {
        return Task.FromResult(new ProcessPaymentResult());
    }
    ```

- **PostProcessPaymentAsync**：此方法在顧客下訂單後被立即呼叫。通常，當您需要將顧客重新導向至第三方網站以完成付款時（例如 PayPal Standard），會使用此方法。
- **HidePaymentMethodAsync**：您可以在此處放置任何邏輯。例如，如果購物車中的所有商品均為「可下載商品」，則隱藏此付款提供程序；或者如果目前的顧客來自特定國家/地區，則隱藏此付款提供程序。
- **GetAdditionalHandlingFeeAsync**：您可以返回任何會加總到訂單總額中的額外處理費。
- **CaptureAsync**：某些付款閘道允許您在請款前先進行付款授權。這允許商店擁有者在付款完成前審核訂單詳情。在此情況下，您只需在 **ProcessPaymentAsync** 或 **PostProcessPaymentAsync** 方法（如上所述）中授權付款，然後再進行請款 (Capture)。在這種情況下，後台管理的訂單詳情頁面上將會顯示一個「請款」按鈕。請注意，訂單必須已完成授權，且 **SupportCapture** 屬性必須返回 **`true`**。
- **RefundAsync**：此方法允許您進行退款。在此情況下，後台管理的訂單詳情頁面上將會顯示一個「退款」按鈕。請注意，訂單必須已付款，且 **SupportRefund** 或 **SupportPartiallyRefund** 屬性必須返回 **`true`**。
- **VoidAsync**：此方法允許您撤銷已授權但未請款的付款。在此情況下，後台管理的訂單詳情頁面上將會顯示一個「撤銷」按鈕。請注意，訂單必須已授權，且 **SupportVoid** 屬性必須返回 **`true`**。
- **ProcessRecurringPaymentAsync**：使用此方法來處理定期購付款。
- **CancelRecurringPaymentAsync**：使用此方法來取消定期購付款。
- **CanRePostProcessPaymentAsync**：通常此方法用於將顧客重新導向至第三方網站以完成付款的情境。如果第三方付款失敗，此選項將允許顧客稍後嘗試重新下單，而無需建立新訂單。若要啟用此功能，**CanRePostProcessPaymentAsync** 應返回 **`true`**。
- **GetConfigurationPageUrl**：正如您記得的，我們在前面的步驟中建立了一個控制器。此方法應返回該控制器 `Configure` 方法的 URL。例如：

    ```csharp
    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/PaymentCheckMoneyOrder/Configure";
    }
    ```

- **GetPaymentMethodDescriptionAsync**：此方法取得將顯示在前台商店結帳頁面上的付款提供程序描述。

    ```csharp
    public async Task<string> GetPaymentMethodDescriptionAsync()
    {
        return await _localizationService.GetResourceAsync("Plugins.Payment.CheckMoneyOrderPaymentMethodDescription");
    }
    ```

- **SupportCapture, SupportPartiallyRefund, SupportRefund, SupportVoid**：這些屬性指出是否支援付款提供程序的對應方法。
- **RecurringPaymentType**：此屬性指出是否支援定期購付款。
- **PaymentMethodType**：此屬性指出付款提供程序的類型。目前有三種類型：**`Standard`** 用於顧客不需要重新導向至第三方網站的付款方式；**`Redirection`** 用於顧客會被重新導向至第三方網站的付款方式；**`Button`** 類似於 **`Redirection`**，唯一的差別在於它會以按鈕形式顯示在購物車頁面上（例如 *Google Checkout*）。
- **SkipPaymentInfo**：指出我們是否應為此外掛顯示付款資訊頁面。

## 結論

希望這些資訊能幫助您順利開始新增付款提供程序。