---
title: Entity events and how they work
uid: en/developer/tutorials/entity-event
author: git.sinaislam
contributors: git.DmitriyKulagin
---

# Entity events and how they work

The **EntityInserted**, **EntityUpdated** and **EntityDeleted** [extension methods](https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) with **BaseEntity** [constraint](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters) of **IEventPublisher** interface  are responsible to broadcast the inserted,updated and deleted entity respectively.

1. The **EntityInserted** extension method is invoked to publish an inserted entity. It invokes the parameterized constructor of the **EntityInsertedEvent** [generic class](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/generic-classes) and expose the inserted entity by its **Entity** property. The **EntityInsertedEvent** has the same constraint like **EntityInserted**. Developer can get the new inserted entity by implement **IConsumer** interface and can do other necessary works.

1. The **EntityUpdated** extension method of **IEventPublisher** interface is implemented as the same way like **EntityInserted**. This extension method is called when an existing entity is updated. It invokes the parameterized constructor of **EntityUpdatedEvent** [generic class](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/generic-classes) and expose the updated entity by its **Entity** property. **EntityUpdatedEvent** has the same constraint of **EntityUpdated**. By implementing the **IConsumer** interface developer can get the updated entity.

1. The **EntityDeleted** extension method of **IEventPublisher** interface is implemented as the same way like **EntityInserted** and **EntityUpdated**. This extension method is called when an existing entity is deleted. It invokes the parameterized constructor of **EntityDeletedEvent** [generic class](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/generic-classes) and publish the deleted entity by its **Entity** property. **EntityDeletedEvent** has the same constraint of **EntityDeleted**.  By implementing the **IConsumer** interface developer can get the deleted entity.
