---
title: প্রকল্পে web.config ফাইলের বর্ণনা
uid: bn/developer/tutorials/description-of-the-web-config-file-in-project 
author: nop.sea
contributors: git.AfiaKhanom
---

# প্রকল্পে web.config ফাইলের বর্ণনা

## Web.config ফাইল কি

`web.config` ফাইলটি একটি এক্সএমএল ভিত্তিক কনফিগারেশন ফাইল যা ASP.NET ভিত্তিক অ্যাপ্লিকেশনে ব্যবহৃত হয় যা আমাদের ওয়েবসাইটের কনফিগারেশনের সাথে সম্পর্কিত বিভিন্ন সেটিংস পরিচালনা করে। এভাবে আমরা আমাদের অ্যাপ্লিকেশন লজিককে কনফিগারেশন লজিক থেকে আলাদা করতে পারি। এবং এর প্রধান সুবিধা হল, যদি আমরা কিছু কনফিগারেশন সেটিংস পরিবর্তন করতে চাই তাহলে নতুন পরিবর্তনগুলি প্রয়োগ করার জন্য আমাদের অ্যাপ্লিকেশন পুনরায় চালু করার প্রয়োজন নেই, ASP.NET স্বয়ংক্রিয়ভাবে পরিবর্তনগুলি সনাক্ত করে এবং চলমান ASP.NET অ্যাপ্লিকেশনে প্রয়োগ করে।

ASP.NET ফ্রেমওয়ার্ক একটি শ্রেণিবিন্যাস কনফিগারেশন সিস্টেম ব্যবহার করে। আপনি একটি অ্যাপ্লিকেশনের যেকোনো সাবডিরেক্টরিতে একটি `web.config` ফাইল রাখতে পারেন। ফাইলটি একই ডিরেক্টরি বা যেকোনো সাবডিরেক্টরিতে থাকা যেকোনো পৃষ্ঠায় প্রযোজ্য।

## নপকমার্স এর জন্য web.config

নপকমার্স `Nop.Web` প্রকল্পে web.config ব্যবহার করে যা প্রেজেন্টেশন ডিরেক্টরিতে পাওয়া যাবে। প্রজেক্ট ডাইরেক্টরির মূলে, আপনি একটি web.config ফাইল দেখতে পারেন। যদি আপনার সমাধান নপকমার্স এর নতুন ইনস্টলেশন হয় তবে সেই ফাইলের বিষয়বস্তু এইরকম কিছু দেখায়:

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

প্রতিটি কনফিগারেশন নিয়ম "`<configuration>`" এলিমেন্টের ভিতরে যায়।

```xml
<system.webServer>
    ...
</system.webServer>
```

`<System.webServer>` উপাদানটি আইআইএস-এর জন্য অনেক সাইট-লেভেল এবং অ্যাপ্লিকেশন-লেভেল কনফিগারেশন সেটিংসের মূল উপাদান নির্দিষ্ট করে এবং কনফিগারেশন উপাদান থাকে যা ওয়েব সার্ভার ইঞ্জিন এবং মডিউল দ্বারা ব্যবহৃত সেটিংস সংজ্ঞায়িত করে।

```xml
<modules>
    <!-- Remove WebDAV module so that we can make DELETE requests -->
    <remove name="WebDAVModule" />
</modules>
```

`<modules>` উপাদানটি একটি অ্যাপ্লিকেশনের জন্য নিবন্ধিত নেটিভ-কোড মডিউল এবং পরিচালিত-কোড মডিউল সংজ্ঞায়িত করে। আমরা সাধারণত কাস্টমাইজড কার্যকারিতা বাস্তবায়নের জন্য মডিউল ব্যবহার করি।

`<modules>` এলিমেন্টে `<add>`, `<remove>` এবং `<clear>` এলিমেন্টের একটি সংগ্রহ রয়েছে।

এখানে নপকমার্স অ্যাপ্লিকেশন ব্যবহার করে WebDAVModule মডিউল অপসারণ করতে `<remove>` উপাদান ব্যবহার করছে।

```xml
<handlers>
    <!-- Remove WebDAV module so that we can make DELETE requests -->
    <remove name="WebDAV" />
    <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
</handlers>
```

হ্যান্ডলারগুলি হল ইন্টারনেট আইআইএস উপাদান যা নির্দিষ্ট বিষয়বস্তুতে অনুরোধ প্রক্রিয়া করার জন্য কনফিগার করা হয়, সাধারণত অনুরোধ রিসোর্সের জন্য একটি প্রতিক্রিয়া তৈরি করতে। উদাহরণস্বরূপ, ASP.NET ওয়েব পেজ হল এক ধরনের হ্যান্ডলার। আপনি হ্যান্ডলার ব্যবহার করতে পারেন এমন কোনো রিসোর্সে রিকোয়েস্ট প্রসেস করার জন্য যা ব্যবহারকারীদের তথ্য ফেরত দিতে হবে যা স্ট্যাটিক ফাইল নয়।

`<handlers>` উপাদানটিতে `<add>`, `<remove>` এবং `<clear>` উপাদানগুলির একটি সংগ্রহ রয়েছে, যার প্রত্যেকটিই অ্যাপ্লিকেশনের জন্য একটি হ্যান্ডলার ম্যাপিং সংজ্ঞায়িত করে। `<add>` উপাদান হ্যান্ডলার সংগ্রহে একটি হ্যান্ডলার যোগ করে, `<remove>` উপাদান হ্যান্ডলার সংগ্রহ থেকে হ্যান্ডলারের একটি রেফারেন্স সরিয়ে দেয় এবং `<clear>` উপাদান হ্যান্ডলার সংগ্রহ থেকে হ্যান্ডলারের সমস্ত রেফারেন্স সরিয়ে দেয়। এখানে উপরের কোডে "WebDAV" হ্যান্ডলার সরানো হয়েছে এবং মডিউল `AspNetCoreModuleV2` এর জন্য হ্যান্ডলার যোগ করা হয়েছে।

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

`<httpProtocol>` উপাদানটির `<customHeaders>` উপাদান কাস্টম এইচটিটিপি হেডার নির্দিষ্ট করে যে আইআইএস ওয়েব সার্ভার থেকে এইচটিটিপি প্রতিক্রিয়াগুলিতে ফিরে আসবে।

এইচটিটিপি হেডার হল নাম এবং মান জোড়া যা একটি ওয়েব সার্ভার থেকে প্রতিক্রিয়াগুলিতে ফেরত দেওয়া হয়। ডিফল্ট এইচটিটিপি হেডার সহ ক্লায়েন্টকে কাস্টম রেসপন্স হেডার পাঠানো হয়। রিডাইরেক্ট রেসপন্স হেডারের বিপরীতে, যেগুলো রিডাইরেকশন হলেই রেসপন্সে ফেরত আসে, কাস্টম রেসপন্স হেডার প্রতিটি রেসপন্সে ফেরত দেওয়া হয়।

## আইআইএস -এ পুননির্দেশিত নিয়ম কনফিগার করুন

আমরা উপরের কনফিগারেশনে অতিরিক্ত কনফিগারেশন যোগ করতে পারি। এখানে আমরা দেখব কিভাবে আইআইএস -এ পুননির্দেশিত নিয়ম কনফিগার করতে হয়।

একটি পুননির্দেশিত নিয়ম একাধিক ওয়েবকে একটি একক ওয়েব পেজে নির্দেশ করতে সক্ষম করে। আপনি একটি সার্ভারে অন্য সার্ভারে অনুরোধ পুননির্দেশিত করতে চান তার বিভিন্ন কারণ থাকতে পারে। উদাহরণস্বরূপ, আপনার কোম্পানির নাম পরিবর্তন করা হতে পারে এবং আপনি আপনার কোম্পানির জন্য একটি নতুন ডোমেইন নিবন্ধন করতে এবং আপনার ওয়েবসাইটকে নতুন ডোমেইনে স্থানান্তর করতে চাইতে পারেন, সেক্ষেত্রে আপনি আপনার পুরানো ডোমেইন থেকে সমস্ত অনুরোধ নতুন ডোমেইনে পুননির্দেশিত করতে চাইতে পারেন।

আমাদের ওয়েবসাইট পুননির্দেশিত নিয়মগুলি ব্যবহার করতে সক্ষম হওয়ার জন্য, আমাদের ইউআরএল পুনর্লিখন মডিউল ইনস্টল করতে হবে যা আইআইএসের একটি এক্সটেনশন।

বিক্ষোভের উদ্দেশ্যে বলা যাক আমাদের পুরনো সাইটের অনুরোধ আমাদের নতুন সাইটে রিডাইরেক্ট করতে হবে, এজন্য আমাদের web.config ফাইলে নিম্নলিখিত নিয়ম লিখতে হবে।

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
> এই নিয়মটি ব্যবহার করে আমরা একটি পুরানো ডোমেইন নামের সমস্ত পৃষ্ঠাগুলিকে একটি নতুন ডোমেইন নামের একই পৃষ্ঠায় পুননির্দেশ করতে পারি।

এখানে আমাদের উপযুক্ত নিয়ম দিয়ে [RULE NAME], [OLD URL] এবং [NEW URL] প্রতিস্থাপন করতে হবে।

* [RULE NAME] এই নিয়মটি কী করছে তা বর্ণনা করার মতো যে কোনও জিনিস হতে পারে।
* [OLD URL] আপনি যে পুরানো ইউআরএল থেকে পুননির্দেশিত করতে চান।
* [NEW URL] হল নতুন ইউআরএল যা আপনি পুনdনির্দেশিত করতে চান।

```xml
<match url="(.*)" />
```

উপরের উপাদানটি বোঝায় যে এই নিয়মটি সমস্ত ইউআরএল স্ট্রিংয়ের সাথে মিলবে।

```xml
<add input="{HTTP_HOST}{REQUEST_URI}" pattern="[OLD URL]" />
```

উপরের উপাদানটি নিয়মে একটি শর্ত জুড়ে দেয় যা হোস্ট পুনরুদ্ধার করে এবং সার্ভার ভেরিয়েবল HTTP_HOST এবং REQUEST_URI পড়ে উরি হেডার মান অনুরোধ করে এবং [OLD URL] এর জন্য সরবরাহকৃত মান সহ প্যাটার্নের সাথে এটি মেলে।

```xml
<action type="Redirect" url="http://[NEW URL]/{R:1}" redirectType="Permanent"/>
```

এই উপাদানটি পুরানো ইউআরএল কে নতুন ইউআরএল- এ পুননির্দেশিত করে।
