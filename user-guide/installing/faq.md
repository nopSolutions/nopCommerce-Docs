---
title: FAQ
author: AndreiMaz
uid: user-guide/installing/faq
---
# FAQ

**Q: Why choose nopCommerce?** 
**A:** [https://www.nopcommerce.com/whychoose.aspx](https://www.nopcommerce.com/whychoose.aspx)


**Q: Error Message: "We're sorry, an internal error occurred. Our supporting staff has been notified of this error and will address the issue shortly..."**

**A:** For versions 4.00 and above to find out what went wrong you need to turn off the custom errors mode.

1. Open appsettings.json file
2. Find out the following line "DisplayFullErrorStack": false,
3. Replace it with "DisplayFullErrorStack": true
4. Restart the site

Also, check Admin > System > Log for any errors.

Also, you can enable "stdoutLogEnabled" (set to "true") in your web.config. Then you'll see a detailed error at the \logs\ directory


For versions before 4.00 to find out what went wrong you need to turn off the custom errors mode.
1. Open web.config file
2. Find out the following line <customErrors defaultRedirect="errorpage.htm" mode="RemoteOnly">
3. Replace it with <customErrors defaultRedirect="errorpage.htm" mode="Off">
Also, check Admin > System > Log for any errors.

**Q: Why does my home page take so long to load?**
**A:** Large ASP.NET apps take a while to initially load into memory.  Once loaded, the ASP app pool will unload your app if it goes idle (20 minutes by default).  Check that the KeepAlive scheduled task is running (Admin > System > Scheduled Tasks) and consider using a 3rd party monitor, and keep alive. Alternatively, to see what is causing delay in loading pages you can enable Mini-Profiler available in the nopCommerce. To enable the same, You can go into the admin area > Configuration > Advanced Setting, and search for the Setting with name "DisplayMiniProfilerInPublicStore". Enable this setting. This will turn on a Mini Profiler, which will display time it took to generate page with information on which operation is taking what time. 

**Q: Users are logged out frequently**
**A:** Adding a machine key to the web.config file should fix the problem of being logged out. You can generate the machine key or read more about it [here](https://www.developerfusion.com/tools/generatemachinekey/) or [here](https://www.codeproject.com/Articles/16645/ASP-NET-machineKey-Generator)

**Q: How do I upgrade to the latest version**
**A:** Please see [Upgrading nopCommerce](xref:user-guide/installing/upgrading)

**Q: How to change texts strings in the store.**
**A:** Go to admin area > configuration > languages > "view strings resources" [of your language], with the help of filters (funnel tools) you can find and edit the texts whose value you want to change.

**Q: Getting "A generic error occurred in GDI+" error**
**A:** Give write permissions to \Content\Images\ and \Content\Images\Thumbs\ directories

**Q: How to configure Multi-Vendor feature** 
**A:** Check these links:
[https://www.nop-templates.com/whats-new-in-nopcommerce-30-part-1-multi-vendor-support](https://www.nop-templates.com/whats-new-in-nopcommerce-30-part-1-multi-vendor-support)
[https://www.nopcommerce.com/boards/t/22403/multi-vendor-roadmap-lets-discuss-update-done.aspx?p=4#92622](https://www.nopcommerce.com/boards/t/22403/multi-vendor-roadmap-lets-discuss-update-done.aspx?p=4#92622)

**Q: How to configure Multi-Store feature**
**A:** Check these links:
[https://www.nop-templates.com/whats-new-in-nopcommerce-30-part-2-multi-store-support](https://www.nop-templates.com/nopcommerce-multi-store)
[https://www.nopcommerce.com/boards/t/21356/multi-store-roadmap-lets-discuss-update-done.aspx?p=3#89679](https://www.nopcommerce.com/boards/t/21356/multi-store-roadmap-lets-discuss-update-done.aspx?p=3#89679)
[https://www.nopaccelerate.com/setup-nopcommerce-multi-store-shared-hosting-environment/](https://www.nopaccelerate.com/setup-nopcommerce-multi-store-shared-hosting-environment/)

**Q: I accidentally deleted the admin user.  How can I get back in as admin?**
**A:** If you have access to the database  (using a tool like SSMS, etc) then un-delete the user:

````SQL
update Customer
set Deleted = 0
where Id = 1
````
> [!TIP]
> <Typically the Id for Admin is 1, but if not, then find Id, or use the Username - e.g where Username = 'email@domain.com'>

**Q: How to install nopCommerce on a local machine?**
**A:** [https://www.nop-templates.com/how-to-install-nopcommerce-on-a-local-machine](https://www.nop-templates.com/how-to-install-nopcommerce-on-a-local-machine?utm_source=nopcommerce&utm_medium=forum&utm_campaign=faqs)

**Q: How to search and change resources in nopCommerce?**
**A:** [https://www.nop-templates.com/how-to-search-and-change-resources-in-nopcommerce](https://www.nop-templates.com/how-to-search-and-change-resources-in-nopcommerce?utm_source=nopcommerce&utm_medium=forum&utm_campaign=faqs)

**Q: How to set the number of products per page?**
**A:** [https://www.nop-templates.com/how-to-set-the-number-of-products-shown-per-page-in-a-category](https://www.nop-templates.com/how-to-set-the-number-of-products-shown-per-page-in-a-category?utm_source=nopcommerce&utm_medium=forum&utm_campaign=faqs)

**Q: How do I stop logging "The controller for path ... was not found or does not implement IController"?**
**A:** Change to false...
Admin Site > Configuration > Settings > All Settings >>  commonsettings.log404errors

**Q: What are the best practices to apply change sets?**
**A:** [https://www.nopcommerce.com/boards/t/11806/custom-dev-source-control-changesets-best-practices.aspx](https://www.nopcommerce.com/boards/t/11806/custom-dev-source-control-changesets-best-practices.aspx)

**Q: How to add a new custom page in nopCommerce**
**A:** Check this tutorial: [http://www.strivingprogrammers.com/Steps-to-add-a-new-custom-page-in-nopCommerce-ASP-NET-MVC-based-e-Commerce-solution](http://www.strivingprogrammers.com/Steps-to-add-a-new-custom-page-in-nopCommerce-ASP-NET-MVC-based-e-Commerce-solution)

**Q: How can I re-assign an email address if I've deleted the customer?**
**A:**  There is a [setting that will Suffix a customer email with "-DELETED" when the record is deleted](https://blog.arvixe.com/how-to-re-assign-an-email-address-if-you-have-deleted-a-customer-in-nopcommerce/)
All Settings - customersettings.suffixdeletedcustomers
Out of the box, the setting is FALSE.  If you deleted the customer prior to making the change to TRUE, then you need to manually suffix the email using a SQL tool (e.g. SSMS)

**Q: How to create a new language set?**
A: [https://www.nopcommerce.com/boards/t/25984/how-to-create-a-new-language-pack-for-310-based-on-300.aspx](https://www.nopcommerce.com/boards/t/25984/how-to-create-a-new-language-pack-for-310-based-on-300.aspx)

**Q: How do I import product pictures?**
**A:** The pictures filename path must be relative to the host file system;  not your local PC/browser. On the admin>products page, select one product that has a picture, and then do an "Export to Excel (selected)".  Look at the Excel sheet.  Note the host path for the picture.  You will need to prefix your filenames with that path when creating the CSV file.   Don't use the exact path.  You need to create a temp folder up a level or two, and put your files in that temp folder.  If hosted, you will need to FTP the image files prior to import.

**Q: No Source (Web) OR Source Code? Need help in deciding, which one to use?**
**A:** Check this tutorial: [http://www.strivingprogrammers.com/Difference-between-No-Source-Web-OR-Source-Code-of-nopCommerce](http://www.strivingprogrammers.com/Difference-between-No-Source-Web-OR-Source-Code-of-nopCommerce)

**Q: What do you for selling by sizes, colors, material, etc., specification attributes or product attributes?**
**A:** Product attributes will do it. Don't forget to set Manage inventory method: Track inventory by product attributes in product info tab and then to define stock in the product attributes>combinations tab.

**Q: Is nopCommerce PCI compliant?**
**A:** nopCommerce meets all PCI Compliance requirements, but it hasn't  try to pass any official certification.
