---
title: Affiliates
uid: en/running-your-store/promotional-tools/affiliates
author: git.AndreiMaz
contributors: git.rajupaladiya, git.exileDev, git.IvanIvanIvanov, git.mariannk, git.DmitriyKulagin
---

# Affiliates

Affiliate marketing is an internet-based marketing practice in which affiliates are rewarded for the website traffic generated (each visitor or customer). It is a web-based pay-for-performance program designed to compensate affiliate partners for driving qualified leads or sales from their websites to the merchant's website.

Affiliates are third parties who refer customers to your site. The nopCommerce software can track those referrals so that the store administrator can determine the commission to be paid to affiliates. Once a customer is assigned an affiliate ID, every order they place will also be tagged with that ID.

 In nopCommerce, an affiliate partner link looks as follows: `http://www.yourstore.com/?AffiliateID=N` (where N is an affiliate ID). A store owner can also specify the *friendly URL* name field for marketing purposes: `http://www.yourstore.com/?affiliate=your_friendly_name_here`. This URL is displayed when you visit the affiliate details page. Whenever this hyperlink is clicked on the affiliate site, nopCommerce looks for an affiliate ID query string parameter.

![nopCommerce affiliates](_static/affiliates/affiliates.jpg.png)

## Add a new affiliate

To add an affiliate, go to **Promotions → Affiliates** and click **Add new**.

![Add a new affiliate](_static/affiliates/affiliate_new.png)

Define the affiliate details:

- Select the **Active** checkbox to activate the affiliate.
- **First name**.
- **Last name**.
- **Email**.
- **Company** name.
- Select the **Country** from the dropdown list.
- If the selected country is the USA, specify the **State/province** as well.
- **County/region**.
- **City**.
- **Address 1**.
- **Address 2**.
- **Zip/postal code**.
- **Phone number**.
- **Fax number**.
- In the **Admin comment** field, you can enter an optional comment or information for internal use.
- You can specify the **Friendly URL name**, which is a friendly affiliate URL link for marketing purposes, or you can leave this field empty, then the default URL will be used. By default, affiliate partners have URLs like: `http://www.yourstore.com/?AffiliateID=N` (where N is an affiliate ID).

If you click **Save and continue edit**, you will see two more panels where you can check how effective this affiliate is:

- The *Affiliated customers* panel shows a list of all affiliated customers.
- The *Affiliated orders* panel shows a list of all affiliated orders. When an affiliated customer places an order, you can see the order in this panel.

## See also

- [Order management](xref:en/running-your-store/order-management/index)
- [Customer management](xref:en/running-your-store/customer-management/index)
