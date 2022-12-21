---
title: Settings API
uid: en/developer/tutorials/settings
author: git.AndreiMaz
contributors: git.exileDev, git.DmitriyKulagin
---

# Settings API

Like any other website platform, nopCommerce has settings such as "Store name" or "One page checkout enabled". There are two ways to manage settings in nopCommerce.

You can use **GetSettingByKeyAsync** and **SetSettingAsync** methods of **ISettingService** implementation for loading and saving individual settings.

```csharp
var setting = await GetSettingByKeyAsync<string>(key);
...
await _settingService.SetSettingAsync(key, value);
```

 The preferred approach for handling settings in nopCommerce is to create a new implementation of the **ISettings** interface. Each setting will be represented by a C# property and developers should rely on setting classes to be injected via the constructor when they are required. Below is an example settings class.

```csharp
public partial class MediaSettings : ISettings
    {
        public int AvatarPictureSize { get; set; }
        public int ProductThumbPictureSize { get; set; }
        public int ProductDetailsPictureSize { get; set; }
        public int ProductThumbPictureSizeOnProductDetailsPage { get; set; }
        public int AssociatedProductPictureSize { get; set; }
        public int CategoryThumbPictureSize { get; set; }
        public int ManufacturerThumbPictureSize { get; set; }
        public int VendorThumbPictureSize { get; set; }
        public int CartThumbPictureSize { get; set; }
        public int OrderThumbPictureSize { get; set; }
        public int MiniCartThumbPictureSize { get; set; }
        public int AutoCompleteSearchThumbPictureSize { get; set; }
        public int ImageSquarePictureSize { get; set; }
        public bool DefaultPictureZoomEnabled { get; set; }
        public bool AllowSVGUploads { get; set; }
        public int MaximumImageSize { get; set; }
        public int DefaultImageQuality { get; set; }
        public bool MultipleThumbDirectories { get; set; }
        public bool ImportProductImagesUsingHash { get; set; }
        public string AzureCacheControlHeader { get; set; }
        public bool UseAbsoluteImagePath { get; set; }
        public string VideoIframeAllow { get; set; }
        public int VideoIframeWidth { get; set; }
        public int VideoIframeHeight { get; set; }
        public int ProductDefaultImageId { get; set; }
    }
```
