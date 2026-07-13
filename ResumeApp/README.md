# ResumeApp (MAUI)

The native app shell. A .NET MAUI Blazor Hybrid project (`Microsoft.NET.Sdk.Razor`, `UseMaui`)
that hosts the exact same pages and components from `ResumeApp.Shared` inside a `BlazorWebView`
— a native window with an embedded web view running Blazor, rather than a browser tab. This is
what produces the downloadable Windows desktop app (and can be built for Android, iOS, and Mac
Catalyst) so the resume can be installed and run as a real app, not just visited as a website.

As with `ResumeApp.Web`, this project holds no resume content or page markup of its own — it
supplies only the native app plumbing needed to boot the Shared library's Blazor components
inside a `BlazorWebView`.

## Target frameworks

```xml
<TargetFrameworks>net10.0-android</TargetFrameworks>
<TargetFrameworks ...>net10.0-ios;net10.0-maccatalyst</TargetFrameworks>   (non-Linux hosts)
<TargetFrameworks ...>net10.0-windows10.0.19041.0</TargetFrameworks>       (Windows hosts)
```

`SingleProject` is enabled, so one `.csproj` produces all four platform heads; platform-specific
code lives under `Platforms/`. `WindowsPackageType` is `None`, meaning the Windows build is
unpackaged (a plain `.exe` + supporting files you can zip and run, not an MSIX you install
through the Store) — this is what gets published as the downloadable zip on the website.

## Files

- **`MauiProgram.cs`** — the MAUI equivalent of `Program.cs`. Builds the `MauiApp`, registers
  fonts, registers the MAUI implementation of `IFormFactor` (see below), and calls
  `AddMauiBlazorWebView()` to enable hosting Blazor components in a native window. In `DEBUG`
  builds it also adds the Blazor WebView developer tools and debug logging.
- **`App.xaml`** / **`App.xaml.cs`** — the MAUI `Application` subclass; creates the app's main
  `Window` wrapping `MainPage`.
- **`MainPage.xaml`** / **`MainPage.xaml.cs`** — the single native page in the app. Its XAML is
  essentially just a `BlazorWebView` control pointed at the Shared library's root component
  (`Routes`), which is what lets the exact same Razor pages used by the website render natively
  here.
- **`Components/_Imports.razor`** — `@using` directives for the Razor code referenced from
  `MainPage.xaml`'s `BlazorWebView`.
- **`Services/FormFactor.cs`** — the MAUI implementation of `IFormFactor` (declared in
  `ResumeApp.Shared`), returning `DeviceInfo.Idiom` (e.g. Desktop/Phone/Tablet) as the form
  factor and `DeviceInfo.Platform` (Windows/Android/iOS/MacCatalyst) as the platform.
- **`wwwroot/index.html`** — the HTML host page rendered inside the `BlazorWebView` (analogous
  to `App.razor` in the web project). Loads the same Bootstrap and `app.css` from
  `ResumeApp.Shared`'s static assets, plus `_framework/blazor.webview.js` instead of the web's
  `blazor.web.js`.
- **`wwwroot/app.css`** — MAUI-host-specific style overrides (e.g. a `status-bar-safe-area`
  spacer for platforms with a native status bar), layered on top of the shared `app.css`.

## Platforms (`Platforms/`)

Per-platform native entry points and manifests, all boilerplate from the MAUI project template
(not resume-specific):

- **`Android/`** — `MainActivity.cs`, `MainApplication.cs`, `AndroidManifest.xml`.
- **`iOS/`** — `AppDelegate.cs`, `Program.cs`, `Info.plist`.
- **`MacCatalyst/`** — `AppDelegate.cs`, `Program.cs`, `Info.plist`, `Entitlements.plist`.
- **`Windows/`** — `App.xaml`/`.xaml.cs` (the WinUI 3 application class), `app.manifest`,
  `Package.appxmanifest` (present for packaging metadata even though the build is unpackaged).

## Resources (`Resources/`)

App icon (`AppIcon/appicon.svg` + foreground layer), splash screen (`Splash/splash.svg`), the
default MAUI bot image (`Images/dotnet_bot.svg` — not currently referenced by any resume page),
and a bundled font (`Fonts/OpenSans-Regular.ttf`). These are template defaults and haven't been
customized with resume-specific branding.

## Building

```
dotnet build ResumeApp.csproj -f net10.0-windows10.0.19041.0          # Windows
dotnet build ResumeApp.csproj -f net10.0-android                       # Android (needs Android SDK)
```

Requires the corresponding MAUI workloads installed (`dotnet workload install maui-windows`,
`android`, `ios`, `maccatalyst` as needed).

## Publishing the Windows download

```
dotnet publish ResumeApp.csproj -f net10.0-windows10.0.19041.0 -c Release
```

Produces a self-contained, unpackaged app at
`bin/Release/net10.0-windows10.0.19041.0/win-x64/publish/`. This folder is zipped and placed at
`ResumeApp.Web/wwwroot/downloads/ResumeApp-Windows.zip`, which the website's Home page links to
as "Download for Windows". See the solution root `README.md` for the full command.
