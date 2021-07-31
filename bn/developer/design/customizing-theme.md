---
title: নপকমার্স থিমগুলি অনুকূলিতকরণ
uid: en/developer/design/customizing-theme
author: git.AndreiMaz
contributors: git.AfiaKhanom
---

# নপকমার্স থিমগুলি অনুকূলিতকরণ

## স্টোর লোগো আপলোড

কোনও নপকমার্স ওয়েবসাইটে আপনার স্টোর লোগো আপলোড করার জন্য, মূলত 2 টি পদ্ধতি রয়েছে:

### প্রথম পদ্ধতি

প্রশাসক অঞ্চল মাধ্যমে আপনার লোগো আপলোড করুন। কিভাবে এটি করতে হয় দেখুন [লোগো আপলোড করা](xref:bn/getting-started/design-your-store/uploading-your-logo) অনুচ্ছেদ.

### দ্বিতীয় পদ্ধতি

১. নপকমার্সের root ফোল্ডারে যান `/Themes/YOUR THEME/Content/images/`
    
২. Logo.png চিত্র ফাইলটি দেখুন
    
৩. আপনার স্টোর লোগোটি `logo.png` প্রতিস্থাপন করুন এবং এটির নাম `logo.png` (একই প্রস্থ: 250px এবং উচ্চতা: 50px) রাখুন

আপনি যদি লোগো সম্পর্কিত স্টাইলশিটে পরিবর্তন করতে চান তবে নীচের কোডটি আপনার `styles.css` এ সন্ধান করুন:

```css
.header-logo {
    margin: 0 0 20px;
    text-align: center;
}
.header-logo a {
    display: inline-block;
    max-width: 100%;
    line-height: 0; /*firefox line-height bug fix*/
}
.header-logo a img {
    max-width: 100%;
    opacity: 1;
}
```

> [!IMPORTANT]
> পরিবর্তনগুলি (নতুন স্টোর লোগো) দেখতে আপনাকে ব্রাউজারের ক্যাশ (বা Ctrl + F5 কী ব্যবহার করে পৃষ্ঠাটি রিফ্রেশ করতে হবে) হতে পারে।

## কিভাবে একটি বিন্যাস পরিবর্তন করতে হয়

১. আপনি যদি নিজের নপকমার্স ওয়েবসাইটের বেস লেআউটটিতে (যেমন `_Root.cshtml`) কাস্টমাইজ / পরিবর্তন করতে চান তবে আপনার `style.css` ফাইলটিতে এই সিএসএস কোডের সন্ধান করুন

    ```css
    .master-wrapper-content {
        position: relative;
        z-index: 0;
        width: 90%;
        margin: 0 auto;
    }
    .master-column-wrapper {
        position: relative;
        z-index: 0;
    }
    .master-column-wrapper:after {
        content: "";
        display: block;
        clear: both;
    }
    ```

২. আপনি যদি `_ ColumnOne.cshtml` এর বিন্যাসে কাস্টমাইজ / পরিবর্তন করতে চান। আপনার সিএসএস কোডটি আপনার `style.css` এ সন্ধান করুন

    ```css
    .center-1 {
        margin: 0 0 100px;
    }
    ```

৩. যদি আপনি `_ColumnTwo.cshtml` এর বিন্যাসে কাস্টমাইজ / পরিবর্তন করতে চান। দয়া করে আপনার `style.css` এ এই CSS কোডটি দেখুন

    ```css
    .center-2, .side-2 {
        margin: 0 0 50px;
    }
    .side-2:after {
        content: "";
        display: block;
        clear: both;
    }
    ```

## হেডার মেনুতে কীভাবে পরিবর্তন করবেন (টপ মেনু)

১. আপনি যদি নিজের নপকমার্স ওয়েবসাইটের শিরোনাম মেনুতে (টপ মেনু) কাস্টমাইজ / পরিবর্তন করতে চান তবে নীচের অবস্থানে যান:
নপকমার্স root ফোল্ডারে যান `/Views/Shared/Components/TopMenu/Default.cshtml`
    
২. ফাইলটি খুলুন `Default.cshtml` - আপনি আপনার প্রয়োজনীয়তা অনুসারে` <li> এর মধ্যে মেনু আইটেমগুলি যুক্ত বা সরাতে পারেন।

## পাদলেখ (বা পাদলেখ লিঙ্ক) এ কীভাবে পরিবর্তন করবেন

১. আপনি যদি আপনার নপকমার্স ওয়েবসাইটের পাদচরণ (বা পাদলেখ লিঙ্কগুলি) কাস্টমাইজ / পরিবর্তন করতে চান তবে নীচের অবস্থানে যান:
নপকমার্স root ফোল্ডারে যান `/Views/Shared/Components/Footer/Default.cshtml`
    
২. ফাইলটি খুলুন `Default.cshtml` - আপনি আপনার প্রয়োজনীয়তা অনুসারে` <li> এর মধ্যে মেনু আইটেমগুলি যুক্ত বা সরাতে পারেন।
