---
title: কোডিং স্ট্যান্ডার্ড
uid: bn/developer/tutorials/coding-standards
author: git.AndreiMaz
contributors: git.AfiaKhanom
---

# কোডিং স্ট্যান্ডার্ড

তিনটি সমর্থিত .NET কোডিং কনভেনশন বিভাগ রয়েছে:

## ভাষা কনভেনশন

### .NET কোড স্টাইল সেটিংস

#### "this." যোগ্যতা

এই স্টাইলের নিয়মটি ফিল্ডস, প্রপার্টিস, মেথড বা ইভেন্টে প্রয়োগ করা যেতে পারে।

- `this.`দিয়ে প্রিফেক্স হতে কোড উপাদান *not* পছন্দ করুন
- `this.`দিয়ে প্রিফেক্স করা ক্ষেত্রগুলি *not* পছন্দ করুন

  ```csharp
  //Right
  capacity = 0;
  ```

  ```csharp
  //Wrong
  this.capacity = 0;
  ```

- `this.` দিয়ে প্রিফেক্স করা প্রপার্টি *not* পছন্দ করুন

  ```csharp
  //Right
  ID = 0;
  ```

  ```csharp
  //Wrong
  this.ID = 0;
  ```

- `this.` দিয়ে প্রিফেক্স করা মেথড *not* পছন্দ করুন

  ```csharp
  //Right
  Display();
  ```

  ```csharp
  //Wrong
  this.Display();
  ```

- `this.` দিয়ে প্রিফেক্স করা ইভেন্টে *not* পছন্দ করুন

  ```csharp
  //Right
  Elapsed += Handler;
  ```

  ```csharp
  //Wrong
  this.Elapsed += Handler;
  ```

#### টাইপ রেফারেন্সের জন্য ফ্রেমওয়ার্ক টাইপ নামের পরিবর্তে ভাষার কীওয়ার্ড

এই স্টাইলের নিয়মটি স্থানীয় ভেরিয়েবল, মেথড প্যারামিটার এবং ক্লাস মেম্বারদের জন্য অথবা মেম্বার অ্যাক্সেস এক্সপ্রেশন টাইপ করার জন্য আলাদা নিয়ম হিসাবে প্রয়োগ করা যেতে পারে।

- টাইপ নামের পরিবর্তে স্থানীয় ভেরিয়েবল, মেথড প্যারামিটার এবং ক্লাস মেম্বারদের জন্য ভাষার কীওয়ার্ড পছন্দ করুন, যে ধরনের কিওয়ার্ড তাদের প্রতিনিধিত্ব করে।

  ```csharp
  //Right
  private int _member;
  ```

  ```csharp
  //Wrong
  private Int32 _member;
  ```

- মেম্বার অ্যাক্সেস এক্সপ্রেশনগুলির জন্য ভাষার কীওয়ার্ড পছন্দ করুন, টাইপ নামের পরিবর্তে, যে ধরনের কিওয়ার্ড তাদের প্রতিনিধিত্ব করার জন্য।

  ```csharp
  //Right
  var local = int.MaxValue;
  ```

  ```csharp
  //Wrong
  var local = Int32.MaxValue;
  ```

#### মডিফায়ার পছন্দ

এই বিভাগে স্টাইলের নিয়মগুলি মডিফায়ার পছন্দগুলির বিষয়ে চিন্তা করে, যার মধ্যে অ্যাক্সেসযোগ্যতা মডিফায়ার প্রয়োজন, পছন্দসই মডিফায়ার সাজানোর ক্রম নির্দিষ্ট করা এবং কেবলমাত্র পঠনযোগ্য মডিফায়ার প্রয়োজন।

- পাবলিক ইন্টারফেসের সদস্যদের বাদে অ্যাক্সেসিবিলিটি মডিফায়ার ঘোষণা করা পছন্দ করুন।

  ```csharp
  //Right
  class MyClass
  {
      private const string thisFieldIsConst = "constant";
  }
  ```

  ```csharp
  //Wrong
  class MyClass
  {
      const string thisFieldIsConst = "constant";
  }
  ```

- নির্দিষ্ট অর্ডার পছন্দ করুন:

    *`public, private, protected, internal, static, extern, new, virtual, abstract, sealed, override, readonly, unsafe, volatile, async:silent`*

  ```csharp
  //Right
  class MyClass
  {
      private static readonly int _daysInYear = 365;
  }
  ```

#### বন্ধনী পছন্দ

এই বিভাগে স্টাইলের নিয়মগুলি গাণিতিক, রিলেশনাল এবং অন্যান্য বাইনারি অপারেটরদের জন্য বন্ধনীর ব্যবহার সহ বন্ধনীর পছন্দগুলির সাথে সম্পর্কিত।

- গাণিতিক অপারেটর (*, /, %, +, -, <<, >>, &, ^, |) অগ্রাধিকার স্পষ্ট করার জন্য বন্ধনী পছন্দ করুন

  ```csharp
  //Right
  var v = a + (b * c);
  ```

  ```csharp
  //Wrong
  var v = a + b * c;
  ```

- রিলেশনাল অপারেটর (>, <, <=, >=, is, as, ==, !=) অগ্রাধিকার স্পষ্ট করার জন্য বন্ধনী পছন্দ করুন

  ```csharp
  //Right
  var v = (a < b) == (c > d);
  ```

  ```csharp
  //Wrong
  var v = a < b == c > d;
  ```

- বাইনারি অপারেটর (&&, ||, ??) অগ্রাধিকার স্পষ্ট করার জন্য বন্ধনী পছন্দ করুন

  ```csharp
  //Right
  var v = a || (b && c);
  ```

  ```csharp
  //Wrong
  var v = a || b && c;
  ```

- অপারেটরের অগ্রাধিকার সুস্পষ্ট হলে বন্ধনী না থাকা পছন্দ করুন

  ```csharp
  //Right
  var v = a.b.Length;
  ```

  ```csharp
  //Wrong
  var v = (a.b).Length;
  ```

#### এক্সপ্রেশন-স্তরের পছন্দ

এই বিভাগে স্টাইলের নিয়মগুলি এক্সপ্রেশন-স্তরের পছন্দগুলিকে উদ্বিগ্ন করে, যার মধ্যে অবজেক্ট ইনিশিয়ালাইজার, কালেকশন ইনিশিয়ালাইজার, এক্সপ্লিসিট বা অনুমিত টুপল নাম এবং অনুমিত অ্যানোনিমাস প্রকারগুলি অন্তর্ভুক্ত রয়েছে।

- সম্ভব হলে অবজেক্ট ইনিশিয়ালাইজার ব্যবহার করে শুরু করা অবজেক্টগুলিকে পছন্দ করুন

  ```csharp
  //Right
  var c = new Customer() { Age = 21 };
  ```

  ```csharp
  //Wrong
  var c = new Customer();
  c.Age = 21;
  ```

- সম্ভব হলে সংগ্রহ আরম্ভকারীদের ব্যবহার করে সংগ্রহগুলি আরম্ভ করা পছন্দ করুন

  ```csharp
  //Right
  var list = new List<int> { 1, 2, 3 };
  ```

  ```csharp
  //Wrong
  var list = new List<int>();
  list.Add(1);
  list.Add(2);
  list.Add(3);
  ```

- আইটেমএক্স বৈশিষ্ট্যে টুপল নাম পছন্দ করুন

  ```csharp
  //Right
  (string name, int age) customer = GetCustomer();
  var name = customer.name;
  ```

  ```csharp
  //Wrong
  (string name, int age) customer = GetCustomer();
  var name = customer.Item1;
  ```

- অনুমিত টুপল উপাদান নাম পছন্দ করুন

  ```csharp
  //Right
  var tuple = (age, name);
  ```

  ```csharp
  //Wrong
  var tuple = (age: age, name: name);
  ```

- এক্সপ্লিসিট অ্যানোনিমাস টাইপ সদস্য নাম পছন্দ করুন

  ```csharp
  //Right
  var anon = new { age = age, name = name };
  ```

  ```csharp
  //Wrong
  var anon = new { age, name };
  ```

- প্রাইভেট ব্যাকিং ফিল্ড সহ প্রোপার্টিগুলির চেয়ে স্বয়ংসম্পূর্ণতাকে অগ্রাধিকার দিন

  ```csharp
  //Right
  private int Age { get; }
  ```

  ```csharp
  //Wrong
  private int age;

  public int Age
  {
      get
      {
          return age;
      }
  }
  ```

- *`object.ReferenceEquals`* প্যাটার্ন-ম্যাচিং ওভার দিয়ে একটি নাল চেক ব্যবহার করতে পছন্দ করুন

  ```csharp
  //Right
  if (value is null)
      return;
  ```

  ```csharp
  //Wrong
  if (object.ReferenceEquals(value, null))
      return;
  ```

- ইফ-এলস স্টেটমেন্টের উপর একটি টার্নারি শর্তাধীন অ্যাসাইনমেন্ট পছন্দ করুন

  ```csharp
  //Right
  string s = expr ? "hello" : "world";
  ```

  ```csharp
  //Wrong
  string s;
  if (expr)
  {
      s = "hello";
  }
  else
  {
      s = "world";
  }
  ```

- ইফ-এলস স্টেটমেন্টের উপর একটি টার্নারি কন্ডিশনাল ব্যবহার করতে রিটার্ন স্টেটমেন্ট পছন্দ করুন

  ```csharp
  //Right
  return expr ? "hello" : "world";
  ```

  ```csharp
  //Wrong
  if (expr)
  {
      return "hello";
  }
  else
  {
      return "world";
  }
  ```

- যৌগিক অ্যাসাইনমেন্ট এক্সপ্রেশন পছন্দ করুন

  ```csharp
  //Right
  x += 1;
  ```

  ```csharp
  //Wrong
  x = x + 1;
  ```

#### নাল-চেকিং পছন্দ

এই বিভাগে স্টাইলের নিয়মগুলি শূন্য-চেকিং পছন্দগুলি সম্পর্কিত।

- টার্নারি অপারেটর চেকিংয়ের জন্য নাল কোয়ালসিং এক্সপ্রেশন পছন্দ করুন

  ```csharp
  //Right
  var v = x ?? y;
  ```

  ```csharp
  //Wrong
  var v = x != null ? x : y; // or
  var v = x == null ? y : x;
  ```

- সম্ভব হলে নাল-কন্ডিশনাল অপারেটর ব্যবহার করতে পছন্দ করুন

  ```csharp
  //Right
  var v = o?.ToString();
  ```

  ```csharp
  //Wrong
  var v = o == null ? null : o.ToString(); // or
  var v = o != null ? o.String() : null;
  ```

### C# কোড স্টাইল সেটিংস

#### ইমপ্লিসিট এবং এক্সপ্লিসিট টাইপ

এই বিভাগে স্টাইল নিয়ম একটি পরিবর্তনশীল ঘোষণায় একটি এক্সপ্লিসিট ধরনের বনাম ভার কীওয়ার্ড ব্যবহার সম্পর্কিত। এই নিয়মটি অন্তর্নির্মিত প্রকারগুলিতে পৃথকভাবে প্রয়োগ করা যেতে পারে, যখন প্রকারটি এক্সপ্লিসিট এবং অন্য কোথাও।

- *`var`* ব্যবহার করা পছন্দ করুন ভেরিয়েবলগুলিকে বিল্ট-ইন সিস্টেম টাইপ যেমন *`int`* ঘোষণা করার জন্য

  ```csharp
  //Right
  var x = 5;
  ```

  ```csharp
  //Wrong
  int x = 5;
  ```

- *`var`* পছন্দ করুন যখন টাইপটি ইতিমধ্যেই একটি ডিক্লারেশন এক্সপ্রেশন এর ডানদিকে উল্লেখ করা আছে

  ```csharp
  //Right
  var obj = new Customer();
  ```

  ```csharp
  //Wrong
  Customer obj = new Customer();
  ```

- অন্য কোন কোড স্টাইলের নিয়ম দ্বারা ওভাররাইড না হওয়া পর্যন্ত সকল ক্ষেত্রে এক্সপ্লিসিট টাইপের চেয়ে *`var`* পছন্দ করুন

  ```csharp
  //Right
  var f = this.Init();
  ```

  ```csharp
  //Wrong
  bool f = this.Init();
  ```

#### এক্সপ্রেশন-বডি সদস্য

এই বিভাগে স্টাইলের নিয়মগুলি ব্যবহার সম্পর্কিত [এক্সপ্রেশন-বডি সদস্য](https://docs.microsoft.com/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members) যখন যুক্তি একটি একক এক্সপ্রেশন নিয়ে গঠিত। এই নিয়ম মেথড, কনস্ট্রাক্টর, অপারেটর, প্রপার্টি, ইনডেক্সার এবং অ্যাক্সেসারের ক্ষেত্রে প্রয়োগ করা যেতে পারে।

- মেথড জন্য ব্লক বডিগুলিকে পছন্দ করুন

  ```csharp
  //Right
  public int GetAge() { return this.Age; }
  ```

  ```csharp
  //Wrong
  public int GetAge() => this.Age;
  ```

- কনস্ট্রাক্টর জন্য এক্সপ্রেশন বডিগুলিকে পছন্দ করুন

  ```csharp
  //Right
  public Customer(int age) { Age = age; }
  ```

  ```csharp
  //Wrong
  public Customer(int age) => Age = age;
  ```

- অপারেটর জন্য এক্সপ্রেশন বডিগুলিকে পছন্দ করুন

  ```csharp
  //Right
  public static ComplexNumber operator + (ComplexNumber c1, ComplexNumber c2)
  { return new ComplexNumber(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary); }
  ```

  ```csharp
  //Wrong
  public static ComplexNumber operator + (ComplexNumber c1, ComplexNumber c2)
      => new ComplexNumber(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
  ```

- প্রপার্টিগুলির জন্য এক্সপ্রেশন বডিগুলিকে পছন্দ করুন যখন তারা একটি একক লাইন হবে

  ```csharp
  //Right
  public int Age => _age;
  ```

  ```csharp
  //Wrong
  public int Age { get { return _age; }}
  ```

- ইনডেক্সার জন্য এক্সপ্রেশন বডিগুলিকে পছন্দ করুন

  ```csharp
  //Right
  public T this[int i] => _values[i];
  ```

  ```csharp
  //Wrong
  public T this[int i] { get { return _values[i]; } }
  ```

- অ্যাক্সেসারের জন্য এক্সপ্রেশন বডিগুলিকে পছন্দ করুন

  ```csharp
  //Right
  public int Age { get => _age; set => _age = value; }
  ```

  ```csharp
  //Wrong
  public int Age { get { return _age; } set { _age = value; } }
  ```

- ল্যাম্বডাসের জন্য এক্সপ্রেশন বডি পছন্দ করুন

  ```csharp
  //Right
  Func<int, int> square = x => x * x;
  ```

  ```csharp
  //Wrong
  Func<int, int> square = x => { return x * x; };
  ```

#### প্যাটার্ন ম্যাচিং

এই বিভাগে স্টাইলের নিয়মগুলি C# এ [প্যাটার্ন ম্যাচিং](https://docs.microsoft.com/dotnet/csharp/pattern-matching)।

- টাইপ কাস্টের সাথে এক্সপ্রেশন এর পরিবর্তে প্যাটার্ন ম্যাচিং পছন্দ করুন

  ```csharp
  //Right
  if (o is int i) {...}
  ```

  ```csharp
  //Wrong
  if (o is int) {var i = (int)o; ... }
  ```

- কিছু নির্দিষ্ট ধরনের কিনা তা নির্ধারণ করতে নাল চেকের সাথে *`as`* এক্সপ্রেশন এর পরিবর্তে প্যাটার্ন ম্যাচিং পছন্দ করুন

  ```csharp
  //Right
  if (o is string s) {...}
  ```

  ```csharp
  //Wrong
  var s = o as string;
  if (s != null) {...}
  ```

#### ইনলাইন ভ্যারিয়েবল ঘোষণা

এই স্টাইল নিয়মটি চিন্তা করে যে আউট ভেরিয়েবলগুলি ইনলাইন ঘোষণা করা হয়েছে কিনা। C# 7 থেকে শুরু করে, আপনি পারেন [একটি মেথড কলের আর্গুমেন্ট তালিকায় একটি আউট ভেরিয়েবল ঘোষণা করুন](https://docs.microsoft.com/dotnet/csharp/language-reference/keywords/out-parameter-modifier#calling-a-method-with-an-out-argument), একটি পৃথক পরিবর্তনশীল ঘোষণার পরিবর্তে।

- সম্ভব হলে মেথড কলের আর্গুমেন্ট তালিকায় ইনলাইন ঘোষণা করার জন্য *`out`* ভেরিয়েবল পছন্দ করুন

  ```csharp
  //Right
  if (int.TryParse(value, out int i)) {...}
  ```

  ```csharp
  //Wrong
  int i;
  if (int.TryParse(value, out i)) {...}
  ```

#### C# এক্সপ্রেশন-লেভেল পছন্দ

এই স্টাইলে নিয়ম [ডিফল্ট মান এক্সপ্রেশন জন্য ডিফল্ট লিটারেল](https://docs.microsoft.com/dotnet/csharp/programming-guide/statements-expressions-operators/default-value-expressions#default-literal-and-type-inference) ব্যবহার করে উদ্বেগ প্রকাশ করে যখন কম্পাইলার প্রকাশের ধরন অনুমান করতে পারে।

- *`default`* উপরে *`default(T)`* পছন্দ করুন

  ```csharp
  //Right
  void DoWork(CancellationToken cancellationToken = default) { ... }
  ```

  ```csharp
  //Wrong
  void DoWork(CancellationToken cancellationToken = default(CancellationToken)) {   ... }
  ```

#### C# নাল-চেকিং পছন্দ

এই স্টাইলের নিয়মগুলি নাল চেকিংয়ের চারপাশের বাক্য গঠনকে উদ্বেগজনক করে তোলে, যার মধ্যে রয়েছে থ্রো এক্সপ্রেশন বা থ্রো স্টেটমেন্ট ব্যবহার করা, এবং একটি নাল চেকিং করা বা কন্ডিশনাল কোলসিং অপারেটর ব্যবহার করা[ল্যামডা এক্সপ্রেশন](https://docs.microsoft.com/dotnet/csharp/lambda-expressions)।

- থ্রো স্টেটমেন্টের পরিবর্তে থ্রো এক্সপ্রেশন ব্যবহার করতে পছন্দ করুন

  ```csharp
  //Right
  this.s = s ?? throw new ArgumentNullException(nameof(s));
  ```

  ```csharp
  //Wrong
  if (s == null) { throw new ArgumentNullException(nameof(s)); }
  this.s = s;
  ```

- একটি নাল চেক করার পরিবর্তে একটি ল্যাম্বডা এক্সপ্রেশন চালানোর সময় কন্ডিশনাল কোলসেসিং অপারেটর (?।) ব্যবহার করতে পছন্দ করুন

  ```csharp
  //Right
  func?.Invoke(args);
  ```

  ```csharp
  //Wrong
  if (func != null) { func(args); }
  ```

#### কোড ব্লক পছন্দ

এই স্টাইলের নিয়ম কোড ব্লকগুলির চারপাশে কোঁকড়া ধনুর্বন্ধনী {} ব্যবহার সম্পর্কে উদ্বিগ্ন।

- Prefer no curly braces if allowed

  ```csharp
  //Right
  if (test) this.Display();
  ```

  ```csharp
  //Wrong
  if (test) { this.Display(); }
  ```

## ফরম্যাটিং কনভেনশন

### .NET ফরম্যাটিং সেটিংস

### ডিরেক্টিভ ব্যবহার করে সংগঠিত করা

.এই বিন্যাসের নিয়মগুলি *`using`* ডিরেক্টিভ এবং *`Imports`* বিবৃতিগুলির বাছাই এবং প্রদর্শন সম্পর্কিত।

- `System.*` *`using`* বর্ণানুক্রমিক ডিরেক্টিভ সাজান, এবং ডিরেক্টিভ ব্যবহার করে তাদের আগে রাখুন।

  ```csharp
  //Right
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Octokit;
  ```

  ```csharp
  //Wrong
  using System.Collections.Generic;
  using Octokit;
  using System.Threading.Tasks;
  ```

- নির্দেশক গোষ্ঠী ব্যবহারের মধ্যে একটি ফাঁকা লাইন রাখবেন না।

  ```csharp
  //Right
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Octokit;
  ```

  ```csharp
  //Wrong
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Octokit;
  ```

### C# বিন্যাস সেটিংস

এই বিভাগে বিন্যাসের নিয়ম শুধুমাত্র C# কোডের ক্ষেত্রে প্রযোজ্য।

#### নতুন-লাইনের বিকল্প

এই ফর্ম্যাটিং নিয়ম কোড ফর্ম্যাট করার জন্য নতুন লাইন ব্যবহার সম্পর্কিত।

- সব অভিব্যক্তি ("অলম্যান" স্টাইল) জন্য একটি নতুন লাইনে থাকতে ধনুর্বন্ধনী প্রয়োজন।

  ```csharp
  //Right
  void MyMethod()
  {
      if (...)
      {
          ...
      }
  }
  ```

  ```csharp
  //Wrong
  void MyMethod() {
      if (...) {
          ...
      }
  }
  ```

- একটি নতুন লাইনে এলস স্টেটমেন্ট রাখুন।

  ```csharp
  //Right
  if (...) 
  {
      ...
  }
  else 
  {
      ...
  }
  ```

  ```csharp
  //Wrong
  if (...) {
      ...
  } else {
      ...
  }
  ```

- একটি নতুন লাইনে ক্যাচ স্টেটমেন্ট রাখুন।

  ```csharp
  //Right
  try 
  {
      ...
  }
  catch (Exception e) 
  {
      ...
  }
  ```

  ```csharp
  //Wrong
  try {
      ...
  } catch (Exception e) {
      ...
  }
  ```

- শেষ বন্ধনীর পরে একটি নতুন লাইনে থাকা স্টেটমেন্ট প্রয়োজন।

  ```csharp
  //Right
  try 
  {
      ...
  }
  catch (Exception e) 
  {
      ...
  }
  finally 
  {
      ...
  }
  ```

  ```csharp
  //Wrong
  try {
      ...
  } catch (Exception e) {
      ...
  } finally {
      ...
  }
  ```

- বস্তুর প্রারম্ভিক সদস্যদের পৃথক লাইনে থাকতে হবে

  ```csharp
  //Right
  var z = new B()
  {
      A = 3,
      B = 4
  }
  ```

  ```csharp
  //Wrong
  var z = new B()
  {
      A = 3, B = 4
  }
  ```

- অ্যানোনিমাস ধরণের সদস্যদের আলাদা লাইনে থাকতে হবে

  ```csharp
  //Right
  var z = new
  {
      A = 3,
      B = 4
  }
  ```

  ```csharp
  //Wrong
  var z = new
  {
      A = 3, B = 4
  }
  ```

- পৃথক লাইনে থাকার জন্য ক্যোয়ারী এক্সপ্রেশন ক্লজের উপাদানগুলির প্রয়োজন

  ```csharp
  //Right
  var q = from a in e
          from b in e
          select a * b;
  ```

  ```csharp
  //Wrong
  var q = from a in e from b in e
          select a * b;
  ```

#### ইন্ডেন্টেশন বিকল্প

এই বিন্যাসের নিয়ম কোডের বিন্যাসে ইন্ডেন্টেশনের ব্যবহার সম্পর্কিত।

- ইন্ডেন্ট *`switch`* কেস বিষয়বস্তু

  ```csharp
  //Right
  switch(c) 
  {
      case Color.Red:
          Console.WriteLine("The color is red");
          break;
      case Color.Blue:
          Console.WriteLine("The color is blue");
          break;
      default:
          Console.WriteLine("The color is unknown.");
          break;
  }
  ```

  ```csharp
  //Wrong
  switch(c) {
      case Color.Red:
      Console.WriteLine("The color is red");
      break;
      case Color.Blue:
      Console.WriteLine("The color is blue");
      break;
      default:
      Console.WriteLine("The color is unknown.");
      break;
  }
  ```

- ইন্ডেন্ট *`switch`* লেবেল

  ```csharp
  //Right
  switch(c) 
  {
      case Color.Red:
          Console.WriteLine("The color is red");
          break;
      case Color.Blue:
          Console.WriteLine("The color is blue");
          break;
      default:
          Console.WriteLine("The color is unknown.");
          break;
  }
  ```

  ```csharp
  //Wrong
  switch(c) {
  case Color.Red:
      Console.WriteLine("The color is red");
      break;
  case Color.Blue:
      Console.WriteLine("The color is blue");
      break;
  default:
      Console.WriteLine("The color is unknown.");
      break;
  }
  ```

- লেবেলগুলি বর্তমান প্রেক্ষাপটের মতো একই ইন্ডেন্টে স্থাপন করা হয়েছে

  ```csharp
  //Right
  class C
  {
      private string MyMethod(...)
      {          
          if (...) 
          {
              goto error;
          }
          error:
          throw new Exception(...);
      }
  }
  ```

  ```csharp
  //Wrong
  class C
  {
      private string MyMethod(...)
      {
          if (...) {
              goto error;
          }
  error:
          throw new Exception(...);
      }
  }
  ```

  ```csharp
  //Wrong
  class C
  {
      private string MyMethod(...)
      {
          if (...) {
              goto error;
          }
      error:
          throw new Exception(...);
      }
  }
  ```

#### স্পেস বিকল্প

এই বিন্যাসের নিয়মগুলি কোড ফরম্যাট করতে স্পেস ক্যারেক্টার ব্যবহার সম্পর্কিত।

- কাস্ট এবং মান মধ্যে স্পেস ক্যারেক্টার সরান

  ```csharp
  //Right
  int y = (int)x;
  ```

  ```csharp
  //Wrong
  int y = (int) x;
  ```

- একটি কন্ট্রোল ফ্লো স্টেটমেন্টে একটি কীওয়ার্ডের পরে একটি স্পেস ক্যারেক্টার রাখুন যেমন একটি *`for`* লুপ

  ```csharp
  //Right
  for (int i;i<x;i++) { ... }
  ```

  ```csharp
  //Wrong
  for(int i;i<x;i++) { ... }
  ```

- টাইপ ডিক্লারেশনে বেস বা ইন্টারফেসের জন্য কোলনের আগে একটি স্পেস ক্যারেক্টার রাখুন

  ```csharp
  //Right
  interface I
  {

  }

  class C : I
  {

  }
  ```

  ```csharp
  //Wrong
  interface I
  {

  }

  class C: I
  {

  }
  ```

- একটি প্রকার ঘোষণায় ঘাঁটি বা ইন্টারফেসের জন্য কোলনের পরে একটি স্পেস ক্যারেক্টার রাখুন

  ```csharp
  //Right
  interface I
  {

  }

  class C : I
  {

  }
  ```

  ```csharp
  //Wrong
  interface I
  {

  }

  class C :I
  {

  }
  ```

- বাইনারি অপারেটরের আগে এবং পরে স্পেস ক্যারেক্টার ঢোকান

  ```csharp
  //Right
  return x * (x - y);
  ```

  ```csharp
  //Wrong
  return x*(x-y);
  ```

  ```csharp
  //Wrong
  return x  *  (x-y);
  ```

- খোলার বন্ধনী পরে এবং একটি পদ্ধতি ঘোষণা প্যারামিটার তালিকার বন্ধের বন্ধনী আগে স্পেস ক্যারেক্টার সরান

  ```csharp
  //Right
  void Bark(int x) { ... }
  ```

  ```csharp
  //Wrong
  void Bark( int x ) { ... }
  ```

- একটি পদ্ধতি ঘোষণার জন্য খালি প্যারামিটার তালিকা বন্ধনীগুলির মধ্যে স্পেস ক্যারেক্টার সরান

  ```csharp
  //Right
  void Goo()
  {
      Goo(1);
  }

  void Goo(int x)
  {
      Goo();
  }
  ```

  ```csharp
  //Wrong
  void Goo( )
  {
      Goo(1);
  }

  void Goo(int x)
  {
      Goo();
  }

  ```

- পদ্ধতির ঘোষণায় মেথডের নাম এবং খোলার বন্ধনীগুলির মধ্যে স্পেস ক্যারেক্টারগুলি সরান

  ```csharp
  //Right
  void M() { }
  ```

  ```csharp
  //Wrong
  void M () { }
  ```

- খোলার বন্ধনী পরে এবং একটি মেথড কল বন্ধ বন্ধনী আগে স্পেস ক্যারেক্টার সরান

  ```csharp
  //Right
  MyMethod(argument);
  ```

  ```csharp
  //Wrong
  MyMethod( argument );
  ```

- খালি যুক্তি তালিকা বন্ধনীগুলির মধ্যে স্পেস ক্যারেক্টার সরান

  ```csharp
  //Right
  void Goo()
  {
      Goo(1);
  }

  void Goo(int x)
  {
      Goo();
  }
  ```

  ```csharp
  //Wrong
  void Goo()
  {
      Goo(1);
  }

  void Goo(int x)
  {
      Goo( );
  }
  ```

- মেথড কল নাম এবং খোলার বন্ধনী মধ্যে স্পেস ক্যারেক্টার সরান

  ```csharp
  //Right
  void Goo()
  {
      Goo(1);
  }

  void Goo(int x)
  {
      Goo();
  }
  ```

  ```csharp
  //Wrong
  void Goo()
  {
      Goo(1);
  }

  void Goo(int x)
  {
      Goo ();
  }
  ```

- একটি কমা পরে স্পেস ক্যারেক্টার সরান

  ```csharp
  //Right
  int[] x = new int[] { 1, 2, 3, 4, 5 };
  ```

  ```csharp
  //Wrong
  int[] x = new int[] { 1,2,3,4,5 }
  ```

- একটি কমা আগে স্পেস ক্যারেক্টার সরান

  ```csharp
  //Right
  int[] x = new int[] { 1, 2, 3, 4, 5 };
  ```

  ```csharp
  //Wrong
  int[] x = new int[] { 1 , 2 , 3 , 4 , 5 };
  ```

- বিবৃতিতে প্রতিটি সেমিকোলনের পরে স্পেস ক্যারেক্টার সন্নিবেশ করান

  ```csharp
  //Right
  for (int i = 0; i < x.Length; i++)
  ```

  ```csharp
  //Wrong
  for (int i = 0;i < x.Length;i++)
  ```

- বিবৃতিতে প্রতিটি সেমিকোলনের আগে স্পেস ক্যারেক্টার সরান

  ```csharp
  //Right
  for (int i = 0; i < x.Length; i++)
  ```

  ```csharp
  //Wrong
  for (int i = 0 ; i < x.Length ; i++)
  ```

- ঘোষণা বিবৃতিতে অতিরিক্ত স্পেস ক্যারেক্টার সরান

  ```csharp
  //Right
  int x = 0;
  ```

  ```csharp
  //Wrong
  int    x    =    0   ;
  ```

- বর্গাকার বন্ধনী খোলার আগে স্পেস ক্যারেক্টার সরান *`[`*

  ```csharp
  //Right
  int[] numbers = new int[] { 1, 2, 3, 4, 5 };
  ```

  ```csharp
  //Wrong
  int [] numbers = new int [] { 1, 2, 3, 4, 5 };
  ```

- খালি বর্গাকার বন্ধনীগুলির মধ্যে স্পেস ক্যারেক্টার সরান *`[]`*

  ```csharp
  //Right
  int[] numbers = new int[] { 1, 2, 3, 4, 5 };
  ```

  ```csharp
  //Wrong
  int[ ] numbers = new int[ ] { 1, 2, 3, 4, 5 };
  ```

- খালি নয় বর্গাকার বন্ধনীতে স্পেস ক্যারেক্টার সরান *`[0]`*

  ```csharp
  //Right
  int index = numbers[0];
  ```

  ```csharp
  //Wrong
  int index = numbers[ 0 ];
  ```

#### মোড়ানো বিকল্প

এই বিন্যাসের নিয়মগুলি বিবৃতি এবং কোড ব্লকের জন্য পৃথক লাইন বনাম একক লাইন ব্যবহার সম্পর্কিত।

- বিবৃতি এবং সদস্য ঘোষণা বিভিন্ন লাইনে ছেড়ে দিন

  ```csharp
  //Right
  int i = 0;
  string name = "John";
  ```

  ```csharp
  //Wrong
  int i = 0; string name = "John";
  ```

- একক লাইনে কোড ব্লক ছেড়ে দিন

  ```csharp
  //Right
  public int Foo { get; set; }
  ```

  ```csharp
  //Wrong
  public int MyProperty
  {
      get; set;
  }
  ```

## নামকরণ অনুষ্ঠান

- ধ্রুবকগুলি শুধুমাত্র একটি ডিলিমিটার *`_`* দিয়ে বড় অক্ষরে লেখা হয়

  ```csharp
  //Right
  const int TEST_CONSTANT = 1;
  ```

  ```csharp
  //Wrong
  const int Test_Constant = 1;
  ```

- *`public`* অ্যাক্সেস সহ ফিল্ডগুলিকে প্যাসকেলকেস নোটেশন বলা হয়

  ```csharp
  //Right
  public int TestField;
  ```

  ```csharp
  //Wrong
  public int testField;
  ```

- ইন্টারফেসের নাম অবশ্যই প্যাসকেলকেস নোটেশনে থাকতে হবে এবং উপসর্গ *`I`* থাকতে হবে

  ```csharp
  //Right
  public interface ITestInterface;
  ```

  ```csharp
  //Wrong
  public interface testInterface;
  ```

- ক্লাস, স্ট্রাকচার, মেথড, এনামস, ইভেন্টস, প্রপার্টি, নেমস্পেস এবং ডেলিগেটের নাম প্যাসকেলকেস নোটেশনে থাকা উচিত

  ```csharp
  //Right
  public class SomeClass;
  ```

  ```csharp
  //Wrong
  public class someClass;
  ```

- একটি জেনেরিক প্রকারের প্যারামিটারে বরাদ্দ করা হয়েছে নোটেশন প্যাসক্যালকেসে এ একটি বর্ণনামূলক নাম, যতক্ষণ না একটি বর্ণ এবং বর্ণনামূলক নামের যথেষ্ট ব্যবহারিক মূল্য না থাকে

  ```csharp
  //Right
  public interface ISessionChannel<TSession> { /*...*/ }
  public delegate TOutput Converter<TInput, TOutput>(TInput from);
  public class List<T> { /*...*/ }
  ```

- প্রকারের নাম *`T`* প্যারামিটার ব্যবহার করুন যে ধরনের শুধুমাত্র একটি অক্ষর টাইপ প্যারামিটার রয়েছে

  ```csharp
  //Right
  public int IComparer<T>() { return 0; }
  public delegate bool Predicate<T>(T item);
  public struct Nullable<T> where T : struct { /*...*/ }
  ```

- টাইপ প্যারামিটারের বর্ণনামূলক নামের জন্য উপসর্গ *`T`* ব্যবহার করুন

  ```csharp
  //Right
  public interface ISessionChannel<TSession>
  {
      TSession Session { get; }
  }
  ```

 তার নামে টাইপ প্যারামিটারের সাথে যুক্ত সীমাবদ্ধতাগুলি নির্দিষ্ট করুন। উদাহরণস্বরূপ, একটি *`ISession`* সীমাবদ্ধতা প্যারামিটারকে *`TSession`* বলা যেতে পারে।

- প্রাইভেট এবং সুরক্ষিত শ্রেণীর ক্ষেত্রগুলি অবশ্যই উপসর্গ *`_`* দিয়ে শুরু করতে হবে

  ```csharp
  //Right
  private int _testField;
  protected int _testField;
  ```

  ```csharp
  //Wrong
  private int testField;
  protected int testField;
  ```

- অন্যান্য সমস্ত কোড উপাদান যেমন ভেরিয়েবল, মেথড প্যারামিটার এবং ক্লাস ফিল্ড (খোলা ছাড়া) ক্যামেলকেস নোটেশনে নামকরণ করা হয়েছে।

  ```csharp
  //Right
  var testVar = new Object();
  public void Foo(int firstParam, string secondParam)
  ```

  ```csharp
  //Wrong
  var TestVar = new Object();
  public void Foo(int FirstParam, string SecondParam)
  ```
