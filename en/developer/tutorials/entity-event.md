---
title: Entity events and how they work
uid: en/developer/tutorials/entity-event
author: git.sinaislam
contributors: git.DmitriyKulagin
---

# Entity events and how they work

The `EntityInserted`, `EntityUpdated` and `EntityDeleted` [extension methods](https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) with `BaseEntity` [constraint](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters) of `IEventPublisher` interface  are responsible to broadcast the inserted,updated and deleted entity respectively.

## EntityInserted

The `EntityInserted` extension method is invoked to publish an inserted entity. It invokes the parameterized constructor of the `EntityInsertedEvent` [generic class](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/generic-classes) and exposes the inserted entity by its `Entity` property. The `EntityInsertedEvent` has the same constraint as `EntityInserted`. The developer can get the newly inserted entity by implementing the `IConsumer` interface and can do other necessary works.

## EntityUpdated

The `EntityUpdated` extension method of the `IEventPublisher` interface is implemented in the same way as `EntityInserted`. This extension method is called when an existing entity is updated. It invokes the parameterized constructor of `EntityUpdatedEvent` [generic class](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/generic-classes) and expose the updated entity by its `Entity` property. `EntityUpdatedEvent` has the same constraint of `EntityUpdated`. By implementing the `IConsumer` interface developer can get the updated entity.

## EntityDeleted

The `EntityDeleted` extension method of the `IEventPublisher` interface is implemented in the same way as `EntityInserted` and `EntityUpdated`. This extension method is called when an existing entity is deleted. It invokes the parameterized constructor of `EntityDeletedEvent` [generic class](https://docs.microsoft.com/dotnet/csharp/programming-guide/generics/generic-classes) and publishes the deleted entity by its `Entity` property. `EntityDeletedEvent` has the same constraint as `EntityDeleted`.  By implementing the `IConsumer` interface developer can get the deleted entity.
