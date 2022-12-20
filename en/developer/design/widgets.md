---
title: Widgets (Designer's Guide)
uid: en/developer/design/widgets
author: git.AndreiMaz
contributors: git.exileDev, git.DmitriyKulagin
---

# Widgets (Designer's Guide)

> A widget is a stand-alone application that can be embedded into third-party sites by any user on a page. It's a small application that can be installed and executed within a web page by an end-user. (Wikipedia).

In nopCommerce, a widget plugin allows you to embed 3rd party code/application in a public store in certain areas (for example, head tag, after the body tag, left column block, and right column block).

Currently, default nopCommerce installation allows the store admin to embed few widget plugins:

1. Google Analytics
1. Nivo Slider
1. AccessiBe
1. What3words
1. Facebook Pixel

## Google Analytics Widget

Google Analytics is a free website stats tool from Google. It keeps track of statistics about the visitors and eCommerce conversion on your website. This widget block can be rendered at:

* HTML Header tag
* After `<body>` end HTML tag.

To configure Google Analytics Widget, go to `Administration → Configuration → Widgets`, click on **Configure** against **Google Analytics** and add your Google Analytics code.

## Nivo Slider

Nivo slider is a nice and clean jquery image slider for your website/homepage to display several images scrolling with unique transition effects.

By default, nopCommerce comes with Nivo slider integration as a widget (Enabled by default) which allows you to display several images scrolling automatically on your homepage.

## accessiBe

This plugin integrates with *accessiBe* - The #1 Fully Automated Web Accessibility Solution for ADA & WCAG Compliance.

## What3words

*What3words* is a proprietary geocode system designed to identify any location with a resolution of about 3 meters (9.8 ft). The system encodes geographic coordinates into three permanently fixed dictionary words. You can use this widget on checkout pages to specify shipping addresses more accurately.

## Facebook Pixel

*Facebook Pixel* is an analytics tool that tracks how users behave on the site: what pages they visit, what buttons and links they click on, what forms they fill out, and other actions. It allows you to create audiences for Facebook ad campaigns.
