---
title: Message Queue
uid: en/user-guide/configuring/system/message-queue
author: git.AndreiMaz
contributors: git.exileDev
---

# Message Queue

Emails are not sent immediately in nopCommerce. They are queued. Message queue contains all emails that are already sent or not yet sent.

To load message queues:

1. From the **System** menu, select **Message queue**. The **Message Queue** window is displayed.

    ![Message queue](_static/message-queue/message-queue.png)
1. Enter one or more of the following information to search for the message queue:
    * From the **Start date** field, select the start date for the message queue.
    * From the **End date** field, select the end date for the message queue.
    * In the **From address** field, enter the source address of the message queue.
    * In the **To address** field, enter the target address of the message queue.
    * Select the **Load not sent emails only** checkbox, to only load emails into the queue that have not yet been sent.
    * In the **Maximum send attempts** field, enter the maximum number of attempts to send a message.
    * In the **Go directly to email** field, enter the email and click Go to display the required email.
1. Click **Load** to load the message queues matching the criteria.

> [!NOTE]
> 
> You can click the Delete selected button to delete selected emails from the grid. You can click Delete All to remove all emails.
