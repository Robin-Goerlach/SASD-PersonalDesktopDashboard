# SASD Personal Desktop Dashboard – Implementation Notes V0.2

**Version:** V0.2 – Window and Display Foundation  
**Date:** 2026-05-13  
**Status:** Copy-&-Paste implementation package

## 1. Goal

V0.2 adds the first real desktop integration layer to the dashboard. The application now prepares itself for realistic laptop and multi-monitor usage by detecting connected displays and by saving/restoring the dashboard window placement.

This version intentionally does **not** add real weather, news, calendar or task integrations. The priority is a robust Windows shell that does not open invisibly after monitor changes.

## 2. New capabilities

V0.2 adds:

- monitor detection via `WindowsDisplayService`,
- platform-independent display models in the Core project,
- persistent window placement in `%APPDATA%\SASD\PersonalDesktopDashboard\window-placement.json`,
- safe validation of saved positions,
- fallback to the primary display when a saved monitor is no longer connected,
- preservation of maximized window state,
- tests for the window placement validation logic.

## 3. Why this matters

A dashboard application will often be used in changing display setups:

- laptop only,
- laptop with one external monitor,
- laptop with two external monitors,
- docked and undocked operation,
- changed monitor order,
- changed monitor resolution.

Without validation, a saved window position can point to a display that no longer exists. The result is an application that seems to start but is actually outside the visible desktop. V0.2 prevents this.

## 4. New Core files

### `IDisplayService`

Defines how the application asks for connected displays without depending on Windows Forms or WPF.

### `IWindowPlacementService`

Defines how the application loads and saves validated window placement.

### `DisplayBounds`

Represents rectangular display areas in virtual desktop coordinates.

### `DisplayInfo`

Describes one monitor: device name, primary flag, full bounds and working area.

### `DashboardWindowState`

Stores the small safe subset of window states that the dashboard persists: normal or maximized.

### `WindowPlacementSettings`

Stores the window rectangle, state and optional monitor metadata.

### `WindowPlacementValidator`

The most important logic in this version. It decides whether a saved placement is still visible and moves it back to the primary display when necessary.

## 5. New Infrastructure files

### `WindowsDisplayService`

Uses `System.Windows.Forms.Screen` to detect Windows displays.

### `JsonWindowPlacementService`

Loads and saves window placement as JSON and always validates it before returning it to the WPF window.

### Updated `DefaultDashboardPaths`

Adds a path for `window-placement.json`.

## 6. Updated App behavior

`App.xaml.cs` now wires:

- `WindowsDisplayService`,
- `JsonWindowPlacementService`,
- existing settings and mock data services.

`MainWindow.xaml.cs` now:

- restores window placement during `SourceInitialized`,
- saves placement during `Closing`,
- keeps the application usable if placement save/load fails.

## 7. AppData files

The application now uses two separate files:

```text
%APPDATA%\SASD\PersonalDesktopDashboard\dashboard.settings.json
%APPDATA%\SASD\PersonalDesktopDashboard\window-placement.json
```

The separation is intentional. Dashboard settings are user configuration, while window placement is frequently changing UI state.

## 8. Build and test

After copying the files into the repository, run:

```bash
dotnet restore
dotnet build
dotnet test
```

Expected result:

- all projects build,
- existing dashboard tests pass,
- new window placement tests pass.

## 9. Suggested commit

```bash
git add .
git commit -m "Add display detection and window placement foundation"
git push
```

## 10. Next recommended step

V0.3 should add the tray icon and first explicit display modes:

- tray icon,
- show/hide dashboard,
- compact/sidebar mode preparation,
- optional start minimized to tray,
- menu entries for Dashboard, Compact, Focus and Exit.
