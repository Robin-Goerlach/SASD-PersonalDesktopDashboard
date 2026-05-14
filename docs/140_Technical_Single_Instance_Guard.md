# Technical Note: Single Instance Guard

## Purpose

The SASD Personal Desktop Dashboard now uses a small single-instance guard so that the application is not started multiple times accidentally.

Without this guard, a user could end up with multiple dashboard windows, multiple tray icons, and multiple processes writing to the same settings and log files.

## Implementation

The guard is implemented in:

```text
src/Sasd.PersonalDesktopDashboard.App/Runtime/SingleInstanceGuard.cs
```

The application wires it in:

```text
src/Sasd.PersonalDesktopDashboard.App/App.xaml.cs
```

The implementation uses a named Windows mutex:

```text
Local\SASD.PersonalDesktopDashboard.SingleInstance
```

The `Local\` prefix scopes the mutex to the current Windows logon session. This is suitable for a desktop tray application where each signed-in user may have their own instance.

## Behavior

First process:

1. Configures logging.
2. Acquires the mutex.
3. Starts the WPF dashboard normally.
4. Creates the tray icon.

Second process:

1. Configures logging.
2. Fails to acquire the mutex.
3. Logs that another instance is already running.
4. Exits without creating a second window or tray icon.

## Deliberately deferred

The guard does not yet activate the already running main window when a second process is started. That would require a small IPC mechanism and should be added separately if needed.
