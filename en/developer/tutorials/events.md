---
title: Exposing and Handling Events
uid: en/developer/tutorials/events
author: git.AndreiMaz
contributors: git.exileDev, git.DmitriyKulagin
---

# Exposing and Handling Events

Events are notifications broadcast to interested parties. Events are triggered on data changes like inserts, updates, and deletes. nopCommerce allows developers to "listen" for events they might be interested in. There are two ways a developer will work with events. A developer will either want to publish events for listeners to consume, or subscribe to events other developers will have programmatically published.

1. To publish an event a developer will need to obtain an instance of **IEventPublisher** and call the **PublishAsync** method with the appropriate event data.

   ```csharp
   await _eventPublisher.PublishAsync(new OrderPlacedEvent(order));
   ```

   The event class looks like this:

   ```csharp
   public class OrderPlacedEvent
   {
       public OrderPlacedEvent(Order order)
       {
           Order = order;
       }
       public Order Order { get; }
   }
   ```

1. To listen for an event the developer will want to create a new implementation of the generic **IConsumer** interface. Once a new consumer implementation has been created nopCommerce uses reflection to find and register the implementation for event handling.

   ```csharp
   public class EventConsumer : IConsumer<OrderPlacedEvent>
   {
       public async Task HandleEventAsync(OrderPlacedEvent eventMessage)
       {
            if (eventMessage?.Order != null)
            {
                //do something
            }
       }
   }
   ```
