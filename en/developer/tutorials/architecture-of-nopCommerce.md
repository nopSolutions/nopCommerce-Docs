---
title: Architecture of nopCommerce
uid: en/developer/tutorials/architecture-of-nopCommerce
author: git.nopsg
contributors: git.nopsg, git.DmitriyKulagin
---

# Architecture of nopCommerce

## Introduction

nopCommerce is a highly customizable and flexible, multi-store, multi-vendor, SEO friendly, full-featured open-source E-Commerce solution. Which is built on top of Microsoft `ASP.NET Core` framework. nopCommerce is always up to date with the latest technology and follows best practices.

## Overview

This document provides a comprehensive architectural overview of the nopCommerce system. This documentation is targeted at new nopCommerce developers. In this documentation, we will be exploring each project in nopCommerce solution, dependencies between those projects, etc.

## Overview of nopCommerce Architecture

nopCommerce is one of the most popular and successful `Dot NET based` open-source `E-Commerce` solutions. The success of nopCommerce is not only because it contains most of the features required by a modern E-Commerce solution, out of the box and its UI is highly customizable and User friendly, but also because the nopCommerce solution is equally organized and developer-friendly. The main strength of nopCommerce is its flexible, extendable architecture and well-organized source code. The nopCommerce architecture is very close to the onion architecture. Which is mainly focused on controlling the code coupling. According to this architecture, all code can depend on layers more central, but code cannot depend on layers further out from the core. In other words, all coupling is toward the center.

![nopCommerceArchitecturalDiagram](_static/architecture-of-nopCommerce/nopCommerceArchitecture.png)

This means projects can only have a dependency on the other projects that reside inward from the current project. For example, if you see the above diagram `Nop.Data` project can depend on the `Nop.Core` project and can have `Nop.Core` as a dependency, but `Nop.Core` cannot depend on `Nop.Data` similarly `Nop.Services` can have `Nop.Data` and `Nop.Core` as it's dependencies but neither `Nop.Core` nor `Nop.Data` can have `Nop.Service` as their dependency. This means projects can only have another project as a dependency only if it resides inward or more center from the layer the current project resides. Which is the key to code decoupling. The main advantage of this approach/architecture is that, now if we want to test the Application Core then we can do so even if we do not have any `UI` for our application. Because the Application core does not depend on `UI layer`. Or we can change our `UI framework` from `Razor` view engine and `JQuery` to `Angular` or `React` or `Vue` without affecting our Application Core and can use the same core to build Mobile Applications or Desktop Applications. All of this without changing a bit of code in our Application Core.

## Projects in nopCommerce Solution

### Application Core

This is the innermost layer in the nopCommerce architecture. It is the core of the application. All the data access logic and business logic classes reside inside this layer. In the nopCommerce solution, we can find all of the projects for this layer inside the "Libraries" directory. This layer contains three projects.

#### Nop.Core

This project contains a set of core classes for nopCommerce. This project is at the center of the architecture and does not have any dependencies on other projects in the solution. This project contains core classes that are shared with the entire solution like Domain Entities, Caching, Events, and other Helper classes.

#### Nop.Data

The `Nop.Data` project depends on the `Nop.Core` project, which is in the middle of the architecture. It does not depend on any other project in the solution. The `Nop.Data` project has classes and functions for reading and writing to a database or other data store. It separates data-access logic from business objects.

#### Nop.Services

The `Nop.Services` project is the outermost layer of the Application Core in nopCommerce architecture. It depends on the other two projects in the Application Core. It has core services, business logic, validations, and calculations for the data. Some people call it the Business Access Layer (BAL) or Business Logic Layer (BLL). It serves as the facade for all the other layers. It has service classes and uses a repository pattern to expose its features and functionalities. This approach decouples the core from the other layer outside the core. It also prevents or reduces code changes in the other layers if the logic for the Application Core changes. This approach is ideal for dependency injection.

### UI Layer

This layer resides outside the "Application Core". In the nopCommerce solution, we can find all of the projects for this layer inside the "Presentation" directory. All the presentation logic and UI resides in this layer. This is the layer where the UI which users can interact with, presents. In nopCommerce, this layer breaks into two other layers.

#### Nop.Web.Framework

The `Nop.Web.Framework` project is the inner layer of the Presentation layer and depends on the Application Core layer. It is a class library project that serves as a framework for the Presentation layer. It has shared logic for both the nopCommerce public website and the admin panel.

#### Nop.Web

The `Nop.Web` project is the outermost layer of the Presentation layer in nopCommerce architecture. It has the E-Commerce front-end website user interface that users can interact with. It is an ASP.NET Core application that depends on `Nop.Web.Framework` and the Application Core. It uses `Nop.Web.Framework` for the shared logic between this and the Admin panel. It uses the Application Core for data access and manipulation.

#### Admin

In nopCommerce, it belongs in the same layer as the `Nop.Web` project. This lives inside the `Nop.Web` project as an area. It is also a UI(User Interface), but this part of the Presentation contains UI for the Admin panel. Admin panel is where all the contents for a public website are maintained and from where we can monitor the activities of our public website. A public website can be accessed without any restriction but the "Admin panel" requires some Authentication and Authorization to access it since it contains information that only the site admin has the right to access.

### Test Layer

This layer resides in the same layer as the "Presentation Layer", right outside the "Application Core" This layer is all about testing the different parts of the application. Testing in nopCommerce is easy and more reliable due to the architecture it follows for its system design. In the nopCommerce solution, we can find all of the projects for this layer inside the "Tests" directory. nopCommerce uses **NUnit** testing framework for *Unit Testing*.

#### Nop.Tests

This layer is the inner layer of the "Test layer" and has a dependency on the "Application Core" layer. This project contains the core logic for testing.

#### Nop.Core.Tests

These tests are built for *Unit Testing* the `Nop.Core` project, it tests for cashing, domain entities, and so on.

#### Nop.Data.Tests

These tests are created for *Unit Testing* the `Nop.Data` project, they test the work of various data providers with operations on an entity, such as insert delete, etc.

#### Nop.Services.Tests

These tests are built for *Unit Testing* the `Nop.Service` project. It contains logic to test every service class for every operation.

#### Nop.Web.Tests

These tests can be used to test the Presentation layer of the nopCommerce, which contains "Public website" and "Admin Panel".
