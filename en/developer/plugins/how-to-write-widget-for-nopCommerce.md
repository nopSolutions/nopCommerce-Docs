---
title: How to write a widget for nopCommerce
uid: en/developer/plugins/how-to-write-widget-for-nopCommerce
author: git.nopsg
contributors: git.nopsg, git.DmitriyKulagin
---

# How to write a widget for nopCommerce

To extend nopCommerce functionality, widgets are used. There are various types of widgets like [NivoSlider](https://github.com/nopSolutions/nopCommerce/tree/master/src/Plugins/Nop.Plugin.Widgets.NivoSlider) and [Google Analytics](https://github.com/nopSolutions/nopCommerce/tree/master/src/Plugins/Nop.Plugin.Widgets.GoogleAnalytics) which are already contained in the nopCommerce's repo. nopCommerce marketplace already contains various widgets (both free and paid) which may meet your requirement(s). If you have not found one, then you are at the right place because this article will guide you through the process of creating widget according to your need.

## The widget structure, required files, and locations

1. Start by creating a new **Class Library** project in the solution. It is recommended to place your widget in the **Plugins** directory, located in the root folder of the source, where other widgets and plugins already resides.

    ![image1](_static/how-to-write-a-widget-for-nopCommerce/image1.png)

    > [!NOTE]
    > Do not get confused this directory with the one which exists in `Presentation\Nop.Web` directory. The Plugins directory in the Nop.Web directory contains the plugins compiled files.

    A recommended name for a widget project is `Nop.Plugin.Widgets.{Name}`. `{Name}` is your widget name (for example, "**GoogleAnalytics**"). For example, *Google Analytics widget* has the following name: `Nop.Plugin.Widgets.GoogleAnalytics`. But please note that it's not a requirement. And you can choose any name for a widget. For example, "*MyFirstNopWidget*". The Plugins directory structure of a solution looks like following.

    ![image2](_static/how-to-write-a-widget-for-nopCommerce/image2.png)

1. Once the widget project is created, the **.csproj** file content should be updated using any available text editor application. Replace the content with following one:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        <PropertyGroup>
            <TargetFramework>netcoreapp3.1</TargetFramework>
            <Copyright>SOME_COPYRIGHT</Copyright>
            <Company>YOUR_COMPANY</Company>
            <Authors>SOME_AUTHORS</Authors>
            <PackageLicenseUrl>PACKAGE_LICENSE_URL</PackageLicenseUrl>
            <PackageProjectUrl>PACKAGE_PROJECT_URL</PackageProjectUrl>
            <RepositoryUrl>REPOSITORY_URL</RepositoryUrl>
            <RepositoryType>Git</RepositoryType>
            <OutputPath>..\..\Presentation\Nop.Web\Plugins\WIDGET_OUTPUT_DIRECTORY</OutputPath>
            <OutDir>$(OutputPath)</OutDir>
            <!--Set this parameter to true to get the dlls copied from the NuGet cache to the output of your    project. You need to set this parameter to true if your plugin has a nuget package to ensure that   the dlls copied from the NuGet cache to the output of your project-->
            <CopyLocalLockFileAssemblies>true</CopyLocalLockFileAssemblies>
        </PropertyGroup>
        <ItemGroup>
            <ProjectReference Include="..\..\Presentation\Nop.Web.Framework\Nop.Web.Framework.csproj" />
            <ClearPluginAssemblies Include="$(MSBuildProjectDirectory)\..\..\Build\ClearPluginAssemblies.csproj" />
        </ItemGroup>
        <!-- This target execute after "Build" target -->
        <Target Name="NopTarget" AfterTargets="Build">
            <!-- Delete unnecessary libraries from plugins path -->
            <MSBuild Projects="@(ClearPluginAssemblies)" Properties="PluginPath=$(MSBuildProjectDirectory)\ $(OutDir)" Targets="NopClear" />
        </Target>
    </Project>
    ```

    > [!NOTE]
    > The **WIDGET_OUTPUT_DIRECTORY** should be replace by the plugin name, for example, Widgets.GoogleAnalytics.

1. After updating the .csproj file, **plugin.json** file should be added which is required for widget.  This file contains meta information describing your widget. Just copy this file from any other existing plugin/widget and modify it for your needs. For example, **Google Analytics** widget has the following *plugin.json* file:

    ```json
    {
        "Group": "Widgets",
        "FriendlyName": "Google Analytics ",
        "SystemName": "Widgets.GoogleAnalytics",
        "Version": "1.62",
        "SupportedVersions": [ "4.30" ],
        "Author": "nopCommerce team, Nicolas Muniere",
        "DisplayOrder": 1,
        "FileName": "Nop.Plugin.Widgets.GoogleAnalytics.dll",
        "Description": "This plugin integrates with Google Analytics. It keeps track of statistics about the visitors and eCommerce conversion on your website"
    }
    ```

    Actually all fields are self-descriptive, but here are some notes:

      - **SystemName** field should be unique.
      - **Version** field is a version number of your widget. You can set it to any value you like.
      - **SupportedVersions** field can contain a list of supported nopCommerce versions separated bycommas (ensure that the current version of nopCommerce is included in this list, otherwise, it willnot be loaded).
      - **FileName** field has the following format `Nop.Plugin.Widgets.{Name}.dll` (it is your plugin assembly filename). Ensure that **Copy to Output Directory** property of this file is set to **Copy if newer**.

    ![image3](_static/how-to-write-a-widget-for-nopCommerce/image3.png)

    The last required step is to create a class which implements **BasePlugin** (*Nop.Core.Plugins* namespace) and **IWidgetPlugin** interface (*Nop.Services.Cms* namespace). IWidgetPlugin allows you to create widgets. Widgets are rendered on some parts of your site. For example, it can be a live chat block on the bottom right of your site.

## Handling requests. Controllers, models and views

Now you can see the widget by going to **Admin area** → **Configuration** → **Local Plugins**.

![image4](_static/how-to-write-a-widget-for-nopCommerce/image4.png)

When a plugin/widget is installed, you will see the **Uninstall** button. *For performance improvement, it is a good practice that you uninstall plugins/widgets which are not necessary*.

![image5](_static/how-to-write-a-widget-for-nopCommerce/image5.png)
There will be **Install** and **Delete** button when a plugin/widget is not installed or uninstalled. *Deleting will remove physical files from the server*.

But as you guessed our widget does nothing. It does not even have a user interface for its configuration. Let's create a page to configure the widget.

What we need to do now is create a controller, a model, a view and a view component.

- **MVC controllers** are responsible for responding to requests made against an *ASP.NET MVC* website. Each browser request is mapped to a particular controller.
- A view contains the **HTML** markup and content that is sent to the browser. A view is the equivalent of a page when working with an *ASP.NET MVC* application.
- A view component which implements **NopViewComponent** which contains logic and codes to render a view.
- The **MVC model** contains all of your application logic that is not contained in a view or a controller.

So let's start:

1. Create the model. Add a `Models` folder in the new widget, and then add a new model class which fits your need.

1. Create the view. Add a `Views` folder in the new widget, and then add a `cshtml` file named `Configure.cshtml`. Set "**Build Action**" property of the view file is set to "**Content**", and the "**Copy to Output Directory**" property is set to "**Copy always**". Note that configuration page should use "**_ConfigurePlugin**" layout.

    ```cs
    @{
        Layout = "_ConfigurePlugin";
    }
    ```

1. Also make sure that you have **_ViewImports.cshtml** file into your `Views` directory. You can just copy it from any other existing plugin or widget.

    ![image7](_static/how-to-write-a-widget-for-nopCommerce/image7.png)

1. Create the controller. Add a `Controllers` folder in the new widget, and then add a new controller class. A good practice is to name plugin controllers `Widgets{Name}Controller.cs`. For example, **WidgetsGoogleAnalyticsController**. Of course it's not a requirement to name controllers this way, but just a recommendation. Then create an appropriate action method for configuration page (in admin area). Let's name it "`Configure`". Prepare a model class and pass it to the following view using a physical view path: `~/Plugins/{PluginOutputDirectory}/Views/Configure.cshtml`.

    ```cs
    public IActionResult Configure()
    {
        if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
            return AccessDeniedView();

        //load settings for a chosen store scope
        var storeScope = _storeContext.ActiveStoreScopeConfiguration;
        var myWidgetSettings = _settingService.LoadSetting<MyWidgetSettings>(storeScope);

        var model = new ConfigurationModel
        {
            // configuration model settings here
        };

        if (storeScope > 0)
        {
            // override settings here based on store scope
        }

        return View("~/Plugins/Widgets.MyFirstNopWidget/Views/Configure.cshtml", model);
    }
    ```

1. Use the following attributes for your action method:

    ```cs
    [AuthorizeAdmin] //confirms access to the admin panel
    [Area(AreaNames.Admin)] //specifies the area containing a controller or action
    [AdminAntiForgery] //Helps prevent malicious scripts from submitting forged page requests.
    ```

    For example, open `GoogleAnalytics` widget and look at its implementation of `WidgetsGoogleAnalyticsController`.
    Then for each widget which has a configuration page you should specify a configuration url. Base class named **BasePlugin** has `GetConfigurationPageUrl` method which returns a configuration url:

    ```cs
    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/{CONTROLLER_NAME}/{ACTION_NAME}";
    }
    ```

    Where `{CONTROLLER_NAME}` is a name of your controller and `{ACTION_NAME}` is a name of action (usually it's "Configure").
    Each widget should specify a list of widget zones. Base class named **IWidgetPlugin** has `GetWidgetZones` method which returns a list of widget zones where it will be rendered.

    ```cs
    public IList<string> GetWidgetZones()
    {
        return new List<string> { PublicWidgetZones.HeadHtmlTag };
    }
    ```

    You can find a list of public widget zones from this [link](https://github.com/nopSolutions/nopCommerce/blob/master/src/Presentation/Nop.Web.Framework/Infrastructure/PublicWidgetZones.cs) and admin widget zones following this [link](https://github.com/nopSolutions/nopCommerce/blob/master/src/Presentation/Nop.Web.Framework/Infrastructure/AdminWidgetZones.cs).
    In addition to `GetWidgetZones`, **IWidgetPlugin** has `GetWidgetViewComponentName` method which returns ViewComponent name. It accepts "*widgetZone*" name as parameter and can be used to render different view based on the selected widget zone.

    ```cs
    public string GetWidgetViewComponentName(string widgetZone)
    {
        return "MyFirstWidget";
    }
    ```

## Project structure of Google Analytics widget

![image11](_static/how-to-write-a-widget-for-nopCommerce/image11.png)

## Handling "Install" and "Uninstall" methods

This step is optional. Some widgets can require additional logic during its installation. For example, a widget can insert new locale resources or settings values. So open your **IWidgetPlugin** implementation (in most case it'll be derived from **BasePlugin** class) and override the following methods:

1. **Install**. This method will be invoked during plugin installation. You can initialize any settings here, insert new locale resources, or create some new database tables (if required).

    ```cs
    public override void Install()
    {
        // custom logic like adding settings, locale resources and database table(s) here

        base.Install();
    }
    ```

1. **Uninstall**. This method will be invoked during plugin uninstallation. You can remove previously initialized settings, locale resources, or database tables by widget during installation.

    ```cs
    public override void Uninstall()
    {
        // custom logic like removing settings, locale resources and database table(s) which was created during widget installation

        base.Uninstall();
    }
    ```

    > [!IMPORTANT]
    > If you override one of these methods, do not hide its base implementation - **base.Install()** and **base.Uninstall()** which has been marked in the above images.
