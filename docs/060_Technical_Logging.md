# 060 Technical Logging

## Purpose

This document describes the first lightweight logging foundation for the SASD Personal Desktop Dashboard.

The goal is not to introduce a large logging framework yet. The goal is to make startup, shutdown, window placement and later tray/compact-mode issues easier to diagnose.

## Log file location

The default log file is written to:

```text
%APPDATA%\SASD\PersonalDesktopDashboard\logs\app.log
```

Typical expanded path:

```text
C:\Users\<UserName>\AppData\Roaming\SASD\PersonalDesktopDashboard\logs\app.log
```

## Log levels

The first version supports:

- `INFO` for normal application lifecycle events.
- `WARN` for recoverable problems or fallback behavior.
- `ERROR` for exceptions and failed operations.

## Privacy note

Do not log passwords, tokens, secrets, private notes, file contents or other sensitive user data.

The current technical shell should only log diagnostic information such as application startup, shutdown, settings load/save operations and window placement decisions.

## Rotation

The file logger rotates `app.log` when it grows beyond the configured maximum size. By default it keeps three rotated files:

```text
app.log
app.1.log
app.2.log
app.3.log
```

This prevents unlimited log growth in the user profile.

## Intended scope for this patch

This is intentionally a V0.3 technical foundation:

- No external NuGet logging package.
- No complex logging configuration UI.
- No log viewer inside the application yet.
- No server logging.
- No telemetry.
