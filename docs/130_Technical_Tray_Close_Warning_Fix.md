# Technical Note: Tray Close Warning Fix

## Purpose

This note documents the small V0.9.1 cleanup after the tray close behavior refinement.

## Problem

The tray close behavior introduced a configuration field controlling whether the main window close button should hide the dashboard to the tray instead of exiting the application.

Originally the field was declared as a compile-time constant:

```csharp
private const bool HideWindowToTrayWhenClosedByUser = true;
```

This produced the compiler warning `CS0162: Unreachable code detected`, because the compiler could evaluate the related branch at compile time.

## Solution

The field was changed to:

```csharp
private static readonly bool HideWindowToTrayWhenClosedByUser = true;
```

This preserves the current runtime behavior while avoiding the unreachable-code warning. It also makes the field easier to replace with a future user setting.

## Behavior

No user-visible behavior changes:

- Clicking the window close button hides the dashboard to the tray.
- The dashboard can be restored from the tray menu.
- The tray menu can still request a real application shutdown.
- Window placement saving during real shutdown remains unchanged.
