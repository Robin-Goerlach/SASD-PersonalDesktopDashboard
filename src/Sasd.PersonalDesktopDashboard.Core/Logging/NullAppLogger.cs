using System;

namespace Sasd.PersonalDesktopDashboard.Core.Logging;

/// <summary>
/// Provides a safe logger implementation that deliberately ignores all log messages.
/// </summary>
/// <remarks>
/// This logger is useful as a fallback during early application startup or in tests.
/// It prevents null checks in application code and avoids crashes if the real logger
/// has not yet been configured.
/// </remarks>
public sealed class NullAppLogger : IAppLogger
{
    /// <summary>
    /// Gets a shared no-operation logger instance.
    /// </summary>
    public static NullAppLogger Instance { get; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="NullAppLogger"/> class.
    /// </summary>
    private NullAppLogger()
    {
        // Private constructor because callers should reuse the shared Instance.
    }

    /// <inheritdoc />
    public void Info(string message)
    {
        // Intentionally empty. This logger is a safe no-op fallback.
    }

    /// <inheritdoc />
    public void Warning(string message)
    {
        // Intentionally empty. This logger is a safe no-op fallback.
    }

    /// <inheritdoc />
    public void Error(string message)
    {
        // Intentionally empty. This logger is a safe no-op fallback.
    }

    /// <inheritdoc />
    public void Error(string message, Exception exception)
    {
        // Intentionally empty. This logger is a safe no-op fallback.
    }
}
