---
title: Email accounts
uid: en/getting-started/email-accounts
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.mariannk
---

# Email accounts

This chapter describes how to set up email accounts associated with your store: a general contact email, a sales representative email, a customer support email, and more.

To manage email accounts, go to **Configuration → Email accounts**. The *Email accounts* window displays the email accounts of the store owner, as shown below. After the email accounts are configured, the store owner can select the required email account on the message template details page as described in the [Message templates](xref:en/running-your-store/content-management/message-templates) chapter.

![Email accounts](_static/email-accounts/email-accounts.png)

## Add a new email account

To add a new email account, click **Add new**. The *Add a new email account* window will be displayed:

![Add a new email account](_static/email-accounts/email-accounts-add-new.png)

Define the following email account information:

* In the **Email address** field, enter the email address for all outgoing emails of your store. Example: `sales@yourstore.com`.
* In the **Email display name** field, enter the displayed name for outgoing emails of your store. Example: "Your store sales department."
* In the **Host** field, enter the host name of IP address of your email server.
* In the **Port** field, enter the SMTP port of your email server.
* In the **User** field, enter the user name of your email server.
* In the **Password** field, enter the password of your email server.
* Select the **SSL** checkbox to use Security Sockets Layer to encrypt the SMTP connection.
* Select the **Use default credentials** checkbox to use default credentials for the connection.

Click **Save**. The window will be expanded as follows:

![Email account - Details](_static/email-accounts/email-accounts-details.png)

In the **Send email to** field, enter the email address for the test email and click **Send test email**.
