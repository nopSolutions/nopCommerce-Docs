---
標題: 內容安全政策 (CSP) 標頭
uid: zh-Hant/developer/tutorials/csp-headers
作者: git.nopsg
貢獻者: git.nopsg, git.DmitriyKulagin
---

# 內容安全政策 (CSP) 標頭

Content-Security-Policy (CSP) 是現代瀏覽器用於增強文件（或網頁）安全性的一種 HTTP 回應標頭。HTTP 內容安全政策回應標頭讓網站管理員能夠限制使用者在網站內可以載入的資源（例如 JavaScript 和 CSS），進而掌握控制權。換句話說，您可以將您網站的內容來源加入白名單。雖然它主要作為 HTTP 回應標頭使用，但您也可以透過 meta 標籤來應用它。

```html
<meta http-equiv="Content-Security-Policy" content="default-src 'self'; img-src 'self' https://img.nopcommerce.com; object-src 'none'; script-src 'self'; style-src 'self'; frame-ancestors 'self'; base-uri 'self'; form-action 'self';">
```

若要新增此自訂 meta 標籤，您可以前往 `www.yourStore.com/Admin/Setting/GeneralCommon`，找到 **`Custom <head> tag`**，並如下圖所示進行新增。

![custom CSP head tag image](_static/csp-headers/custom-csp-head-tag.png)

內容安全政策可防止 **跨網站指令碼 (XSS)** 及其他形式的攻擊，例如 **點擊劫持 (ClickJacking)**。雖然它無法完全消除這些攻擊的可能性，但絕對能將損害降至最低。大多數主流瀏覽器都支援 CSP，因此相容性不是問題。Internet Explorer 不支援此功能。

若要測試您的瀏覽器是否支援 CSP，您可以參考此 [連結](https://content-security-policy.com/browser-test/)。

## CSP 指令參考

**Content-Security-Policy** 標頭值由一個或多個指令（定義如下）組成，多個指令之間以 *分號 (;)* 分隔。

### default-src

*default-src* 指令定義了獲取資源（如 JavaScript、圖片、CSS、字型、AJAX 請求、框架和 HTML5 媒體）的預設政策。並非所有指令都會回退到 *default-src*。

```html
default-src 'self' cdn.nopcommerce.com;
```

### script-src

定義 JavaScript 的有效來源。

```html
script-src 'self' js.nopcommerce.com;
```

### style-src

定義樣式表或 CSS 的有效來源。

```html
style-src 'self' css.nopcommerce.com;
```

### img-src

定義圖片的有效來源。

```html
img-src 'self' img.nopcommerce.com;
```

### connect-src

適用於 *XMLHttpRequest (AJAX)、WebSocket 或 EventSource*。若不允許，瀏覽器將模擬 **400** HTTP 狀態碼。

```html
connect-src 'self';
```

### font-src

定義字型資源的有效來源（透過 *@font-face* 載入）。

```html
font-src font.nopcommerce.com;
```

### object-src

定義外掛的有效來源，例如 `<object>`、`<embed>` 或 `<applet>`。

```html
object-src 'self';
```

### media-src

定義音訊和影片的有效來源，例如 HTML5 `<audio>`、`<video>` 元素。

```html
media-src media.nopcommerce.com;
```

### frame-src

定義載入框架的有效來源。

```html
frame-src 'self';
```

### sandbox

為請求的資源啟用沙盒，類似於 *iframe sandbox* 屬性。沙盒會套用同源政策並防止彈出視窗、外掛和指令碼執行。您可以將 sandbox 值留空以保持所有限制，或新增值：*allow-forms allow-same-origin allow-scripts allow-popups, allow-modals, allow-orientation-lock, allow-pointer-lock, allow-presentation, allow-popups-to-escape-sandbox* 和 *allow-top-navigation*。

```html
sandbox allow-forms allow-scripts;
```

### child-src

定義用於載入 Web Worker 以及使用如 `<frame>` 和 `<iframe>` 等元素載入的巢狀瀏覽內容的有效來源。

```html
child-src 'self'
```

### form-action

定義可用作 HTML `<form>` 動作的有效來源。

```html
form-action 'self';
```

### frame-ancestors

定義使用 `<frame> <iframe> <object> <embed> <applet>` 嵌入資源的有效來源。將此指令設為 **'none'** 大致等同於 **X-Frame-Options: DENY**。

```html
frame-ancestors 'none';
```

### plugin-types

定義透過 `<object>` 和 `<embed>` 調用的外掛之有效 MIME 類型。若要載入 `<applet>`，您必須指定 *application/x-java-applet*。

```html
plugin-types application/pdf;
```

### report-to

定義由 *Report-To* HTTP 回應標頭定義的報告群組名稱。詳情請參閱 [Reporting API](https://w3c.github.io/reporting/)。

```html
report-to groupName;
```

### worker-src

限制可作為 Worker、SharedWorker 或 ServiceWorker 載入的 URL。

```html
worker-src 'none';
```

### manifest-src

限制可載入應用程式清單 (manifest) 的 URL。

```html
manifest-src 'none';
```

### navigate-to

限制文件可以透過任何方式導覽至的 URL。例如，當點擊連結、提交表單或調用 *window.location* 時。如果存在 *form-action*，則此指令在表單提交時會被忽略。[實作狀態](https://www.chromestatus.com/features/6457580339593216)

```html
navigate-to nopcommerce.com
```

## Content-Security-Policy 範例

### 僅允許來自同源的內容

```html
default-src 'self';
```

### 僅允許來自同源的指令碼

```html
script-src 'self';
```

### 允許 Google Analytics、Google AJAX CDN 及同源內容

```html
script-src 'self' www.google-analytics.com ajax.googleapis.com;
```

### 起始政策

```html
default-src 'none'; script-src 'self'; connect-src 'self'; img-src 'self'; style-src 'self';
```

此政策允許來自同源的圖片、指令碼、AJAX 和 CSS，且不允許載入任何其他資源（例如 object、frame、media 等）。對於許多網站來說，這是一個很好的起點。

## Content-Security-Policy 錯誤訊息

根據瀏覽器的不同，CSP 錯誤訊息可能會有所差異。

在 Chrome 開發人員工具中，我們可以看到以下訊息：

```js
Refused to load the script 'script-uri' because it violates the following Content Security Policy directive: "your CSP directive".
```

在 Firefox 開發人員工具中，訊息如下：

```js
Content Security Policy: A violation occurred for a report-only CSP policy ("An attempt to execute inline scripts has been blocked"). The behavior was allowed, and a CSP report was sent.
```

除了主控台訊息外，視窗上還會觸發 **securitypolicyviolation** 事件。更多資訊請參閱此 [連結](https://www.w3.org/TR/CSP2/#firing-securitypolicyviolationevent-events.)。