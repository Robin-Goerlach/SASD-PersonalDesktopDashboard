using System.Windows;
using Sasd.PersonalDesktopDashboard.App.ViewModels;

namespace Sasd.PersonalDesktopDashboard.App;

/// <summary>
/// Main WPF window for the SASD Personal Desktop Dashboard.
/// </summary>
public partial class MainWindow : Window
{
    private readonly DashboardViewModel _viewModel;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// </summary>
    /// <param name="viewModel">The dashboard view model created in the application composition root.</param>
    public MainWindow(DashboardViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        DataContext = _viewModel;
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // WPF event handlers cannot be awaited directly by the framework. For this early shell
        // we call the async view-model method here and let the view model expose any error text.
        await _viewModel.LoadAsync();
    }
}
