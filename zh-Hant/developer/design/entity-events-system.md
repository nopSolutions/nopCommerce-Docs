---
標題: 實體事件系統
uid: zh-Hant/developer/design/entity-events-system
作者: git.nopsg
貢獻者: git.nopsg, git.DmitriyKulagin
---

# 實體事件系統

## 概覽

nopCommerce 實作了事件驅動架構，允許開發者訂閱或取用由事件發布者或事件來源所廣播的事件，當特定動作或事件執行時，這也能讓我們執行特定的業務邏輯。在 nopCommerce 中，我們可以訂閱或監聽由 nopCommerce 系統事件所發布的各種事件，甚至可以編寫程式碼來發射（發布）一個事件，隨後再由其他地方進行監聽或訂閱。例如，假設我們想要將顧客詳細資料同步到其他外部系統，那麼我們可以在新顧客註冊到商店或顧客變更個人資料時觸發一個事件。我們可以監聽該事件並執行業務邏輯，從而提取該新建立的顧客資料並發送到外部服務進行同步。最棒的是，我們可以在不修改 nopCommerce 原始程式碼的情況下完成所有這些操作。

開發者可以發布事件，也可以取用事件：

- 若要發布事件，開發者需要取得 **IEventPublisher** 的實例，並使用適當的事件資料呼叫 **Publish** 方法。

- 若要監聽事件，開發者需要為泛型 **IConsumer** 介面建立一個新的實作。一旦建立了新的消費者實作，nopCommerce 就會使用反射（Reflection）來尋找並註冊該實作，以進行事件處理。

有三個用於資料修改事件的事件發布者擴充方法，分別名為 `EntityInsertedAsync`、`EntityUpdatedAsync` 和 `EntityDeletedAsync`，它們與繼承自 **BaseEntity** 的 **IEventPublisher** 介面搭配使用，分別負責廣播實體的插入、更新和刪除事件。

## EntityInsertedAsync

此擴充方法將 `BaseEntity` 類型的模型實體作為參數。當有新資料插入時，此擴充方法用於發布/廣播 `BaseEntity` 類型的實體插入事件。接著，此擴充方法會呼叫 `EntityInsertedEvent` 泛型類別的參數化建構函式，並透過其 Entity 屬性公開已插入的實體。開發者可以透過實作 `IConsumer<EntyInsertedEvent<BaseEntity>>`（例如我們剛插入的實體類型的 `EntityInsertedEvent`）來訂閱/處理該事件。此處的 `BaseEntity` 可以是任何繼承自 `BaseEntity` 類別的模型類別。

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

在上述範例中，我們使用建構函式依賴注入機制來注入 `IEventPublisher` 介面，以取得 EventPublisher 類別的實例。這裡在 `MyFirstProductInsertMethod` 中，完成產品插入邏輯後，我們呼叫了 `EntityInserted` 方法，並傳入泛型類型 `Product`（需要繼承自 BaseEntity 類別）以及新建立的產品物件作為參數。呼叫此擴充方法後，它將廣播該產品類型的實體插入事件，現在任何訂閱/監聽此事件的人都會收到這個產品物件作為事件參數。現在讓我們看看如何取用此事件。

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

在這裡，我們建立了一個繼承自 `IConsumer<EntityInsertedEvent<Product>>` 的類別。`IConsumer` 介面只有一個需要實作的方法，即 `HandleEvent` 方法。現在，每當觸發 `Product` 類型的 `EntityInserted` 事件時，就會以該產品實體的 `EntityInsertedEvent` 為參數來呼叫此 `HandleEvent` 方法。在此類別內部，我們可以執行業務邏輯以進一步處理這些資料。

## EntityUpdatedAsync

`IEventPublisher` 介面的此擴充方法實作方式與 EntityInserted 相同。此擴充方法同樣將 `BaseEntity` 類型的模型實體作為參數。當現有實體更新時，此擴充方法用於發布/廣播 `BaseEntity` 類型的實體更新事件。此擴充方法會呼叫 `EntityUpdatedEvent` 泛型類別的參數化建構函式，並透過其 Entity 屬性公開更新後的實體。開發者可以透過實作 `IConsumer<EntityUpdatedEvent<BaseEntity>>`（例如我們剛更新的實體類型的 `EntityUpdatedEvent`）來訂閱/處理該事件。

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

此類別的實作方式與 `EntityInserted` 範例大同小異。在 `MyFirstProductInsertMethod` 中，完成產品更新邏輯後，我們呼叫了 `EntityUpdatedAsync` 方法，並傳入剛更新的產品物件作為參數。呼叫此擴充方法後，它將廣播該產品類型的實體更新事件，任何訂閱/監聽此事件的人都會收到該產品物件作為事件參數。現在讓我們看看如何取用此事件。

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

這同樣與實體插入事件的消費者類別相同。我們建立了一個繼承自 `IConsumer<EntityUpdatedEvent<Product>>` 的類別。現在，每當觸發 `Product` 類型的 `EntityUpdated` 事件時，此類別的 `HandleEvent` 方法就會以該產品實體的 `EntityUpdatedEvent` 為參數被呼叫。在此類別內部，我們可以執行業務邏輯以進一步處理這些資料。

## EntityDeletedAsync

實作此擴充方法的邏輯也與 `IEventPublisher` 的 `EntityInsertedAsync` 和 `EntityUpdatedAsync` 擴充方法相同。此擴充方法同樣將 `BaseEntity` 類型的模型實體作為參數。當刪除現有實體時，此擴充方法用於發布/廣播 `BaseEntity` 的實體刪除事件。此擴充方法呼叫 `EntityDeletedEvent` 泛型類別的建構函式，並透過其 Entity 屬性公開已刪除的實體。開發者可以透過實作 `IConsumer<EntityDeletedEvent<BaseEntity>>` 來訂閱/處理該事件。

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

此類別的實作方式也與上述範例相同。這裡在 `MyFirstProductDeleteMethod` 中，完成產品刪除邏輯後，我們呼叫了 `EntityDeleted` 方法，並傳入剛刪除的產品物件作為參數。呼叫此擴充方法後，它將廣播該產品類型的實體刪除事件，任何訂閱/監聽此事件的人都會收到該產品物件作為事件參數。現在讓我們看看如何取用此事件。

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

同樣地，這與實體插入事件或實體更新事件的消費者類別相同。我們在此建立一個繼承自 `IConsumer<EntityDeletedEvent<Product>>` 的類別。

現在，每當觸發 `Product` 類型的 `EntityDeleted` 事件時，此類別的 `HandleEvent` 方法就會以該產品實體的 `EntityDeletedEvent` 為參數被呼叫。在此類別內部，我們可以執行業務邏輯以進一步處理這些資料。