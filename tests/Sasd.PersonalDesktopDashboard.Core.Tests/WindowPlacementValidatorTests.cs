using Sasd.PersonalDesktopDashboard.Core.Configuration;
using Sasd.PersonalDesktopDashboard.Core.Models;
using Sasd.PersonalDesktopDashboard.Core.Services;

namespace Sasd.PersonalDesktopDashboard.Core.Tests;

/// <summary>
/// Tests for the platform-independent window placement validation logic.
/// </summary>
public sealed class WindowPlacementValidatorTests
{
    /// <summary>
    /// Ensures that a missing placement is converted into a centered placement on the primary display.
    /// </summary>
    [Fact]
    public void NormalizeOrCreateDefault_WhenPlacementIsMissing_ShouldCenterOnPrimaryDisplay()
    {
        var displays = CreateTwoDisplaySetup();

        var result = WindowPlacementValidator.NormalizeOrCreateDefault(
            savedPlacement: null,
            displays,
            defaultWidth: 1000,
            defaultHeight: 600);

        Assert.Equal(460, result.Left);
        Assert.Equal(240, result.Top);
        Assert.Equal(1000, result.Width);
        Assert.Equal(600, result.Height);
        Assert.Equal(DashboardWindowState.Normal, result.WindowState);
    }

    /// <summary>
    /// Ensures that a placement on an available secondary monitor is preserved.
    /// </summary>
    [Fact]
    public void NormalizeOrCreateDefault_WhenPlacementIsVisibleOnSecondaryDisplay_ShouldKeepPlacement()
    {
        var displays = CreateTwoDisplaySetup();
        var savedPlacement = new WindowPlacementSettings
        {
            Left = 2100,
            Top = 120,
            Width = 900,
            Height = 600,
            WindowState = DashboardWindowState.Maximized
        };

        var result = WindowPlacementValidator.NormalizeOrCreateDefault(
            savedPlacement,
            displays,
            defaultWidth: 1000,
            defaultHeight: 600);

        Assert.Equal(savedPlacement.Left, result.Left);
        Assert.Equal(savedPlacement.Top, result.Top);
        Assert.Equal(savedPlacement.Width, result.Width);
        Assert.Equal(savedPlacement.Height, result.Height);
        Assert.Equal(DashboardWindowState.Maximized, result.WindowState);
    }

    /// <summary>
    /// Ensures that a placement outside all connected displays is moved back to the primary display.
    /// </summary>
    [Fact]
    public void NormalizeOrCreateDefault_WhenPlacementIsOffscreen_ShouldMoveToPrimaryDisplay()
    {
        var displays = CreateSingleLaptopDisplay();
        var savedPlacement = new WindowPlacementSettings
        {
            Left = 3500,
            Top = 200,
            Width = 1200,
            Height = 700,
            WindowState = DashboardWindowState.Normal
        };

        var result = WindowPlacementValidator.NormalizeOrCreateDefault(
            savedPlacement,
            displays,
            defaultWidth: 1200,
            defaultHeight: 700);

        Assert.True(result.Left >= 0);
        Assert.True(result.Top >= 0);
        Assert.True(result.Left + result.Width <= 1920);
        Assert.True(result.Top + result.Height <= 1040);
        Assert.Equal(DashboardWindowState.Normal, result.WindowState);
    }

    /// <summary>
    /// Ensures that corrupted sizes are replaced by useful fallback dimensions.
    /// </summary>
    [Fact]
    public void NormalizeOrCreateDefault_WhenPlacementSizeIsInvalid_ShouldUseSafeDimensions()
    {
        var displays = CreateSingleLaptopDisplay();
        var savedPlacement = new WindowPlacementSettings
        {
            Left = 100,
            Top = 100,
            Width = double.NaN,
            Height = 10,
            WindowState = DashboardWindowState.Normal
        };

        var result = WindowPlacementValidator.NormalizeOrCreateDefault(
            savedPlacement,
            displays,
            defaultWidth: 1280,
            defaultHeight: 760);

        Assert.True(result.Width >= 640);
        Assert.True(result.Height >= 420);
    }

    private static IReadOnlyList<DisplayInfo> CreateSingleLaptopDisplay()
    {
        return
        [
            new DisplayInfo
            {
                DeviceName = "\\\\.\\DISPLAY1",
                FriendlyName = "Laptop display",
                IsPrimary = true,
                Bounds = new DisplayBounds(0, 0, 1920, 1080),
                WorkingArea = new DisplayBounds(0, 0, 1920, 1040)
            }
        ];
    }

    private static IReadOnlyList<DisplayInfo> CreateTwoDisplaySetup()
    {
        return
        [
            new DisplayInfo
            {
                DeviceName = "\\\\.\\DISPLAY1",
                FriendlyName = "Primary display",
                IsPrimary = true,
                Bounds = new DisplayBounds(0, 0, 1920, 1080),
                WorkingArea = new DisplayBounds(0, 0, 1920, 1080)
            },
            new DisplayInfo
            {
                DeviceName = "\\\\.\\DISPLAY2",
                FriendlyName = "Secondary display",
                IsPrimary = false,
                Bounds = new DisplayBounds(1920, 0, 2560, 1440),
                WorkingArea = new DisplayBounds(1920, 0, 2560, 1400)
            }
        ];
    }
}
