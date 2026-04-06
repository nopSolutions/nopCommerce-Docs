# Installing nopCommerce on macOS

This guide walks you through setting up [nopCommerce](https://www.nopcommerce.com) (v4.90.x) locally on macOS using PostgreSQL. nopCommerce is built on .NET 9 and runs natively on macOS — no virtualisation required.

---

## Prerequisites

- macOS 14.0 (Sonoma) or later
- Xcode Command Line Tools
- [Homebrew](https://brew.sh) package manager
- An IDE: [VS Code](https://code.visualstudio.com) with the C# Dev Kit, or [JetBrains Rider](https://www.jetbrains.com/rider/)

> **Note:** Visual Studio (Windows edition) does not run on macOS. Use VS Code or Rider instead.

Install Xcode Command Line Tools:

```bash
xcode-select --install
```

Install Homebrew (if not already installed):

```bash
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
```

---

## Step 1: Install .NET 9 SDK

nopCommerce 4.80 and later requires **.NET 9**. Download the installer from the [official .NET 9 download page](https://dotnet.microsoft.com/en-us/download/dotnet/9.0), or install via Homebrew:

```bash
brew install --cask dotnet-sdk
```

Confirm the installation:

```bash
dotnet --version
# Expected output: 9.x.x
```

### Troubleshooting .NET SDK Version Mismatch

If multiple SDKs are installed and the wrong version is selected, list them all:

```bash
dotnet --list-sdks
```

To pin a specific version for your project, create a `global.json` in the project root:

```json
{
  "sdk": {
    "version": "9.0.0",
    "rollForward": "latestMinor"
  }
}
```

To remove outdated SDK versions:

```bash
brew uninstall --ignore-dependencies dotnet-sdk
brew cleanup
```

---

## Step 2: Set Up PostgreSQL

### Install via Homebrew

```bash
brew install postgresql@16
brew services start postgresql@16
```

Add the PostgreSQL binaries to your PATH. Append this line to `~/.zshrc` (or `~/.bash_profile`):

```bash
echo 'export PATH="/opt/homebrew/opt/postgresql@16/bin:$PATH"' >> ~/.zshrc
source ~/.zshrc
```

Verify PostgreSQL is running:

```bash
pg_isready
# Expected: /tmp:5432 - accepting connections
```

### Creating a Role and Database

Open the PostgreSQL shell:

```bash
psql postgres
```

Create a dedicated role and database for nopCommerce:

```sql
CREATE ROLE nopcommerce WITH LOGIN PASSWORD 'yourpassword';
ALTER ROLE nopcommerce CREATEDB;
CREATE DATABASE nopcommerce OWNER nopcommerce;
\q
```

### Connection Issues

- Confirm PostgreSQL is running: `brew services list`
- If you see a peer authentication error, open `/opt/homebrew/var/postgresql@16/pg_hba.conf` and set the local connection method to `md5` or `scram-sha-256`, then restart:

  ```bash
  brew services restart postgresql@16
  ```

---

## Step 3: Clone nopCommerce and Restore Packages

Clone the repository:

```bash
git clone https://github.com/nopSolutions/nopCommerce.git
cd nopCommerce
git checkout release-4.90
```

Restore all NuGet packages from the `src` directory:

```bash
cd src
dotnet restore NopCommerce.sln
```

> **No Telerik account required.** The open-source edition uses **Kendo UI Core**, licensed under Apache 2.0 and available directly on [nuget.org](https://www.nuget.org). All packages are restored automatically by `dotnet restore` — no additional NuGet sources need to be configured.
>
> If you do encounter package restore failures, clear the local NuGet cache and retry:
>
> ```bash
> dotnet nuget locals all --clear
> dotnet restore NopCommerce.sln
> ```

---

## Step 4: Set Up HTTPS Development Certificate

ASP.NET Core requires a trusted local HTTPS certificate. Use the built-in .NET tooling — do **not** use OpenSSL for this:

```bash
dotnet dev-certs https --trust
```

macOS will prompt for your password to add the certificate to the system Keychain. Accept the prompt in the dialog that appears.

Verify the certificate was trusted:

```bash
dotnet dev-certs https --check --trust
```

If you need to regenerate the certificate:

```bash
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

---

## Step 5: Run nopCommerce

Navigate to the web application entry point and start the server:

```bash
cd src/Presentation/Nop.Web
dotnet run
```

For development with hot-reload:

```bash
dotnet watch run
```

The application will be available at:

| Protocol | URL |
|----------|-----|
| HTTPS | `https://localhost:5001` |
| HTTP | `http://localhost:5000` |

---

## Step 6: Complete the Web Installer

Open `https://localhost:5001` in your browser. The setup wizard will guide you through:

1. **Store information** — store name, admin email, and password.
2. **Database configuration** — select **PostgreSQL** and enter:

   | Field | Value |
   |-------|-------|
   | Server | `localhost` |
   | Port | `5432` |
   | Database | `nopcommerce` |
   | Username | `nopcommerce` |
   | Password | *(as set in Step 2)* |

3. Click **Install**. The installer creates all tables, stored procedures, and seeds initial data automatically.

---

## Step 7: Apply Database Migrations (Upgrades Only)

When **upgrading** an existing installation, apply pending schema changes:

```bash
dotnet ef database update
```

> **Common mistake:** `dotnet ef migrations update` is not a valid command. The correct command is `dotnet ef database update`.

Before running migrations, verify the connection string in `src/Presentation/Nop.Web/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=5432;Database=nopcommerce;User Id=nopcommerce;Password=yourpassword;"
}
```

---

## Troubleshooting

| Problem | Solution |
|---------|----------|
| `Unable to connect to database` | Run `pg_isready` and verify credentials match Step 2. Check `pg_hba.conf` auth method. |
| Port 5001 already in use | Run `lsof -i :5001`, then `kill -9 <PID>`. Or change the port in `launchSettings.json`. |
| SSL certificate not trusted | Run `dotnet dev-certs https --clean` then `dotnet dev-certs https --trust`. |
| NuGet package restore fails | Run `dotnet nuget locals all --clear` then `dotnet restore NopCommerce.sln`. |
| Wrong .NET version selected | Add a `global.json` pinning version `9.x` to the project root. |

---

## Quick Reference

| Command | Description |
|---------|-------------|
| `dotnet run` | Start the application |
| `dotnet watch run` | Start with hot-reload |
| `dotnet restore` | Restore NuGet packages |
| `dotnet ef database update` | Apply pending migrations |
| `dotnet dev-certs https --trust` | Trust HTTPS dev certificate |
| `brew services restart postgresql@16` | Restart PostgreSQL |
| `pg_isready` | Check if PostgreSQL is accepting connections |

---

## Resources

- [nopCommerce Official Documentation](https://docs.nopcommerce.com)
- [Technology & System Requirements](https://docs.nopcommerce.com/en/installation-and-upgrading/technology-and-system-requirements.html)
- [Installing Locally](https://docs.nopcommerce.com/en/installation-and-upgrading/installing-nopcommerce/installing-local.html)
- [Developer Getting Started](https://docs.nopcommerce.com/en/developer/tutorials/instruction-on-how-to-start-developing-on-nopcommerce.html)
- [nopCommerce GitHub](https://github.com/nopSolutions/nopCommerce)
- [Release Notes](https://www.nopcommerce.com/en/release-notes)
