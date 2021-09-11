---
title: কিভাবে এবং কখন ITypeFinder ইন্টারফেস ব্যবহার করবেন
uid: en/developer/tutorials/type-finder
author: git.skoshelev
contributors: git.AfiaKhanom
---

# কিভাবে এবং কখন ITypeFinder ইন্টারফেস ব্যবহার করবেন

## ITypeFinder

এটি একটি খুব সহজ ইন্টারফেস। এটিতে কেবল দুটি পদ্ধতি রয়েছে (যদিও একটিতে দুটি ওভারলোড রয়েছে)। আপনি নীচের ইন্টারফেসের সংজ্ঞা দেখতে পারেন:

  ```csharp
public interface ITypeFinder
{
    /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
    IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

    /// <param name="assignTypeFrom">Assign type from</param>
    /// <param name="onlyConcreteClasses">A value indicating whether to find only concrete classes</param>
    IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

    /// <summary>
    /// Gets the assemblies related to the current implementation.
    /// </summary>
    IList<Assembly> GetAssemblies();
}
  ```

  এই ইন্টারফেস প্রয়োগকারী একটি ক্লাসের প্রধান কাজ হল নির্দিষ্ট ক্লাস বা ইন্টারফেসের প্রকারের জন্য সমাবেশে অনুসন্ধান করা। নপকমার্স এর সোর্স কোড থেকে উদাহরণ ব্যবহার করে এটি কোথায় কাজে লাগতে পারে তা আমরা দেখব। কিন্তু প্রথমে, আসুন ``GetAssemblies`` মেথডটি দেখি। এই মেথদটির কাজটি কেবল সেই সমাবেশের তালিকা ফিরিয়ে দেওয়া যেখানে অনুসন্ধান করা হয়। এই তালিকাটি ঠিক কিভাবে গঠিত হয় তা নির্ভর করে ইন্টারফেসের নির্দিষ্ট বাস্তবায়নের উপর।

## ITypeFinder এর ডিফল্ট বাস্তবায়ন

এই ইন্টারফেসের প্রধান ডিফল্ট বাস্তবায়ন হল ``WebAppTypeFinder`` ক্লাস। পরিবর্তে ``WebAppTypeFinder`` শুধুমাত্র সামান্য ``AppDomainTypeFinder`` ক্লাস প্রসারিত করে, যা মূলত অনুসন্ধানের সমস্ত কাজ করে। কিন্তু আমরা প্রধান ক্লাসটি ব্যবহার করি কারণ এটি **\Bin** ডিরেক্টরি থেকে সমস্ত অ্যাসেম্বলিগুলিতে প্রকারের অনুসন্ধানের সুযোগ প্রসারিত করে, যখন প্রধান ক্লাস শুধুমাত্র বর্তমান অ্যাপ্লিকেশন ডোমেন থেকে অ্যাসেম্বলিগুলির সাথে কাজ করে।

``FindClassesOfType`` মেথডের বাস্তবায়নের বিশদ বিবরণ না রেখে (যেহেতু তারা উভয়ই খুব সহজ ফাংশন যা কোড পাওয়া যায় [এই লিঙ্ক](https://github.com/nopSolutions/nopCommerce/blob/develop/src/Libraries/Nop.Core/Infrastructure/AppDomainTypeFinder.cs#L184)) আসুন সবচেয়ে গুরুত্বপূর্ণ বিষয়ে এগিয়ে যাই।

## তাহলে আমাদের কেন ITypeFinder ইন্টারফেস দরকার

আসলে, এই ইন্টারফেসটি নপকমার্স কিভাবে কাজ করে তার অনেক গুরুত্বপূর্ণ দিকগুলিতে ব্যবহৃত হয়:

১. মাইগ্রেশন মেকানিজম কনফিগার করার জন্য অ্যাসেম্বলি খুঁজছেন ([মাইগ্রেশন কিভাবে কাজ করে?](xref:bn/developer/tutorials/migrations))

২. সাইটের সঠিক প্রবর্তনের জন্য প্রয়োজনীয় কিছু ইন্টারফেসের ক্লাস অনুসন্ধান করা, যেমন:

  * ``IStartupTask`` - মডিউল এবং প্লাগইনগুলির প্রাথমিক সূচনা
  * ``INopStartup`` - অ্যাপ্লিকেশন প্রারম্ভে পরিষেবা এবং মিডলওয়্যার কনফিগার করা
  * ``IDependencyRegistrar`` - IoC এর জন্য ডিপেন্ডেন্সি নিবন্ধন
  * ``IOrderedMapperProfile`` - **AutoMapper** কনফিগারেশন তৈরি করুন
  * ``IEntityBuilder``, ``INameCompatibility`` - টেবিল নামকরণের পিছনে সামঞ্জস্যের জন্য ডাটাবেস এন্টিটি নির্মাতা কনফিগার করুন **Linq2Db**([নপকমার্স ডেটা অ্যাক্সেস লেয়ার](xref:bn/developer/tutorials/source-code-organization#librariesnopdata))
  * ``IRouteProvider`` - রুট নিবন্ধন করুন
  * ``IConsumer<T>`` - অভ্যন্তরীণ ইভেন্টগুলির জন্য হ্যান্ডলার নিবন্ধন করুন যেমন ডাটাবেস এন্টিটির পরিবর্তন
  * ``IExternalAuthenticationRegistrar`` - বাহ্যিক প্রমাণীকরণ মেথড নিবন্ধন এবং কনফিগার করুন

৩. রিয়েল-টাইমে উপযুক্ত শিপমেন্ট ট্র্যাকার খুঁজেন

## উপসংহার

আপনি দেখতে পাচ্ছেন, ইন্টারফেসটি ছোট কিন্তু খুব মূল্যবান। এই ধরনের নমনীয় মডুলার কাঠামো বাস্তবায়ন করা কঠিন হবে কারণ এটি ITypeFinder ইন্টারফেস পদ্ধতির মাধ্যমে বাস্তবায়িত মেথড ব্যবহার না করে নপকমার্স এ সম্পন্ন করা হয়েছে।
