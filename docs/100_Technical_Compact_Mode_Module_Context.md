# 100 Technical Note: Compact Mode and Module Context

## Purpose

This document describes the small technical follow-up after the internal dashboard module foundation. The goal is to make sure that the visual window mode and the data mode used by the internal dashboard modules stay synchronized.

## Problem

After the compact mode and the internal module foundation were added, the WPF window could visually switch to compact mode, but the dashboard refresh path still used the preferred display mode stored in the settings file.

That meant the log could show a situation like this:

```text
Switching dashboard display mode from Dashboard to Compact.
Building dashboard snapshot for display mode 'Dashboard' using 6 internal modules.
```

For the current placeholder widgets this was not dangerous, but it would become confusing once modules start adapting their data to compact mode.

## Implemented change

The active display mode is now stored in `DashboardViewModel` as `CurrentDisplayMode`.

The main window passes the current visual mode to the view model:

- during initial loading,
- when switching to compact mode,
- when switching back to dashboard mode,
- when refreshing the dashboard.

The refresh command now uses `CurrentDisplayMode` instead of always using `DashboardSettings.PreferredDisplayMode`.

## Affected files

```text
src/Sasd.PersonalDesktopDashboard.App/ViewModels/DashboardViewModel.cs
src/Sasd.PersonalDesktopDashboard.App/MainWindow.xaml.cs
```

## Expected logging behavior

When compact mode is activated, the log should now include lines similar to:

```text
Switching dashboard display mode from Dashboard to Compact.
Reloading dashboard data for display mode 'Compact'.
Building dashboard snapshot for display mode 'Compact' using 6 internal modules.
Dashboard display mode is now Compact.
```

When normal mode is restored, the log should include lines similar to:

```text
Switching dashboard display mode from Compact to Dashboard.
Reloading dashboard data for display mode 'Dashboard'.
Building dashboard snapshot for display mode 'Dashboard' using 6 internal modules.
Dashboard display mode is now Dashboard.
```

## Design note

This is still not a full display-mode persistence system. Closing the app in compact mode still stores the remembered normal window placement so the next start is not unexpectedly tiny.

The purpose of this change is narrower: module data requests must receive the same display mode that the WPF window visually uses.

## Follow-up ideas

Possible later improvements:

- compact-specific widget layouts,
- module-level decisions about compact visibility,
- persisted user preference for startup mode,
- separate normal and compact window placement settings,
- tray integration using the same active display mode model.
