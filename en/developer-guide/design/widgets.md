---
title: Widgets (Designer's Guide)
uid: en/developer/design/widgets
author: git.AndreiMaz
contributors: git.exileDev, git.DmitriyKulagin
---

# Widgets (Designer's Guide)

> A widget is a stand-alone application that can be embedded into third party sites by any user on a page. It's a small application that can be installed and executed within a web page by an end user. (Wikipedia).

In nopCommerce, a widget plugin allows you to embed 3rd party code/application in public store in certain areas (for example, head tag, after body tag, left column block and right column block).

Currently, default nopCommerce installation allows the store admin to embed two widget plugins:

1. Google Analytics
1. Nivo Slider

## Google Analytics Widget

Google Analytics is a free website stats tool from Google. It keeps track of statistics about the visitors and e-commerce conversion on your website. This widget block can be rendered at:

* HTML Header tag
* After `<body>` end HTML tag.

To configure Google Analytics Widget, go to `Administration → Configuration → Widgets`, click on **Configure** against **Google Analytics** and add your Google Analytics code.

## Nivo Slider

Nivo slider is a nice and clean jquery image slider for your website/homepage to display a number of images scrolling with unique transition effects.

By default, nopCommerce comes with nivo slider integration as a widget (Enabled by default) which allows you to display a number of images scrolling automatically on your homepage.
