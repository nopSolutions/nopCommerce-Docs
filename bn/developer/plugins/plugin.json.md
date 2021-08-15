---
title: Plugin.json ফাইলের কাঠামোর বর্ণনা
uid: bn/developer/plugins/plugin_json
author: git.cromatido
contributors: git.AfiaKhanom
---

# `plugin.json` ফাইলের কাঠামোর বর্ণনা

 এই ফাইলে একটি প্লাগইন এর জন্য মেটা তথ্যের বিবরণ রয়েছে যা নপকমার্স দ্বারা এই প্লাগইনটি কোন গ্রুপের অন্তর্গত তা নির্ধারণ করতে ব্যবহার করা হয়, যদি প্লাগইনটি নপকমার্স এর বর্তমান সংস্করণের সাথে সামঞ্জস্যপূর্ণ হয় বা না হয়, প্লাগইনটির সংস্করণ কি এবং অন্যান্য তথ্য। প্রতিটি নপকমার্স প্লাগইন এ ফাইল থাকতে হবে।

## ফাইল কাঠাম

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
- **Group**. এটি 'Admin/Configuration/LocalPlugin' মেনুর অধীনে প্লাগইন তালিকায় প্লাগইনটিকে চিহ্নিত বা অনুসন্ধান বা ফিল্টার করতে নপকমার্স দ্বারা ব্যবহৃত হয়। এটি আপনার কোম্পানির নাম হতে পারে।

- **FriendlyName**. এটি প্লাগইন এর প্রদর্শনের নাম। এটি প্লাগইন তালিকা থেকে আমাদের প্লাগইন সনাক্ত করতে ব্যবহৃত হয়।

- **SystemName**. এটি নপকমার্স দ্বারা প্লাগইনটিকে অনন্যভাবে চিহ্নিত করতে ব্যবহার করা হয়, তাই এটি অন্য সব প্লাগইন থেকে অনন্য হওয়া প্রয়োজন। আমরা একই `SystemName` দিয়ে একাধিক প্লাগইন নিবন্ধন করতে পারি না।

- **Version**. এটি প্লাগইনটির সংস্করণ নম্বর, আপনি এটি আপনার পছন্দ মতো যেকোনো মান নির্ধারণ করতে পারেন। এই নম্বরটি প্লাগইনটির কোন সংস্করণটি বর্তমানে নপকমার্স অ্যাপ্লিকেশনে ইনস্টল করা আছে তা সনাক্ত করতে ব্যবহৃত হয়।

- **SupportedVersions**. এটি স্ট্রিং এর অ্যারে। এতে নপকমার্স এর এক বা একাধিক সংস্করণ রয়েছে যা এই প্লাগইনটি সমর্থিত বা আমরা বলতে পারি যে এই প্লাগইনটি টার্গেট। উন্নয়নের সময় নিশ্চিত করুন যে নপকমার্স এর বর্তমান সংস্করণ যেখানে আপনি এই প্লাগইনটি বিকাশ করছেন তা এই তালিকায় অন্তর্ভুক্ত করা হয়েছে, অন্যথায়, এটি প্লাগইন তালিকায় লোড হবে না।

- **Author**. এটি প্লাগইন এর স্রষ্টা সম্পর্কে তথ্য। এটি একজন ব্যক্তির নাম বা কোম্পানির নাম বা একটি দল হতে পারে যারা এই প্লাগইনটি তৈরি করেছে।

- **DisplayOrder**. প্লাগইন তালিকায় এই প্লাগইনটি যে অর্ডারে প্রদর্শিত হবে সেটি সেট করতে এটি ব্যবহার করা হয়। এর মান সংখ্যা হয়।

- **FileName**. এর নিম্নোক্ত বিন্যাস আছে **Nop.Plugin.{Group}.{Name}.dll** (এটি আপনার প্লাগইন সমাবেশ ফাইলের নাম)।

- **Description**. এতে আপনার প্লাগইন সম্পর্কে একটি সংক্ষিপ্ত বিবরণ রয়েছে যেমন এই প্লাগইনটি কী, এই প্লাগইনটি কী করে। এটি প্লাগইন নামের প্লাগইন তালিকায় দেখানো হয়েছে।
- **LimitedToStores** - স্টোর শনাক্তকারীর তালিকা যেখানে এই প্লাগইনটি পাওয়া যায়। যদি খালি থাকে, তাহলে এই প্লাগইনটি সব দোকানে পাওয়া যায়।
- **LimitedToCustomerRoles** - গ্রাহক ভূমিকা সনাক্তকারীদের তালিকা যার জন্য এই প্লাগইনটি উপলব্ধ। যদি খালি থাকে, তাহলে এই প্লাগইনটি সবার জন্য উপলব্ধ।
- **DependOnSystemNames** - প্লাগইনগুলির সিস্টেমের নামের তালিকা যা এই প্লাগইন নির্ভর করে 

> [!TIP]
> আপনি আপনার **plugin.json** ফাইলের বিষয়বস্তু সম্পাদনা করার পর আপনাকে তার 'Copy to Output Directory' সম্পত্তি মান 'Copy if newer' করতে হবে।
>! [image3](_static/plugin.json/plugin_json_0.jpg)
> এটি প্রয়োজন কারণ আমাদের এই ফাইলটি সংকলিত ডিরেক্টরিতে অনুলিপি করতে হবে যেখানে থেকে নপকমার্স এই ফাইলটি ব্যবহার করতে পারে আমাদের প্লাগইন অ্যাডমিন প্যানেলে প্লাগইন তালিকায় প্রদর্শন করতে।

## উদাহরণ

- **FixedOrByCountryStateZip** প্লাগইনটিতে নিম্নলিখিত `plugin.json` ফাইল আছে:

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

- **Google Analytics** উইজেটে নিম্নলিখিত *plugin.json* ফাইল রয়েছে:

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
