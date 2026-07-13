# ResumeApp.Shared

A Razor Class Library (`Microsoft.NET.Sdk.Razor`, targeting `net10.0`) containing every page,
component, model, and piece of content in the application. Neither host project
(`ResumeApp.Web` or `ResumeApp`) defines any resume-specific UI of its own — they just render
`Routes.razor` from this assembly inside their own render mode/window. This is the only project
you need to touch to change what the resume says or how it looks.

## Folder structure

```
Models/           Plain C# records describing the shape of resume content.
Data/             The actual resume content, seeded as static data.
Components/       Small, reusable presentational components used by multiple pages.
Layout/           The app shell: sidebar navigation and the page frame around @Body.
Pages/            One .razor file per route.
Services/         Cross-platform service abstraction (form factor / platform detection).
wwwroot/          Static assets: Bootstrap, the resume theme stylesheet, favicon.
Routes.razor      The Blazor Router — maps URLs to pages in this assembly.
_Imports.razor    Shared @using directives so pages don't repeat them.
```

## Models (`Models/ResumeModels.cs`)

Five immutable C# records used throughout the app:

- **`ContactInfo`** — name, title, location, phone, email, LinkedIn URL, GitHub URL.
- **`SkillCategory`** — a named group of skills (e.g. "Cloud & Infrastructure") plus its items.
- **`TimelineEntry`** — a generic dated entry with a title, optional subtitle, date range,
  bullet points, and an optional link. Used for both jobs and the "Cloud Resume Application"
  entry under Experience.
- **`EducationEntry`** — school, degree, date range, GPA, description, relevant courses, tools.
- **`ProjectEntry`** — project name, date range, bullets, and an optional link.

## Data (`Data/ResumeData.cs`)

A single static class, `ResumeData`, holding every piece of resume content as typed properties:
`Contact`, `ProfessionalSummary`, `KeySkillsSummary`, `TechnicalSkills`, `Experience`,
`Education`, and `Projects`. This is the single source of truth for content — pages read from it
directly (via `@using ResumeApp.Shared.Data`, imported globally in `_Imports.razor`) rather than
receiving it as parameters, since it's effectively static reference data rather than
page-specific state. To update the resume, edit this file only.

## Components (`Components/`)

Reusable, parameterized pieces composed together by the pages:

- **`ProfileHeader.razor`** — takes a `ContactInfo` and renders the avatar (auto-generated
  initials), name, title, contact details, and LinkedIn/GitHub buttons. Used on `Home.razor`.
- **`SectionHeading.razor`** — a page-section `<h2>` with an optional subtitle, giving every
  page a consistent heading style (`Title`, optional `Subtitle` parameters).
- **`BulletList.razor`** — renders an `IReadOnlyList<string>` as a `<ul>`; renders nothing if
  the list is empty. Reused for key skills, education courses/tools, and inside `EntryCard`.
- **`EntryCard.razor`** — the main content card, used for Experience, Projects, and (via its
  `ChildContent` render fragment) Education. Takes `Title`, `Subtitle`, `DateRange`, `Bullets`,
  an optional `Url`/`UrlLabel`, and an optional `ChildContent` for entries that need extra
  markup (Education uses `ChildContent` to inject a GPA line and course/tool bullet lists
  instead of the standard `Bullets` list).
- **`SkillBadge.razor`** — a single pill-styled skill tag (`Text` parameter).
- **`SkillCategoryCard.razor`** — takes a `SkillCategory` and renders its name plus a
  `SkillBadge` for each item.

## Layout (`Layout/`)

- **`MainLayout.razor`** — the app shell: a sidebar (`NavMenu`) plus a top bar with LinkedIn/
  GitHub quick links and a `@Body` content area. Includes the Blazor error UI banner.
- **`NavMenu.razor`** / **`NavMenu.razor.css`** — the sidebar navigation with links to all six
  pages (Home, Experience, Education, Skills, Projects, Contact). Each link has a hand-drawn
  inline-SVG icon defined as a CSS background-image data URI in `NavMenu.razor.css` (briefcase,
  mortarboard, bar chart, folder, envelope — following the same pattern as the template's
  original house icon). Collapses into a hamburger toggle below 641px viewport width.

## Pages (`Pages/`)

Each file is a routed page (`@page` directive) that composes the components above with data
pulled from `ResumeData`:

- **`Home.razor`** (`/`) — `ProfileHeader`, professional summary, key skills summary, quick-nav
  buttons, and a "Get the App" section linking to the Windows download.
- **`Experience.razor`** (`/experience`) — loops `ResumeData.Experience` into `EntryCard`s.
- **`Education.razor`** (`/education`) — loops `ResumeData.Education`, using `EntryCard`'s
  `ChildContent` to render GPA, description, and course/tool bullet lists per entry.
- **`Skills.razor`** (`/skills`) — key skills summary plus a responsive grid of
  `SkillCategoryCard`s built from `ResumeData.TechnicalSkills`.
- **`Projects.razor`** (`/projects`) — loops `ResumeData.Projects` into `EntryCard`s.
- **`Contact.razor`** (`/contact`) — a grid of clickable contact cards (mailto, tel, LinkedIn,
  GitHub) built from `ResumeData.Contact`.
- **`NotFound.razor`** (`/not-found`) — fallback page for unmatched routes, rendered by
  `Routes.razor`'s `NotFoundPage`.

## Services (`Services/IFormFactor.cs`)

Declares `IFormFactor`, an interface with `GetFormFactor()` and `GetPlatform()`. Each host
project supplies its own implementation (web vs. MAUI) via dependency injection, so shared code
can (if needed) branch on whether it's running in the browser or as a native app without the
Shared project taking a dependency on either host.

## Styling (`wwwroot/app.css`)

Extends the default Blazor template stylesheet with the resume's visual theme: profile header
and avatar, section headings, entry cards, skill badges, the skill category grid, and the
contact card grid. Bootstrap (`wwwroot/lib/bootstrap`) is used for base styles and buttons; all
resume-specific styling lives in the `/* Resume site theme */` block at the bottom of `app.css`
so it's easy to find and adjust independently of the framework defaults.
