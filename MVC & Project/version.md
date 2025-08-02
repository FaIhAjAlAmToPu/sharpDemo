
## Setting Up .NET 8 for ASP.NET Core MVC
To use .NET 8 in your project, follow these steps:

### 1. Check Your Visual Studio Version
.NET 8 requires **Visual Studio 2022 version 17.8 or later**.

- Go to **Help > About Microsoft Visual Studio** to check your version.
- If outdated, update via:
  - Open **Visual Studio Installer**.
  - Click **Update** next to your Visual Studio instance.
  - Or download the latest version: [visualstudio.microsoft.com/downloads/](https://visualstudio.microsoft.com/downloads/)

### 2. Install .NET 8 SDK
- Download the .NET 8 SDK for your OS: [dotnet.microsoft.com/en-us/download/dotnet/8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Run the installer.
- Verify installation by running:
  ```bash
  dotnet --version
  ```
  Expected output: `8.0.x`

### 3. Enable .NET 8 Workload in Visual Studio
If .NET 8 is not available in project settings:
- Open **Visual Studio Installer**.
- Click **Modify** on your Visual Studio installation.
- Select **.NET Desktop Development** or **ASP.NET and web development** workload.
- Ensure **.NET 8 SDK** is checked under "Optional Components".
- Click **Modify** to apply.

### 4. Restart Visual Studio
- Restart Visual Studio.
- .NET 8.0 should now appear when creating a new project or setting the Target Framework in project settings.