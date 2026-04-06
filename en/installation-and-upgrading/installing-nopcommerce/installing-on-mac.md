# Installing nopCommerce on macOS

## Prerequisites
- Ensure that you have the latest version of macOS installed.
- You will need Xcode command line tools. Use the command:  `xcode-select --install`

## Step 1: Install .NET SDK
- Download the latest .NET SDK from the [official site](https://dotnet.microsoft.com/download/dotnet).
- Install using the downloaded installer package.

### Troubleshooting .NET SDK Version Mismatch
- After installation, confirm the installation with:
  ```bash
  dotnet --version
  ```
- If you encounter version mismatch errors, consider cleaning up old SDK versions with:
  ```bash
  brew uninstall --ignore-dependencies dotnet-sdk
  brew cleanup
  ```

## Step 2: Set Up PostgreSQL
- Install PostgreSQL via Homebrew:
  ```bash
  brew install postgresql
  ```
- Start PostgreSQL service:
  ```bash
  brew services start postgresql
  ```

### Creating a Role in PostgreSQL
- Open the PostgreSQL shell:
  ```bash
  psql postgres
  ```
- Create a new role with:
  ```sql
  CREATE ROLE nopcommerce WITH LOGIN PASSWORD 'yourpassword';
  ALTER ROLE nopcommerce CREATEDB;
  ```

### Connection Issues
- Ensure that PostgreSQL is running and listening on the correct port.
- Update `pg_hba.conf` if necessary to allow connections from your application.

## Step 3: Install Telerik NuGet Packages
- Add NuGet source for Telerik:
  ```bash
  dotnet nuget add source https://nuget.telerik.com/your-source --name Telerik
  ```
- Install required packages:
  ```bash
  dotnet add package Telerik.UI.for.AspNet.Core
  ```

### Troubleshooting Telerik NuGet Errors
- Clear your NuGet cache if you face installation issues:
  ```bash
  dotnet nuget locals all --clear
  ```

## Step 4: Setup HTTPS Certificates
- Use OpenSSL to generate a self-signed certificate:
  ```bash
  openssl req -x509 -newkey rsa:2048 -keyout key.pem -out cert.pem -days 365 -nodes
  ```
- Trust the generated certificate in macOS Keychain.

## Step 5: Configure Database Migrations
- Run the following command to apply any pending migrations:
  ```bash
  dotnet ef migrations update
  ```
- Ensure that you have your connection string set in appsettings.json correctly.

## Conclusion
Following these steps will help you successfully install nopCommerce on your macOS. Address any issues by referring to the troubleshooting sections above.