---
title: Settings API
uid: en/developer/tutorials/settings
author: git.AndreiMaz
contributors: git.exileDev
---

# Settings API

Like any other website platforms nopCommerce has settings such as "Store name" or "One page checkout enabled". There are two ways to manage settings in nopCommerce.

You can use **GetSettingByKey** and **SetSetting** methods of **ISettingService** implementation for loading and saving individual settings. The preferred approach for handling settings in nopCommerce is to create a new implementation of the **ISettings** interface. Each setting will be represented by a C# property and developers should rely on setting classes to be injected via the constructor when they are required. Below is an example settings class.

```csharp
public class MediaSettings : ISettings
{
    public int AvatarPictureSize { get; set; }
    public int ProductThumbPictureSize { get; set; }
    public int ProductDetailsPictureSize { get; set; }
    public int ProductThumbPictureSizeOnProductDetailsPage { get; set; }
    public int ProductVariantPictureSize { get; set; }
    public int CategoryThumbPictureSize { get; set; }
    public int ManufacturerThumbPictureSize { get; set; }
    public int CartThumbPictureSize { get; set; }
    public bool DefaultPictureZoomEnabled { get; set; }
    public int MaximumImageSize { get; set; }
}
```
