---
title: Step by step to deploy on Azure with GIT and automatic builds
uid: en/developer/tutorials/azure-deploy
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Step by step to deploy on Azure with GIT and automatic builds

## Step by step guide for automatic deployment of nopCommerce with git on azure

1. **Your git-repository** You need your repository, you cannot just build nopCommerce. It's designed to be used with the "Publish" function in Visual Studio as default. I use Bitbucket myself and keep that in sync with the official repository.

1. Setup git on Azure
    - Tutorial: [https://azure.microsoft.com/documentation/articles/web-sites-publish-source-control/](https://azure.microsoft.com/documentation/articles/web-sites-publish-source-control/)

    - There's a great video here: [http://channel9.msdn.com/Shows/Azure-Friday/What-is-Kudu-Azure-Web-Sites-Deployment-with-David-Ebbo](http://channel9.msdn.com/Shows/Azure-Friday/What-is-Kudu-Azure-Web-Sites-Deployment-with-David-Ebbo)

1. **Prepare for local deployment** When you ensure that the automatic build works, we are ready to customize our deployment scripts. This is needed because the default automatic build only builds `nop.web` projects. The problem with this is that it does not build the admin website, and none of the plugins are built. You cannot refer to the plugins as they would create circular references. So now we need to get the custom build working, these are the install steps (also mention other places)
    - Install NodeJs: [https://nodejs.org](https://nodejs.org)

    - Install Azure CLI: [https://azure.microsoft.com/documentation/articles/xplat-cli-install/](https://azure.microsoft.com/documentation/articles/xplat-cli-install/)

1. **Get NuGet to work at the command line level.** The default behavior of the KUDO script is to check for NuGet packages.
   - To get access to the `Nuget.exe` file you could either download from here: [https://docs.nuget.org/consume/command-line-reference](https://docs.nuget.org/consume/command-line-reference). You can also "Enable automatic restore of NuGet packages" in your Visual Studio, and it will be added to your project automatically.

   - Ensure that NuGet is in the PATH. Copy the `nuget.exe` file to the preferred location (I use `c:/Program Files/Nuget/Nuget.exe`). Add it to the PATH environment variable.
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
    - You must keep the .deployment and deploy.cmd file to the root of a git repository
    - Edit the `deploy.cmd` as the `%DEPLOYMENT_SOURCE%` variable containing the root of the git repository. So I would add `%DEPLOYMENT_SOURCE%\src\Presentation\Nop.Web\Nop.Web.csproj` instead of `%DEPLOYMENT_SOURCE%\Presentation\Nop.Web\Nop.Web.csproj`. All paths in the deployment section must be corrected.
    - Run `deploy.cmd` to see if the default deploy script works locally. It should create an `\artifact` folder just outside of your git repository.

1. **Customize the deployment script** So now we are at the final part. This is where all that work pays off. We want to alter the following piece:

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

    So between no ::1 and ::2 that's where we are gonna place our commands for building plugins.

    An example for the first plugin would be:

    ```sh
    :: 1.01 Build plugin customer roles to temporary path
    call :ExecuteCmd dotnet build "%DEPLOYMENT_SOURCE%\src\Plugins\Nop.Plugin.DiscountRules.CustomerRoles\Nop.Plugin.DiscountRules.CustomerRoles.csproj" -c Release
    ```

Now the plugin is built when you run the deploy scripts.
