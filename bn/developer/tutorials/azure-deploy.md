---
title: ধাপে ধাপে জিআইটি এবং স্বয়ংক্রিয় বিল্ডের সাহায্যে অজুরে স্থাপন
uid: bn/developer/tutorials/azure-deploy
author: git.AndreiMaz
contributors: git.AfiaKhanom
---

# ধাপে ধাপে জিআইটি এবং স্বয়ংক্রিয় বিল্ডের সাহায্যে অজুরে স্থাপন

## অজুরে গিট সহ নপকমার্স এর স্বয়ংক্রিয় স্থাপনার জন্য ধাপে ধাপে নির্দেশিকা

১. **Your own git-repository** আপনার নিজের সংগ্রহস্থল প্রয়োজন, আপনি কেবল নপকমার্স তৈরি করতে পারবেন না। এটি ডিফল্ট হিসাবে ভিজ্যুয়াল স্টুডিও ২০১৯ এ "Publish" ফাংশনের সাথে ব্যবহার করার জন্য ডিজাইন করা হয়েছে। আমি নিজে বিটবকেট ব্যবহার করি এবং এটিকে সরকারী সংগ্রহস্থলের সাথে সমন্বয় করে রাখি।

২. অজুরে গিট সেটআপ করুন

- টিউটোরিয়াল: [https://azure.microsoft.com/documentation/articles/web-sites-publish-source-control/](https://azure.microsoft.com/documentation/articles/web-sites-publish-source-control/)

- এখানে একটি দুর্দান্ত ভিডিও আছে: [http://channel9.msdn.com/Shows/Azure-Friday/What-is-Kudu-Azure-Web-Sites-Deployment-with-David-Ebbo](http://channel9.msdn.com/Shows/Azure-Friday/What-is-Kudu-Azure-Web-Sites-Deployment-with-David-Ebbo)

৩. **Prepare for local deploy** যখন আপনি নিশ্চিত করেছেন যে স্বয়ংক্রিয় বিল্ড কাজ করে, আমরা আমাদের স্থাপনার স্ক্রিপ্টগুলি কাস্টমাইজ করতে প্রস্তুত। এটি প্রয়োজন কারণ ডিফল্ট স্বয়ংক্রিয় বিল্ড শুধুমাত্র `nop.web` প্রকল্প তৈরি করে। এর সাথে সমস্যা হল যে এটি অ্যাডমিন ওয়েবসাইট তৈরি করে না, এবং প্লাগইনগুলির কোনটিই তৈরি হয় না। আপনি প্লাগইনগুলিকে উল্লেখ করতে পারবেন না কারণ এটি বৃত্তাকার রেফারেন্স তৈরি করবে। সুতরাং এখন আমাদের কাস্টম বিল্ড কাজ করতে হবে, এগুলি ইনস্টল করার পদক্ষেপ (অন্যান্য জায়গাগুলিও উল্লেখ করুন)

- ইনস্টল নোড জএস: [https://nodejs.org](https://nodejs.org)

- ইনস্টল অজুর সিআলআই: [https://azure.microsoft.com/documentation/articles/xplat-cli-install/](https://azure.microsoft.com/documentation/articles/xplat-cli-install/)

৪. **Get NuGet to work at command line level.** কুড স্ক্রিপ্টের ডিফল্ট আচরণ হলো নাগেট প্যাকেজ পরীক্ষা করা।
    - `Nuget.exe` ফাইলের অ্যাক্সেস পেতে আপনি হয় এখান থেকে ডাউনলোড করতে পারেন: [https://docs.nuget.org/consume/command-line-reference](https://docs.nuget.org/consume/command-line-reference)। আপনি ভিজুয়াল স্টুডিও ২০১৯ এ "নাগেট প্যাকেজগুলির স্বয়ংক্রিয় পুনরুদ্ধার সক্ষম করুন" এবং এটি স্বয়ংক্রিয়ভাবে আপনার প্রকল্পে যুক্ত হবে।

- ফাইলটি পছন্দের স্থানে অনুলিপি করুন (আমি ব্যবহার করি `c:/programFiles/Nuget/Nuget.exe`)। এটি পথ এনভায়রনমেন্ট ভেরিয়েবলে যোগ করুন।
- নিশ্চিত করুন যে `cmd.exe` শুরু করে নাগেট আপনার পথ এ আছে এবং *nuget* লিখুন। আপনি কমান্ড বিকল্প দেখতে হবে।

৫. **Generate deployment scripts locally**

- "Microsoft Azure Command Prompt" খুলুন
- আপনার প্রকল্পের সোর্স ফোল্ডারে নেভিগেট করুন যেমন আপনি সাধারণত শেল উইন্ডোতে থাকবেন
- কুদু স্ক্রিপ্ট জেনারেটর চালান (আপনি উইকি দ্বারা খুঁজে পেতে পারেন [এই লিঙ্কে](https://github.com/projectkudu/kudu/wiki) or see [এই ভিডিওতে](https://azure.microsoft.com/resources/videos/custom-web-site-deployment-scripts-with-kudu/)).

        So you would write something like:

        `kuduscript site deploymentscript --aspNetCore Presentation\Nop.Web\Nop.Web.csproj -s NopCommerce.sln -y`
- যাচাই করুন যে এটি 2 টি ফাইল তৈরি করেছে (আপনার স্থানীয় রিপোজিটরির রুটটিতে):

        `.deployment`

        `deploy.cmd`

৬. **Run generated script**

- আপনাকে অবশ্যই .deployment রাখতে হবে এবং গিট রিপোজিটরির রুট এ deploy.cmd স্থাপন করুন
- `%DEPLOYMENT_SOURCE%` ভেরিয়েবলে গিট রিপোজিটরির মূল থাকে deploy.cmd সম্পাদনা করুন। তাই আমি `%DEPLOYMENT_SOURCE%\src\Presentation\Nop.Web\Nop.Web.csproj` এর পরিবর্তে `%DEPLOYMENT_SOURCE%\Presentation\Nop.Web\Nop.Web.csproj` যোগ করব। স্থাপনার বিভাগে সমস্ত পথ সংশোধন করা আবশ্যক।
- ডিফল্ট স্থাপনার স্ক্রিপ্ট স্থানীয়ভাবে কাজ করে কিনা তা দেখতে deploy.cmd চালান। এটি আপনার গিট রিপোজিটরির ঠিক বাইরে একটি `\artifact` ফোল্ডার তৈরি করা উচিত।

৭. **Customize the deployment script** তাই এখন আমরা চূড়ান্ত অংশে আছি। এখানেই সমস্ত কাজ বন্ধ হয়ে যায়। আমরা নিচের অংশটি পরিবর্তন করতে চাই:

    ```sh
    ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    :: Deployment
    :: ----------

    echo Handling ASP.NET Core Web Application deployment.

    :: 1. Restore nuget packages
    call :ExecuteCmd dotnet restore "%DEPLOYMENT_SOURCE%\NopCommerce.sln"
    IF !ERRORLEVEL! NEQ 0 goto error

    :: 2. Build and publish
    call :ExecuteCmd dotnet publish "%DEPLOYMENT_SOURCE%\Presentation\Nop.Web\Nop.Web.csproj" --output "%DEPLOYMENT_TEMP%" --configuration Release
    IF !ERRORLEVEL! NEQ 0 goto error

    :: 3. KuduSync
    call :ExecuteCmd "%KUDU_SYNC_CMD%" -v 50 -f "%DEPLOYMENT_TEMP%" -t "%DEPLOYMENT_TARGET%" -n "%NEXT_MANIFEST_PATH%" -p "%PREVIOUS_MANIFEST_PATH%" -i ".git;.hg;.deployment;deploy.cmd"
    IF !ERRORLEVEL! NEQ 0 goto error
    ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    ```

    So between no ::1 and ::2 that's where we are gonna place our commands for building plugins.

    প্রথম প্লাগইন এর জন্য একটি উদাহরণ হবে:

    ```sh
    :: 1.01 Build plugin customer roles to temporary path
    call :ExecuteCmd dotnet build "%DEPLOYMENT_SOURCE%\src\Plugins\Nop.Plugin.DiscountRules.CustomerRoles\Nop.Plugin.DiscountRules.CustomerRoles.csproj" -c Release
    ```

এখন আপনি যখন ডিপ্লাই স্ক্রিপ্ট চালান তখন প্লাগইন তৈরি হয়।
