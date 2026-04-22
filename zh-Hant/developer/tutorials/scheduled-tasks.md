---
標題: 排程工作
uid: zh-Hant/developer/tutorials/scheduled-tasks
作者: git.AndreiMaz
貢獻者: git.sinaislam, git.DmitriyKulagin, git.exileDev
---

# 排程工作

透過「排程工作」，您可以設定工作在特定的時間週期執行。例如，nopCommerce 會定期發送佇列中的 Email。建立新工作的基本步驟如下：

1. 定義一個實作 **IScheduleTask** 介面的類別。該介面僅包含一個不帶參數的方法：**ExecuteAsync**。正如您所預料，當任務需要執行時，就會呼叫此方法。

    ```csharp
    public partial class KeepAliveTask : IScheduleTask
    {
        private readonly StoreHttpClient _storeHttpClient;
        public KeepAliveTask(StoreHttpClient storeHttpClient)
        {
            _storeHttpClient = storeHttpClient;
        }
        public async System.Threading.Tasks.Task ExecuteAsync()
        {
            await _storeHttpClient.KeepAliveAsync();
        }
    }
    ```

1. 若要排程工作，開發人員應在對應的資料庫表中插入一筆新的 **ScheduleTask** 紀錄。您可以使用 **IScheduleTaskService** 來進行插入。

    ```csharp
    await _scheduleTaskService.InsertTaskAsync(new ScheduleTask
    {
        Name = "Keep alive",
        Seconds = 300,
        Type = "Nop.Services.Common.KeepAliveTask, Nop.Services",
        Enabled = true,
        LastEnabledUtc = lastEnabledUtc,
        StopOnError = false
    });
    ```

> [!IMPORTANT]
> 當為新的排程工作在 **ScheduleTask** 資料庫表中插入新紀錄時，請務必將 **Type** 欄位格式保持為 **命名空間.工作類別名稱, 組件名稱**。

## 疑難排解

- 請確保您的商店擁有有效的 URL。
- 新增排程工作後，請重新啟動應用程式。