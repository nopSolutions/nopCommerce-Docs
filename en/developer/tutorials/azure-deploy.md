---
title: Step by step to deploy on Azure with GIT and automatic builds
uid: en/developer/tutorials/azure-deploy
author: git.AndreiMaz
contributors: git.DmitriyKulagin, git.exileDev
---

# Step by step to deploy on Azure with GIT and automatic builds

## Step by step guide for automatic deployment of nopCommerce with git on azure

1. **Your own git-repository** You need your own repository, you cannot just build nopCommerce. It's designed to be used with "Publish" function in Visual Studio 2017 as default. I use Bitbucket myself and keep that in sync with official repository.

1. Setup git on Azure
    - Tutorial: [https://azure.microsoft.com/da-dk/documentation/articles/web-sites-publish-source-control/](https://azure.microsoft.com/da-dk/documentation/articles/web-sites-publish-source-control/)

    - There's a great video here: [http://channel9.msdn.com/Shows/Azure-Friday/What-is-Kudu-Azure-Web-Sites-Deployment-with-David-Ebbo](http://channel9.msdn.com/Shows/Azure-Friday/What-is-Kudu-Azure-Web-Sites-Deployment-with-David-Ebbo)

1. **Prepare for local deploy** When you ensured that the automatic build works, we are ready to customize our deployment scripts. This is needed because the default automatic build only builds `nop.web` projects. The problem with this is that it does not build the admin website, and none of the plugins are build. You cannot refer to the plugins as it would create circular references. So now we need to get the custom build working, these are the install steps (also mention other places)
    - Install NodeJs: [https://nodejs.org](https://nodejs.org)

    - Install Azure CLI: [https://azure.microsoft.com/documentation/articles/xplat-cli-install/](https://azure.microsoft.com/documentation/articles/xplat-cli-install/)

1. **Get NuGet to work at command line level.** The default behavior of the KUDO script is to check for NuGet packages.
   - To get access to the `Nuget.exe` file you could either download from here: [https://docs.nuget.org/consume/command-line-reference](https://docs.nuget.org/consume/command-line-reference). You can also "Enable automatic restore of NuGet packages" in you Visual Studio 2017, and it will be added to your project automatically.

   - Ensure that NuGet is in the PATH. Copy the `nuget.exe` file to preferred location (I use `c:/Program Files/Nuget/Nuget.exe`). Add it to PATH environment variable.
   - Confirm that NuGet is in your PATH by starting `cmd.exe` and write *nuget*. you should see the command options.

1. **Generate deployment scripts locally**
    - Open the "Microsoft Azure Command Prompt"
    - Navigate to the src folder of your project as you normally would in a shell window
    - Execute the azure script generator (found this nice tutorial: [http://blog.amitapple.com/post/38418009331/azurewebsitecustomdeploymentpart2/#.VWyO3qikLjQ](http://blog.amitapple.com/post/38418009331/azurewebsitecustomdeploymentpart2/#.VWyO3qikLjQ)).

        So you would write something like:

        `azure site deploymentscript --aspWAP Presentation\Nop.Web\Nop.Web.csproj -s NopCommerce.sln`
    - Verify that it has generated 2 files (in your local repository root): 
	
		`.deployment` 
		`deploy.cmd`

1. **Run generated script**
    - You must keep the .deployment and deploy.cmd file to the root of git repository
    - Edit the deploy.cmd as the `%DEPLOYMENT_SOURCE%` variable contain the root of the git repository. So I would add `%DEPLOYMENT_SOURCE%\src\Presentation\Nop.Web\Nop.Web.csproj` instead of `%DEPLOYMENT_SOURCE%\Presentation\Nop.Web\Nop.Web.csproj`. All paths in the deployment section must be corrected.
    - Run deploy.cmd to see if the default deploy script works locally. It should create an \artifact folder just outside of your git repository.

1. **Customize the deployment script** So now we are at the final part :smile:. This is where all that work pays off :smile:. We want to alter the following piece:

    ```sh
    ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    :: Deployment
    :: ----------echo Handling .NET Web Application deployment.
    :: 1. Restore NuGet packages
    IF /I "NopCommerce.sln" NEQ "" (
    call :ExecuteCmd nuget restore "%DEPLOYMENT_SOURCE%\NopCommerce.sln"
    IF !ERRORLEVEL! NEQ 0 goto error
    )
    :: 2. Build to the temporary path
    IF /I "%IN_PLACE_DEPLOYMENT%" NEQ "1" (
    call :ExecuteCmd "%MSBUILD_PATH%" "%DEPLOYMENT_SOURCE%\Presentation\Nop.Web\Nop.Web.csproj" /nologo /verbosity:m /t:Build /t:pipelinePreDeployCopyAllFilesToOneFolder /p:_PackageTempDir="%DEPLOYMENT_TEMP%";AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release /p:SolutionDir="%DEPLOYMENT_SOURCE%\.\\" %SCM_BUILD_ARGS%
    ) ELSE (
    call :ExecuteCmd "%MSBUILD_PATH%" "%DEPLOYMENT_SOURCE%\Presentation\Nop.Web\Nop.Web.csproj" /nologo /verbosity:m /t:Build /p:AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release /p:SolutionDir="%DEPLOYMENT_SOURCE%\.\\" %SCM_BUILD_ARGS%
    )
    IF !ERRORLEVEL! NEQ 0 goto error
    :: 3. KuduSync
    IF /I "%IN_PLACE_DEPLOYMENT%" NEQ "1" (
    call :ExecuteCmd "%KUDU_SYNC_CMD%" -v 50 -f "%DEPLOYMENT_TEMP%" -t "%DEPLOYMENT_TARGET%" -n "%NEXT_MANIFEST_PATH%" -p "%PREVIOUS_MANIFEST_PATH%" -i ".git;.hg;.deployment;deploy.cmd"
    IF !ERRORLEVEL! NEQ 0 goto error
    )
    ::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    ```

    So between no ::1 and ::2 that's where we are gonna place our commands for building plugins.
	
	An example for the first plugin would be:

    ```sh
    :: 1.01 Build plugin customer roles to temporary path
    call :ExecuteCmd "%MSBUILD_PATH%" "%DEPLOYMENT_SOURCE%\src\Plugins\Nop.Plugin.DiscountRules.CustomerRoles\Nop.Plugin.DiscountRules.CustomerRoles.csproj" /nologo /verbosity:m /t:Build /p:AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release /p:SolutionDir="%DEPLOYMENT_SOURCE%\.\\" %SCM_BUILD_ARGS%
    ```

Now the plugin is built when you run the deploy scripts :)
