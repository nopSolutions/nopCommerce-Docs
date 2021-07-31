---
title: সত্তা ইভেন্ট সিস্টেম
uid: en/developer/design/entity-events-system
author: git.nopsg
contributors: git.AfiaKhanom
---

# সত্তা ইভেন্ট সিস্টেম

## ওভারভিউ

নপকমার্স ইভেন্ট-ড্রিভেন আর্কিটেকচার প্রয়োগ করে যা ডেভেলপারদের ইভেন্ট প্রকাশক বা ইভেন্ট সোর্স দ্বারা সম্প্রচারিত ইভেন্টগুলি সাবস্ক্রাইব করতে বা গ্রহন করতে দেয় যখন কিছু অ্যাকশন/ইভেন্ট করা হয় এবং কিছু নির্দিষ্ট ইভেন্ট ট্রিগার হলে আমাদের নির্দিষ্ট ব্যবসায়িক যুক্তি সম্পাদন করতে সক্ষম করে। নপকমার্স এ, আমরা নপকমার্স সিস্টেম ইভেন্ট দ্বারা প্রকাশিত বিভিন্ন ইভেন্ট সাবস্ক্রাইব করতে বা শুনতে পারি অথবা এমনকি আমরা একটি যুক্তি লিখতে পারি যা একটি ইভেন্ট নির্গত/প্রকাশ করে যা পরে শোনা বা সাবস্ক্রাইব করা যায়। উদাহরণস্বরূপ, ধরুন আমরা গ্রাহকের বিবরণ অন্য কোনো বহিরাগত সিস্টেমে সিঙ্ক করতে চাই, তাই সেক্ষেত্রে, যখন নতুন গ্রাহক আমাদের দোকানে নিবন্ধন করেন বা গ্রাহক তাদের প্রোফাইল পরিবর্তন করেন তখন আমরা একটি ইভেন্ট চালু করতে পারি। আমরা সেই ইভেন্টটি শুনতে পারি এবং ব্যবসায়িক যুক্তি সম্পাদন করতে পারি যা তখন সেই নতুন তৈরি করা গ্রাহককে বের করতে পারে এবং সিঙ্কের জন্য বাহ্যিক পরিষেবাতে পাঠায়। এবং সবচেয়ে ভাল দিক হল যে আমরা নপকমার্স সোর্স কোড পরিবর্তন না করেই এই সব করতে পারি।

ডেভেলপাররা ইভেন্ট প্রকাশ করতে পারে অথবা ইভেন্ট গ্রহন করতে পারে:

- একটি ইভেন্ট প্রকাশ করতে একজন ডেভেলপারকে **IEventPublisher** এর একটি উদাহরণ পেতে হবে এবং উপযুক্ত ইভেন্ট ডেটা সহ **Publish** পদ্ধতিতে কল করতে হবে।

- একটি ইভেন্ট শোনার জন্য ডেভেলপার জেনেরিক **IConsumer** ইন্টারফেসের একটি নতুন বাস্তবায়ন তৈরি করতে চাইবে। একবার একটি নতুন ভোক্তা বাস্তবায়ন তৈরি হয়ে গেলে নপকমার্স ইভেন্ট পরিচালনার জন্য বাস্তবায়ন খুঁজে পেতে এবং নিবন্ধনের জন্য প্রতিফলন ব্যবহার করে।

তিনটি ইভেন্ট পাবলিশার এক্সটেনশন পদ্ধতি রয়েছে যা ডেটা পরিবর্তনের ইভেন্টের জন্য ব্যবহার করা হয় যার নাম `EntityInsteredAsync`,`EntityUpdatedAsync` এবং `EntityDeletedAsync` যার সাথে **BaseEntity** উত্তরাধিকারী **IEventPublisher** ইন্টারফেস যা ইনসার্ট, আপডেট এবং সম্প্রচারের জন্য দায়ী। যথাক্রমে সত্তা ইভেন্ট মুছে ফেলা।

## EntityInsertedAsync

এই এক্সটেনশন পদ্ধতিটি `BaseEntity` টাইপের মডেল সত্তাকে একটি প্যারামিটার হিসেবে নেয়। এই এক্সটেনশন পদ্ধতিটি BaseEntity ধরনের সত্তা সন্নিবেশিত ইভেন্ট প্রকাশ/সম্প্রচার করতে ব্যবহৃত হয় যখন একটি নতুন ডেটা iঢোকান হয়। এই এক্সটেনশন পদ্ধতিটি `EntityInsoredEvent` জেনেরিক ক্লাসের প্যারামিটারাইজড কনস্ট্রাক্টরকে আহ্বান করে এবং সন্নিবেশিত সত্তাকে তার সত্তা সম্পত্তি দ্বারা প্রকাশ করে। যা পরে ডেভেলপার কর্তৃক সাবস্ক্রাইব/হ্যান্ডেল করা যেতে পারে। এখানে `BaseEntity` যে কোন মডেল শ্রেণী হতে পারে যা `BaseEntity` শ্রেণী থেকে উত্তরাধিকার সূত্রে প্রাপ্ত হয়।

### EntityInsored ইভেন্টের জন্য প্রকাশক বাস্তবায়ন

```cs
public class MyFirstPublisherClass
{
    IEventPublisher _eventPublisher;
    public MyFirstPublisherClass(IEventPublisher eventPublisher)
    {
        this._eventPublisher = eventPublisher;
    }

    public async Task MyFirstProductInsertMethod(Product product)
    {
        //Logic to insert goes here
        await _eventPublisher.EntityInsertedAsync(product);
    }
}
```

উপরের উদাহরণে, আমরা কনস্ট্রাক্টর নির্ভরতা ইনজেকশন প্রক্রিয়া ব্যবহার করে ইভেন্টপাবলিশার ক্লাসের উদাহরণ পেতে `IEventPublisher` ইন্টারফেস ইনজেকশন করছি। এখানে `MyFirstProductInsertMethod` তে পণ্য সন্নিবেশ করার জন্য যুক্তি সম্পন্ন করার পর আমরা একটি 'প্যারামিটার' হিসাবে নতুন তৈরি পণ্য বস্তুর সাথে একটি জেনেরিক ধরনের 'পণ্য' (যা বেসএন্টিটি ক্লাস থেকে উত্তরাধিকারী হওয়া প্রয়োজন) দিয়ে `EntityInsored` পদ্ধতি চালু করছি। এখন এই এক্সটেনশন পদ্ধতিটি চালু করার পরে, এটি পণ্যের প্রকারের জন্য সত্তা সন্নিবেশিত ইভেন্টটি সম্প্রচার করবে এবং এখন যে কেউ এই ইভেন্টের জন্য সাবস্ক্রাইব/শুনবে সে ইভেন্ট প্যারামিটার হিসাবে এই পণ্য বস্তুটি পাবে। এখন দেখা যাক কিভাবে এই অনুষ্ঠানটি গ্রহন করা যায়।

### EntityInsored ইভেন্টের জন্য ভোক্তা বাস্তবায়ন

```cs
public class MyFirstConsumerClass : IConsumer<EntityInsertedEvent<Product>>
{
    public async Task HandleEventAsync(EntityInsertedEvent<Product> insertEvent)
    {
        //you can access entity using insertEvent.Entity
        var insertedEntity = insertEvent.Entity;

        //Here goes the business logic you want to perform...

    }
}
```

এখানে আমরা একটি ক্লাস তৈরি করছি যা `IConsumer <EntityInsoredEvent <Product>>` থেকে সহজাত। `IConsumer` ইন্টারফেসের একটি মাত্র পদ্ধতি আছে যা বাস্তবায়ন করা প্রয়োজন অর্থাৎ `HandleEvent` পদ্ধতি। এখন যখনই টাইপ প্রোডাক্টের EntityInsored ইভেন্টটি বহিস্কার করা হয় তখন এই `HandleEvent` পদ্ধতিটি `EntityInsteredEvent` টাইপ প্রোডাক্ট সত্তা বস্তুর সাথে চালু করা হবে। এবং এখানে এই শ্রেণীর ভিতরে, আমরা সেই ডেটার আরও প্রক্রিয়াকরণের জন্য আমাদের ব্যবসায়িক যুক্তি সম্পাদন করতে পারি।

## EntityUpdatedAsync

`IEventPublisher` ইন্টারফেসের এই এক্সটেনশন পদ্ধতিটিও `EntityInsored` এর মতোই প্রয়োগ করা হয়েছে। এই এক্সটেনশন পদ্ধতিটি আর্গুমেন্ট/প্যারামিটার হিসেবে `BaseEntity` টাইপের মডেল সত্তাকেও গ্রহণ করে। এই এক্সটেনশন পদ্ধতিটি একটি বিদ্যমান সত্তা আপডেট করা হলে `BaseEntity` টাইপের সত্তা আপডেটেড ইভেন্ট প্রকাশ/সম্প্রচার করতে ব্যবহৃত হয়। এই এক্সটেনশন পদ্ধতিটি `EntityUpdatedEvent` জেনেরিক ক্লাসের প্যারামিটারাইজড কনস্ট্রাক্টরকে আহ্বান করে এবং তার সত্তা সম্পত্তি দ্বারা আপডেট করা সত্তাকে প্রকাশ করে। যা পরে ডেভেলপার কর্তৃক সাবস্ক্রাইব/হ্যান্ডেল করা যেতে পারে আইকনসুমার ইন্টারফেসটি `EntityUpdatedEvent` টাইপের `Entity` টাইপ করে আমরা সম্প্রতি সন্নিবেশিত
কিয়েছি যেমন: `IConsumer <EntityUpdatedEvent <BaseEntity>>`।

### EntityUpdated ইভেন্টের জন্য প্রকাশক বাস্তবায়ন

```cs
public class MyFirstPublisherClass
{
    IEventPublisher _eventPublisher;
    public MyFirstPublisherClass(IEventPublisher eventPublisher)
    {
        this._eventPublisher = eventPublisher;
    }

    public async Task MyFirstProductUpdateMethod(Product product)
    {
        //Logic to insert goes here
        await _eventPublisher.EntityUpdatedAsync(product);
    }
}
```

এই শ্রেণীর বাস্তবায়ন এছাড়াও `EntityInsored` এর উদাহরণের মতো। এখানে `MyFirstProductInsertMethod` তে পণ্য আপডেট করার লজিক শেষ করার পর আমরা `EntityUpdatedAsync` পদ্ধতি চালু করছি একটি জেনেরিক ধরনের সাম্প্রতিক আপডেট পণ্য অবজেক্টের সাথে একটি প্যারামিটার হিসেবে। এখন এই এক্সটেনশন পদ্ধতিটি চালু করার পরে, এটি পণ্যের প্রকারের জন্য সত্তা আপডেটেড ইভেন্ট সম্প্রচার করবে এবং এখন যে কেউ এই ইভেন্টের জন্য সাবস্ক্রাইব/শুনছে সে এই পণ্য বস্তুটি একটি ইভেন্ট প্যারামিটার হিসাবে পাবে। এখন দেখা যাক কিভাবে এই অনুষ্ঠানটি গ্রহন করা যায়।

### EntityUpdated ইভেন্টের জন্য ভোক্তা বাস্তবায়ন

```cs
public class MyFirstConsumerClass : IConsumer<EntityUpdatedEvent<Product>>
{
    public void HandleEvent(EntityUpdatedEvent<Product> updateEvent)
    {
        //you can access entity using updateEvent.Entity
        var updatedEntity = updateEvent.Entity;

        //Here goes the business logic you want to perform...

    }
}
```

আবার এটি সত্তা সন্নিবেশিত ইভেন্টের জন্য ভোক্তা শ্রেণীর মতোই। এখানে আমরা একটি শ্রেণী তৈরি করছি যা `IConsumer <EntityUpdatedEvent <Product>>` থেকে উত্তরাধিকারসূত্রে প্রাপ্ত। এখন, যখনই `Product` টাইপের 'EntityUpdated' ইভেন্টটি বরখাস্ত করা হয় তখন এই শ্রেণীর `HandleEvent` পদ্ধতিটি `EntityUpdatedEvent` পণ্য সত্তা বস্তুর প্রকারের পরামিতি দিয়ে চালু করা হবে। এবং এখানে এই শ্রেণীর ভিতরে, আমরা সেই ডেটার আরও প্রক্রিয়াকরণের জন্য আমাদের ব্যবসায়িক যুক্তি সম্পাদন করতে পারি।

## EntityDeletedAsync

এই এক্সটেনশন পদ্ধতিটি বাস্তবায়নের যুক্তিও `IEventPublisher` এর `EntityInsoredAsync` এবং `EntityUpdatedAsync` এক্সটেনশন পদ্ধতির মতোই। এই এক্সটেনশন পদ্ধতিটি `BaseEntity` টাইপের মডেল সত্তাকেও যুক্তি হিসেবে নেয়। এই এক্সটেনশন পদ্ধতিটি `BaseEntity` এর সত্তা মুছে ফেলা ইভেন্টটি প্রকাশ/সম্প্রচার করতে ব্যবহৃত হয় যখন একটি বিদ্যমান সত্তা মুছে ফেলা হয়। এই এক্সটেনশন পদ্ধতিটি `EntityDeletedEvent` জেনেরিক ক্লাসের কনস্ট্রাক্টরকে আহ্বান জানায় এবং তার সত্তা সম্পত্তি দ্বারা মুছে ফেলা সত্তাকে প্রকাশ করে। যা তারপর `IConsumer <EntityDeletedEvent <BaseEntity>>` বাস্তবায়নের মাধ্যমে ডেভেলপার দ্বারা সাবস্ক্রাইব/পরিচালনা করা যাবে।

### EntityDleted ইভেন্টের জন্য প্রকাশক বাস্তবায়ন

```cs
public class MyFirstPublisherClass
{
    IEventPublisher _eventPublisher;
    public ExamplePublisherClass(IEventPublisher eventPublisher)
    {
        this._eventPublisher = eventPublisher;
    }

    public async Task MyFirstProductDeleteMethod(Product product)
    {
        //Logic to insert goes here
        await _eventPublisher.EntityDeletedAsync(product);
    }
}
```

এই শ্রেণীর বাস্তবায়নও উপরের উদাহরণের মতই। এখানে `MyFirstProductDeleteMethod` তে পণ্যটি মুছে ফেলার যুক্তি সম্পন্ন করার পর আমরা সামগ্রিকভাবে একটি প্যারামিটার হিসাবে সাম্প্রতিকভাবে মুছে যাওয়া পণ্য বস্তুর সাথে `EntityDeleted` পদ্ধতি প্রয়োগ করছি। এখন এই এক্সটেনশন পদ্ধতিটি চালু করার পরে, এটি পণ্যের প্রকারের জন্য সত্তা মুছে ফেলা ইভেন্টটি সম্প্রচার করবে এবং এখন যে কেউ এই ইভেন্টের জন্য সাবস্ক্রাইব করবে/শুনবে সে ইভেন্ট প্যারামিটার হিসাবে এই পণ্য বস্তুটি পাবে। এখন দেখা যাক কিভাবে এই অনুষ্ঠানটি গ্রহন করা যায়।

### EntityDleted ইভেন্টের জন্য ভোক্তা বাস্তবায়ন

```cs
public class MyFirstConsumerClass : IConsumer<EntityDeletedEvent<Product>>
{
    public void HandleEvent(EntityDeletedEvent<Product> deleteEvent)
    {
        //you can access entity using deleteEvent.Entity
        var updatedEntity = deleteEvent.Entity;

        //Here goes the business logic you want to perform...

    }
}
```

আবার এটি সত্তা সন্নিবেশিত ইভেন্ট বা সত্তা আপডেট হওয়া ইভেন্টের জন্য ভোক্তা শ্রেণীর মতোই। এখানে আমরা একটি ক্লাস তৈরি করছি যা `IConsumer <EntityDeletedEvent <Product>>` থেকে উত্তরাধিকারসূত্রে প্রাপ্ত।

এখন, যখনই `Product` টাইপের `EntityDeleted` ইভেন্টটি বরখাস্ত করা হয় তখন এই শ্রেণীর `HandleEvent` পদ্ধতিটি `EntityDeletedEvent` প্রোডাক্ট সত্তা বস্তুর প্যারামিটারের সাথে চালু করা হবে। এবং এখানে এই শ্রেণীর ভিতরে, আমরা সেই ডেটার আরও প্রক্রিয়াকরণের জন্য আমাদের ব্যবসায়িক যুক্তি সম্পাদন করতে পারি।
