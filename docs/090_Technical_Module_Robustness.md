# 090 Technical Module Robustness

## Purpose

This document describes the V0.6 technical hardening step for the internal dashboard module foundation.

The dashboard now uses small internal modules to provide dashboard cards. This is useful because later real modules such as weather, RSS, tasks and calendar integration can be developed independently. It also creates a new risk: one module can fail while the rest of the dashboard would still be useful.

The V0.6 goal is therefore simple:

> A single broken dashboard module must not crash the whole application.

## Scope

This step keeps the current internal module architecture. It does not introduce a dynamic plugin system and it does not load external assemblies.

Implemented scope:

- keep `MockDashboardDataService` as the internal module runner for now,
- execute built-in modules in stable sort order,
- skip modules that are not visible in the current display mode,
- log module execution,
- log module failures,
- convert module exceptions into visible diagnostic dashboard cards,
- add tests for the module catalog,
- add tests for module execution behavior.

Deferred scope:

- real plugin loading,
- user-configurable module enable/disable state,
- module settings UI,
- retry policies,
- background refresh scheduling,
- advanced log levels such as DEBUG or TRACE.

## Design notes

`MockDashboardDataService` still keeps its historic name because the early application code already depends on it. Internally, however, it now behaves like a small module runner.

Each internal module implements `IDashboardModule` and can return one `DashboardWidgetModel`. If the module has no data, it may return `null`. If it throws an exception, the data service logs the failure and adds a diagnostic widget with status `Critical`.

This keeps the application usable during development and later during partial service failures.

## Test coverage

The added tests cover:

- default module catalog contains expected modules,
- module identifiers are unique,
- catalog order is stable,
- module metadata is non-empty,
- widgets are returned in sort order,
- hidden modules are skipped,
- modules returning `null` create a warning but no widget,
- failing modules create a diagnostic widget instead of crashing the snapshot.

## Expected developer workflow

After applying this patch, run:

```powershell
dotnet restore
dotnet build
dotnet test
dotnet run --project src/Sasd.PersonalDesktopDashboard.App/Sasd.PersonalDesktopDashboard.App.csproj
```

The application should behave as before, but the test suite should contain additional module robustness tests.
