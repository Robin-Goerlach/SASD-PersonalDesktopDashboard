# V0.11 Technical AppData Diagnostics

## Goal

This step adds lightweight startup diagnostics for the SASD Personal Desktop Dashboard.
The application already stores settings, window placement and logs in the user profile.
When the application is tested on different Windows systems, it is useful to see the
exact effective paths directly in `app.log`.

## Files changed

- `src/Sasd.PersonalDesktopDashboard.App/App.xaml.cs`
  - Creates the logger options explicitly before constructing the file logger.
  - Calls `AppDataDiagnostics.LogStartupPaths(...)` directly after logging is configured.

- `src/Sasd.PersonalDesktopDashboard.App/Diagnostics/AppDataDiagnostics.cs`
  - New helper that logs AppData, settings, window placement and log paths.
  - Also logs a few harmless runtime diagnostics such as process id, runtime version,
    operating system string and whether the process is running in an interactive session.

## Expected log output

After startup the log should contain entries similar to:

```text
Application data diagnostics started.
Process ID: 12345.
Runtime version: 8.0.x.
Operating system: Microsoft Windows ...
User interactive session: True.
Roaming AppData directory: C:\Users\...\AppData\Roaming
Dashboard AppData directory: C:\Users\...\AppData\Roaming\SASD\PersonalDesktopDashboard
Dashboard settings file: C:\Users\...\AppData\Roaming\SASD\PersonalDesktopDashboard\dashboard.settings.json
Dashboard window placement file: C:\Users\...\AppData\Roaming\SASD\PersonalDesktopDashboard\window-placement.json
Dashboard log directory: C:\Users\...\AppData\Roaming\SASD\PersonalDesktopDashboard\logs
Dashboard log file: C:\Users\...\AppData\Roaming\SASD\PersonalDesktopDashboard\logs\app.log
Application data diagnostics completed.
```

## Notes

The diagnostics are intentionally read-only. They do not create, delete or modify
settings files. The existing file logger creates the log directory as before.
