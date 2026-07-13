# ResumeApp.Web

The public website, and the direct replacement for the old ASP.NET MVC site at
`web-gc-resume-central-dev.azurewebsites.net`. An ASP.NET Core project
(`Microsoft.NET.Sdk.Web`, targeting `net10.0`) that hosts the pages and components defined in
`ResumeApp.Shared` using Blazor's **Interactive Server** render mode - the browser holds a thin
UI over a SignalR connection back to this server process, which runs the actual component code.

This project intentionally contains almost no resume-specific code. Its job is entirely
"host the Shared library as a website": routing, static files, and the two or three
web-only concerns (error handling, reconnection UI, platform detection) that don't belong in a
platform-agnostic library.

## Files

- **`Program.cs`** - the entry point. Registers Blazor server-side rendering
  (`AddRazorComponents().AddInteractiveServerComponents()`), registers the web implementation of
  `IFormFactor` (see below), configures the standard ASP.NET Core middleware pipeline (HTTPS
  redirection, static assets, antiforgery, exception/status-code pages), and maps `App` as the
  root component with `AddInteractiveServerRenderMode()`. The key line for pulling in the
  Shared project's pages is `.AddAdditionalAssemblies(typeof(ResumeApp.Shared._Imports).Assembly)`.
  This tells the Blazor router to also discover `@page` routes declared in `ResumeApp.Shared`,
  which is how `/experience`, `/education`, etc. get served even though they're not defined in
  this project.
- **`Components/App.razor`** - the root HTML document: `<head>` with the Bootstrap and
  `app.css` stylesheets (pulled from `ResumeApp.Shared` via its static web assets path,
  `_content/ResumeApp.Shared/...`), the SEO meta description, favicon, and the `<Routes>`
  component rendered in `InteractiveServer` mode.
- **`Components/Layout/ReconnectModal.razor`** (+ `.css`/`.js`) - the built-in Blazor Server
  "reconnecting..." overlay shown if the SignalR connection to the browser drops.
- **`Components/Pages/Error.razor`** - the default ASP.NET Core error page, shown in production
  when an unhandled exception occurs (`app.UseExceptionHandler("/Error", ...)` in `Program.cs`).
- **`Services/FormFactor.cs`** - the web's implementation of `IFormFactor` (declared in
  `ResumeApp.Shared`), returning `"Web"` as the form factor and the browser's OS as the
  platform. Registered as a singleton in `Program.cs`.
- **`Properties/launchSettings.json`** - local dev run profiles (`http` on port 5174,
  `https` on 7218/5174).
- **`appsettings.json`** / **`appsettings.Development.json`** - standard ASP.NET Core
  configuration (logging levels, allowed hosts).
- **`wwwroot/downloads/ResumeApp-Windows.zip`** - the self-contained, unpackaged Windows build
  of the MAUI app (`ResumeApp` project), linked from the Home page's "Get the App" download
  button. This is a generated build artifact, excluded from source control by `.gitignore`, and
  must be regenerated (`dotnet publish` the MAUI project, then re-zip) whenever the app changes.
  See the solution root `README.md` for the exact command.

## Running

```
dotnet run --project ResumeApp.Web.csproj
```

Defaults to `http://localhost:5174` per `launchSettings.json`. All page content and styling
comes from `ResumeApp.Shared`; there is nothing to edit in this project to change what the site
says - see `ResumeApp.Shared/README.md` for that.

## Deploying

Deploys like any standard ASP.NET Core app (this is what would replace the old MVC deployment on
Azure App Service): `dotnet publish -c Release`, then push the output to the App Service. Because
this uses Interactive Server render mode (not static WebAssembly), the target host must be able
to run a persistent ASP.NET Core process with WebSocket/SignalR support - it will not work on
purely static hosting (e.g. GitHub Pages) without first switching the render mode to WebAssembly
or Auto.
