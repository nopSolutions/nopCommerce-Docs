---
title: Manufacturers
author: AndreiMaz
uid: user-guide/running/product-management/manufacturers
---
# Manufacturers

To manage manufacturers go to **Catalog → Manufacturers.**

![](/user-guide/running/_static/manufactures.png)

**Search** for a manufacturer in the Manufacturers window by **entering the Manufacturer name** (or a part of the name), or among all the manufacturers of a certain Store.

Click the **Edit** button to edit the manufacturer’s details.

### Adding new manufacturers
![](/user-guide/running/_static/add_a_new_manufacturer.png)
In the **Manufacturer Info panel**, define the following **details**:

- **Name**.
- **Description** - description of the manufacturer. Use the editor for layout and fonts.
- **Picture** - an image, or a logo, representing the manufacturer. Upload the image from your device.

### Display
![](/user-guide/running/_static/display2.png)
In the **Manufacturer Display panel**, define the following **details**:

- Select the **Published** checkbox, to enable the manufacturer to be visible in the public store.
- Select the **Allow customers to select page size** checkbox, to enable customers to select a page size, i.e. the number of products displayed on the Manufacturer Details page. The page size can be selected by customers from the page sizes list entered by the store owner in the Page size options field.
- When this option is disabled, customers will not be able to select a page size on the Manufacturer Details page and the store owner enters a certain page size. In this case, the Page size field becomes visible in the Administration area.

> [!TIP] 
> For example, when you add seven products to a manufacturer and you set its page size to three. Three products per page will be displayed on this manufacturer details page in the public store, and the total amount of pages will be three.

- **Price ranges** - allow defining ranges of price by which customers can filter the manufacturers. Enter a price range in the currency that you defined in the Currencies window. Separate the ranges by a semicolon, for example, 0-999; 1000-1200; 1201 - (1201 means 1201 and over).
- **Display Order** - the order number for displaying the manufacturer. This display number is used to sort manufacturers in the public store (ascending). The manufacturer with the display order 1 will be placed at the top of the list.

### Mappings
![](/user-guide/running/_static/mappings.png)
In the **Manufacturer Mappings panel**, define the following **details**:

- **Discounts** - select all discounts associated with this manufacturer. Discounts can be created in the [Promotions](xref:/user-guide/marketing/promotional/index) menu. Note that only discounts with Assigned to categories type are visible here. After discounts are mapped to the manufacturer, they are applied to all products of this manufacturer.
- **Limited to customer roles** option allows showing this manufacturer only to selected customer roles. Choose the required customer roles from the list that can be created/edited on the [Customer roles](xref:/user-guide/configuring/settingup/customers/customer-roles) page of the Customers menu. If you want the manufacturer to be visible to all - leave the field empty.
- Select the **Limited to stores** option to make this manufacturer limited to one or more stores. Note that this checkbox is only used when you have several stores configured. For further details refer to [Multi-store support](xref:/user-guide/configuring/settingup/mainstore/multiple-store).
