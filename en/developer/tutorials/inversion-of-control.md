---
title: Inversion of Control and Dependency Injection
uid: en/developer/tutorials/inversion-of-control
author: git.AndreiMaz
contributors: git.exileDev
---

# Inversion of Control and Dependency Injection

Inversion of Control and Dependency Injection are two related ways to break apart dependencies in your applications. [Inversion of Control (IoC)](https://en.wikipedia.org/wiki/Inversion_of_control) means that objects do not create other objects on which they rely to do their work. Instead, they get the objects that they need from an outside source. [Dependency Injection (DI)](http://en.wikipedia.org/wiki/Dependency_injection) means that this is done without the object intervention, usually by a framework component that passes constructor parameters and sets properties. Martin Fowler has written a great description of Dependency Injection or Inversion of Control. I'm not going to duplicate his work, and you can find his article here. nopCommerce uses [Autofac](https://autofac.org/) library as its IoC container. Once a service and an appropriate interface, which the service implements, are written you should register them in any class implementing the IDependencyRegistrar interface (Nop.Core.Infrastructure.DependencyManagement namespace). For example, all core nopCommerce services are registered in the **DependencyRegistrar** class located in the Nop.Web.Framework library.

```csharp
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
                builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            ...
        }
    }
```

You can create as many dependency registrar classes as you need. Each class implementing **IDependencyRegistrar** interface has an **Order** property. It allows you to replace existing dependencies. To override nopCommerce dependencies, set the Order property to something greater than 0. nopCommerce orders dependency classes and runs them in ascending order. The higher the number the later your objects will be registered.
