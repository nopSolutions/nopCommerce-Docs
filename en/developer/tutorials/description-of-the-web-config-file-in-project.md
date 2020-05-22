---
title: Description of the web.config file in project
uid: en/developer/tutorials/description-of-the-web-config-file-in-project
author: nop.sea
contributors: git.RomanovM, git.DmitriyKulagin
---

# Description of the web.config file in project

## What is web.config file

web.config file is a xml based configuration file used in ASP.NET based application to manage various settings that concerned with configuration of our website. In this way we can separate our application logic from configuration logic. And the main benefit of this is, if we want to change some configuration settings then we do not need to restart our application to apply new changes, ASP.NET automatically detects the changes and applies them to the running ASP.NET application.

The ASP.NET framework uses a hierarchical configuration system. You can place a Web.Config file in any subdirectory of an application. The file then applies to any pages located in the same directory or any subdirectories.

## web.config for nopCommerce

nopCommerce uses web.config in `Nop.Web` project which can be found inside Presentation directory. In the root of  the project directory there you can see a web.config file. If your solution is fresh installation of nopCommerce then the content of that file looks something like this.

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
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModule" resourceType="Unspecified" />
    </handlers>
    <!-- When deploying on Azure, make sure that "dotnet" is installed and the path to it is registered in the PATH environment variable or specify the full path to it -->
    <aspNetCore requestTimeout="23:00:00" processPath="%LAUNCHER_PATH%" arguments="%LAUNCHER_ARGS%" forwardWindowsAuthToken="false" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" startupTimeLimit="3600">
        <environmentVariables />
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
        <add name="Content-Security-Policy" value="default-src 'self'; connect-src *; font-src *; frame-src *; img-src * data:; media-src *; object-src *; script-src * 'unsafe-inline' 'unsafe-eval'; style-src * 'unsafe-inline';" />
        <!-- Prevents from leaking referrer data over insecure connections. ref.: https://scotthelme.co.uk/a-new-security-header-referrer-policy/ -->
        <add name="Referrer-Policy" value="strict-origin" />
        <!--Feature-Policy is a new header that allows a site to control which features and APIs can be used in the browser. ref.: https://wicg.github.io/feature-policy/ -->
        <add name="Feature-Policy" value="accelerometer 'none'; camera 'none'; geolocation 'none'; gyroscope 'none'; magnetometer 'none'; microphone 'none'; payment *; usb 'none'" />
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

Every configuration rules goes inside "`<configuration>`" element.

```xml
<system.webServer>
    ...
</system.webServer>
```

The `<system.webServer>` element specifies the root element for many of the site-level and application-level configuration settings for IIS, and contains configuration elements that define the settings used by the Web server engine and modules.

```xml
<modules>
    <!-- Remove WebDAV module so that we can make DELETE requests -->
    <remove name="WebDAVModule" />
</modules>
```

The `<modules>` element defines the native-code modules and managed-code modules that are registered for an application. We commonly use modules to implement customized functionality.

The `<modules>` element contains a collection of `<add>`, `<remove>` and `<clear>` elements.

Here nopCommerce is using `<remove>` element to remove WebDAVModule module form the application.

```xml
<handlers>
    <!-- Remove WebDAV module so that we can make DELETE requests -->
    <remove name="WebDAV" />
    <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModule" resourceType="Unspecified" />
</handlers>
```

Handlers are Internet IIS components that are configured to process requests to specific content, typically to generate a response for the request resource. For example, an ASP.NET Web page is one type of handler. You can use handlers to process requests to any resource that needs to return information to users that is not a static file.

The `<handlers>` element contains a collection of `<add>`, `<remove>` and `<clear>` elements, each of which defines a handler mapping for the application. The `<add>` element adds a handler to the collection of handlers, `<remove>` element removes a references of handler from the handlers collection and `<clear>` element removes all references of handlers from the handlers collection. Here in above code  "WebDAV" handler is removed and handler for module "AspNetCoreModele" is added.

```xml
<aspNetCore requestTimeout="23:00:00" processPath="%LAUNCHER_PATH%" arguments="%LAUNCHER_ARGS%" forwardWindowsAuthToken="false" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" startupTimeLimit="3600">
    <environmentVariables />
</aspNetCore>
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
        <add name="Content-Security-Policy" value="default-src 'self'; connect-src *; font-src *; frame-src *; img-src * data:; media-src *; object-src *; script-src * 'unsafe-inline' 'unsafe-eval'; style-src * 'unsafe-inline';" />
        <!-- Prevents from leaking referrer data over insecure connections. ref.: https://scotthelme.co.uk/a-new-security-header-referrer-policy/ -->
        <add name="Referrer-Policy" value="strict-origin" />
        <!--Feature-Policy is a new header that allows a site to control which features and APIs can be used in the browser. ref.: https://wicg.github.io/feature-policy/ -->
        <add name="Feature-Policy" value="accelerometer 'none'; camera 'none'; geolocation 'none'; gyroscope 'none'; magnetometer 'none'; microphone 'none'; payment *; usb 'none'" />
    </customHeaders>
</httpProtocol>
```

The `<customHeaders>` element of the `<httpProtocol>` element specifies custom HTTP headers that IIS will return in HTTP responses from the Web server.

HTTP headers are name and value pairs that are returned in responses from a Web server. Custom response headers are sent to the client together with the default HTTP header. Unlike redirect response headers, which are returned in responses only when redirection occurs, custom response headers are returned in every response.

## Configure the redirect rules in IIS

We can add other configurations additional to the above configurations. Here we will see how to configure redirect rules in IIS.

A redirect rule enables more than one URL to point to a single Web page. There may be several reasons why you want to redirect request to one server to another. For example, May be your company name is changed and you may want to register a new domain for your company and move your website to new domain, so in that case you may want to redirect all request from your old domain to new domain.

In order for our website to be able to use redirect rules, we need to install URL rewrite module which is an extension to IIS.

For demonstration purpose lets say we have to redirect request to our old site to our new site, for that we need to write following rules in our web.config file.

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
>
> By using this rule we can redirect all pages of an old domain name to the same page on a new domain name.

Here we need to replacing [RULE NAME], [OLD URL] and [NEW URL] with the appropriate information.

* [RULE NAME] can be any that thing like describes what this rule is doing
* [OLD URL] is the old url you want to redirect from.
* [NEW URL] is the new url you want to redirect to.

```xml
<match url="(.*)" />
```

The above element refers that this rule will match all URL string.

```xml
<add input="{HTTP_HOST}{REQUEST_URI}" pattern="[OLD URL]" />
```

The element above adds a condition to the rule that retrieves the host and request uri header value by reading the server variable HTTP_HOST and REQUEST_URI and matches it against the pattern with value supplied for [OLD URL].

```xml
<action type="Redirect" url="http://[NEW URL]/{R:1}" redirectType="Permanent"/>
```

This element redirects the matching old url to new url.
