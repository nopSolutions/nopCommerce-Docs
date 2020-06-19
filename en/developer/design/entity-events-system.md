---
title: Entity events system
uid: en/developer/design/entity-events-system
author: git.nopsg
contributors: git.nopsg, git.DmitriyKulagin
---

# Entity events system

## OverView

nopCommerce implements event-driven architecture which allows developers to subscribe or consume events broadcast by event publishers or event source when some action/event is performed and also enables us to perform certain business logic when some specific event is triggered. In nopCommerce, we can subscribe or listen to various events published by nopCommerce system event or even we can write a logic which emits/publish an event which then can be listened or subscribed. For example, let's suppose we want to sync customer details to some other external system, so in that case, we can fire an event when new customer registers to our store or customer changes their profile. We can listen to that event and perform business logic which then can extract that newly created customer and sends to the external service for sync. And the best part is that we can do all these without changing nopCommerce source code.

Developers can either publish an event or consume an event:

- To publish an event a developer will need to obtain an instance of **IEventPublisher** and call the **Publish** method with the appropriate event data.

- To listen for an event the developer will want to create a new implementation of the generic **IConsumer** interface. Once a new consumer implementation has been created nopCommerce uses reflection to find and register the implementation for event handling.

There are three event publisher extension methods which are used for data modification events named `EntityInserted`, `EntityUpdated` and `EntityDeleted` with **BaseEntity** inherit of **IEventPublisher** interface which is responsible to broadcast the insert, update and deleting entity events respectively.

## EntityInserted

This extension method takes the model entity of type `BaseEntity` as a parameter. This extension method is used to publish/broadcast the entity inserted event of type `BaseEntity` when a new data is inserted. This extension method then invokes the parameterized constructor of the EntityInsertedEvent generic class and expose the inserted entity by its Entity property. Which then can be subscribed/handled by the developer by implementing IConsumer interface of type EntityInsertedEvent of type Entity we recently inserted eg: `IConsumer<EntyInsertedEvent<BaseEntity>>`. Here `BaseEntity` can be any model class that inherits from `BaseEntity` class.

### Publisher Implementation for EntityInserted Event

```cs
public class MyFirstPublisherClass
{
    IEventPublisher _eventPublisher;
    public MyFirstPublisherClass(IEventPublisher eventPublisher)
    {
        this._eventPublisher = eventPublisher;
    }

    public void MyFirstProductInsertMethod(Product product)
    {
        //Logic to insert goes here
        _eventPublisher.EntityInserted<Product>(product);
    }
}
```

In the above example, we are injecting `IEventPublisher` Interface to get the instance of EventPublisher class using constructor dependency injection mechanism. Here in `MyFirstProductInsertMethod` after completing the logic to insert product we are invoking `EntityInserted` method with a generic type of `Product` (which needs to inherit from BaseEntity class) with newly created product object as a parameter. Now upon invoking this extension method, it will broadcast entity inserted event for product type and now whoever is subscribing/listening for this event will receive this product object as an event parameter. Now let's see how to consume this event.

### Consumer Implementation for EntityInserted Event

```cs
public class MyFirstConsumerClass : IConsumer<EntityInsertedEvent<Product>>
{
    public void HandleEvent(EntityInsertedEvent<Product> insertEvent)
    {
        //you can access entity using insertEvent.Entity
        var insertedEntity = insertEvent.Entity;

        //Here goes the business logic you want to perform...

    }
}
```

Here we are creating a class that is inherent from `IConsumer<EntityInsertedEvent<Product>>`. `IConsumer` interface has only one method that needs to be implemented i.e. `HandleEvent` method. Now whenever the EntityInserted event of type Product is fired this `HandleEvent` method will be invoked with `EntityInsertedEvent` of type product entity object. And here inside this class, we can perform our business logic for further processing of that data.

## EntityUpdated

This extension method of `IEventPublisher` interface is also implemented in the same way as EntityInserted.  This extension method also takes the model entity of type `BaseEntity` as an argument/parameter. This extension method is used to publish/broadcast the entity updated event of type `BaseEntity` when an existing entity is updated. This extension method invokes the parameterized constructor of the `EntityUpdatedEvent` generic class and exposes the updated entity by its Entity property. Which then can be subscribed/handled by the developer by implementing IConsumer interface of type `EntityUpdatedEvent` of type `Entity` we recently inserted eg: `IConsumer<EntyUpdatedEvent<BaseEntity>>`.

### Publisher Implementation for EntityUpdated Event

```cs
public class MyFirstPublisherClass
{
    IEventPublisher _eventPublisher;
    public MyFirstPublisherClass(IEventPublisher eventPublisher)
    {
        this._eventPublisher = eventPublisher;
    }

    public void MyFirstProductUpdateMethod(Product product)
    {
        //Logic to insert goes here
        _eventPublisher.EntityUpdated<Product>(product);
    }
}
```

The implementation of this class is moreover the same as the example in the `EntityInserted`. Here in `MyFirstProductInsertMethod` after completing the logic to update product we are invoking `EntityUpdated` method with a generic type of with recently updated product object as a parameter. Now upon invoking this extension method, it will broadcast entity updated event for product type and now whoever is subscribing/listening for this event will receive this product object as an event parameter. Now let's see how to consume this event.

### Consumer Implementation for EntityUpdated Event

```cs
public class MyFirstConsumerClass : IConsumer<EntityUpdatedEvent<Product>>
{
    public void HandleEvent(EntityUpdatedEvent<Product> updateEvent)
    {
        //you can access entity using updateEvent.Entity
        var updatedEntity = updateEvent.Entity;

        //Here goes the business logic you want to perform...

    }
}
```

Again this is also same as the consumer class for entity inserted event. Here we are creating a class that inherits from `IConsumer<EntityUpdatedEvent<Product>>`. Now, whenever the `EntityUpdated` event of type `Product` is fired the `HandleEvent` method of this class will be invoked with the parameter of type `EntityUpdatedEvent` product entity object. And here inside this class, we can perform our business logic for further processing of that data.

## EntityDeleted

The logic to implement this extension method is also the same as the `EntityInserted` and `EntityUpdated` extension method of `IEventPublisher`. This extension method also takes model entity of type `BaseEntity` as an argument. This extension method is used to publish/broadcast the entity deleted event of `BaseEntity` when an existing entity is deleted. This extension method invokes the constructor of `EntityDeletedEvent` generic class and exposes the deleted entity by its Entity property. Which then can be subscribed/handled by the developer by implementing `IConsumer<EntityDeletedEvent<BaseEntity>>`.

### Publisher Implementation for EntityDeleted Event

```cs
public class MyFirstPublisherClass
{
    IEventPublisher _eventPublisher;
    public ExamplePublisherClass(IEventPublisher eventPublisher)
    {
        this._eventPublisher = eventPublisher;
    }

    public void MyFirstProductDeleteMethod(Product product)
    {
        //Logic to insert goes here
        _eventPublisher.EntityDeleted<Product>(product);
    }
}
```

The implementation of this class is also the same as in the above example. Here in `MyFirstProductDeleteMethod` after completing the logic to delete product we are invoking `EntityDeleted` method with the product object we recently deleted as of a parameter. Now upon invoking this extension method, it will broadcast entity deleted event for product type and now whoever is subscribing/listening for this event will receive this product object as an event parameter. Now let's see how to consume this event.

### Consumer Implementation for EntityDeleted Event

```cs
public class MyFirstConsumerClass : IConsumer<EntityDeletedEvent<Product>>
{
    public void HandleEvent(EntityDeletedEvent<Product> deleteEvent)
    {
        //you can access entity using deleteEvent.Entity
        var updatedEntity = deleteEvent.Entity;

        //Here goes the business logic you want to perform...

    }
}
```

Again this is also same as the consumer class for entity inserted event or entity updated event. Here we are creating a class that inherits from `IConsumer<EntityDeletedEvent<Product>>`.

Now, whenever the `EntityDeleted` event of type Product is fired the `HandleEvent` method of this class will be invoked with the parameter of type `EntityDeletedEvent` product entity object. And here inside this class, we can perform our business logic for further processing of that data.
