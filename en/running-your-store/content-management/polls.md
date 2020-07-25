---
title: Polls
uid: en/running-your-store/content-management/polls
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Polls

Polls functionality in nopCommerce allows you to make your eCommerce site more interactive. There are many ways you can use polls for an eCommerce site. One popular way is to use them as a short customer satisfaction survey. People like being asked for a feedback and this is a good opportunity to see how you are doing as an online merchant.

The poll on the home page of the Deafult Clean nopCommerce theme looks like:
![Home page poll](_static/polls/polls_3.png)

To view all the polls and add new ones go to **Content management → Polls**.

![Polls list](_static/polls/polls_1.png)

To search for polls that were used in a certain store, select a name of the store from the list.

## Adding polls

To add a new poll click the **Add new** button in the top right.

![Add a new poll](_static/polls/add-new.jpg)

### Poll info
Define the following details for the new poll:
- If more than one language is enabled, from the **Language** dropdown list, select the language of this poll. Customers will only see polls for their selected language.
- Enter the descriptive **Name** of this poll. This is the text the customers will see. For example, "What do you think about our store?".
- Tick the **Published** checkbox to make this poll active.
- Tick the **Show polls on home page** checkbox if you want to show the poll on the home page.
- Tick the **Allow guests to vote** checkbox to enable unregistered guests to vote for the poll.
- Enter the **Start date** and **End date** in Coordinated Universal Time (UTC).
  > [!NOTE]
  > 
  > You can leave these fields empty if you do not want to define news item start and end dates.

- Choose the stores in the **Limited to stores** field to enable this poll for specific stores only. Leave the field empty in case this functionality is not required.
  > [!NOTE]
  >
	> In order to use this functionality, you have to disable the following setting: **Catalog settings → Ignore "limit per store" rules (sitewide)**. Read more about multi-store functionality [here](xref:en/getting-started/advanced-configuration/multi-store).

- In the **System keyword** field you can specify where the poll will be displayed. For example, LeftColumnPoll.
- Enter the **Display order** of the poll. A value of 1 represents the top of the list.

Click **Save and continue edit** to proceed to the *Poll answers* panel.

### Poll answers

Fill the following poll answer info:
* The **Name** that will be displayed to a customer.
* The **Display order**. A value of 1 represents the top of the list.

Then click **Add new record** button to save the answer.

The complete list of answers can look the following way:
![Poll answers](_static/polls/answers.jpg)

You can then **Edit** records, if required and **Delete** them.

## Tutorials

- [Managing polls in nopCommerce](https://www.youtube.com/watch?v=RJP45cUhuZQ)
