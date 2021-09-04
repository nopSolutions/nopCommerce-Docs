---
title: এন্টিটি ইভেন্ট এবং তারা কিভাবে কাজ করে
uid: bn/developer/tutorials/entity-event
author: git.sinaislam
contributors: git.DmitriyKulagin
---

# এন্টিটি ইভেন্ট এবং তারা কিভাবে কাজ করে

**EntityInserted**, **EntityUpdated** এবং **EntityDeleted** [সম্প্রসারণ পদ্ধতি](https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) **IEventPublisher** ইন্টারফেস সম্প্রচারের জন্য দায়ী **BaseEntity** [কন্সট্রেট](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters) যথাক্রমে সন্নিবেশিত, আপডেট এবং মুছে ফেলা এন্টিটি।

১. সন্নিবেশিত এন্টিটি প্রকাশের জন্য **EntityInserted** এক্সটেনশন পদ্ধতি চালু করা হয়েছে। এটি **EntityInsoredEvent** [জেনেরিক ক্লাস](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/generic-classes) এর প্যারামিটারাইজড কনস্ট্রাক্টরকে আহ্বান জানায় এবং সন্নিবেশিত এন্টিটিকে তার দ্বারা প্রকাশ করে **Entity** প্রপার্টি। **EntityInsoredEvent** এর একই সীমাবদ্ধতা আছে যেমন **EntityInsored**। ডেভেলপার **IConsumer** ইন্টারফেস প্রয়োগ করে নতুন সন্নিবেশিত এন্টিটি পেতে পারে এবং অন্যান্য প্রয়োজনীয় কাজ করতে পারে।

২. **IEventPublisher** ইন্টারফেসের **EntityUpdated** এক্সটেনশন মেথড **EntityInsored** এর মতো একইভাবে প্রয়োগ করা হয়েছে। একটি বিদ্যমান সত্তা আপডেট করা হলে এই এক্সটেনশন পদ্ধতি বলা হয়। এটি **EntityUpdatedEvent** [জেনেরিক ক্লাস](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/generic-classes) এর প্যারামিটারাইজড কনস্ট্রাক্টরকে আহ্বান করে এবং তার **EntityUpdatedEvent** প্রপার্টি। **EntityUpdatedEvent** এর **EntityUpdated** এর একই সীমাবদ্ধতা রয়েছে। **IConsumer** ইন্টারফেস ডেভেলপার বাস্তবায়নের মাধ্যমে আপডেট হওয়া এন্টিটি পেতে পারেন।

৩. **IEventPublisher** ইন্টারফেসের **EntityDeletted** এক্সটেনশন পদ্ধতি একইভাবে প্রয়োগ করা হয় যেমন **EntityInsored** এবং **EntityUpdated**। একটি বিদ্যমান এন্টিটি মুছে ফেলা হলে এই এক্সটেনশন পদ্ধতি বলা হয়। এটি **EntityDeletedEvent** [জেনেরিক ক্লাস](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/generic-classes) এর প্যারামিটারাইজড কনস্ট্রাক্টরকে আহ্বান করে এবং মুছে ফেলা এন্টিটিকে তার **Entity** প্রপার্টি দ্বারা প্রকাশ করে। **EntityDeletedEvent** এর **EntityDeleted** এর একই সীমাবদ্ধতা রয়েছে। **IConsumer** ইন্টারফেস ডেভেলপার প্রয়োগ করে মুছে ফেলা এন্টিটি পেতে পারেন।
