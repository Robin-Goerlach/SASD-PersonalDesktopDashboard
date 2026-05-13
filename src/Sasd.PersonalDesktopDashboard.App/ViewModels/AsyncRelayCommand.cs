using System.Windows.Input;

namespace Sasd.PersonalDesktopDashboard.App.ViewModels;

/// <summary>
/// Small command helper for asynchronous WPF commands.
/// </summary>
/// <remarks>
/// This is intentionally simple. It is good enough for the first shell and avoids pulling in a
/// larger MVVM framework before the application really needs one.
/// </remarks>
public sealed class AsyncRelayCommand : ICommand
{
    private readonly Func<Task> _executeAsync;
    private readonly Func<bool>? _canExecute;
    private bool _isExecuting;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncRelayCommand"/> class.
    /// </summary>
    /// <param name="executeAsync">The asynchronous action to execute.</param>
    /// <param name="canExecute">An optional predicate that controls command availability.</param>
    public AsyncRelayCommand(Func<Task> executeAsync, Func<bool>? canExecute = null)
    {
        _executeAsync = executeAsync;
        _canExecute = canExecute;
    }

    /// <inheritdoc />
    public event EventHandler? CanExecuteChanged;

    /// <inheritdoc />
    public bool CanExecute(object? parameter) => !_isExecuting && (_canExecute?.Invoke() ?? true);

    /// <inheritdoc />
    public async void Execute(object? parameter)
    {
        if (!CanExecute(parameter))
        {
            return;
        }

        try
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();
            await _executeAsync();
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }

    /// <summary>
    /// Notifies WPF that the command availability may have changed.
    /// </summary>
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
