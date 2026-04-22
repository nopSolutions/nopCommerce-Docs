---
標題: 概覽 (設計師指南)
uid: zh-Hant/developer/design/overview
作者: git.AndreiMaz
貢獻者: git.exileDev, git.DmitriyKulagin
---

# 概覽 (設計師指南)

## 什麼是佈景主題

佈景主題是一組屬性設定的集合，讓您可以定義網頁和控制項的外觀，並將此視覺風格一致地應用於 Web 應用程式中的各個頁面、整個 Web 應用程式，或伺服器上的所有 Web 應用程式。

佈景主題由一系列元素組成：面板 (skins)、階層式樣式表 (CSS)、圖片以及其他資源。佈景主題至少會包含面板。佈景主題定義在您網站或 Web 伺服器上的特定目錄中。

佈景主題也可以包含階層式樣式表（`.CSS` 檔案）。當您將 `.CSS` 檔案放置在佈景主題資料夾中時，該樣式表會作為佈景主題的一部分自動套用。您可以在佈景主題資料夾中使用副檔名為 `.CSS` 的檔案來定義樣式表。（來源：[msdn.microsoft.com](https://msdn.microsoft.com)）

## nopCommerce 佈景主題的定義

nopCommerce 佈景主題用於在所有頁面或整個網站上維持一致的版面配置與外觀。nopCommerce 佈景主題由多個支援檔案組成，包括用於頁面外觀的樣式表以及輔助圖片。

![location-of-themes](_static/overview/location-of-themes.png)

**nopCommerce 中佈景主題的位置**：所有佈景主題都位於 `[nopCommerce root folder]/Themes/` 下方。