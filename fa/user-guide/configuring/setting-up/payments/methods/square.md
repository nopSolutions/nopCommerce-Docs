---
title: Square
uid: fa/user-guide/configuring/setting-up/payments/methods/square
---

# Square

To configure **Square** plugin as a payment method follow these steps:

1. Sign up for a Square merchant account
    
    * Go to <https://squareup.com/signup/>
        
        ![SquareSignUp1](_static/square/squareSignUp1.png)

*     Provide information about yourself and your business.
        
          ![SquareSignUp2](_static/square/squareSignUp2.png)
        

1. Create business location

* Sign in to Square merchant portal.
*     In the Square merchant admin go to **Account & Settings → Locations** tab.
        
          ![SquareSignUp3](_static/square/squareSignUp3.png)
        

*     Create new location. Your merchant account must have at least one location with enabled credit card processing. Please refer to the Square customer support if you have any questions about how to set this up.
        

1. Create new Square application

*     Sign in to Square developer portal at [https://connect.squareup.com/apps](https://connect.squareup.com/apps) using the same sign in credentials as your merchant account.
        
          ![SquareSignUp4](_static/square/squareSignUp4.png)
        

*     Click on **+New Application** and fill in the application name. This name is for you to recognize the application in the developer portal and is not used by the plugin. Click **Create Application** at the bottom of the page.
        
          ![SquareSignUp5](_static/square/squareSignUp5.png)
        

1. To configure plugin in the nopCommerce admin panel go to **Configuration → Payment methods** → click **Configure** for **Payments.Square.**
    
    ![Squareplugin1](_static/square/Squareplugin1.png)

*     In the Square developer admin go to **Credentials** tab. Copy the **Application ID** and paste it into **Application ID** on the plugin configuration page.
        
          ![Squareplugin2](_static/square/Squareplugin2.png)
        

*     In the Square developer admin go to **OAuth** tab. Click **Show Secret.** Copy the **Application Secret** and paste it into **Application Secret** on the plugin configuration page. Click **Save** on this page.
        
    
    ![Squareplugin3](_static/square/Squareplugin3.png)

*     Copy the displayed URL on the plugin configuration page. Go to the Square developer admin, go to **OAuth** tab, and paste this URL into **Redirect URL.** Click **Save.**
        
          ![Squareplugin4](_static/square/Squareplugin4.png)
        

*     On the plugin configuration page click **Obtain access token** below; the **Access token** field should populate. Click **Save.**
        

* Choose the previously created business location. Location is a required parameter for payment requests.
*     Fill in the remaining fields and click **Save** to complete the configuration.
        
    
    If for whatever reason you would like to disable an access to your accounts, simply revoke access tokens from the plugin configuration page.
    
    ![Squareplugin5](_static/square/Squareplugin5.png)