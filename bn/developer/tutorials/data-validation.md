---
title: ডেটা যাচাইকরণ
uid: bn/developer/tutorials/data-validation
author: git.AndreiMaz
contributors: git.AfiaKhanom
---

# ডেটা যাচাইকরণ

ডেটা যাচাইকরণ হল একটি প্রোগ্রাম পরিষ্কার, সঠিক এবং দরকারী ডেটার উপর কাজ করে তা নিশ্চিত করার প্রক্রিয়া। বেশিরভাগ .NET ডেভেলপাররা 'ডেটা টীকা যাচাইকারী' ব্যবহার করে। কিন্তু নপকমার্স **`Fluent Validation`** ব্যবহার করে। এটি .NET এর জন্য একটি ছোট বৈধতা লাইব্রেরি যা আপনার ব্যবসায়িক বস্তুর জন্য বৈধতা বিধি তৈরির জন্য একটি সাবলীল ইন্টারফেস এবং ল্যাম্বদা এক্সপ্রেশন ব্যবহার করে। নপকমার্স- এ কিছু মডেলের বৈধতা যোগ করার জন্য আপনাকে দুটি ধাপ সম্পূর্ণ করতে হবে:

১. `AbstractValidator` শ্রেণী থেকে প্রাপ্ত একটি ক্লাস তৈরি করুন এবং সেখানে সমস্ত প্রয়োজনীয় যুক্তি রাখুন।
   ক্লাসের পথ হল Presentation > Nop.Web > Validators > Common > AddressValidator.cs

একটি ধারণা পেতে নীচের সোর্স কোড দেখুন:

```csharp
    public class AddressValidator : BaseNopValidator<AddressModel>
    {
        public AddressValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(localizationService.GetResource("Admin.Address.Fields.FirstName.Required"))
            .When(x => x.FirstNameEnabled && x.FirstNameRequired);
        }
    }
```

২. `ValidatorAttribute` দিয়ে আপনার মডেল ক্লাস টীকা করুন। নির্দেশনার জন্য নীচের উদাহরণ পড়ুন।

```csharp
    [Validator(typeof(AddressValidator))]
    public partial class AddressModel : BaseNopEntityModel
    {
      //...
    }
```

ASP.NET Core যথাযথ যাচাইকারীকে কার্যকর করবে যখন একটি ভিউ মডেল কন্ট্রোলারে পোস্ট করা হবে।
