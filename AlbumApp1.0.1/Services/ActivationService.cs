using AlbumApp1._0._1.Activation;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Views;
using AlbumApp1._0._1.WindowsViews;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Gaming.Input;

namespace AlbumApp1._0._1.Services;

public partial class ActivationService : IActivationService 
{
    private readonly ActivationHandler<LaunchActivatedEventArgs> _defaultHandler;
    private readonly IEnumerable<IActivationHandler> _activationHandlers;
    private readonly IThemeSelectorService _themeSelectorService;


      

    public ActivationService(ActivationHandler<LaunchActivatedEventArgs> defaultHandler, IEnumerable<IActivationHandler> activationHandlers, IThemeSelectorService themeSelectorService)
    {
        _defaultHandler = defaultHandler;
        _activationHandlers = activationHandlers;
        _themeSelectorService = themeSelectorService;

    }

    public async void ActivateAsync<W, V> (W window, V view, object activationArgs) where W : Window where V : UIElement
    {
        // Execute tasks before activation.
        await InitializeAsync();

        if (window == null)
        {
            window = App.GetService<W>();
        }
        // Set the MainWindow Content.
        if (window.Content == null)
        {
            view = App.GetService<V>();
            //_main = App.GetService<V>();
            window.Content = view;
        }

        //// Handle activation via ActivationHandlers.
        await HandleActivationAsync(activationArgs);

        // Activate the MainWindow.
        window.Activate();

        //// Execute tasks after activation.
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
