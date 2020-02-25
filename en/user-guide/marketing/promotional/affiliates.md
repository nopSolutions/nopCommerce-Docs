---
title: Affiliates
uid: en/user-guide/marketing/promotional/affiliates
author: git.AndreiMaz
contributors: git.rajupaladiya, git.exileDev, git.IvanIvanIvanov
---

# Affiliates

**Affiliate Marketing** is an Internet-based marketing practice in which affiliates are rewarded for the website traffic generated (each visitor or customer). It is a web-based pay-for-performance program designed to compensate affiliate partners for driving qualified leads or sales from their websites to a merchant website.

**Affiliates** are third parties who refer customers to your site. The nopCommerce software can track those referrals so that the store administrator can determine the commission to be paid to them. Once a customer is assigned an affiliate ID, every order they place is also tagged with that ID.

 In nopCommerce, an affiliate partner link looks as follows: `http://www.yourstore.com/?AffiliateID=N` (where N is an affiliate ID). A store owner can also specify Friendly URL name field for marketing purposes: `http://www.yourstore.com/?affiliate=your_friendly_name_here`. This URL is displayed **when you visit the affiliate details page.** When this **hyperlink is clicked** from the affiliate site, the default.aspx looks for an Affiliate ID query string parameter.

![nopcommerce-affiliates](_static/affiliates/affiliates.jpg.png)

## Adding new affiliates

To add an affiliate go to **Promotions → Affiliates** and click **Add new.**

![adding_new_affiliate](_static/affiliates/affiliate_new.png)

Define the **affiliate details:**

- Select the Active checkbox, to activate the affiliate
- First Name and Last Name
- Email
- Company name
- Select the Country from the drop-down list. If the selected country is the USA also specify the State/Province as well
- County
- City
- Address 1 and Address 2
- Zip/ Postal code
- Phone number
- Fax number
- In the Admin comment field, you can enter an optional comment or information for internal use
- You can **specify Friendly affiliate URL link** for marketing purposes or you leave this field empty, then the default URL will be used. By default In nopCommerce, affiliate partners have URL: `http://www.yourstore.com/?AffiliateID=N` (where N is an affiliate ID).

> [!TIP]
> 
> **When you fill all the fields Save the changes you’ll see two more tabs Affiliate customers and Affiliate orders, where you can check how effective this affiliate is.**

The store owner can see a list of all affiliated customers on the affiliate details page, which is the Affiliated Customers in nopCommerce. When an affiliated customer places an order, you can see this order on the affiliate details page under the Affiliate orders tab.
