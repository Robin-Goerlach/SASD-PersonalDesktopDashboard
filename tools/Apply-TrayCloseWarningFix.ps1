<#
.SYNOPSIS
    Removes the CS0162 unreachable-code warning from MainWindow.xaml.cs.

.DESCRIPTION
    This small patch changes the tray-close configuration field from a compile-time
    constant to a static readonly field. The runtime behavior stays the same:
    clicking the window X hides the dashboard to the tray, while the tray menu can
    still request a real application shutdown.

    Why this fixes the warning:
    A const bool with value true is known at compile time. Therefore the compiler
    knows that the branch "if (!HideWindowToTrayWhenClosedByUser)" can never run
    and reports CS0162. A static readonly bool is intentionally not treated as a
    compile-time constant, so the branch remains reachable from the compiler's
    perspective and can later be replaced by a user setting.

.USAGE
    Run this script from the repository root:

        powershell -ExecutionPolicy Bypass -File .\tools\Apply-TrayCloseWarningFix.ps1

    or, in PowerShell 7:

        pwsh -ExecutionPolicy Bypass -File .\tools\Apply-TrayCloseWarningFix.ps1
#>

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$relativePath = 'src/Sasd.PersonalDesktopDashboard.App/MainWindow.xaml.cs'
$filePath = Join-Path (Get-Location) $relativePath

if (-not (Test-Path -LiteralPath $filePath)) {
    throw "MainWindow.xaml.cs was not found at expected path: $relativePath. Please run this script from the repository root."
}

$oldLine = 'private const bool HideWindowToTrayWhenClosedByUser = true;'
$newLine = 'private static readonly bool HideWindowToTrayWhenClosedByUser = true;'

$content = Get-Content -LiteralPath $filePath -Raw

if ($content.Contains($newLine)) {
    Write-Host 'Patch already applied: HideWindowToTrayWhenClosedByUser is already static readonly.'
    exit 0
}

if (-not $content.Contains($oldLine)) {
    throw "Expected line was not found. The file may already have changed. Search manually for: $oldLine"
}

$content = $content.Replace($oldLine, $newLine)
Set-Content -LiteralPath $filePath -Value $content -NoNewline -Encoding UTF8

Write-Host 'Patch applied successfully.'
Write-Host "Updated: $relativePath"
Write-Host 'Next commands:'
Write-Host '  dotnet restore'
Write-Host '  dotnet build'
Write-Host '  dotnet test'
Write-Host '  dotnet run --project src/Sasd.PersonalDesktopDashboard.App/Sasd.PersonalDesktopDashboard.App.csproj'
