---
title: Log
uid: en/running-your-store/system-administration/log
author: git.AndreiMaz
contributors: git.exileDev, git.mariannk, git.DmitriyKulagin
---

# Log

The system log report displays a list of all the errors, warnings, and information messages created in the system. To view the log, go to **System → Log**. The *Log* window will be displayed as follows:

![Log](_static/log/log.png)

A log item includes the log type, description of the error, and date. You can click the **Delete selected** button to remove the selected log items or click the **Clear log** button to clear the entire log.

To search for a system log, enter one or more of the following pieces of information:

* In the **Created from** field, select the start date for the search.
* In the **Created to** field, select the end date for the search.
* In the **Message** field, select the message or its fragment to search by.
* From the **Log level** dropdown list, select the type of log information to display as follows:
  * *All*
  * *Debug*
  * *Information*
  * *Warning*
  * *Error*
  * *Fatal*

Click **Search**. The log system window will be displayed based on the search criteria.

## View system log details

Clicking **View** displays additional details of the error that occurred, as follows:

![Log entry - Details](_static/log/log-details.jpg)

You can click **Delete** to remove a log from the system if required.
