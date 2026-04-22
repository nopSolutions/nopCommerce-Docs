---
標題: 自訂 HTML
uid: zh-Hant/getting-started/advanced-configuration/custom-html
作者: git.DmitriyKulagin
---

# 自訂 HTML 設定

若要管理 *自訂 HTML* 設定，請前往 **組態 → 設定 → 一般設定**。

此頁面支援多商店設定；這意味著相同的設定可以套用於所有商店，或者各商店設定不同。如果您想管理特定商店的設定，請從多商店設定下拉選單中選擇該商店名稱，並選取左側所需的核取方塊以設定自訂值。如需詳細資訊，請參閱 [多商店](xref:zh-Hant/getting-started/advanced-configuration/multi-store)。

## 自訂 HTML

雖然充分使用 nopCommerce 並不需要具備專業程式設計知識，但在某些情況下，您可能需要新增頁首 (header) 與頁尾 (footer) 程式碼。例如，您可能想要使用分析工具，這是網站檔案遭到駭客入侵的一個常見原因。

許多工具和追蹤腳本需要您將程式碼片段新增至網站的頁首或頁尾。在本項目中，我們將示範如何將程式碼新增至 nopCommerce 的頁首或頁尾 HTML 中。

標準網站會像文檔一樣，拆分為幾個不同的元件：

- 頁首 (Header)：網站的頁首包含許多「預載入」元素以及有關您的 *安全通訊端層 (SSL)* 憑證、加密、任何 JavaScript 等詳細資訊。
- 主體 (Body)。
- 頁尾 (Footer)：運作方式與頁首類似，但位於頁面底部。

伺服器會依序載入頁面——先是頁首，接著是主體，最後是頁尾。這意味著頁首程式碼會優先載入，而頁尾程式碼會在其他所有內容之後載入。

請依照下列方式定義 *自訂 HTML* 設定：
![Security](_static/custom-html/custom-html.jpg)

這將會在全域（即全站範圍）層級新增任何程式碼。