---
title: How and when to use the ITypeFinder interface
uid: en/developer/tutorials/type-finder
author: git.skoshelev
contributors: git.mariannk, git.DmitriyKulagin
---

# How and when to use the ITypeFinder interface

## ITypeFinder

This is a very simple interface. It contains only two methods (although one has two overloads). You can see the definition of the interface below:

  ```csharp
/// <summary>
/// Classes implementing this interface provide information about types 
/// to various services in the Nop engine.
/// </summary>
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

  The main task of a class that implements this interface is to search in assemblies for the types of the specified class or interface. We'll look at where this can come in handy using the examples from the source code of nopCommerce. But first, let's take a look at the ``GetAssemblies`` method. The task of this method is simply to return a list of the assemblies in which the search is performed. How exactly this list is formed depends on the specific implementation of the interface.

## Default implementation of ITypeFinder

The main default implementation of this interface is the ``WebAppTypeFinder`` class. In turn ``WebAppTypeFinder`` only slightly extends the ``AppDomainTypeFinder`` class, which essentially does all the work of searching for types. But we use the derived class since it extends the scope of the search for types to all assemblies from the **\Bin** directory, while the main class works with assemblies from the current application domain only.

Without dwelling on the implementation details of the ``FindClassesOfType`` methods (since they both boils down to the very simple function which code is available at [this link](https://github.com/nopSolutions/nopCommerce/blob/develop/src/Libraries/Nop.Core/Infrastructure/AppDomainTypeFinder.cs#L184)) let's move on to the most important thing.

## So why do we need the ITypeFinder interface

This interface is used in some very important aspects of how nopCommerce works:

1. Searching assemblies for configuring the migration mechanism ([How do migrations work?](xref:en/developer/tutorials/migrations))
1. Searching classes of certain interfaces necessary for the correct launch of the site, such as:
    * ``IStartupTask`` - Initial initialization of the modules and plugins
    * ``INopStartup`` - Configuring services and middleware on application startup
    * ``IOrderedMapperProfile`` - Create **AutoMapper** configuration
    * ``IEntityBuilder``, ``INameCompatibility`` - Configure database entity builder for backward compatibility of table naming for **Linq2Db**([nopCommerce Data Access Layer](xref:en/developer/tutorials/source-code-organization#librariesnopdata))
    * ``IRouteProvider`` - Register routes
    * ``IConsumer<T>`` - Register handlers for internal events such as changes of database entities
    * ``IExternalAuthenticationRegistrar`` - Register and configure external authentication methods
1. Searching for a suitable shipment tracker in real-time

## Conclusion

As you can see, the interface is small but very valuable. It would be difficult to implement such a flexible modular structure as it's done in nopCommerce without using the approach implemented through the `ITypeFinder` interface methods.
