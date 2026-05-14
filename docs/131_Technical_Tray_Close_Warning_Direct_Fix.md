# Technical Note: Direct Tray Close Warning Fix

This document records a small corrective patch after the first warning-fix package only added a helper script and documentation but did not modify `MainWindow.xaml.cs`.

## Problem

The build still reported:

```text
warning CS0162: Unreachable code detected
```

The reason was a compile-time constant:

```csharp
private const bool HideWindowToTrayWhenClosedByUser = true;
```

Because the value is a constant, the compiler can determine that the `false` branch in `ShouldHideToTrayInsteadOfClosing()` can never be reached.

## Fix

The flag is now defined as:

```csharp
private static readonly bool HideWindowToTrayWhenClosedByUser = true;
```

This keeps the current behavior unchanged while avoiding the compiler warning. It also leaves a clean path to replace the field with a real user setting later.
