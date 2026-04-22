---
標題: Microsoft Dynamics 365
uid: zh-Hant/developer/ms-dynamics-365/index
作者: git.DmitriyKulagin
貢獻者: git.DmitriyKulagin
---

# Microsoft Dynamics 365

[Dynamics 365](https://www.microsoft.com/en-us/dynamics-365) 是一套智慧型商業應用程式組合，旨在提供卓越的營運效率與突破性的顧客體驗，協助企業變得更靈活，並在不增加成本的情況下降低複雜度。

本節說明如何將 Dynamics 365 外掛整合到您的商店中。

請在此處取得 Dynamics 365 的官方整合方案：[這裡](https://www.nopcommerce.com/microsoft-dynamics-365)。

## Dynamics 365 外掛的功能

nopCommerce 的 Dynamics 365 外掛允許商店擁有者在 nopCommerce 商店與 Dynamics 365 之間，單向同步以下資料：

- 顧客。
- 商品。
- 訂單。

## 連接到 Microsoft Dataverse

若要將商店資料同步至 Dynamics 365，您需要連接到 Dataverse 環境。這需要該環境的 URL 以及具備該環境存取權限的使用者帳號憑證。若要連接到 Microsoft Dataverse，您可以使用一般使用者的憑證，或是透過 Microsoft Entra ID 建立應用程式使用者（App User）。

使用一般使用者的憑證並非推薦的做法，因為這通常需要付費授權。

為了克服此限制，您可以建立一個綁定到已註冊 Microsoft Entra ID 應用程式的特殊應用程式使用者，並使用為該應用程式配置的金鑰密鑰（Key Secret）。此方法的另一個優點是不需要付費授權。

當您建立使用 Dataverse Web API 的用戶端應用程式時，您需要進行身分驗證才能取得資料存取權。OAuth 是首選的驗證方式，因為它提供了對所有 Web API 的存取權限。

> [!NOTE]
>
> 用戶端應用程式必須支援使用 OAuth 來透過 Web API 存取資料。
> OAuth 需要身分識別提供者來進行驗證。對於 Dataverse 而言，身分識別提供者即為 Microsoft Entra ID。

### 以應用程式身分連接的需求

若要以應用程式身分連接，您需要：

- 已註冊的應用程式
- 綁定到該註冊應用程式的 Dataverse 使用者
- 使用應用程式密鑰進行連接

## 應用程式註冊

當您使用 OAuth 連接時，必須先在您的 Microsoft Entra ID 租戶（Tenant）中註冊應用程式。

應用程式若要向 Dataverse 進行身分驗證並取得商業資料的存取權，必須先在 Microsoft Entra ID 中註冊。該應用程式註冊會在驗證過程中被使用。

建立用戶端應用程式有助於驗證與授權，您可以據此授予存取權限。

### 建立應用程式的步驟

註冊應用程式會在您的應用程式與 Microsoft 身分識別平台之間建立信任關係。此信任是單向的：您的應用程式信任 Microsoft 身分識別平台，反之則不然。一旦建立，應用程式物件便無法在不同租戶之間移動。

1. 登入 [Microsoft Entra 系統管理中心](https://entra.microsoft.com/)。
1. 瀏覽至 **身分識別 > 應用程式 > 應用程式註冊**，然後選擇 **新註冊**。
1. 輸入應用程式的顯示 **名稱**。
1. 指定誰可以使用該應用程式 - 選擇「任何組織目錄中的帳戶」。
1. **重新導向 URI (選用)** 無需輸入任何內容。您將在下一個章節配置重新導向 URI。
1. 選擇 **註冊** 以完成初始應用程式註冊。

    ![image](./_static/register_app.png)

    註冊完成後，Microsoft Entra 系統管理中心會顯示應用程式註冊的 **概觀** 窗格。您會看到 **應用程式 (用戶端) ID**。此值也稱為 *用戶端 ID*，用於在 Microsoft 身分識別平台中唯一識別您的應用程式。

    ![image](./_static/app_overview.png)

### 配置應用程式

1. 在 **概觀** 頁面的 **基本資訊** 下，選擇 **新增重新導向 URI** 連結。透過先選擇 **新增平台**、輸入 URI 值，然後選擇 **配置** 來設定重新導向 URI。請使用 `http://localhost` 作為 URI 值。
1. 在您剛建立的應用程式的 **概觀** 頁面上，將游標懸停在 **應用程式 (用戶端) ID** 值上，然後選擇複製圖示將 ID 值複製到剪貼簿。請將此值記錄下來，稍後會用到。
1. 新增憑證。憑證允許應用程式以自身身分進行驗證，執行時無需使用者互動。

    ![image](./_static/app_client_secrets.png)

    - 在 Microsoft Entra 系統管理中心中，進入 **應用程式註冊**，然後選擇您的應用程式。
    - 選擇 **憑證與密鑰 > 用戶端密鑰 > 新增用戶端密鑰**。
    - 為您的用戶端密鑰新增說明。
    - 選擇密鑰的過期時間或指定自訂有效期。
        - 用戶端密鑰的有效期限制為兩年（24 個月）或更短。您無法指定超過 24 個月的自訂有效期。
        - Microsoft 建議您設定少於 12 個月的過期值。
    - 選擇 **新增**。

    > [!NOTE]
    >
    > 請記錄密鑰的值，以供用戶端應用程式程式碼使用。
    > 離開此頁面後，將無法再查看此密鑰值。

1. 然後切換到 **管理 > 資訊清單**，您可以在此看到許多屬性。我們在此關注的是 **allowPublicClient**。將 *allowPublicClient* 設定為 *true* 並 **儲存**。

    ![image](./_static/manifest.png)

1. 最後，在 **管理 > API 權限 > 新增權限** 中，搜尋 **Dynamics CRM**，選取 **user_impersonation** 並新增。

    ![image](./_static/api_permission.png)

1. 同時，為您的組織授予系統管理員同意，因為若沒有管理員同意，連接可能會引發錯誤。

    ![image](./_static/request_api_permission.png)

### 建立新的應用程式使用者

請按照下列步驟建立應用程式使用者並將其綁定到您的應用程式註冊。

1. 使用與您的應用程式註冊相同租戶中的帳號，登入 [Power Platform 系統管理中心](https://admin.powerplatform.microsoft.com/)。
1. 在左側導覽窗格中選擇 **環境**，然後在清單中選取目標環境以顯示環境資訊。
1. 選擇頁面右側的 **S2S** 連結。

    ![image](./_static/power_platform_admin_center_environments.png)

1. 選擇 **新應用程式使用者**。
1. 在「建立新應用程式使用者」的側邊欄中，選擇 **+ 新增應用程式**。
1. 在搜尋欄位中輸入您的應用程式註冊名稱，然後在結果清單中選取（勾選）它。接下來，選擇 **新增**。

    ![image](./_static/environment_app_user.png)

1. 回到 **建立新應用程式使用者** 側邊欄，從下拉式選單中選擇目標 **業務單位**，並為該應用程式使用者（也稱為服務主體）新增安全性角色。
1. 選擇 **儲存**，然後 **建立**。您應該會在顯示的應用程式使用者清單中看到您剛建立的新應用程式使用者。

    ![image](./_static/environment_business_unit.png)

1. 最後，系統會跳出通知，確認 Power Apps 已成功與我們的用戶端應用程式連結。

![image](./_static/environment_app_user_successfull.png)

## Dynamics 365 Sales 與 Business Central 整合設定

由 nopCommerce 提供的 Microsoft Dynamics 365 外掛支援與兩種主要的 Dynamics 365 應用程式同步：Business Central 與 Sales。
Dynamics 365 Business Central 是用於管理財務、營運與庫存的完整 ERP 解決方案。Dynamics 365 Sales 是提供銷售流程自動化、顧客行為分析與銷售績效追蹤的 CRM 解決方案。

![image](./_static/sales_hub.png)

### 連接 Microsoft Dataverse 與 Dynamics 365 Business Central

與 Business Central 的整合是透過 Dataverse 進行，整合方案提供了許多預設設定與資料表。

在 Business Central 中選擇 **設定 -> 進階設定**。

![image](./_static/advanced_settings.png)

> [!NOTE]
>
> 在 Business Central 中選擇 **設定 -> 輔助設定**。
> 您也可以找到「連接到其他系統」設定群組，以了解更多整合選項。
> ![image](./_static/assisted_setup.png)

與 Business Central 的整合是透過 Dataverse 進行，整合方案提供了許多預設設定與資料表。
因此，您必須先配置與 Dataverse 的連接。

> [!WARNING]
> 請勿嘗試先連接到 *Dynamics 365 Sales*。

![image](./_static/dataverse_connection_setup.png)

選擇 **下一步**。

![image](./_static/dataverse_connection_setup_1.png)

點擊「安裝 Business Central 虛擬資料表」。

![image](./_static/dataverse_connection_setup_2.png)

選取您的 Dynamics 365 環境 URL，然後選擇 **確定**。

使用管理員使用者帳號登入，並授予將用於連接 Dataverse 的應用程式權限。

選擇 **以管理員使用者登入**。

![image](./_static/dataverse_connection_setup_3.png)

您可以在 [Power Platform 系統管理中心](https://admin.powerplatform.microsoft.com/environments) 查看此連結。

在 *以管理員使用者登入* 變為綠色且粗體後，選擇 **下一步**。

![image](./_static/dataverse_connection_setup_3_1.png)

選擇擁有權模型。推薦使用 **團隊**。

![image](./_static/dataverse_connection_setup_4.png)

選擇 **完成** 以完成設定。

![image](./_static/dataverse_connection_setup_5.png)

您可以嘗試測試與 Dataverse 的連接。前往 **Dataverse 連接設定** 頁面檢查您的設定。

![image](./_static/dataverse_connection_setup_6.png)

現在您可以在 Business Central 中開啟 Dataverse 頁面。

![image](./_static/dataverse_connection_setup_7.png)

### 連接到 Dynamics 365 Sales

一旦 Dataverse 整合設定完成，您就可以開始進行與 Dynamics 365 Sales 的整合。

在 **輔助設定** 頁面上選擇 **設定與 Dynamics 365 Sales 的連接**。

![image](./_static/sales_connection_setup_1.png)

所有整合設定步驟都與 Dataverse 的整合類似。您需要指定與上次相同的環境。

結果，如果一切順利且無錯誤，您將會看到這兩個連接皆已配置並正常運作。

![image](./_static/sales_connection_setup_2.png)

![image](./_static/sales_connection_setup_3.png)

現在您可以在 Business Central 中開啟 Dynamics 365 Sales 頁面。

![image](./_static/sales_connection_setup_4.png)

例如，**商品 – Microsoft Dynamics 365 Sales**：

![image](./_static/sales_connection_setup_5.png)

例如，**銷售訂單 – Microsoft Dynamics 365 Sales**：

![image](./_static/sales_connection_setup_6.png)

> [!NOTE]
>
> 為確保整合流程正常運作，請確保「所有解決方案」頁面上的設定中存在以下行：
> ![image](./_static/sales_connection_setup_7.png)
>
> 作業佇列項目將會自動建立：
> ![image](./_static/sales_connection_setup_8.png)

### 貨幣

請確保 Business Central 與 Dataverse 的貨幣相符，以避免同步錯誤。您可以透過前往下列設定來確認。

![image](./_static/currencies.png)

組織在 Dynamics 365 Sales 中的基礎貨幣只能在組織建立時設定。

![image](./_static/currencies_1.png)

## 外掛安裝

本節說明如何將 Dynamics 365 服務整合到您的商店中。

1. 在 [此處](https://www.nopcommerce.com/microsoft-dynamics-365) 購買整合方案。
1. 下載外掛壓縮檔。
1. 前往 **後台管理 > 設定 > 本地外掛**。
1. 使用「上傳外掛或佈景主題」按鈕上傳外掛壓縮檔。
1. 向下捲動外掛清單以找到剛上傳的外掛。
1. 點擊 **安裝** 按鈕來安裝外掛。

![Find the plugin](_static/plugin_list.png)

## 如何配置外掛

點擊 **配置** 按鈕。您將會看到 *配置 - Dynamics 365* 視窗：

![Find the plugin](_static/plugin_disconnected.png)

若要將 Dynamics 365 與 nopCommerce 一起使用，您首先需要依照前述說明註冊並設定您的 MS Dynamics 365 帳戶，並在外掛配置表單的欄位中輸入所有必要的設定：

- **應用程式 (用戶端) ID**。在 Azure 入口網站註冊的用戶端 ID。
- **目錄 (租戶) ID**。在 Azure 入口網站註冊的租戶 ID。
- **用戶端密鑰**。應用程式 ID 的用戶端密鑰。應用程式在請求權杖時用於證明其身分的密鑰字串。
- **環境 URL**。要連接的 Dataverse 執行個體直接 URL。
- **貨幣代碼**。顯示您商店的主要貨幣代碼。
    > [!NOTE]
    >
    > 如果您更改商店的主要貨幣，外掛設定將會在您儲存後更新。
- **啟用圖片同步**。決定是否預設同步所選商品的圖片。
- **啟用檢查 Dynamics 商品是否存在**。當此設定啟用時，同步訂單時會檢查訂單中包含的商品在 Dataverse 中是否存在對應記錄。注意：這會大幅增加流量並對效能產生負面影響。
- **啟用自動同步**。決定是否啟用自動同步。若停用，必須在此頁面上手動啟動同步。
- **自動同步週期**。設定自動同步的週期（以分鐘為單位）。

點擊 **儲存** 按鈕。

![Find the plugin](_static/plugin_connected.png)

前往 **同步** 面板，將您的 nopCommerce 顧客、商品與訂單與您的 Dynamics 365 環境進行同步。

![Find the plugin](_static/plugin_connected_sync.png)

## 連絡人同步

該外掛實現了所有現有連絡人（顧客）的初始匯入。這是當使用者剛安裝完外掛並希望將所有現有的商店連絡人匯入 Dynamics 365 時執行的動作。之後，新增與編輯顧客的操作會根據對應的事件自動執行。

![image](_static/sales_hub_contacts.png)

## 商品同步

該外掛實現了商品的匯入。支援以下兩種類型商品的同步：

- 單一商品
- 組合商品

![image](_static/sales_hub_products.png)

在外掛中追蹤了許多事件以通知 Dynamics 365 服務：

- 建立商品。
- 變更商品（增加數量、新增圖片等）。

## 訂單同步

在外掛中追蹤了許多事件以通知 Dynamics 365 服務：

- 下訂單。
- 支付訂單。
- 取消訂單。
- 完成訂單處理。
- 變更訂單狀態。

![image](_static/sales_hub_orders.png)

### 將訂單狀態變更轉移至 Dynamics 365 的情境

下圖顯示了 nopCommerce 系統與 Dynamics 365 之間訂單狀態變更的關聯。

![Find the plugin](_static/Dynamics_365_Order_status.png)

> [!NOTE]
>
> 刪除已付款狀態的訂單存在限制；這類已付款的訂單無法從 Dynamics 365 系統中刪除。