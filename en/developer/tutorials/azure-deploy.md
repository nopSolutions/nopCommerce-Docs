---
title: Deployment on Azure with Git and Automatic Builds
uid: en/developer/tutorials/azure-deploy
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Deployment on Azure with Git and Automatic Builds

## Step by step guide for automatic deployment of nopCommerce with git on Azure

1. **Setup your git-repository**

The default way to deploy nopCommerce is by using the “Publish” function in Visual Studio. But for publishing to Azure you need to have your own repository. You may use Bitbucket and keep that in sync with the official repository. Or you can [setup git on Azure](https://azure.microsoft.com/documentation/articles/web-sites-publish-source-control/). There's a great video [here](http://channel9.msdn.com/Shows/Azure-Friday/What-is-Kudu-Azure-Web-Sites-Deployment-with-David-Ebbo).

1. **Prepare for local deployment**
   
   When you ensure that the automatic build works, we are ready to customize our deployment scripts. This is needed because the default automatic build only builds `Nop.Web` project. The problem with this is that it does not build any of the plugins. You cannot add their reference to `Nop.Web' project either as they would create circular references. So, we need to get the custom build working, these are the tools we need to install:
    - Install NodeJs: [https://nodejs.org](https://nodejs.org)

    - Install Azure CLI: [https://azure.microsoft.com/documentation/articles/xplat-cli-install/](https://azure.microsoft.com/documentation/articles/xplat-cli-install/)

1. **Get NuGet to work at the command line level.**
   
   The default behavior of the KUDU script (which is a tool for generating deployment scripts for Azure App Service) is to check for NuGet packages.
   - To get access to the `Nuget.exe` file you could either download it from [here](https://docs.nuget.org/consume/command-line-reference). You can also "Enable automatic restore of NuGet packages" in your Visual Studio, and it will be added to your project automatically.

   - Ensure that NuGet is in the PATH. Copy the `nuget.exe` file to the preferred location (e.g. `c:/Program Files/Nuget/Nuget.exe`). Add it to the PATH environment variable.
   - Confirm that NuGet is in your PATH by starting `cmd.exe` and writing *nuget*. you should see the command options.

1. **Generate deployment scripts locally**
   
    - Open the "Microsoft Azure Command Prompt"
    - Navigate to the src folder of your project as you normally would in a shell window
    - Execute the kudu script generator (You may find wiki by [this link](https://github.com/projectkudu/kudu/wiki) or see [this video](https://azure.microsoft.com/resources/videos/custom-web-site-deployment-scripts-with-kudu/)).

        So you would write something like:

        `kuduscript site deploymentscript --aspNetCore Presentation\Nop.Web\Nop.Web.csproj -s NopCommerce.sln -y`
    - Verify that it has generated 2 files (in your local repository root):

        `.deployment`

        `deploy.cmd`

1. **Run generated script**
   
    - You must keep the .deployment and deploy.cmd file in the root of the git repository
    - Edit the `deploy.cmd` as the `%DEPLOYMENT_SOURCE%` variable containing the root of the git repository. So we would add `%DEPLOYMENT_SOURCE%\src\Presentation\Nop.Web\Nop.Web.csproj` instead of `%DEPLOYMENT_SOURCE%\Presentation\Nop.Web\Nop.Web.csproj`. All paths in the deployment section must be corrected.
    - Run `deploy.cmd` to see if the default deploy script works locally. It should create an `\artifact` folder just outside of your git repository.

1. **Customize the deployment script**
   
   Now we are at the final part. This is where all that work pays off. We want to alter the following piece:

    ```sh
    ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    :: Deployment
    :: ----------

    echo Handling ASP.NET Core Web Application deployment.

    :: 1. Restore nuget packages
    call :ExecuteCmd dotnet restore "%DEPLOYMENT_SOURCE%\NopCommerce.sln"
    IF !ERRORLEVEL! NEQ 0 goto error

    :: 2. Build and publish
    call :ExecuteCmd dotnet publish "%DEPLOYMENT_SOURCE%\Presentation\Nop.Web\Nop.Web.csproj" --output "%DEPLOYMENT_TEMP%" --configuration Release
    IF !ERRORLEVEL! NEQ 0 goto error

    :: 3. KuduSync
    call :ExecuteCmd "%KUDU_SYNC_CMD%" -v 50 -f "%DEPLOYMENT_TEMP%" -t "%DEPLOYMENT_TARGET%" -n "%NEXT_MANIFEST_PATH%" -p "%PREVIOUS_MANIFEST_PATH%" -i ".git;.hg;.deployment;deploy.cmd"
    IF !ERRORLEVEL! NEQ 0 goto error
    ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    ```

   Between no ::1 and ::2 is where we are gonna place our commands for building plugins.

    An example for the first plugin would be:

    ```sh
    :: 1.01 Build plugin customer roles to temporary path
    call :ExecuteCmd dotnet build "%DEPLOYMENT_SOURCE%\src\Plugins\Nop.Plugin.DiscountRules.CustomerRoles\Nop.Plugin.DiscountRules.CustomerRoles.csproj" -c Release
    ```

Now the plugin is built when you run the deploy scripts.
