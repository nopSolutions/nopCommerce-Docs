---
title: Return requests
uid: en/running-your-store/order-management/return-requests
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev, git.mariannk
---

# Return requests

Return request feature enables customers to request a return on items previously purchased. These are also known as RMA requests. This option is only available for completed orders. Return requests settings are managed in **Configuration → Settings → Order settings** in the *Return request settings* panel.

To enable return requests tick the **Enable returns system** checkbox.
When this option is enabled, a **Return item(s)** button is displayed for completed orders on the order details page in the public store.

To proceed to the return request settings section click [here](#return-request-settings).

In the next sections we will describe how the return request functionality can be used by your customers and how to manage return requests in admin area.

## Submit a return request
To submit a return request, a customer would need to take the following steps:

1. In the public store, go to the *My account* window and click **Orders**. The following page will be displayed: 
![My account - Orders](_static/return-requests/my-account-orders.jpg)

1. Click the **Return Item(s)** button beside the completed order that is to be returned. The *Return item(s) from order #* window is displayed, as shown in the following example: 
  ![Return items](_static/return-requests/return-items.jpg)
    * **Qty to return** dropdown list allows selecting the number of items to be returned.
    * **Return reason** dropdown list allows selecting the reason for requesting a return. For example, wrong product ordered, wrong product received and more. Read [below](#return-request-settings) how to manage return reasons.
    * **Return action** dropdown list allows selecting the required return action to take. For example, repair product, replace product, issue credit and so on. Read [below](#return-request-settings) how to manage return actions.
    * Use the **Upload a file** option if you want to attach some additional documents or pictures to your request. 
	    > [!NOTE]
	    >
	    > This option is available only when the **Allow file uploads** checkbox is checked. Read [below](#return-request-settings) how to set this up.

    * In the **Comments** field, a customer can enter an optional comment for information purposes.
1. After using the return request feature, the customer can see the created return requests and its' statuses from the *My Account* page in the public store, by clicking **Return requests**: 
  ![Return requests public](_static/return-requests/return-requests.jpg)

## Manage return requests
The store owner can now manage this return request in the administration area.

To view and edit return requests, go to **Sales → Return requests**. All return requests will be displayed, as follows:
![Return requests admin](_static/return-requests/return-requests-admin.jpg)

Click **Edit** beside the return request, the *Edit return request details* window is displayed:
![Edit return request](_static/return-requests/edit-return-request.jpg)

The store administrator is allowed to:
* View the return request **ID**.
* View the **Order #**. Clicking on the order number redirects to the associated order details page.
* View the **Customer**. Clicking on the customer email redirects to the associated customer details page.
* View the **Product**. Clicking on the product name redirects to the associated product details page.
* Enter the **Quantity** of the returned product.
* Select the **Return request status**:  
  * *Pending*
  * *Received*
  * *Return authorized*
  * *Item(s) repaired*
  * *Item(s) refunded*
  * *Request rejected*
  * *Cancelled*

* In the **Reason for return** field, edit the reason for return, if necessary.
* In the **Requested action** field, edit the requested action, if necessary.
* In the **Customer comments** field, edit the comment entered by the customer, if necessary.
* In the **Staff notes** field, enter an optional note for information purposes. These notes will not be displayed to a customer.
* View the **Date** when the return request was submitted.

> [!NOTE]
> 
> Click the **Notify customer about status change** button to send an email to the customer informing on the return request status change. ![Control buttons](_static/return-requests/control-elements.png)

## Return request settings
To define the return request settings, go to **Configuration → Settings → Order settings**. 

This page enables multi-store configuration, it means that the same settings can be defined for all stores, or differ from store to store. If you want to manage settings for a certain store, choose its name from multi-store configuration drop-down list and tick all needed checkboxes at the left side to set custom value for them. For further details refer to [Multi-store](xref:en/getting-started/advanced-configuration/multi-store).

Go to the *Return request settings* panel:
![Return request settings](_static/return-requests/return-request-settings.jpg)

In this panel you can define:
* To **Enable returns system**, to enable your customers to submit return requests for purchased items.
* In the field **Return request number mask** specify custom return request number if needed.
* **Number of days that the return request is available**, to set the number of days that the return request link will be available in the customer area.
  > [!TIP]
  > 
  > For example, if the store owner allows returns within 30 days after the purchase, this field will be set to 30. When the customer logs into the website and looks at "My account", orders completed earlier than 30 days ago will not have a **Return item(s)** button.

* Tick the **Allow file uploads** checkbox if you want to allow uploading files (pictures, for example) when submitting a return request. This option is especially useful for customers who faced some problems with their orders, such as receiving damaged items or wrong products, etc.

### Return request reasons
This panel represents a list of reasons a customer can choose from when submitting a return request.
![Return request reasons](_static/return-requests/return-request-reasons.jpg)

Click **Add new** to add a new request reason. The *Add new return request reason* window will be displayed, as follows:
![Add a return request reason](_static/return-requests/add-reason.jpg)

Enter the return request reason **Name** and **Display order** number (1 represents the first item in the list). Click **Save** to save the changes.

### Return request actions
This panel represents a list of actions a customer can choose from when submitting a return request.
![Return request actions](_static/return-requests/return-request-actions.jpg)

Click **Add new** to add a new request action. The *Add new return request action* window will be displayed, as follows:
![Add a return request action](_static/return-requests/add-action.jpg)

Enter the return request action **Name** and **Display order** number (1 represents the first item in the list). Click **Save** to save the changes.

## See also

* [YouTube tutorial: managing return requests](https://www.youtube.com/watch?v=VqF2GZ2ip_0&list=PLnL_aDfmRHwsbhj621A-RFb1KnzeFxYz4&index=17)
* [Order settings](xref:en/running-your-store/order-management/order-settings)
* [Orders](xref:en/running-your-store/order-management/orders)