using System;

namespace Sasd.PersonalDesktopDashboard.Core.Logging;

/// <summary>
/// Defines the minimal logging contract used by the SASD Personal Desktop Dashboard.
/// </summary>
/// <remarks>
/// The interface is intentionally small. At this project stage we only need a few
/// reliable methods for diagnostic messages. A larger logging framework can still
/// be introduced later without forcing the application code to know about the
/// concrete logging implementation.
/// </remarks>
public interface IAppLogger
{
    /// <summary>
    /// Writes an informational message to the application log.
    /// </summary>
    /// <param name="message">The diagnostic message that should be written.</param>
    void Info(string message);

    /// <summary>
    /// Writes a warning message to the application log.
    /// </summary>
    /// <param name="message">The diagnostic warning that should be written.</param>
    void Warning(string message);

    /// <summary>
    /// Writes an error message to the application log.
    /// </summary>
    /// <param name="message">The diagnostic error message that should be written.</param>
    void Error(string message);

    /// <summary>
    /// Writes an error message together with exception details to the application log.
    /// </summary>
    /// <param name="message">The diagnostic error message that should be written.</param>
    /// <param name="exception">The exception that contains the technical failure details.</param>
    void Error(string message, Exception exception);
}
