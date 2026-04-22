---
標題: web.config 中的設定
uid: zh-Hant/developer/tutorials/description-of-the-web-config-file-in-project
作者: nop.sea
貢獻者: git.RomanovM, git.DmitriyKulagin
---

# web.config 中的設定

## 什麼是 web.config 檔案

`web.config` 檔案是一個基於 XML 的設定檔，用於 ASP.NET 應用程式中，以管理與網站設定相關的各種參數。透過這種方式，我們可以將應用程式邏輯與設定邏輯分離。其主要優點在於，當我們需要變更某些設定時，無需重新啟動應用程式即可套用新變更，ASP.NET 會自動偵測變更並將其套用到正在執行的 ASP.NET 應用程式中。

ASP.NET 框架使用階層式的設定系統。您可以將 `web.config` 檔案放置在應用程式的任何子目錄中。該檔案將適用於位於相同目錄或任何子目錄中的所有頁面。

## nopCommerce 的 web.config

nopCommerce 在 `Nop.Web` 專案中使用 `web.config`，您可以在 Presentation 目錄內找到它。在專案目錄的根目錄中，您可以看到一個 web.config 檔案。如果您的解決方案是全新安裝的 nopCommerce，該檔案的內容看起來會像這樣：

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <system.webServer>
    <modules>
        <!-- Remove WebDAV module so that we can make DELETE requests -->
        <remove name="WebDAVModule" />
    </modules>
    <handlers>
        <!-- Remove WebDAV module so that we can make DELETE requests -->
        <remove name="WebDAV" />
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
    </handlers>
    <!-- When deploying on Azure, make sure that "dotnet" is installed and the path to it is registered in the PATH environment variable or specify the full path to it -->
    <aspNetCore requestTimeout="23:00:00" processPath="%LAUNCHER_PATH%" arguments="%LAUNCHER_ARGS%" forwardWindowsAuthToken="false" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" startupTimeLimit="3600" hostingModel="InProcess">
    </aspNetCore>
    <httpProtocol>
        <customHeaders>
        <remove name="X-Powered-By" />
        <!-- Protects against XSS injections. ref.: https://www.veracode.com/blog/2014/03/guidelines-for-setting-security-headers/ -->
        <add name="X-XSS-Protection" value="1; mode=block" />
        <!-- Protects against Clickjacking attacks. ref.: http://stackoverflow.com/a/22105445/1233379 -->
        <add name="X-Frame-Options" value="SAMEORIGIN" />
        <!-- Protects against MIME-type confusion attack. ref.: https://www.veracode.com/blog/2014/03/guidelines-for-setting-security-headers/ -->
        <add name="X-Content-Type-Options" value="nosniff" />
        <!-- Protects against Clickjacking attacks. ref.: https://www.owasp.org/index.php/HTTP_Strict_Transport_Security_Cheat_Sheet -->
        <add name="Strict-Transport-Security" value="max-age=31536000; includeSubDomains" />
        <!-- CSP modern XSS directive-based defence, used since 2014. ref.: http://content-security-policy.com/ -->
        <add name="Content-Security-Policy" value="default-src 'self'; connect-src *; font-src * data:; frame-src *; img-src * data:; media-src *; object-src *; script-src * 'unsafe-inline' 'unsafe-eval'; style-src * 'unsafe-inline';" />
        <!-- Prevents from leaking referrer data over insecure connections. ref.: https://scotthelme.co.uk/a-new-security-header-referrer-policy/ -->
        <add name="Referrer-Policy" value="same-origin" />
        <!-- Permissions-Policy is a new header that allows a site to control which features and APIs can be used in the browser. ref.: https://w3c.github.io/webappsec-permissions-policy/ -->
        <add name="Permissions-Policy" value="accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=*, usb=()" />
      </customHeaders>
    </httpProtocol>
    </system.webServer>
</configuration>
```

```xml
<configuration>
    ...
</configuration>
```

每個設定規則都位於 "`<configuration>`" 元素內。

```xml
<system.webServer>
    ...
</system.webServer>
```

`<system.webServer>` 元素指定了 IIS 許多網站層級和應用程式層級設定的根元素，並包含定義 Web 伺服器引擎與模組所使用設定的設定元素。

```xml
<modules>
    <!-- Remove WebDAV module so that we can make DELETE requests -->
    <remove name="WebDAVModule" />
</modules>
```

`<modules>` 元素定義了為應用程式註冊的原生程式碼模組與受控程式碼模組。我們通常使用模組來實作自定義功能。

`<modules>` 元素包含 `<add>`、`<remove>` 和 `<clear>` 元素的集合。

這裡 nopCommerce 使用 `<remove>` 元素來從應用程式中移除 WebDAVModule 模組。

```xml
<handlers>
    <!-- Remove WebDAV module so that we can make DELETE requests -->
    <remove name="WebDAV" />
    <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
</handlers>
```

處理常式（Handlers）是 IIS 的元件，配置用於處理對特定內容的請求，通常是為請求的資源產生回應。例如，ASP.NET 網頁就是一種處理常式。您可以使用處理常式來處理任何需要向使用者傳回資訊（而非靜態檔案）的資源請求。

`<handlers>` 元素包含 `<add>`、`<remove>` 和 `<clear>` 元素的集合，每一個都定義了應用程式的處理常式映射。`<add>` 元素將處理常式新增至處理常式集合中，`<remove>` 元素從處理常式集合中移除處理常式的參照，而 `<clear>` 元素則從處理常式集合中移除所有處理常式的參照。在上述程式碼中，「WebDAV」處理常式被移除，並新增了 `AspNetCoreModuleV2` 模組的處理常式。

```xml
<aspNetCore requestTimeout="23:00:00" processPath="%LAUNCHER_PATH%" arguments="%LAUNCHER_ARGS%" forwardWindowsAuthToken="false" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" startupTimeLimit="3600" hostingModel="InProcess"/>
```

```xml
<httpProtocol>
        <customHeaders>
        <remove name="X-Powered-By" />
        <!-- Protects against XSS injections. ref.: https://www.veracode.com/blog/2014/03/guidelines-for-setting-security-headers/ -->
        <add name="X-XSS-Protection" value="1; mode=block" />
        <!-- Protects against Clickjacking attacks. ref.: http://stackoverflow.com/a/22105445/1233379 -->
        <add name="X-Frame-Options" value="SAMEORIGIN" />
        <!-- Protects against MIME-type confusion attack. ref.: https://www.veracode.com/blog/2014/03/guidelines-for-setting-security-headers/ -->
        <add name="X-Content-Type-Options" value="nosniff" />
        <!-- Protects against Clickjacking attacks. ref.: https://www.owasp.org/index.php/HTTP_Strict_Transport_Security_Cheat_Sheet -->
        <add name="Strict-Transport-Security" value="max-age=31536000; includeSubDomains" />
        <!-- CSP modern XSS directive-based defence, used since 2014. ref.: http://content-security-policy.com/ -->
        <add name="Content-Security-Policy" value="default-src 'self'; connect-src *; font-src * data:; frame-src *; img-src * data:; media-src *; object-src *; script-src * 'unsafe-inline' 'unsafe-eval'; style-src * 'unsafe-inline';" />
        <!-- Prevents from leaking referrer data over insecure connections. ref.: https://scotthelme.co.uk/a-new-security-header-referrer-policy/ -->
        <add name="Referrer-Policy" value="same-origin" />
        <!-- Permissions-Policy is a new header that allows a site to control which features and APIs can be used in the browser. ref.: https://w3c.github.io/webappsec-permissions-policy/ -->
        <add name="Permissions-Policy" value="accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=*, usb=()" />
      </customHeaders>
    </httpProtocol>
```

`<httpProtocol>` 元素的 `<customHeaders>` 元素指定了 IIS 將在來自 Web 伺服器的 HTTP 回應中傳回的自定義 HTTP 標頭。

HTTP 標頭是成對的名稱與值，會在 Web 伺服器的回應中傳回。自定義回應標頭會與預設的 HTTP 標頭一起傳送到用戶端。與僅在發生重新導向時才傳回的重新導向回應標頭不同，自定義回應標頭會包含在每一次的回應中。

## 在 IIS 中設定重新導向規則

我們可以在上述設定之外新增其他設定。在這裡，我們將了解如何在 IIS 中設定重新導向規則。

重新導向規則可以讓多個 URL 指向同一個網頁。您可能基於多種原因需要將請求從一台伺服器重新導向到另一台。例如，您的公司名稱變更了，您可能想要為公司註冊一個新網域並將網站遷移過去，在這種情況下，您會希望將所有來自舊網域的請求重新導向到新網域。

為了讓我們的網站能夠使用重新導向規則，我們需要安裝「URL Rewrite」模組，這是 IIS 的一個擴充功能。

為了示範，假設我們必須將請求從舊網站重新導向到新網站，我們需要在 `web.config` 檔案中撰寫以下規則。

```xml
<rewrite>
  <rules>
     <rule name="[RULE NAME]" stopProcessing="true">
     <match url="(.*)" />
     <conditions logicalGrouping="MatchAny" trackAllCaptures="false">
        <add input="{HTTP_HOST}{REQUEST_URI}" pattern="[OLD URL]" />
     </conditions>
     <action type="Redirect" url="http://[NEW URL]/{R:1}" redirectType="Permanent"/>
     </rule>
  </rules>
</rewrite>
```

> [!NOTE]
> 透過使用此規則，我們可以將舊網域名稱的所有頁面重新導向到新網域名稱上的相同頁面。

這裡我們需要將 [RULE NAME]、[OLD URL] 和 [NEW URL] 替換為適當的資訊。

* [RULE NAME] 可以是任何描述該規則用途的名稱。
* [OLD URL] 是您想要重新導向的舊 URL。
* [NEW URL] 是您想要重新導向到的新 URL。

```xml
<match url="(.*)" />
```

上述元素宣告此規則將符合所有的 URL 字串。

```xml
<add input="{HTTP_HOST}{REQUEST_URI}" pattern="[OLD URL]" />
```

上面的元素為規則新增了一個條件，它透過讀取伺服器變數 HTTP_HOST 和 REQUEST_URI 來擷取主機與請求的 Uri 標頭值，並將其與為 [OLD URL] 提供的值所組成的模式進行比對。

```xml
<action type="Redirect" url="http://[NEW URL]/{R:1}" redirectType="Permanent"/>
```

此元素將符合條件的舊 URL 重新導向到新 URL。