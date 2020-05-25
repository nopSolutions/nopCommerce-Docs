---
title: Return requests
uid: en/running-your-store/order-management/return-requests
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Return requests

Return Request feature enables customers to request a return on items previously purchased. This option is only available for completed orders. Return requests settings are managed in **Configuration → Settings → Order Settings on the Return Request settings tab**.

When this option is enabled, a **Return item(s)** button is displayed for completed orders on the order details page in the public store.

**To submit a return request**, a customer would need to take the following steps:

In the public store, go to the My Account window and click Orders, as follows: ![My Account Orders](_static/return-requests/my-account-orders.png)

1. Click the **Return Item(s)** button beside the complete order that is to be returned. The **Return Item(s) from Order #** is displayed, as shown in the following example: ![Return Items](_static/return-requests/return-items.png)
    * **Qty. to return** dropdown list allows selecting the number of items to be returned.
    * **Return Reason** dropdown list allows selecting the reason for requesting a return. For example, wrong product ordered, wrong product received and more. These reasons are defined by the store owner in the Return Request Settings.
    * **Return Action** dropdown list allows selecting the required return action to take. For example, repair product, replace product, issue credit and so on. These actions are defined by the store owner in the Return Request Settings.
    * Choose the **Upload a file** option if you want to attach some additional documents, pictures to your request. 
	
	  > [!NOTE]
	  >
	  > This option is available only when the checkbox **Allow file uploads** in **Configuration → Settings → Order Settings** on the **Return Request settings tab** is checked.

    * In the **Comments** field, a customer can enter an optional comment for information purposes.
1. After using the Return Request feature, the customer can manage his/her requests from the **My Account** page in the public store, by clicking **Return Requests**: ![Return Requests](_static/return-requests/return-requests.jpeg)

The store owner can now manage this return request in the Administration Area.

**To view and edit return requests**, go to **Sales → Return requests**. Click Edit beside the return request, the Edit Return Request window is displayed.

![Edit return request](_static/return-requests/edit-return-requests.jpeg)

> [!NOTE]
> 
> The Request ID, Product, and Date fields are read-only and cannot be edited.

* **Order link** allows viewing the associated order details page.
* **Customer link** allows viewing the customer details page.
* **Status** dropdown list allows to select the request status: In the **Reason for Return** field, edit the reason for return, if necessary.
  * Pending
  * Received
  * Return Authorized
  * Item(s) Repaired
  * Item(s) Refunded
  * Request rejected
  * Cancelled
* In the **Requested Action** field, edit the requested action, if necessary.
* In the **Customer Comments** field, edit the comment entered by the customer, if necessary.
* In the **Staff Notes** field, enter an optional note for information purposes.

> [!NOTE]
> 
> Click the Notify customer about status change button to send an email to the customer informing him/her on the return request status change. ![Control buttons](_static/return-requests/control-elements.png)

## See also

* [Return request settings](xref:en/running-your-store/order-management/return-requests)
* [Orders](xref:en/running-your-store/order-management/orders)
* [YouTube Tutorial: Managing Return Requests](https://www.youtube.com/watch?v=VqF2GZ2ip_0&list=PLnL_aDfmRHwsbhj621A-RFb1KnzeFxYz4&index=17)


# Return request settings

To define the return request settings, go to **Configuration → Settings → Order Settings**. The Order Settings window will be displayed, in the advanced mode by default. Switch to the **Return request settings panel**.

![Return Requests Settings](_static/return-requests/return-requests-settings.png)

In return request settings you can define:

* To **Enable Returns System**, to enable your customers to submit return requests for purchased items.
* In the field **Return request number mask** specify custom return request number if needed.
* **Number of days that the return request is available**, to set the number of days that the return request link will be available in the customer area.

  > [!TIP]
  > 
  > For example, if the store owner allows returns within 30 days after the purchase, this field will be set to 30. When the customer logs into the website and looks at My Account, orders completed earlier than 30 days will not have a Return Request button.
* Tick the **Allow file uploads** checkbox if you want to allow uploading files (pictures for example) when submitting a return request. This option is especially useful for customers who faced some problems with their orders, such as receiving damaged items or wrong product, etc.
* **Return request reasons.** Enter a list of reasons that a customer can choose from when submitting a return request.
* **Return request actions.** Enter a list of actions that a customer can choose from when submitting a return request.

This page enables **multi-store configuration**, it means that the same settings can be defined for all stores, or differ from store to store. If you want to manage settings for a certain store, choose its name from Multi-store configuration drop-down list and tick all needed checkboxes at the left side to set custom value for them.

## See also

* [Return Requests](xref:en/running-your-store/order-management/return-requests)
* [Order Settings](xref:en/running-your-store/order-management/order-settings)
