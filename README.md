# ResumeApp

Michael Gustavus's resume, rebuilt as a .NET solution that replaces the legacy ASP.NET MVC site
previously hosted at `web-gc-resume-central-dev.azurewebsites.net`. The content (profile,
experience, education, skills, projects, contact info) is authored once as C# data and rendered
through a set of shared Razor components, so the same UI runs both as a website and as a native
desktop/mobile app.

Live source: https://github.com/mgustavus/resume-app

## Solution layout

This is a **3-project solution** generated from the .NET `maui-blazor-web` template and then
built out with resume-specific content:

```
ResumeApp.sln
|-- ResumeApp.Shared/   Razor Class Library - all pages, components, models, and data.
|                       Contains 100% of the resume content and UI. See its README for details.
|-- ResumeApp.Web/      ASP.NET Core Blazor Web App (Interactive Server render mode).
|                       The public website - the direct replacement for the old MVC site.
`-- ResumeApp/          .NET MAUI Blazor Hybrid app (Windows, Android, iOS, Mac Catalyst).
                        A native app shell that hosts the same Shared UI in a BlazorWebView.
```

Each project has its own `README.md` with a full description of the code inside it:

- [`ResumeApp.Shared/README.md`](ResumeApp.Shared/README.md)
- [`ResumeApp.Web/README.md`](ResumeApp.Web/README.md)
- [`ResumeApp/README.md`](ResumeApp/README.md)

## Why this shape

A Razor Class Library (`ResumeApp.Shared`) holds every page and component. Both hosts
(`ResumeApp.Web` and `ResumeApp`) reference it and add nothing of their own except the
platform-specific plumbing needed to boot Blazor in that environment (an ASP.NET Core pipeline
for the web, a `BlazorWebView` inside a native window for MAUI). This means:

- The resume content and UI are written and maintained **once**.
- The website and the native app can never visually drift apart - they render the exact same
  `.razor` files.
- Adding a page (e.g. a new "Certifications" page) means adding one `.razor` file to
  `ResumeApp.Shared/Pages`; both hosts pick it up automatically through `Routes.razor`.

## Pages

All routed from `ResumeApp.Shared/Pages`:

| Route         | Page             | Content                                                        |
|---------------|------------------|-----------------------------------------------------------------|
| `/`           | `Home.razor`     | Profile header, professional summary, key skills, Windows app download |
| `/experience` | `Experience.razor` | Professional experience timeline (job + Cloud Resume Application project) |
| `/education`  | `Education.razor`  | Degrees, GPA, relevant coursework, tools |
| `/skills`     | `Skills.razor`     | Key skills summary + categorized technical skill badges |
| `/projects`   | `Projects.razor`   | Cloud Resume Application, Karate School Finder, Desk Creator |
| `/contact`    | `Contact.razor`    | Email, phone, location, LinkedIn, GitHub |

## Building and running

Prerequisites: .NET 10 SDK, and for the MAUI project, the relevant MAUI workloads
(`android`, `ios`, `maccatalyst`, `maui-windows`) via `dotnet workload install`.

Run the website:

```
dotnet run --project ResumeApp.Web/ResumeApp.Web.csproj
```

Run the native app (Windows):

```
dotnet build ResumeApp/ResumeApp.csproj -f net10.0-windows10.0.19041.0
```

Or open `ResumeApp.sln` in Visual Studio and set `ResumeApp.Web` or `ResumeApp` as the startup
project.

## Downloadable Windows build

`ResumeApp.Web/wwwroot/downloads/ResumeApp-Windows.zip` is a self-contained, unpackaged
`dotnet publish` of the MAUI Windows head, linked from the Home page's "Get the App" section.
It's a generated build artifact (not committed to source - see `.gitignore`) and needs to be
regenerated any time the app changes:

```
dotnet publish ResumeApp/ResumeApp.csproj -f net10.0-windows10.0.19041.0 -c Release
# then zip bin/Release/net10.0-windows10.0.19041.0/win-x64/publish/ into
# ResumeApp.Web/wwwroot/downloads/ResumeApp-Windows.zip
```
