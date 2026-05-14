# SASD Personal Desktop Dashboard – Technical Note: Tray Behavior Refinement

## Purpose

This document describes the V0.9 refinement of the Windows tray behavior.

The previous tray foundation created a notification-area icon and allowed the user to show, hide, toggle compact mode, and exit the application through the tray menu. V0.9 adds a more typical desktop-dashboard behavior: clicking the normal window close button no longer immediately exits the process. Instead, the dashboard window is hidden and can be restored through the tray icon.

## Implemented behavior

The main window now distinguishes between two close scenarios:

1. A normal user close request, for example clicking the window X button.
2. An explicit application exit request through the tray menu item `Beenden`.

When the user clicks the window close button, the `Closing` event is cancelled and the window is hidden to the tray. The application process remains alive.

When the user chooses `Beenden` from the tray menu, the main window sets an explicit-exit flag and then calls WPF shutdown. The existing shutdown flow is then allowed to continue and the window placement is saved.

## Files changed

- `src/Sasd.PersonalDesktopDashboard.App/MainWindow.xaml.cs`
  - Adds an explicit-exit guard flag.
  - Converts normal close requests into hide-to-tray behavior.
  - Keeps the existing placement saving path for real shutdown.

- `src/Sasd.PersonalDesktopDashboard.App/Tray/MainWindowTrayActions.cs`
  - Sets the explicit-exit flag before requesting WPF shutdown from the tray menu.

## Design notes

This implementation intentionally keeps the behavior simple. The hide-to-tray setting is currently represented by a constant in `MainWindow.xaml.cs` so the algorithm is easy to understand. Later, this can be replaced by a user setting without changing the main close-handling logic.

The tray controller itself remains focused on notification-area concerns. It does not duplicate window lifecycle rules. The main window remains responsible for deciding whether a close request means hide-to-tray or real shutdown.

## Expected log entries

When the user clicks the window close button:

```text
Main window close requested by user; hiding dashboard to tray instead of exiting.
Hiding dashboard window to tray.
```

When the user exits through the tray menu:

```text
Tray requested application shutdown.
Saving main window placement.
Main window placement saved.
Application shutdown started.
Tray icon disposed.
Tray icon controller disposed.
Application shutdown completed.
```

## Manual test checklist

1. Start the application.
2. Click the window close button.
3. Verify that the process remains running and the tray icon remains visible.
4. Restore the window from the tray icon.
5. Hide the window again through the tray menu.
6. Restore it again.
7. Choose `Beenden` from the tray menu.
8. Verify that the process exits and the log contains the shutdown entries.
