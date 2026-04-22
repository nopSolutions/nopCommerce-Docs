---
標題: 信用卡（人工處理）
uid: zh-Hant/getting-started/configure-payments/payment-methods/credit-card-manual-processing
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.exileDev, git.ivkadp, git.mariannk
---

# 信用卡（人工處理）

這是一個特殊的付款外掛，允許所有訂單成功輸入網站，但它不會實際向顧客扣款，也不會呼叫任何即時付款閘道。如果您想要執行下列任一操作，建議使用此付款方式：

* 線下處理所有訂單
* 透過其他後台系統手動處理訂單
* 在網站正式上線前進行端對端測試

若要設定此付款方式，請前往 **設定 → 付款方式**。然後在付款方式列表中找到 **信用卡 (Payments.Manual)** 付款方式：

![List](_static/credit-card-manual-processing/list.jpg)

## 啟用付款方式、編輯名稱與顯示順序

您可以編輯將在前台商店顯示給顧客看的付款方式名稱，或調整其顯示順序。若要這麼做，請點擊付款方式列表頁面上該外掛列的 **編輯** 按鈕。您可以輸入 **友善名稱 (Friendly name)** 與 **顯示順序 (Display order)**。在此列中，您還可以透過 **是否啟用 (Is active)** 欄位來啟用或停用此外掛。點擊 **更新** 按鈕，您的變更即會儲存。

## 設定付款方式

在 **設定 → 付款方式** 頁面上，找到 **信用卡 (Payments.Manual)** 付款方式並點擊 **設定** 按鈕。將顯示如下的 *設定 - 信用卡* 視窗：

![manualprocessing](_static/credit-card-manual-processing/manualprocessing.png)

請依照下列方式設定此付款方式：

* 在 **結帳後標記付款為** 欄位中，指定交易模式。
* 定義使用此方式的 **額外費用**。
* 在 **額外費用為百分比** 欄位中，定義是否對訂單總額套用額外百分比費用。若未啟用，則使用固定金額。

點擊 **儲存**。

## 限制商店與顧客角色

您可以將任何付款方式限制於特定商店與顧客角色。這表示該方式僅供特定的商店或顧客角色使用。您可以從 *外掛列表* 頁面進行此設定。

1. 前往 **設定 → 本地外掛**。找到您想要限制的外掛。以本例來說是 **信用卡**。若要快速找到它，請使用頁面上方的 *搜尋* 面板，並透過 *付款方式* 選項依 **外掛名稱** 或 **群組** 搜尋。

   ![Plugins](_static/credit-card-manual-processing/plugin.jpg)

1. 點擊 **編輯** 按鈕，將顯示如下的 *編輯外掛詳細資料* 視窗：

   ![Plugins](_static/credit-card-manual-processing/edit.jpg)

1. 您可以設定下列限制：

   * 在 **限制於顧客角色** 欄位中，選擇一個或多個顧客角色（例如管理員、供應商、訪客），這些角色將能使用此外掛。如果您不需要此選項，請將此欄位留空。

     > [!Important]
     > 若要使用此功能，您必須停用下列設定：**目錄設定 → 忽略 ACL 規則（全站適用）**。閱讀更多關於權限控制（ACL）的內容 [here](xref:zh-Hant/running-your-store/customer-management/access-control-list)。

   * 使用 **限制於商店** 選項將此外掛限制於特定商店。如果您有多商店，請從列表中選擇一個或多個。如果您不使用此選項，請將此欄位留空。

     > [!Important]
     > 若要使用此功能，您必須停用下列設定：**目錄設定 → 忽略「限制於商店」規則（全站適用）**。閱讀更多關於多商店功能的內容 [here](xref:zh-Hant/getting-started/advanced-configuration/multi-store)。

 點擊 **儲存**。

## 教學課程

* [設定信用卡（人工處理）付款方式](https://www.youtube.com/watch?v=dN2q27dKvUU)