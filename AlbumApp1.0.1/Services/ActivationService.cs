using AlbumApp1._0._1.Activation;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Views;
using AlbumApp1._0._1.WindowsViews;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AlbumApp1._0._1.Services;

public partial class ActivationService : IActivationService
{
    private readonly  MainWindow mainwindow;
    private readonly ActivationHandler<LaunchActivatedEventArgs> _defaultHandler;
    private readonly IEnumerable<IActivationHandler> _activationHandlers;
    private readonly IThemeSelectorService _themeSelectorService;
    private UIElement? _main = null;

    public ActivationService(ActivationHandler<LaunchActivatedEventArgs> defaultHandler, IEnumerable<IActivationHandler> activationHandlers, IThemeSelectorService themeSelectorService, MainWindow mainwindow)
    {
        _defaultHandler = defaultHandler;
        _activationHandlers = activationHandlers;
        _themeSelectorService = themeSelectorService;
        this.mainwindow = mainwindow;
    }

    public async Task ActivateAsync(object activationArgs)
    {
        // Execute tasks before activation.
        await InitializeAsync();

        // Set the MainWindow Content.
        if (mainwindow.Content == null)
        {
            _main = App.GetService<AuthPageView>();
            mainwindow.Content = _main ?? new Frame();
        }

        // Handle activation via ActivationHandlers.
        await HandleActivationAsync(activationArgs);

        // Activate the MainWindow.
        mainwindow.Activate();

        // Execute tasks after activation.
        await StartupAsync();
    }

    private async Task HandleActivationAsync(object activationArgs)
    {
        var activationHandler = _activationHandlers.FirstOrDefault(h => h.CanHandle(activationArgs));

        if (activationHandler != null)
        {
            await activationHandler.HandleAsync(activationArgs);
        }

        if (_defaultHandler.CanHandle(activationArgs))
        {
            await _defaultHandler.HandleAsync(activationArgs);
        }
    }

    private async Task InitializeAsync()
    {
        await _themeSelectorService.InitializeAsync().ConfigureAwait(false);
        await Task.CompletedTask;
    }

    private async Task StartupAsync()
    {
        await _themeSelectorService.SetRequestedThemeAsync();
        await Task.CompletedTask;
    }
}
