---
title: সিএসএস এবং জেএস রিসোর্স ফাইলগুলিকে নপকমার্স প্লাগইন এ যোগ করা
uid: bn/developer/plugins/resource-files
author: git.AndreiMaz
contributors: git.AfiaKhanom
---

# সিএসএস এবং জেএস রিসোর্স ফাইলগুলিকে নপকমার্স প্লাগইন এ যোগ করা

রিসোর্স ফাইল সঠিকভাবে লোড করার জন্য আপনার প্লাগইন এর ভিউ ফাইলে এর রেফারেন্স যোগ করতে হবে।

আপনি `Html.AddScriptParts()` অথবা `Html.AddCssFileParts()` সাহায্যকারী পদ্ধতি ব্যবহার করতে পারেন।

- `Html.AddCssFileParts()`
- `Html.AddScriptParts()`

আপনি আপনার নপকমার্স প্রকল্পের সংজ্ঞায় গিয়ে এই পদ্ধতিগুলি সম্পর্কে আরও বিশদ জানতে পারেন।

```csharp
@{
     //Loading CSS file
     Html.AddCssFileParts(ResourceLocation.Head, "~/Plugins/{PluginName}/Content/{CSSFileName.Css}");

     //Loading js file
     //Third parameter value indicating whether to exclude this script from bundling
     Html.AddScriptParts(ResourceLocation.Footer, "~/Plugins/{PluginName}/Scripts/{JSFileName.js}", true);
}
```

আপনি যদি হেডারে রিসোর্স লিঙ্ক যোগ করতে চান তাহলে আপনি রিসোর্সলোকেশন ব্যবহার করতে পারেন। হেড এবং ফুটার জন্য আপনি ব্যবহার করতে পারেন *ResourceLocation.Footer*।
