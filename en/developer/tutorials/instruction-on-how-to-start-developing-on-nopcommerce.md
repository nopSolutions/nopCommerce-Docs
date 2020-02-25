---
title: Instruction on how to start developing on nopCommerce
uid: en/developer/tutorials/instruction-on-how-to-start-developing-on-nopcommerce
---

# Instruction on how to start developing on nopCommerce 4.2
## Things covered in this tutorial.
1. Tools Required for Development.
2. Stack of technologies used in nopCommerce.
3. Instructions on how to download the project and run it on the local machine.
4. How to configure nopCommerce to run on HTTPS.

## Summery
nopCommerce is open-source Microsoft Asp.Net based e-commerce solution. This is a basic guide for developers on how to start developing on nopCommerce.

## 1. Tools Required for Development.
Since it is based on Microsoft’s Asp.Net framework we need to install a few tools before starting developing on top of nopCommerce.
### 1.1 \.Net Core 2.2 runtime & .Net Core SDK
Since nopCommerce 4.2 is based on .Net Core 2.2 framework. We need to install .Net Core 2.2 runtime and .Net Core SDK before we start development on nopCommerce.

### 1.2 Visual Studio 2017 or Above / Visual Studio Code
As we know nopCommerce is based on ‘Microsoft’s Asp.Net framework’ and Visual Studio IDE is best for developing Dot Net based Applications. Since .Net Core is platform independent so we can develop and deploy .Net based application on any platform but visual studio is not available in other platforms than window. So we can use Visual Studio Code as the alternative of Visual Studio for developing on Windows as well as in other platform.

### 1.3 Microsoft SQL Server 2012 or Above
nopCommerce uses Entity Framework as a ORM Framework. Entity Framework is an object-relational mapper (O/RM) that enables .NET developers to work with a database using .NET objects. It can map .Net objects to various numbers of Database providers. But unfortunately out of the box nopCommerce doesn’t support other database than MSSQL.

### 1.4 Internet Information Service (IIS) 7.0 or above
For hosting nopCommerce app/project we can use IIS. Which is Microsoft technology used to host Microsoft web based applications on windows platform. But since nopCommerce 4.2 is built on top of .Net core technology. Which enables .Net developers to create platform in depend applications. So due to which we can deploy nopCommerce on multi platform like: Windows, Linux, MacOs (To learn more about Support operating system please visit [this page](https://docs.microsoft.com/en-gb/dotnet/core/install/dependencies)) And can host on various hosting tools like: IIS, Apache, NGINX etc.

> [!NOTE]
> 
> You can learn more about technology and system requirement from [here](https://docs.nopcommerce.com/user-guide/installing/technology-system-requirements.html)

## 2. Stack of technologies used in nopCommerce.
The best part of nopCommerce is that its source code is fully customizable and its pluggable architecture makes it easy to develop custom functionality and follow any business requirements by using plugin system. It follows well-known software architectures, patterns and the best security practices. And above all of that it runs on latest technologies to offer the best experience possible to end-users. So, in order to achieve all of this nopCommerce uses a stack of technologies in its architecture.
* Application Layer
    * Razor View Engine

        It is to render html page on client side.         Razor View engine is a markup syntax which helps us to write HTML and server-side code in web pages using C# or VB.NET.
    * Jquery

        It is a javascript library used to extend the UI & UX functionality of html pages.
    * Kendo UI

        Kendo UI is a comprehensive HTML5 user interface framework for building interactive and high-performance websites and applications

* Business Layer
    *  Fluent Validation

        It is a validation library for .NET that uses a fluent interface and lambda expressions for building validation rules.
    * AutoMapper

        AutoMapper is a simple library that helps us to transform one object type to another. It is a convention-based object-to-object mapper that requires very little configuration.
    * AutoFac

        Autofac is an addictive IoC container for .NET. It manages the dependencies between classes so that applications stay easy to change as they grow in size and complexity.
    * Entity Framework

        Entity Framework is an open-source ORM framework for .NET applications supported by Microsoft. It enables developers to work with data using objects of domain specific classes without focusing on the underlying database tables and columns where this data is stored. So, it is the bridge between Business Layer and Data Layer.
* Data Layer
    * Microsoft SQL Server

        SQL Server is Microsoft's full-featured relational database management system (RDBMS). 
    * Redis (cache)

        Redis is an open source (BSD licensed), in-memory data structure store, used as a database, cache and message broker. So, in nopCommerce Redis is used to store old data as in-memory cash dataset. Which boosts the speed and performance of application.
    * Microsoft Azure(Optonal)

        Azure is a public cloud computing platform with solutions including Infrastructure as a Service (IaaS), Platform as a Service (PaaS), and Software as a Service (SaaS) that can be used for services such as analytics, virtual computing, storage, networking, and much more. 

## 3. How to download the project and run it on the local machine.
Before we begin to work with nopCommerce we need to ensure that our local machine is configured and need to ensure that all of our tools required to develop in nopCommerce are installed properly and working correctly. Now, let us go to step by step instructions on how to download and run nopCommerce on our local machine.
### Step 1: Download nopCommerce source code.
To download please visit [this site](https://www.nopcommerce.com/download-nopcommerce). There you can see two download buttons one with a source code and one without source code like shown in the picture below.

![image1](_static/instruction-on-how-to-start-developing-on-nopcommerce/image1.png)

Since we are downloading nopCommerce for development purpose so we need to download the one that says “Package with source code” which contains all source code of nopCommerce. In order to download nopCommerce you need to be logged in or register a new account. Now you can download nopCommerce as a RAR file, and extract it to your desired folder location.

### Step 2: Open nopCommerce solution in Visual Studio.
Open the folder, Inside that folder you will see a bunch of files and folders which contains all of the sources code for nopCommerce.

![image2](_static/instruction-on-how-to-start-developing-on-nopcommerce/image2.png)

In there you will also see a solution file with extension of `.sln`, please double click that solution file to open nopCommerce project in you visual studio.
### Step 3: Running nopCommerce project using visual studio.
nopCommerce does not require you to have any further configuration just to run the project. nopCommerce is ready to run out of the box. So, now you can run project using visual studio by hitting ctrl+f5 or just f5 to run project in debugging mode, or you can run using physical button with play icon in visual studio. After you run the project for the first time you will see a installation page like below:

![image3](_static/instruction-on-how-to-start-developing-on-nopcommerce/image3.png)

Here you need to fill all required information to complete your installation.

**Store information**

Here you are required to provide an email address and password which then be used as your administrator account in your nopCommerce shop.

**Database information**

Here you need to provide your information you want to use for this project.

Here you have two options for your database storage.

**Use built-in data storage (SQL Server Compact)**

You may choose this option to create a database on your file system which will not go to create an actual database in your database server.

**Use SQL Server (or SQL Express) database** [Recommended]

You may choose this option to create database in your SQL server installed in your system and get access to it from SSMS (SQL Management Studio).

It is your decision which one you want to use but for the sake of this tutorial we will be using the second option because it is the recommended option by nopCommerce. After choosing a second option you will see the checkbox asking if you want to create a database if not exist, please check the checkbox. 

Moving further you need to set up your connection string. For that, you have two options. One is “Enter SQL Connection Value”. If you choose this option then a form with “SQL Server name” and “Database name” will appear. In SQL Server name you need to provide your SQL server’s name and in Database name you need to provide a database name you want to create or if you already have one then it will not create but use the existing one. However, you also can choose the option “Enter raw connection string” then you need to write whole connection string by yourself. After that you need to provide your SQL server’s credentials for authentication.

After you fill all of this information you need to press “install” button, it will take about 1 minute to complete the installation, then you will be redirected to online shop homepage.

### 4. How to configure nopCommerce to run on HTTPS.
In order to set SSL/HTTPS for your nopCommerce you need to go to the property window of Nop.Web project under Presentation folder since it is the startup project for nopCommerce. To open property window right click on Nop.Web project and at the bottom of the context menu you will see a menu named “Properties”, just click on that menu then a property window will appear. In property window navigate to “Debug” tab.

![image4](_static/instruction-on-how-to-start-developing-on-nopcommerce/image4.png)

Check the "Enable SSL", and enter the HTTPS URL besides it. Then save this project.

Now run your project again and navigate to the given URL and you can see that now it is running on SSL/HTTPS. So this is the one way for configuring HTTPS in you WebProject but there is also another way to configure SSL. For that go to your Nop.Web project and expand the project inside there you will see a virtual file named “Properties” in your project structure just below “Dependencies”. Inside Properties there you will find a JSON file called launchSetting.json. Open that file and you will see bunch of configuration setting already written in that file.

Inside that file you may have a section as shown in the figure above. So to enable SSL you just need to replace 0 under “sslPort” property to the port you want to run for SSL connection, make sure the port is available. To test, run your project and navigate to https://localhost:yourport. 