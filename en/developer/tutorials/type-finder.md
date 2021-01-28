---
title: How and when to use the ITypeFinder interface?
uid: en/developer/tutorials/type-finder
author: git.AndreiMaz
contributors: git.skoshelev
---

# How and when to use the ITypeFinder interface

## ITypeFinder

This is a very simple interface it is contains only two methods (although one has two overloaded versions, but their essence is the same). You can see the definition of the interface below:

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

  The main task of a class that implements this interface is to search in assemblies for the types of the specified class or interface. We'll look at where this can come in handy using an example of a real problem within the source code of nopCommerce. But first, let's take a look at the ``GetAssemblies`` method. The task of this method is simply to return a list of those assemblies in which the search is performed (How exactly this list is formed is decided by a specific implementation of the interface)

## Default implementation of ITypeFinder

The main default implementation of this interface is the ``WebAppTypeFinder`` class. In turn, ``WebAppTypeFinder`` only slightly extends the ``AppDomainTypeFinder`` class, which essentially does all the work of searching for types. But we are not accidentally using the derived class, since it is able to extend the scope of search for types to all assemblies from the **\Bin** directory, while the main class by default only works with assemblies from the curent application domain.

Without dwelling on the implementation details of the ``FindClassesOfType`` methods, since them boils down to one very simple function, the code of which is available at [this link](https://github.com/nopSolutions/nopCommerce/blob/develop/src/Libraries/Nop.Core/Infrastructure/AppDomainTypeFinder.cs#L184), let's move on to the most important thing.

## So why do we need the ITypeFinder interface

In fact, this class is used in a number of very important aspects of how nopCommerce works:

1. Searching assemblies for configuring the migration mechanism ([How do migrations work?](xref:en/developer/tutorials/migrations/))
1. Search for classes of certain interfaces necessary for the correct launch of the site, such as:
    * ``IStartupTask`` - Initial initialization of these modules and plugins 
    * ``INopStartup`` - Configuring services and middleware on application startup
    * ``IDependencyRegistrar`` - Dependency registration for IoC
    * ``IOrderedMapperProfile`` - Create **AutoMapper** configuration
    * ``IEntityBuilder``, ``INameCompatibility`` - Configure database entity builder a backward compatibility of table naming for **Linq2Db**([nopCommerce Data Access Layer](xref:en/developer/tutorials/data-access-layer))
    * ``IRouteProvider`` - Register routes
    * ``IConsumer<T>`` - Registering handlers for internal events such as changes on database entities
    * ``IExternalAuthenticationRegistrar`` - Register and configure external authentication methods
1. Search for for suitable shipment tracker in real time

## Conclusion

As you can see, the interface is small but very valuable. It would be difficult to implement such a flexible modular structure like that of nopCommerce but not using the approach implemented through ITypeFinder interface methods
