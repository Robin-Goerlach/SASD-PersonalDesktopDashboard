# 080 Technical Internal Dashboard Modules

## Purpose

This document describes the first internal dashboard module foundation for the SASD Personal Desktop Dashboard.

The goal is not a full plugin system. The goal is a small, understandable structure where dashboard cards are no longer hard-coded in one large data service method. Instead, each card is produced by a small internal module class.

## Version Scope

This patch belongs to the technical V0.x foundation phase.

It introduces:

- a small module interface in the Core project,
- a module context that carries display mode, timestamp and logger,
- built-in placeholder modules in the Modules project,
- an internal module catalog,
- a dashboard data service that aggregates module output,
- logging from the data service and from individual modules.

It does not introduce:

- dynamic plugin loading,
- external module installation,
- runtime assembly discovery,
- network access,
- real weather, RSS, task or calendar integrations.

## File Structure

```text
src/Sasd.PersonalDesktopDashboard.Core/Modules/
  DashboardModuleContext.cs
  IDashboardModule.cs

src/Sasd.PersonalDesktopDashboard.Modules/Abstractions/
  DashboardModuleBase.cs

src/Sasd.PersonalDesktopDashboard.Modules/Registration/
  DashboardModuleCatalog.cs

src/Sasd.PersonalDesktopDashboard.Modules/Weather/
  WeatherPlaceholderModule.cs

src/Sasd.PersonalDesktopDashboard.Modules/Tasks/
  TasksPlaceholderModule.cs

src/Sasd.PersonalDesktopDashboard.Modules/Calendar/
  CalendarPlaceholderModule.cs

src/Sasd.PersonalDesktopDashboard.Modules/News/
  NewsPlaceholderModule.cs

src/Sasd.PersonalDesktopDashboard.Modules/SystemStatus/
  SystemStatusModule.cs

src/Sasd.PersonalDesktopDashboard.Modules/SasdProjects/
  SasdProjectsModule.cs

src/Sasd.PersonalDesktopDashboard.Modules/MockData/
  MockDashboardDataService.cs
```

## Design

The WPF application still talks to one interface:

```csharp
IDashboardDataService
```

The ViewModel therefore does not need to know whether the dashboard cards come from one mock service, several internal modules or later a more advanced module pipeline.

The Modules project contains the current built-in modules. Each module returns one `DashboardWidgetModel` that the existing WPF view can already render.

## Logging

The logger is passed from the application startup into the dashboard data service. The data service then creates a `DashboardModuleContext` and passes it to every module.

This means internal modules can now write log messages without depending on WPF and without knowing the concrete `FileAppLogger` implementation.

Example log entries:

```text
Registered 6 internal dashboard modules.
Building dashboard snapshot for display mode 'Dashboard' using 6 internal modules.
Executing dashboard module 'weather.placeholder' (Weather Placeholder).
Building weather placeholder dashboard widget.
Dashboard module 'weather.placeholder' produced widget 'weather.now'.
Dashboard snapshot built with 6 widgets.
```

## Compact Mode

Compact mode keeps the earlier behavior that only the most important cards are visible.

Currently visible in compact mode:

- Weather
- Tasks
- Calendar

The other modules are skipped for compact mode and this is logged for diagnostics.

## Error Handling

If one module throws an exception, the whole dashboard should not fail during this early phase. The data service catches the exception, logs it and adds a visible diagnostic card.

This makes module failures easy to notice without preventing the application from starting.

## Later Development

This foundation prepares later work without forcing it now:

- real weather module,
- RSS/news module,
- local task module,
- calendar module,
- module-specific settings,
- user-controlled module visibility,
- optional plugin system in a later major phase.
