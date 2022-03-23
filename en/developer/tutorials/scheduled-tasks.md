---
title: Scheduled Tasks
uid: en/developer/tutorials/scheduled-tasks
author: git.AndreiMaz
contributors: git.sinaislam, git.DmitriyKulagin, git.exileDev
---

# Scheduled Tasks

With Scheduled tasks, you can schedule a task to run at certain periods. For example, nopCommerce sends queued emails periodically. The basic steps to create a new task are:

1. Define a class that implements the **IScheduleTask** interface. It has only one method that takes no arguments; **ExecuteAsync**. As you guessed this method is invoked when the task should be run.

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

1. To schedule a task the developer should insert a new **ScheduleTask** record into the appropriate database table. You can use **IScheduleTaskService** for inserting such a record.

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
> When inserting the new record into **ScheduleTask** database table for a new scheduled task, it is important to keep **Type** column format **Namespace.TaskClassName, AssemblyName**.

## Troubleshooting

- Make sure your store has a valid URL.
- Restart the application after adding a new schedule task.
