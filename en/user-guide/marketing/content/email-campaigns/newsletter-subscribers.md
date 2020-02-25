---
title: Newsletter subscribers
uid: en/user-guide/marketing/content/email-campaigns/newsletter-subscribers
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.IvanIvanIvanov
---

# Newsletter subscribers

During customer registration, a customer can select the **Newsletters option** to receive nopCommerce newsletters.

![customers-subscribing](_static/newsletter-subscribers/customer-subs.png)

The other way of filling in email subscribers is to **export the list of subscribers** to an external CSV file as well **import list of subscribers from an external CSV** file into nopCommerce.

To Export/Import Newsletter subscribers go to **Promotions  → Newsletter Subscribers.**

![newsletter-subscribers](_static/newsletter-subscribers/NewsLetterSubscription.png)

> [!NOTE]
> 
> You can click **Import from CSV** to import subscriber lists in CSV format. Ensure that each line of the CSV file is in the following format: email_address,is_active,store_id (store_id parameter is optional). For example, `test@test.com`, true. You can click **Export to CSV**  to export subscriber lists.

## Searching for the subscribers

On the **Newsletter subscribers page** you can find certain subscribers using following fields for **search:**

- Specify **Start date** and **End date** for the search
- Enter the **email** of the subscriber to find, or leave this field empty and click Search to load all the newsletter subscribers registered in the system
- From the **Active** dropdown list, choose between active and inactive subscribers
- From the **Customer roles** dropdown list, select the customer role in which a user subscribed to newsletters

## See also

- [Campaigns](xref:en/user-guide/marketing/content/email-campaigns/all-campaigns)
