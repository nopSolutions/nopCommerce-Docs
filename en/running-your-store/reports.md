---
title: Reports
uid: en/running-your-store/reports
author: git.AndreiMaz
contributors: git.exileDev, git.mariannk
---

# Reports

Reports are important for management, they allow to monitor store performance, track key metrics and support decision making. nopCommerce reports provide access to sales and customer information.

## Dashboard

The dashboard is the first page you see when accessing the admin area. It enables you to view your store statistics, including the total number of orders that were processed over the period of time you choose (year, month, week), registered customers, low stock products, the most popular products in your store, etc.

![Dashboard](_static/reports/dashboard.png)

The dashboard consist of several blocks:

### nopCommerce news
Displays general nopCommerce news like new version releases.
![News](_static/reports/news.png)

### Common statistics 
![Common](_static/reports/common.png)

Here you can find links to more detailed reports:
* Sales → Orders
* Sales → Return requests
* Customers → Registered customers
* Reports → Low stock

### Orders
This diagram enables you to know the number of orders that were processed in the last week, month, year.
![Orders](_static/reports/orders.jpg)

### New customers
This diagram shows the number of customers registered in last week, month, year.
![New customers](_static/reports/customers.jpg)

### Order totals
This section enables you to know the total of orders that were processed in the last day, week, month, year. Orders are shown by order status.
![Order totals](_static/reports/order-totals.png)

### Incomplete orders
This section enables you to know the number of orders that are currently incomplete.
![Orders incomplete](_static/reports/order-incomplete.png)

### Latest orders
Latest orders section shows you the latest placed orders.
![Latest orders](_static/reports/order-latest.png)

### Popular search keywords
This block displays most used keywords.

![Keywords](_static/reports/keywords.png)

### Bestsellers
This section displays the bestsellers by quantity and by amount.
![Bestsellers](_static/reports/bestsellers.png)


## Country report
Country report contains a list of orders that includes the number of orders and the total order sum in each country. This enables store owners to view the orders per country.

To view country reports, go to **Reports → Country Report**.

![Country report](_static/reports/country-report.png)

To set up the report, enter one or more of the following search criteria:
* **Start date** for the search.
* **End date** for the search.
* **Order status**, such as *All*, *Pending*, *Processing*, *Complete*, or *Cancelled*.
* **Payment status**, such as *All*, *Pending*, *Authorized*, *Paid*, *Refunded*, *Partially refunded*, or *Voided*.

Then click **Run report**.


## Customer reports
Customer reports give a store owner general information about registered customers and their orders. You can find different reports in the **Reports → Customer reports** menu.

### Registered customers
To run this report go to **Reports → Customer reports → Registered customers**.
This report displays the number of registered customers for a certain period.
You can track the number of users registered within the last day, week, two weeks, month and year.

![Registered customers](_static/reports/customer-registered.png)

### Customers by order total
To run this report go to **Reports → Customer reports → Customers by order total**.
In this report, you can see the orders total spent by customers and the number of orders by customers.

![Customers by order total](_static/reports/Customers-by-order-total.png)

Enter one or several search criteria to compile a report:
* Registration **Start date**.
* Registration **End date**.
* **Order status**, such as *All*, *Pending*, *Processing*, *Complete*, or *Cancelled*.
* **Payment status**, such as *All*, *Pending*, *Authorized*, *Paid*, *Refunded*, *Partially refunded*, or *Voided*.
* **Shipping Status** as *All*, *Shipping not required*, *Not yet shipped*, *Partially shipped*, *Shipped*, *Delivered*.

Then click **Run report**.

### Customers by number of orders
To run this report go to **Reports → Customer reports → Customers by number of orders**.
This report displays top 20 customers based on the total number of orders issued.

![Customers by number of orders](_static/reports/Customers-by-number-of-orders.png)

Enter one or several search criteria to compile a report:

* Registration **Start date**.
* Registration **End date**.
* **Order status**, such as *All*, *Pending*, *Processing*, *Complete*, or *Cancelled*.
* **Payment status**, such as *All*, *Pending*, *Authorized*, *Paid*, *Refunded*, *Partially refunded*, or *Voided*.
* **Shipping Status** as *All*, *Shipping not required*, *Not yet shipped*, *Partially shipped*, *Shipped*, *Delivered*.

Then click **Run report**.


## Low stock report

The low stock report contains a list of products that are currently under stock. In the example shown below, the min stock quantity was set to 20 and the stock quantity is 0, therefore a low stock report is generated for this product. You can set up low stock settings when adding the product.

To view low stock reports, go to **Reports → Low stock**. The *Low stock* report window is displayed, as follows: 
![Low stock report](_static/reports/low-stock-reports.png)

The low stock reports could be filtered by the **Published** property, which represents the *Published* property of products.

In the displayed table, click **View** to view the product details page where the stock quantity can be updated.


## Bestsellers and never purchased

Knowing the bestselling products and products never purchased is very important for any shop owner. 

First of all, this can help in making better purchasing decisions: you can scale up on your popular items and exclude unpopular ones from your products list. When analyzing, consider, for example, whether certain colors sell faster, or whether your product sales depend on a season. 

Secondly, defining most and least selling goods can help you to *re-evaluate product design and marketing*. Maybe your best items go faster just because of their placement in your web store, or because of a better description. Come up with different options and test them. To do it more effectively, engage with your customers. *Conduct different surveys* to find out why the best selling items are preferred, what makes them special for your buyers. Use the insights to improve your marketing and increase sales.

### Bestsellers
To view bestsellers in nopCommerce, go to **Reports → Bestsellers**. Enter one or more of the following search criteria to run the report:
* **Start date** and/or **End date**.
* **Store**, if you want to select one of your stores.
* **Order status**, such as *All*, *Pending*, *Processing*, *Complete*, or *Cancelled*.
* **Payment status**, such as *All*, *Pending*, *Authorized*, *Paid*, *Refunded*, *Partially refunded*, or *Voided*.
* Choose the **Category**. 
* Choose the **Manufacturer**.
* Choose the **Billing country**.
* Choose the **Vendor**.

Then click **Run report**.

The report will break down your best-selling products based on both units sold and revenue:

![Bestsellers](_static/reports/bestsellers.jpeg)

### Products never purchased

To view products never purchased, go to **Reports → Products never purchased**. Enter one or more of the following search criteria to run the report:
* Choose the **Category**. 
* Choose the **Manufacturer**.
* **Store**, if you want to select one of your stores.
* Choose the **Vendor**.
* **Start date** and/or **End date**.

Then click **Run report**.

![Products never purchased](_static/reports/never-purchased.jpg)


## Tutorials

* [Running reports in nopCommerce](https://www.youtube.com/watch?v=IbfoppTG9tM)
