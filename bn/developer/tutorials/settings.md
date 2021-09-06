---
title: সেটিংস এপিআই
uid: bn/developer/tutorials/settings
author: git.AndreiMaz
contributors: git.AfiaKhanom
---

# সেটিংস এপিআই

অন্য কোন ওয়েবসাইট প্ল্যাটফর্মের মত নপকমার্সে "Store name" বা "One page checkout enabled" এর মতো সেটিংস আছে। নপকমার্সের সেটিংস পরিচালনা করার দুটি উপায় রয়েছে।

আপনি পৃথক সেটিংস লোড এবং সংরক্ষণের জন্য **ISettingService** বাস্তবায়নের **GetSettingByKey** এবং **SetSetting** পদ্ধতি ব্যবহার করতে পারেন। নপকমার্সের সেটিংস পরিচালনার জন্য পছন্দের পদ্ধতি হল **ISettings** ইন্টারফেসের একটি নতুন বাস্তবায়ন তৈরি করা। প্রতিটি সেটিং একটি C# সম্পত্তি দ্বারা প্রতিনিধিত্ব করা হবে এবং ডেভেলপারদের প্রয়োজন হবে যখন কনস্ট্রাক্টরের মাধ্যমে ইনজেকশনের ক্লাস সেট করার উপর নির্ভর করা উচিত। নীচে একটি উদাহরণ সেটিংস ক্লাস।

```csharp
public class MediaSettings : ISettings
{
    public int AvatarPictureSize { get; set; }
    public int ProductThumbPictureSize { get; set; }
    public int ProductDetailsPictureSize { get; set; }
    public int ProductThumbPictureSizeOnProductDetailsPage { get; set; }
    public int ProductVariantPictureSize { get; set; }
    public int CategoryThumbPictureSize { get; set; }
    public int ManufacturerThumbPictureSize { get; set; }
    public int CartThumbPictureSize { get; set; }
    public bool DefaultPictureZoomEnabled { get; set; }
    public int MaximumImageSize { get; set; }
}
```
