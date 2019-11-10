---
title: Entity events and how they work
author: Sina
uid: developer/tutorials/entity-event
---
# Entity events and how they work
The **EntityInserted**, **EntityUpdated** and **EntityDeleted** [extension methods](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-meth) with **BaseEntity** [constraint](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters) of **IEventPublisher** interface  are responsible to broadcast the inserted,updated and deleted entity respectively.

1. The **EntityInserted** extension method is invoced to publish an inserted entity. It invocs the paramitarized constructor of the **EntityInsertedEvent** [generic class](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-classes) and expose the inserted entity by its **Entity** property. The **EntityInsertedEvent** has the same constraint like **EntityInserted**. Developer can get the new inserted entity by implement **IConsumer** interface and can do other necessary works.

2. The **EntityUpdated** extension method of **IEventPublisher** interface is also implemented as the same way like **EntityInserted**. This extension method is called when an existing entity is updated. It invoces the paramitarized constructor of **EntityUpdatedEvent** [generic class](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-classes) and expose the updated entity by its **Entity** property. **EntityUpdatedEvent** has the same constraint of **EntityUpdated**. By implementing the **IConsumer** interface developer can get the updated entity.

3. The **EntityDeleted** extension method of **IEventPublisher** interface is also implemented as the same way like **EntityInserted** and **EntityUpdated**. This extension method is called when an existing entity is deleted. It invoces the paramitarized constructor of **EntityDeletedEvent** [generic class](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-classes) and publish the deleted entity by its **Entity** property. **EntityDeletedEvent** has the same constraint of **EntityDeleted**.  By implementing the **IConsumer** interface developer can get the deleted entity.

