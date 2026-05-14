# SASD Personal Desktop Dashboard — V0.8 Tray Icon Foundation

## Purpose

This step adds a small Windows notification-area foundation to the WPF application.
It is intentionally technical and conservative. The goal is not a polished tray
workflow yet, but a reliable base for later Compact Mode and background-running
behavior.

## Implemented behavior

- A tray icon is created during application startup.
- The tray icon uses a simple context menu.
- The menu can show the dashboard window.
- The menu can hide the dashboard window while keeping the process alive.
- The menu can toggle Compact Mode / Normal Mode.
- The menu can shut down the application.
- Tray actions are logged through the existing application logger.
- The tray icon is disposed during application shutdown to avoid stale Windows icons.

## Technical design

The implementation uses `System.Windows.Forms.NotifyIcon` because WPF does not
provide a built-in tray icon control. This keeps the first implementation small
and avoids a third-party dependency.

The implementation is split into two parts:

- `TrayIconController` owns the Windows Forms tray icon and context menu.
- `MainWindowTrayActions` exposes safe tray actions on the existing WPF window.

The tray controller does not directly manipulate private window state. Instead,
it calls explicit methods on `MainWindow`. This keeps responsibilities clear:

- the tray controller knows about notification-area UI;
- the main window knows how to show, hide, activate, and switch its display mode.

## Deliberate limitations

This step does not yet implement:

- close-to-tray behavior when the user clicks the window X button;
- a custom SASD icon file;
- autostart with Windows;
- persisted tray preferences;
- background module refresh scheduling;
- balloon notifications or toast notifications.

Those topics should be treated as later steps, because they require product
and usability decisions.

## Expected log entries

Typical log entries after this patch:

```text
[INFO] Tray icon controller initialized.
[INFO] Tray icon created and made visible.
[INFO] Tray action started: hide dashboard to tray.
[INFO] Hiding dashboard window to tray.
[INFO] Tray action completed: hide dashboard to tray.
[INFO] Tray action started: show dashboard from tray.
[INFO] Showing dashboard window from tray.
[INFO] Tray action completed: show dashboard from tray.
```

## Test checklist

1. Run `dotnet restore`.
2. Run `dotnet build`.
3. Run `dotnet test`.
4. Run the WPF application.
5. Confirm that a tray icon appears.
6. Right-click the tray icon and show the menu.
7. Select `Dashboard ausblenden`.
8. Select `Dashboard anzeigen`.
9. Select `Compact Mode`.
10. Select `Normal Mode`.
11. Select `Beenden`.
12. Check `app.log`.
