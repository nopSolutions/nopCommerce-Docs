---
title: Schedule tasks
uid: en/running-your-store/system-administration/schedule-tasks
author: git.AndreiMaz
contributors: git.exileDev, git.mariannk, git.DmitriyKulagin
---

# Schedule tasks

The *Schedule tasks* window enables the store owner to schedule a task to run during certain periods in the background and to view useful information regarding the task and whether it was completed successfully. For example, nopCommerce sends queued emails periodically. The tasks run on a separate thread coming from the ASP.NET thread pool.

To view the scheduled tasks, in the **System** menu, select **Schedule tasks**. The *Schedule tasks* window will be displayed as follows:
![Schedule tasks](_static/schedule-tasks/schedule-tasks.png)

To edit a scheduled task, click the **Edit** button beside the task. The window will expand as follows:
![Schedule tasks - Edit](_static/schedule-tasks/schedule-tasks-edit.png)

You can edit the scheduled task as follows:

* Edit the **Name**.
* Edit the number of **Seconds (run period)**. A task period should not exceed 24 days.
* Select the **Enabled** checkbox in order to enable the task.
* Select the **Stop on error** checkbox in order to stop the task when an error occurs.

Click **Update** to save your changes.

> [!NOTE]
>
> Do not forget to restart the application once a task has been modified.

If required, you can click **Run now** to run a scheduled task on demand.
