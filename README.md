# SASD Personal Desktop Dashboard

**SASD Personal Desktop Dashboard** is a privacy-aware Windows desktop dashboard for weather, tasks, calendar events, news, system status and SASD project information.

This repository starts with **V0.1 Technical Shell**. The goal of this first implementation is not yet to connect real APIs, but to create a clean, understandable and extensible Windows/WPF foundation.

## V0.1 scope

Implemented in this initial shell:

- .NET 8 / WPF desktop application
- clean multi-project solution structure
- dark SASD-style dashboard window
- dummy cards for:
  - weather
  - tasks
  - calendar
  - news
  - system status
  - SASD projects
- Core models and service abstractions
- infrastructure service for JSON-based settings
- module service with mock dashboard data
- first xUnit tests for core defaults
- generous XML comments and explanatory inline comments

Intentionally not implemented yet:

- real weather API
- real RSS/news integration
- real calendar integration
- TaskHost or Microsoft To Do integration
- tray icon
- autostart
- advanced monitor-profile detection
- privacy mode behavior beyond the architectural placeholders

These items are planned for later milestones as described in the project documentation.

## Recommended environment

- Windows 10 or Windows 11
- Visual Studio 2022
- .NET 8 SDK

The WPF application targets `net8.0-windows`. It is meant to be built and executed on Windows.

## Build from command line

```bash
cd SASD-PersonalDesktopDashboard

dotnet restore
dotnet build
```

Run the WPF app:

```bash
dotnet run --project src/Sasd.PersonalDesktopDashboard.App/Sasd.PersonalDesktopDashboard.App.csproj
```

Run tests:

```bash
dotnet test
```

## Open in Visual Studio

Open:

```text
Sasd.PersonalDesktopDashboard.sln
```

Set this project as startup project:

```text
src/Sasd.PersonalDesktopDashboard.App
```

Then start with `F5`.

## Suggested first commit

```bash
git add .
git commit -m "Add initial WPF technical shell"
git push
```

## Architecture summary

The solution is intentionally split into four production projects:

```text
src/
├── Sasd.PersonalDesktopDashboard.App
│   WPF user interface, windows, view models and converters.
│
├── Sasd.PersonalDesktopDashboard.Core
│   Pure domain models, configuration models and service abstractions.
│
├── Sasd.PersonalDesktopDashboard.Infrastructure
│   Technical services such as settings persistence and filesystem paths.
│
└── Sasd.PersonalDesktopDashboard.Modules
    Data modules. V0.1 only contains mock data, later versions add weather,
    tasks, calendar, news, system status and SASD project integrations.
```

This separation keeps the UI replaceable and the data modules testable.

## Notes for the next development steps

A sensible next sequence is:

1. Add basic monitor detection and safe window placement.
2. Save and restore window position.
3. Add a tray icon and compact mode.
4. Replace the weather dummy card with a real cached weather provider.
5. Add RSS/news integration.
6. Add local tasks and later TaskHost integration.

