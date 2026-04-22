---
標題: 實體事件系統
uid: zh-Hant/developer/design/entity-events-system
作者: git.nopsg
貢獻者: git.nopsg, git.DmitriyKulagin
---

# 實體事件系統

## 概覽

nopCommerce 實作了事件驅動架構，允許開發者訂閱或取用由事件發布者（Event Publisher）或事件來源在執行某些動作/事件時所廣播的事件，這也使我們能在特定事件觸發時執行特定的商業邏輯。在 nopCommerce 中，我們可以訂閱或監聽由系統事件所發布的各種事件，甚至可以編寫邏輯來發射/發布一個事件，隨後再進行監聽或訂閱。例如，假設我們想要將顧客詳細資料同步到其他外部系統，在這種情況下，當有新顧客在我們的商店註冊或顧客變更其個人資料時，我們可以觸發一個事件。我們可以監聽該事件並執行商業邏輯，該邏輯隨後可提取出該新建立的顧客並將其發送到外部服務進行同步。最棒的是，我們無需更改 nopCommerce 的原始碼即可完成上述所有操作。

開發者可以發布事件或取用事件：

- 若要發布事件，開發者需要取得 **IEventPublisher** 的實例，並呼叫包含適當事件資料的 **Publish** 方法。

- 若要監聽事件，開發者需要建立泛型 **IConsumer** 介面的新實作。一旦建立了新的消費者實作，nopCommerce 就會使用反射（reflection）來尋找並註冊該實作以進行事件處理。

有三個用於資料修改事件的事件發布者擴充方法，分別命名為 `EntityInsertedAsync`、`EntityUpdatedAsync` 與 `EntityDeletedAsync`，它們繼承自 **BaseEntity** 與 **IEventPublisher** 介面，分別負責廣播實體的插入、更新與刪除事件。

## EntityInsertedAsync

此擴充方法將 `BaseEntity` 型別的模型實體作為參數。當有新資料插入時，此擴充方法用於發布/廣播 `BaseEntity` 型別的實體插入事件。接著，此擴充方法會呼叫 `EntityInsertedEvent` 泛型類別的參數化建構函式，並透過其 Entity 屬性公開已插入的實體。開發者隨後可以透過實作特定實體型別的 `EntityInsertedEvent` 之 `IConsumer` 介面來訂閱/處理該事件，例如：`IConsumer<EntyInsertedEvent<BaseEntity>>`。此處的 `BaseEntity` 可以是任何繼承自 `BaseEntity` 類別的模型類別。

### EntityInserted 事件的發布者實作

```cs
public class MyFirstPublisherClass
{
    IEventPublisher _eventPublisher;
    public MyFirstPublisherClass(IEventPublisher eventPublisher)
    {
        this._eventPublisher = eventPublisher;
    }

    public async Task MyFirstProductInsertMethod(Product product)
    {
        //Logic to insert goes here
        await _eventPublisher.EntityInsertedAsync(product);
    }
}
```

在上述範例中，我們透過建構函式依賴注入機制注入 `IEventPublisher` 介面，以取得 `EventPublisher` 類別的實例。在此 `MyFirstProductInsertMethod` 方法中，完成插入商品的邏輯後，我們呼叫了 `EntityInserted` 方法，其泛型型別為 `Product`（需繼承自 `BaseEntity` 類別），並將新建立的商品物件作為參數傳入。現在，呼叫此擴充方法後，它將針對商品型別廣播實體插入事件，而此時任何訂閱/監聽此事件的對象都將收到此商品物件作為事件參數。現在讓我們看看如何取用此事件。

### EntityInserted 事件的消費者實作

```cs
public class MyFirstConsumerClass : IConsumer<EntityInsertedEvent<Product>>
{
    public async Task HandleEventAsync(EntityInsertedEvent<Product> insertEvent)
    {
        //you can access entity using insertEvent.Entity
        var insertedEntity = insertEvent.Entity;

        //Here goes the business logic you want to perform...

    }
}
```

在這裡，我們建立了一個繼承自 `IConsumer<EntityInsertedEvent<Product>>` 的類別。`IConsumer` 介面只有一個需要實作的方法，即 `HandleEvent` 方法。現在，每當觸發 `Product` 型別的 `EntityInserted` 事件時，就會呼叫此 `HandleEvent` 方法，並傳入該產品實體物件的 `EntityInsertedEvent`。在此類別內部，我們可以執行後續處理該資料的商業邏輯。

## EntityUpdatedAsync

`IEventPublisher` 介面的此擴充方法其實作方式與 `EntityInserted` 相同。此擴充方法同樣將 `BaseEntity` 型別的模型實體作為引數/參數。當現有實體更新時，此擴充方法用於發布/廣播 `BaseEntity` 型別的實體更新事件。此擴充方法呼叫 `EntityUpdatedEvent` 泛型類別的參數化建構函式，並透過其 Entity 屬性公開已更新的實體。開發者隨後可以透過實作特定實體型別的 `EntityUpdatedEvent` 之 `IConsumer` 介面來訂閱/處理該事件，例如：`IConsumer<EntityUpdatedEvent<BaseEntity>>`。

### EntityUpdated 事件的發布者實作

```cs
public class MyFirstPublisherClass
{
    IEventPublisher _eventPublisher;
    public MyFirstPublisherClass(IEventPublisher eventPublisher)
    {
        this._eventPublisher = eventPublisher;
    }

    public async Task MyFirstProductUpdateMethod(Product product)
    {
        //Logic to insert goes here
        await _eventPublisher.EntityUpdatedAsync(product);
    }
}
```

此類別的實作方式與 `EntityInserted` 中的範例大致相同。在 `MyFirstProductInsertMethod` 方法中，完成更新商品的邏輯後，我們呼叫了 `EntityUpdatedAsync` 方法，並將最近更新的商品物件作為參數傳入。呼叫此擴充方法後，它將針對商品型別廣播實體更新事件，而任何訂閱/監聽此事件的對象都將收到此商品物件作為事件參數。現在讓我們看看如何取用此事件。

### EntityUpdated 事件的消費者實作

```cs
public class MyFirstConsumerClass : IConsumer<EntityUpdatedEvent<Product>>
{
    public async Task HandleEventAsync(EntityUpdatedEvent<Product> updateEvent)
    {
        //you can access entity using updateEvent.Entity
        var updatedEntity = updateEvent.Entity;

        //Here goes the business logic you want to perform...

    }
}
```

這同樣與實體插入事件的消費者類別相同。在這裡，我們建立了一個繼承自 `IConsumer<EntityUpdatedEvent<Product>>` 的類別。現在，每當觸發 `Product` 型別的 `EntityUpdated` 事件時，就會呼叫此類別的 `HandleEvent` 方法，並傳入該產品實體物件的 `EntityUpdatedEvent`。在此類別內部，我們可以執行後續處理該資料的商業邏輯。

## EntityDeletedAsync

實作此擴充方法的邏輯也與 `IEventPublisher` 的 `EntityInsertedAsync` 和 `EntityUpdatedAsync` 擴充方法相同。此擴充方法同樣將 `BaseEntity` 型別的模型實體作為引數。當現有實體被刪除時，此擴充方法用於發布/廣播 `BaseEntity` 的實體刪除事件。此擴充方法呼叫 `EntityDeletedEvent` 泛型類別的建構函式，並透過其 Entity 屬性公開已刪除的實體。開發者隨後可以透過實作 `IConsumer<EntityDeletedEvent<BaseEntity>>` 來訂閱/處理該事件。

### EntityDeleted 事件的發布者實作

```cs
public class MyFirstPublisherClass
{
    IEventPublisher _eventPublisher;
    public ExamplePublisherClass(IEventPublisher eventPublisher)
    {
        this._eventPublisher = eventPublisher;
    }

    public async Task MyFirstProductDeleteMethod(Product product)
    {
        //Logic to insert goes here
        await _eventPublisher.EntityDeletedAsync(product);
    }
}
```

此類別的實作方式也與上述範例相同。在 `MyFirstProductDeleteMethod` 方法中，完成刪除商品的邏輯後，我們呼叫了 `EntityDeleted` 方法，並將最近刪除的商品物件作為參數傳入。呼叫此擴充方法後，它將針對商品型別廣播實體刪除事件，而任何訂閱/監聽此事件的對象都將收到此商品物件作為事件參數。現在讓我們看看如何取用此事件。

### EntityDeleted 事件的消費者實作

```cs
public class MyFirstConsumerClass : IConsumer<EntityDeletedEvent<Product>>
{
    public async Task HandleEventAsync(EntityDeletedEvent<Product> deleteEvent)
    {
        //you can access entity using deleteEvent.Entity
        var updatedEntity = deleteEvent.Entity;

        //Here goes the business logic you want to perform...
    }
}
```

同樣地，這也與實體插入事件或實體更新事件的消費者類別相同。在這裡，我們建立了一個繼承自 `IConsumer<EntityDeletedEvent<Product>>` 的類別。

現在，每當觸發 `Product` 型別的 `EntityDeleted` 事件時，就會呼叫此類別的 `HandleEvent` 方法，並傳入該產品實體物件的 `EntityDeletedEvent`。在此類別內部，我們可以執行後續處理該資料的商業邏輯。