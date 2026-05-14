# SASD Personal Desktop Dashboard – Technical Compact Mode Foundation

## Purpose

This document describes the first technical implementation of the Compact Mode foundation. The goal of this step is not to deliver the final tray-driven dashboard experience yet. The goal is to create a small, understandable and testable WPF foundation that can later be connected to tray-icon behavior, saved display modes and real dashboard modules.

## Scope of this patch

The patch adds a real Compact Mode toggle to the existing WPF shell:

- the sidebar Compact Mode button now switches modes,
- a second Compact button is available in the header so the user can return from Compact Mode after the sidebar is hidden,
- the sidebar is hidden in Compact Mode,
- the window is resized to a smaller dashboard-like shape,
- the previous normal window placement is remembered in memory,
- returning to normal mode restores the previous normal placement,
- closing the application while in Compact Mode saves the remembered normal placement instead of the compact rectangle,
- mode changes are written to the existing application log.

## Files changed

```text
src/Sasd.PersonalDesktopDashboard.App/MainWindow.xaml
src/Sasd.PersonalDesktopDashboard.App/MainWindow.xaml.cs
docs/070_Technical_Compact_Mode.md
```

No new NuGet packages are required.

## Design notes

The project already contains the `DashboardDisplayMode` enum in the Core project. This patch reuses that existing model and does not introduce a second mode enum. The window starts in `DashboardDisplayMode.Dashboard` and can temporarily switch to `DashboardDisplayMode.Compact`.

The current version deliberately keeps the selected mode in memory only. This avoids surprising startup behavior and keeps the V0.4 implementation small. Persisting the last selected display mode can be added later when the expected startup behavior is decided in the strategy chat.

## Logging

Mode switching is logged through the existing `ApplicationLogger` facade. Typical log lines are:

```text
[INFO] Switching dashboard display mode from Dashboard to Compact.
[INFO] Dashboard display mode is now Compact.
[INFO] Switching dashboard display mode from Compact to Dashboard.
[INFO] Dashboard display mode is now Dashboard.
```

If the user closes the app while Compact Mode is active, the log should also contain:

```text
[INFO] Saving remembered normal placement because the window is currently in compact mode.
```

## Manual smoke test

1. Start the app.
2. Click `Compact` in the header or `Compact Mode` in the sidebar.
3. Check that the sidebar disappears and the window becomes smaller.
4. Click `Normal` in the header.
5. Check that the sidebar returns and the previous normal window size is restored.
6. Start again, enter Compact Mode, close the app, and start again.
7. Check that the app starts in the normal dashboard layout, not stuck in the small compact size.
8. Check `%APPDATA%\SASD\PersonalDesktopDashboard\logs\app.log` for the mode-switch log entries.

## Deferred work

The following topics are intentionally not part of this patch:

- tray icon support,
- persisting the selected display mode,
- separate persisted placements for dashboard, compact, focus and wallboard mode,
- animation during mode switching,
- keyboard shortcuts,
- real compact-card templates.

These are good candidates for later technical steps after this foundation has been tested.
