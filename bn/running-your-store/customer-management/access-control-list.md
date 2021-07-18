---
title: Access control list
uid: en/running-your-store/customer-management/access-control-list
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.ivkadp, git.mariannk
---

# Access control list

Access Control List (ACL) restricts or grants users an access to certain areas of the site. This list is managed by administrators. Therefore, the user must have administrator rights to be able to access it. The access list has the following characteristics:

* Access control list is role-based, i.e. manages roles such as *Global administrators*, *Content managers*, etc. This list of roles can be managed on the **Customers → Customer roles** page. For further details refer to [Customer roles](xref:en/running-your-store/customer-management/customer-roles).
* Access control list appears in the administration area. Make sure, the user is an administrator in order to access the ACL.
* Predefined administrator actions exist. These include *Manage orders*, *Manage customers* and much more.

To manage an access control list go to **Configuration → Access control list**. The *Access control list* window is displayed:

![Access control list](_static/access-control-list/acl.png)

Select the required roles beside the *Permission* items. The selected roles will have access to the selected actions accordingly.

Click **Save**.

> [!TIP]
> 
> Example: We need a role called *Content manager*. The *Content manager* must have access to new products and manufacturers management, editing of the reviews on the site, blogs, campaigns, and have no access to shopping carts.
> To do this:
> 1. Create a **Customer role** called *Content manager* on the **Customers → Customer roles** page.
> 1. In the ACL tick the checkbox next to the following permissions: *Access admin area, Admin area. Manage blog, Admin area. Manage campaigns, Admin area. Manage forums, Admin area. Manage news, Admin area. Manage newsletter subscribers, Public store. Allow navigation, Public store. Display prices*.
> 1. Save the changes.
