# 060 Implementation Notes V0.1

## Purpose

This ZIP contains the first technical shell for the SASD Personal Desktop Dashboard.

The goal is not to provide a finished product, but a clean and understandable foundation for the next development steps.

## Important design decisions

- WPF is used as the first UI technology.
- The application targets .NET 8 and Windows.
- The solution is split into App, Core, Infrastructure and Modules.
- The first version contains only mock data.
- Real integrations should be added module by module.
- User-specific settings are stored below the user's AppData folder and should not be committed to Git.

## Next suggested implementation step

The next useful development step is monitor-aware window placement:

- detect connected monitors,
- identify primary and secondary monitor,
- keep the window inside the visible work area,
- move the dashboard back to the primary monitor if a previously used monitor is disconnected,
- store and restore window position safely.

