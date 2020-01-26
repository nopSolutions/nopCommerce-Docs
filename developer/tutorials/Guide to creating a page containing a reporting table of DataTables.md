# Guide to creating a page containing a reporting table of DataTables #2
In this tutorial we will be learning about how to extend the functionality of the nopCommerce with custom functionality for admin panel, 
and create a page containing a table with some data as a report. So before starting on this tutorial you need to have some prior 
knowledge and understanding on some of the topics like.

* nopCommerce architecture. (https://docs.nopcommerce.com/developer/tutorials/source-code-organization.html)
* nopCommerce Plugin.
* Entity framework.
* nopCommerce routing.

If you are not familiar with the above topics, we highly recommend you to learn about those first. However, if you are comfortable or at least have some basic understanding on the above topic then you are good enough to continue on this tutorial.

So in this tutorial we will be creating a plugin with a page containing the table displaying information on the distribution of users by country (based on the billing address). Let’s go through the step by step process to create above mentioned functionality.

## Step 1: Create a nopCommerce plugin project.
I am assuming that you already know where and how to create nopcommerce plugin project and configure the project according to nopCommerce standard. If you don’t know then you can visit
https://docs.nopcommerce.com/developer/plugins/how-to-write-plugin_4.20.html link to know how to create and configure nopcommerce plugin project.

If you have followed the above provided link to create and configure your plugin project then you may end up with the folder structure like this.
_static[
