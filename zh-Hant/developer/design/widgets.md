---
標題: 小部件 (設計師指南)
uid: zh-Hant/developer/design/widgets
作者: git.AndreiMaz
貢獻者: git.exileDev, git.DmitriyKulagin
---

# 小部件 (設計師指南)

> 小部件 (Widget) 是一種獨立的應用程式，使用者可以將其嵌入到第三方網站的頁面中。這是一種可以在網頁內由終端使用者安裝並執行的微型應用程式。(維基百科)。

在 nopCommerce 中，小部件外掛允許您將第三方程式碼或應用程式嵌入到前台商店的特定區域中（例如 head 標籤內、body 標籤之後、左側欄區塊以及右側欄區塊）。

目前，nopCommerce 預設安裝允許商店管理員嵌入少數幾個小部件外掛：

1. Google Analytics
1. Swiper
1. Facebook Pixel

## Google Analytics 小部件

Google Analytics 是來自 Google 的免費網站統計工具。它會追蹤您網站上的訪客統計資訊以及電子商務轉換率。此小部件區塊可渲染於：

* HTML Header 標籤
* `<body>` 結束 HTML 標籤之後。

若要設定 Google Analytics 小部件，請前往「後台管理」→「設定」→「小部件」，點擊 **Google Analytics** 對應的 **設定**，並加入您的 Google Analytics 代碼。

## Swiper

Swiper 是一個精美且簡潔的 jQuery 圖片輪播器，適用於您的網站或首頁，能夠以獨特的轉場效果顯示多張滾動圖片。

nopCommerce 預設整合了 Swiper 作為小部件（預設為啟用），讓您可以在首頁自動輪播顯示多張圖片。

## Facebook Pixel

*Facebook Pixel* 是一種分析工具，用於追蹤使用者在網站上的行為：他們造訪了哪些頁面、點擊了哪些按鈕與連結、填寫了哪些表單，以及其他各類操作。它能讓您為 Facebook 廣告活動建立目標受眾。