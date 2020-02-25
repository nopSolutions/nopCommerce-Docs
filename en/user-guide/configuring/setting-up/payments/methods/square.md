---
title: Square
uid: en/user-guide/configuring/setting-up/payments/methods/square
author: git.AndreiMaz
contributors: git.holydk, git.exileDev, git.ivkadp
---

# Square

To configure **Square** plugin as a payment method follow these steps:

## Square Account Setup

1. Sign up for a Square Merchant account.
    - Go to [https://squareup.com/signup/](http://squ.re/nopcommerce)

        ![square_sign_up](_static/square/square_sign_up.png)
    - Provide information about yourself and your business.

        ![square_business_information](_static/square/square_business_information.png)
1. Create business location.

    - Sign in to **Square Merchant Dashboard**.
    - Go to **Account & Settings → Locations** tab.

        ![square_locations_page](_static/square/square_locations_page.png)
    - Create new location.

    > [!IMPORTANT]
    > 
    > Your merchant account must have at least one location with enabled credit card processing. Please refer to the Square customer support if you have any questions about how to set this up.

1. Create new Square application.

    - Sign in to **Square Developer Dashboard** at [https://connect.squareup.com/apps](http://squ.re/nopcommerce1) using the same login credentials as your merchant account.

        ![square_sign_in](_static/square/square_sign_in.png)
    - Click on **Create Your First Application**.

        ![square_create_app](_static/square/square_create_app.png)
    - Name your application. This name is for you to recognize the application in the developer portal and is not used by the plugin. Agree to the **Square Developer Terms of Service** and click **Create Application** at the bottom of the page.

        ![square_create_app_page](_static/square/square_create_app_page.png)
    - Now you are on the details page of the previously created application. On the **Credentials** tab click on the **Change Version** button and choose **2019-09-25**.

        ![square_app_credentials_change_api_version](_static/square/square_app_credentials_change_api_version.png)

1. To configure plugin in the NopCommerce admin panel go to **Configuration → Payment methods** → click **Configure** for **Payments.Square**.

## Production application mode

Production application mode is used to accept real payments in a live store.

- On the plugin configuration page make sure you uncheck **Use sandbox** and fill in the related fields.

    ![square_prod_configuration_page](_static/square/square_prod_configuration_page.png)
- In the **Square Developer Dashboard** go to the details page of the your previously created application:
  - On the **Credentials** tab make sure the *Application mode* setting value is **Production**.

    ![square_prod_app](_static/square/square_prod_app.png)
  - On the **Credentials** tab copy the **Application ID** and paste it into **Application ID** on the plugin configuration page.

    ![square_prod_app_credentials](_static/square/square_prod_app_credentials.png)
  - Go to **OAuth** tab. Click **Show** on the **Application Secret** field. Copy the **Application Secret** and paste it into **Application Secret** on the plugin configuration page.
  - Copy the displayed URL on the plugin configuration page. On the **OAuth** tab paste this URL into **Redirect URL**. Click **Save**.

    ![square_prod_app_oauth](_static/square/square_prod_app_oauth.png)
- Click **Save** on the plugin configuration page.

    ![square_prod_configuration_page_save](_static/square/square_prod_configuration_page_save.png)
- On the plugin configuration page click **Obtain access token**; the **Access token** field should populate.

    ![square_prod_configuration_page_get_access_token](_static/square/square_prod_configuration_page_get_access_token.png)

    > [!NOTE]
    > 
    > If for whatever reason you would like to disable an access to your accounts, simply **Revoke access tokens** from the plugin configuration page.

- Choose the previously created location. **Location** is a required parameter for payment requests.

    ![square_prod_configuration_page_select_location](_static/square/square_prod_configuration_page_select_location.png)
- Fill in the remaining fields and click **Save** to complete the configuration.

## Sandbox application mode

Sandbox application mode is used to test the Square payment configuration.

- On the plugin configuration page check **Use sandbox** and fill in the related fields

    ![square_sandbox_configuration_page](_static/square/square_sandbox_configuration_page.png)
- In the **Square Developer Dashboard** go to the details page of the your previously created application:
  - On the **Credentials** tab make sure the *Application mode* setting value is **Sandbox**.

    ![square_sandbox_app](_static/square/square_sandbox_app.png)
  - On the **Credentials** tab copy the **Sandbox Application ID** and **Sandbox Access Token** and paste it into same fields on the plugin configuration page.

    ![square_sandbox_app_credentials](_static/square/square_sandbox_app_credentials.png)
- Click **Save** on the plugin configuration page.

    ![square_sandbox_configuration_page_save](_static/square/square_sandbox_configuration_page_save.png)
- Choose the location. **Location** is a required parameter for payment requests.

    ![square_sandbox_configuration_page_select_location](_static/square/square_sandbox_configuration_page_select_location.png)

> [!NOTE]
> 
> By default, you can select the **Default Test Account** location. Learn more about using the Square sandbox environment [here](https://developer.squareup.com/docs/testing/sandbox).

- Fill in the remaining fields and click **Save** to complete the configuration.

## Multi-store support

- To configure plugin for multi-store select the required store and fill in the fields below.

    ![square_prod_configuration_page_multi_store](_static/square/square_prod_configuration_page_multi_store.png)
