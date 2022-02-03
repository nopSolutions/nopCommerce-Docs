---
title: Inversion of Control and Dependency Injection
uid: en/developer/tutorials/inversion-of-control
author: git.AndreiMaz
contributors: git.exileDev, git.DmitriyKulagin
---

# Inversion of Control and Dependency Injection

Inversion of Control and Dependency Injection are two related ways to break apart dependencies in your applications. [Inversion of Control (IoC)](https://en.wikipedia.org/wiki/Inversion_of_control) means that objects do not create other objects on which they rely to do their work. Instead, they get the objects that they need from an outside source. [Dependency Injection (DI)](http://en.wikipedia.org/wiki/Dependency_injection) means that this is done without the object intervention, usually by a framework component that passes constructor parameters and sets properties. Martin Fowler has written a great description of Dependency Injection or *Inversion of Control*. I'm not going to duplicate his work, and you can find his article [here](https://martinfowler.com/articles/injection.html). nopCommerce uses ASP.NET Core's built-in DI container, which is represented by the `IServiceProvider` interface. This container is responsible for mapping dependencies to specific types and for injecting dependencies into various objects. Once a service and an appropriate interface, which the service implements, are written you should register them in any class implementing the `INopStartup` interface (`Nop.Core.Infrastructure` namespace). The `ConfigureServices` method is responsible for installing services in the application. Services are added to the project through the `IServiceCollection` parameter.

```csharp
    public class NopStartup : INopStartup
    {
        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
                services.AddScoped<IWebHelper, WebHelper>();
            ...
        }

        public void Configure(IApplicationBuilder application)
        {
        }

        public int Order => 2000;
    }
```

You can create as many dependency registrar classes as you need. Each class implementing the **INopStartup** interface has an **Order** property. It allows you to replace existing dependencies. To override nopCommerce dependencies, set the `Order` property to something greater than 0. nopCommerce orders dependency classes and runs them in ascending order. The higher the number the later your objects will be registered.

This way you will register both built-in ASP.NET Core services and your own, the same registration mechanism applies to registering services in plugins.
