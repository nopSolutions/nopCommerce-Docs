---
title: Exposing and Handling Events
uid: en/developer/tutorials/events
author: git.AndreiMaz
contributors: git.exileDev
---

# Exposing and Handling Events

Events are notifications broadcasted to interested parties. Events are triggered on data changes like inserts, updates and deletes. nopCommerce allows developers to "listen" for events they might be interested in. There are two ways a developer will work with events. A developer will either want to publish events for listeners to consume, or subscribe to events other developers will have programmatically published.

1. To publish an event a developer will need to obtain an instance of **IEventPublisher** and call the **Publish** method with the appropriate event data.
1. To listen for an event the developer will want to create a new implementation of the generic **IConsumer** interface. Once a new consumer implementation has been created nopCommerce uses reflection to find and register the implementation for event handling.
