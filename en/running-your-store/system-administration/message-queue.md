---
title: Message queue
uid: en/running-your-store/system-administration/message-queue
author: git.AndreiMaz
contributors: git.exileDev, git.mariannk
---

# Message queue

Emails are not sent immediately in nopCommerce. They are queued. A message queue contains all emails already sent or not yet sent.

To load the message queue, in the **System** menu, select **Message queue**. The *Message queue* window will be displayed as follows:
![Message queue](_static/message-queue/message-queue.png)

Enter one or more of the following criteria to search for messages:

* In the **Start date** field, select the start date.
* In the **End date** field, select the end date.
* In the **From address** field, enter the source address of a message.
* In the **To address** field, enter the target address of a message.
* Select the **Load not sent emails only** checkbox to only load emails that have not been sent yet.
* In the **Maximum send attempts** field, enter the maximum number of attempts to send a message.
* In the **Go directly to email #** field, enter the email number and click **Go** to display the required email.

Click **Search** to load the message queue matching the criteria.

On this page, you can click the **Delete selected** button to delete selected emails from the grid. You can click **Delete all** to remove all emails.

## Message queue item details

To view the message queue item details, click the **Edit** button beside the message. The *Edit message queue item* window will be displayed:

![Message queue item details](_static/message-queue/edit.jpg)

In this window, you can delete the message by clicking the **Delete** button. Or you can requeue the message using the **Requeue** button.

On this page, you can edit the following message details:

* **From** email address.
* **From name**.
* **To** email address.
* **To name**.
* **ReplyTo** email address.
* **ReplyTo name**.
* **Cc** email address.
* **Bcc** email address.
* Email message **Subject**.
* Email message **Body**.
* Select the **Send immediately** checkbox to send this message immediately.
* Enter the number of **Sent attempts**. This is the number of attempts to send this message.

Click **Save** or **Save and continue edit** to save the message details.
