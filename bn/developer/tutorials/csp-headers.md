---
title: কন্টেন্ট সিকিউরিটি পলিসি (সিএসপি) হেডার
uid: bn/developer/tutorials/csp-headers
author: git.nopsg
contributors: git.AfiaKhanom
---

# কন্টেন্ট সিকিউরিটি পলিসি (সিএসপি) হেডার

কন্টেন্ট-সিকিউরিটি-পলিসি হল একটি এইচটিটিপি  রেসপন্স হেডারের নাম যা আধুনিক ব্রাউজার ডকুমেন্টের (বা ওয়েব পেজ) নিরাপত্তা বাড়ানোর জন্য ব্যবহার করে। এইচটিটিপি কনটেন্ট সিকিউরিটি পলিসি রেসপন্স হেডার ওয়েবসাইট অ্যাডমিনদের নিয়ন্ত্রণের অনুভূতি দেয় যাতে তাদের জাভাস্ক্রিপ্ট এবং সি এর মতো সম্পদ সীমিত করার ক্ষমতা দেওয়া হয় যা ব্যবহারকারীকে সাইটের মধ্যে লোড করার অনুমতি দেওয়া হয়। অন্য কথায়, আপনি আপনার সিএসএস ইটের কন্টেন্ট সোর্সগুলিকে হোয়াইটলিস্ট করতে পারেন। যদিও এটি প্রাথমিকভাবে একটি এইচটিটিপি  প্রতিক্রিয়া শিরোনাম হিসাবে ব্যবহৃত হয়, আপনি এটি একটি মেটা ট্যাগের মাধ্যমেও প্রয়োগ করতে পারেন।

```html
<meta http-equiv="Content-Security-Policy" content="default-src 'self'; img-src 'self' https://img.nopcommerce.com; object-src 'none'; script-src 'self'; style-src 'self'; frame-ancestors 'self'; base-uri 'self'; form-action 'self';">
```

এই কাস্টম মেটা ট্যাগ যোগ করার জন্য, আপনি `www.yourStore.com/Admin/Setting/GeneralCommon` এ গিয়ে **`Custom <head> tag`**  খুঁজে পেতে পারেন এবং নিচের ছবিতে দেখানো হিসাবে এটি যোগ করতে পারেন।

![custom CSP head tag image](_static/csp-headers/custom-csp-head-tag.png)

কন্টেন্ট সিকিউরিটি পলিসি **Cross Site Scripting (XSS)** এবং অন্যান্য ধরনের আক্রমণের বিরুদ্ধে সুরক্ষা দেয় যেমন **Click Jacking**। যদিও এটি তাদের সম্ভাবনা পুরোপুরি দূর করে না, তবে এটি নিশ্চিতভাবে ক্ষয়ক্ষতি কমিয়ে আনতে পারে। সামঞ্জস্য একটি সমস্যা নয় কারণ বেশিরভাগ প্রধান ব্রাউজার সিএসপি সমর্থন করে। এটি ইন্টারনেট এক্সপ্লোরারে সমর্থিত নয়।

আপনার ব্রাউজারটি সিএসপি সমর্থন করে কি না তা পরীক্ষা করার জন্য, আপনি এটি অনুসরণ করতে পারেন [লিঙ্ক](https://content-security-policy.com/browser-test/).

## সিএসপি ডিরেক্টিভস রেফারেন্স

**Content-Security-Policy** হেডার ভ্যালু এক বা একাধিক ডিরেক্টিভস (নীচে সংজ্ঞায়িত) দিয়ে গঠিত, একাধিক ডিরেক্টিভস *semicolon (;)* দিয়ে আলাদা করা হয়

### ডিফল্ট-এসআরসি

ডিরেক্টিভ জাভাস্ক্রিপ্ট, ছবি, সিএসএস, ফন্ট, এজাক্স অনুরোধ, ফ্রেম, এইচটিএমএল ৫ মিডিয়ার মতো সম্পদ আনার জন্য ডিফল্ট নীতি নির্ধারণ করে। সমস্ত নির্দেশনা *default-src* এ পড়ে না।

```html
default-src 'self' cdn.nopcommerce.com;
```

### স্ক্রিপ্ট-এসআরসি

জাভাস্ক্রিপ্টের বৈধ উৎস সংজ্ঞায়িত করে।

```html
script-src 'self' js.nopcommerce.com;
```

### স্টাইল-এসআরসি

স্টাইলশীট বা সিএসএস এর বৈধ উৎসগুলি সংজ্ঞায়িত করে।

```html
style-src 'self' css.nopcommerce.com;
```

### আইএমজি-এসআরসি

ছবির বৈধ উৎস সংজ্ঞায়িত করে।

```html
img-src 'self' img.nopcommerce.com;
```

### কানেক্ট-এসআরসি

*XMLHttpRequest (AJAX), WebSocket or EventSource* এ প্রযোজ্য। অনুমতি না থাকলে ব্রাউজার একটি **400** এইচটিটিপি স্ট্যাটাস কোড অনুকরণ করে।

```html
connect-src 'self';
```

### ফন্ট-এসআরসি

ফন্ট রিসোর্সের বৈধ উৎস সংজ্ঞায়িত করে (*@font-face* এর মাধ্যমে লোড করা)।

```html
font-src font.nopcommerce.com;
```

### অবজেক্ট-এসআরসি

প্লাগইনগুলির বৈধ উৎসগুলি সংজ্ঞায়িত করে, যেমন `<object>`, `<embed>` অথবা `<applet>`।

```html
object-src 'self';
```

### মিডিয়া-এসআরসি

অডিও এবং ভিডিওর বৈধ উত্স সংজ্ঞায়িত করে, যেমন এইচটিএমএল ৫ `<audio>`, `<video>` উপাদান।

```html
media-src media.nopcommerce.com;
```

### ফ্রেম-এসআরসি

ফ্রেম লোড করার জন্য বৈধ উৎস নির্ধারণ করে।

```html
frame-src 'self';
```

### স্যান্ডবক্স

*iframe sandbox* বৈশিষ্ট্যের অনুরূপ অনুরোধকৃত সম্পদের জন্য একটি স্যান্ডবক্স সক্ষম করে। স্যান্ডবক্স একই মূল নীতি প্রয়োগ করে, পপআপ, প্লাগইন এবং স্ক্রিপ্ট এক্সিকিউশন রোধ করে। সব সীমাবদ্ধতা রাখার জন্য আপনি স্যান্ডবক্সের মান খালি রাখতে পারেন, অথবা মান যোগ করতে পারেন: *allow-forms allow-same-original allow-scripts allow-popups, allow-modals, allow-orientation-lock, allow-pointer-lock, allow-presentation, allow-popups-to-escape-sandbox,* এবং *allow-top-navigation*

```html
sandbox allow-forms allow-scripts;
```

### চাইল্ড-এসআরসি

ওয়েব কর্মীদের জন্য বৈধ উৎস এবং `<frame>` এবং `<iframe>` এর মতো উপাদান ব্যবহার করে লোড করা নেস্টেড ব্রাউজিং প্রসঙ্গ নির্ধারণ করে

```html
child-src 'self'
```

### ফর্ম-অ্যাকশন

একটি এইচটিএমএল `<form>` ক্রিয়া হিসাবে ব্যবহার করা যেতে পারে এমন বৈধ উৎসগুলি সংজ্ঞায়িত করে।

```html
form-action 'self';
```

### ফ্রেম-আনসেস্টরস

 `<frame> <iframe> <object> <embed> <applet>` ব্যবহার করে সম্পদ এম্বেড করার জন্য বৈধ উৎস সংজ্ঞায়িত করে। এই নির্দেশনাটি **'none'** এ সেট করা মোটামুটি **X-Frame-Options: DENY**

```html
frame-ancestors 'none';
```

### প্লাগিন-টিপস

`<object>` এবং `<embed>` এর মাধ্যমে আহ্বান করা প্লাগইনগুলির জন্য বৈধ এমআইএমই প্রকার নির্ধারণ করে। একটি `<applet>` লোড করতে আপনাকে অবশ্যই *application/x-java-applet* নির্দিষ্ট করতে হবে।

```html
plugin-types application/pdf;
```

### রিপোর্ট-টু

 *Report-To* এইচটিটিপি রেসপন্স হেডার দ্বারা সংজ্ঞায়িত একটি রিপোর্টিং গ্রুপের নাম নির্ধারণ করে। আরও তথ্যের জন্য [রিপোর্টিং এপিআই](https://w3c.github.io/reporting/) দেখুন।

```html
report-to groupName;
```

### ওয়ার্কার-এসআরসি

ইউআরএলগুলিকে সীমাবদ্ধ করে যা কর্মী, শেয়ার্ড ওয়ার্কার বা সার্ভিস ওয়ার্কার হিসাবে লোড হতে পারে।

```html
worker-src 'none';
```

### ম্যানিফেস্ট-এসআরসি

ইউআরএলগুলি সীমাবদ্ধ করে যা অ্যাপ্লিকেশনটি লোড হতে পারে।

```html
manifest-src 'none';
```

### নেভিগেট-টু

ইউআরএলগুলিকে সীমাবদ্ধ করে যে ডকুমেন্টটি যে কোনও উপায়ে নেভিগেট করতে পারে। উদাহরণস্বরূপ যখন একটি লিঙ্ক ক্লিক করা হয়, একটি ফর্ম জমা দেওয়া হয়, অথবা *window.location* আহ্বান করা হয়। যদি *form-action* উপস্থিত থাকে তবে ফর্ম জমা দেওয়ার জন্য এই নির্দেশ উপেক্ষা করা হয়। [বাস্তবায়নের অবস্থা](https://www.chromestatus.com/features/6457580339593216)

```html
navigate-to nopcommerce.com
```

## কন্টেন্ট সিকিউরিটি পলিসি উদাহরণ

### সবকিছুর অনুমতি দিন কিন্তু শুধুমাত্র একই মূল থেকে

```html
default-src 'self';
```

### শুধুমাত্র একই মূল থেকে স্ক্রিপ্ট অনুমতি দিন

```html
script-src 'self';
```

### গুগল এনালিটিক্স, গুগল এজাক্স সিডন এবং একই মূল কে অনুমতি দিন

```html
script-src 'self' www.google-analytics.com ajax.googleapis.com;
```

### স্টার্টার নীতি

```html
default-src 'none'; script-src 'self'; connect-src 'self'; img-src 'self'; style-src 'self';
```

এই নীতি একই মূল থেকে ছবি, স্ক্রিপ্ট, এজাক্স, এবং সিএসএস এর অনুমতি দেয় এবং অন্য কোন সম্পদ লোড করার অনুমতি দেয় না (যেমন অবজেক্ট, ফ্রেম, মিডিয়া ইত্যাদি)। এটি অনেক সাইটের জন্য একটি ভাল সূচনা পয়েন্ট।

## কন্টেন্ট সিকিউরিটি পলিসি ত্রুটি বার্তা

ব্রাউজারের উপর ভিত্তি করে, সিএসপি ত্রুটি বার্তাগুলি ভিন্ন হতে পারে।

ক্রোম ডেভেলপার সরঞ্জামগুলিতে, আমরা নিম্নলিখিত বার্তা দেখতে পারি।

```js
Refused to load the script 'script-uri' because it violates the following Content Security Policy directive: "your CSP directive".
```

নিম্নলিখিত হিসাবে ফায়ারফক্স ডেভেলপার সরঞ্জামগুলিতে।

```js
Content Security Policy: A violation occurred for a report-only CSP policy ("An attempt to execute inline scripts has been blocked"). The behavior was allowed, and a CSP report was sent.
```

একটি কনসোল বার্তা ছাড়াও, একটি **securitypolicyviolation** ইভেন্টটি জানালায় ফায়ার করা হয়। আরও তথ্যের জন্য, এটি অনুসরণ করুন [লিঙ্ক](https://www.w3.org/TR/CSP2/#firing-securitypolicyviolationevent-events.).
