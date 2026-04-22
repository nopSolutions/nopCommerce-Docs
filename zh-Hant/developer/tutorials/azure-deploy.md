---
標題: 使用 Git 與自動化建置在 Azure 上進行部署
uid: zh-Hant/developer/tutorials/azure-deploy
作者: git.AndreiMaz
貢獻者: git.DmitriyKulagin, git.exileDev
---

# 使用 Git 與自動化建置在 Azure 上進行部署

## 在 Azure 上透過 git 自動部署 nopCommerce 的逐步指南

1. **設定您的 git 儲存庫**

   部署 nopCommerce 的預設方式是使用 Visual Studio 中的「發佈」功能。但若要發佈到 Azure，您需要擁有自己的儲存庫。您可以使用 Bitbucket 並使其與官方儲存庫保持同步，或者您可以在 [Azure 上設定 git](https://azure.microsoft.com/documentation/articles/web-sites-publish-source-control/)。這裡有一部很棒的影片 [這裡](http://channel9.msdn.com/Shows/Azure-Friday/What-is-Kudu-Azure-Web-Sites-Deployment-with-David-Ebbo)。

1. **準備本機部署**

   當您確認自動化建置運作正常後，我們就可以準備自訂部署指令碼。這是必要的，因為預設的自動化建置只會建置 `Nop.Web` 專案。問題在於它不會建置任何外掛。您也無法將它們的參考新增至 `Nop.Web` 專案，因為這會產生循環參考。因此，我們需要讓自訂建置運作，以下是我們需要安裝的工具：
    - 安裝 NodeJs：[https://nodejs.org](https://nodejs.org)

    - 安裝 Azure CLI：[https://azure.microsoft.com/documentation/articles/xplat-cli-install/](https://azure.microsoft.com/documentation/articles/xplat-cli-install/)

1. **在命令列層級使用 NuGet**

   KUDU 指令碼（用於為 Azure App Service 產生部署指令碼的工具）的預設行為是檢查 NuGet 套件。
   - 若要存取 `Nuget.exe` 檔案，您可以從 [這裡](https://docs.nuget.org/consume/command-line-reference) 下載。您也可以在 Visual Studio 中「啟用 NuGet 套件自動還原」，它會自動新增至您的專案中。

   - 確保 NuGet 在 PATH 環境變數中。將 `nuget.exe` 檔案複製到偏好的位置（例如 `c:/Program Files/Nuget/Nuget.exe`）。將其新增至 PATH 環境變數。
   - 啟動 `cmd.exe` 並輸入 *nuget* 以確認 NuGet 是否在您的 PATH 中，您應該會看到指令選項。

1. **在本機產生部署指令碼**

    - 開啟「Microsoft Azure Command Prompt」
    - 像在一般 shell 視窗中一樣，導航至專案的 src 資料夾
    - 執行 kudu 指令碼產生器（您可以透過 [此連結](https://github.com/projectkudu/kudu/wiki) 找到 wiki，或查看 [此影片](https://azure.microsoft.com/resources/videos/custom-web-site-deployment-scripts-with-kudu/)）。

        您應該會輸入類似這樣的指令：

        `kuduscript site deploymentscript --aspNetCore Presentation\Nop.Web\Nop.Web.csproj -s NopCommerce.sln -y`
    - 驗證它已產生 2 個檔案（在您的本機儲存庫根目錄中）：

        `.deployment`

        `deploy.cmd`

1. **執行產生的指令碼**

    - 您必須將 .deployment 和 deploy.cmd 檔案保留在 git 儲存庫的根目錄中
    - 編輯 `deploy.cmd`，因為 `%DEPLOYMENT_SOURCE%` 變數包含了 git 儲存庫的根目錄。因此，我們將新增 `%DEPLOYMENT_SOURCE%\src\Presentation\Nop.Web\Nop.Web.csproj` 以取代 `%DEPLOYMENT_SOURCE%\Presentation\Nop.Web\Nop.Web.csproj`。部署區段中的所有路徑都必須進行修正。
    - 執行 `deploy.cmd` 以查看預設部署指令碼是否在本機運作正常。它應該會在您的 git 儲存庫之外建立一個 `\artifact` 資料夾。

1. **自訂部署指令碼**

   現在我們來到最後的部分。這是所有努力獲得回報的地方。我們想要修改以下片段：

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

   在 ::1 和 ::2 之間，就是我們要放置建置外掛指令的地方。

    第一個外掛的範例如下：

    ```sh
    :: 1.01 Build plugin customer roles to temporary path
    call :ExecuteCmd dotnet build "%DEPLOYMENT_SOURCE%\src\Plugins\Nop.Plugin.DiscountRules.CustomerRoles\Nop.Plugin.DiscountRules.CustomerRoles.csproj" -c Release
    ```

現在，當您執行部署指令碼時，外掛就會被建置。