---
title: Countries, states
uid: en/getting-started/configure-shipping/advanced-configuration/countries-states
author: git.AndreiMaz
contributors: git.rajupaladiya, git.DmitriyKulagin, git.exileDev, git.ivkadp, git.mariannk
---

# Countries, states

This section describes how to manage countries (where your customers are located) and states.

To define the countries and states, go to **Configuration → Countries**.

![countries](_static/countries-states/countries1_1.png)

> [!TIP]
>
> By default, all the countries are uploaded. You can *publish* and *unpublish* countries by selecting them and clicking the corresponding buttons at the top of the page.

## Add a new country

To add a new country, click **Add new**.

![addcountry](_static/countries-states/addcountry.png)

In the *country info* panel, define the following country settings:

* The **Name** of the country.
* **Allows billing** to customers located in this country.
* **Allows shipping** to customers located in this country.
* Enter **Two letter ISO** code of this country.
* Enter **Three letter ISO** code of this country.
* Enter **Numeric ISO code** of this country.
* Select the **Subject to VAT** checkbox to indicate whether customers in this country are charged the EU VAT (the European Union value added tax).

> [!NOTE]
>
> This field is only used when the EU VAT option is enabled on the tax settings page (Configuration → Settings → Tax settings).

* To tie a certain store to a country, select the required stores in the **Limited to stores** field, as follows:

> [!NOTE]
>
> This list is only used when you have several stores configured. For further details, refer to [Multi-store](xref:en/getting-started/advanced-configuration/multi-store).

* Select the **Published** checkbox to enable this country to be visible for new account registrations and creation of shipping and billing addresses.
* Enter the **Display order** of this country. A value of 1 represents the top of the list.

Click **Save**.

## Adding new states and provinces

In the *State and provinces* panel, you can add states and provinces of the country.

> [!TIP]
>
> By default, the states of the USA are added.

![addcountry2](_static/countries-states/states.jpg)

Click the **Add a new state/province** button at the bottom of the panel to add a new state or province.

![countries3](_static/countries-states/countries3.png)

Define the following state/province details:

* **Name** of the state or province.
* **Abbreviation** for the province or state.
* Select the **Published** checkbox to publish the state or province on the website.
* In the **Display order** field, enter the display order of this province or state. A value of 1 represents the top of the list.

Click **Save**

## Export/import states

You can export a list of states of all the countries that were added to the system or import the additional ones using the corresponding buttons at the top of the **Configuration → Countries** page.

![Export/import](_static/countries-states/export-import.jpg)

> [!NOTE]
>
> The imported file should have the same format as the exported one.
