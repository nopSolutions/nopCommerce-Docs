---
title: Logs
uid: en/user-guide/configuring/system/log
author: git.AndreiMaz
contributors: git.exileDev
---

# Logs

The system log report displays a list of all the errors that were created in the system. This information includes, the log type, the customer that created the error, the date, and the description of the error. Clicking View, displays additional details of the error that occurred. You can click **Delete** to remove a log from the system if required.

## View system log information

1. From the **System** menu, select **Log**. The **System Log** window is displayed.

    ![Logs](_static/log/log.png)
1. Enter one or more of the following information to search for the system log information:
    * From the **Created from** field, select the start date for the search.
    * From the **Created to** field, select the end date for the search.
    * In the **Message** field, select the message or part of the message to search by.
    * From the **Log level** dropdown list, select the type of log information to display, as follows:
      * All
      * Debug
      * Information
      * Warning
      * Error
      * Fatal
1. Click **Search**. The log system window is displayed based on the search criteria

    > [!NOTE]
    > 
    > You can click the Clear Log button at any time to remove all log entries from the system.
1. Click **View** to view additional details of the specific log, as follows:

    ![Log entry - Details](_static/log/log-details.jpg)
