---
標題: 觸發與處理事件
uid: zh-Hant/developer/tutorials/events
作者: git.AndreiMaz
貢獻者: git.exileDev, git.DmitriyKulagin
---

# 觸發與處理事件

事件是廣播給感興趣對象的通知。當發生資料異動（如新增、更新或刪除）時會觸發事件。nopCommerce 允許開發者「監聽」他們可能感興趣的事件。開發者處理事件的方式主要有兩種：一種是發布事件供監聽者使用，另一種是訂閱其他開發者以程式方式發布的事件。

1. 若要發布事件，開發者需要取得 **IEventPublisher** 的實例，並使用對應的事件資料呼叫 **PublishAsync** 方法。

   ```csharp
   await _eventPublisher.PublishAsync(new OrderPlacedEvent(order));
   ```

   事件類別如下所示：

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

1. 若要監聽事件，開發者需要建立一個實作泛型 **IConsumer** 介面的新類別。一旦建立了新的消費者實作，nopCommerce 就會使用反射（reflection）來尋找並註冊該實作以進行事件處理。

   ```csharp
   public class EventConsumer : IConsumer<OrderPlacedEvent>
   {
       public async Task HandleEventAsync(OrderPlacedEvent eventMessage)
       {
            if (eventMessage?.Order != null)
            {
                //執行某些操作
            }
       }
   }
   ```