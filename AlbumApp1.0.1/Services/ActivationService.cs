using AlbumApp1._0._1.Activation;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.ViewModels;
using AlbumApp1._0._1.ViewModels.Basic;
using AlbumApp1._0._1.ViewModels.SplashScreen;
using AlbumApp1._0._1.Views;
using AlbumApp1._0._1.Views.Basic.Users;
using AlbumApp1._0._1.WindowsViews;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.Marshalling;
using Windows.ApplicationModel.Store;
using Windows.Devices.PointOfService;
using Windows.Gaming.Input;
using WinRT.AlbumApp1_0_1VtableClasses;

namespace AlbumApp1._0._1.Services;

public partial class ActivationService : IActivationService 
{
    public readonly Dictionary<Type, Window> _mappings = new();

    public MainWindow _MainWindow { get; set; }
    public BasicWindow _BasicWindow { get; set; }

    private readonly ActivationHandler<LaunchActivatedEventArgs> _defaultHandler;
    private readonly IEnumerable<IActivationHandler> _activationHandlers;
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly IContentDialogExit _contentDialogExit;
    public void RegisterMapping <TViewModel, TWindow>(TWindow window) where TViewModel : class where TWindow : Window
    {
        _mappings[typeof(TViewModel)] = window;
    }
      
    public Window? GetWindowTypeForViewModel(Type viewModelType)
    {
        _mappings.TryGetValue(viewModelType, out var type);
        return type;
    }
   

   
   
   
    private readonly IDispatcherQueueService dispatcher;
    public ActivationService( ActivationHandler<LaunchActivatedEventArgs> defaultHandler, IEnumerable<IActivationHandler> activationHandlers, IThemeSelectorService themeSelectorService)
    {
        _defaultHandler = defaultHandler;
        _activationHandlers = activationHandlers;
        _themeSelectorService = themeSelectorService;
        dispatcher = App.GetService<IDispatcherQueueService>();
        _contentDialogExit = App.GetService<IContentDialogExit>();
        //_BasicWindow = App.GetService<BasicWindow>();
        //_MainWindow = App.GetService<MainWindow>();
        //RegisterMapping<BasicViewModel,BasicWindow>(_BasicWindow);
        //RegisterMapping<MainViewModel, MainWindow>(_MainWindow);
       
      
    }

    public async void ActivateAsync<W, V> (W window, V view, object activationArgs) where W : Window where V : UIElement
    {
        // Execute tasks before activation.
        await InitializeAsync();

        if (window == null)
        {
            window = App.GetService<W>();
            // Set the MainWindow Content.
            if (window.Content == null)
            {
                view = App.GetService<V>();
                //_main = App.GetService<V>();
                window.Content = view;
            }
        }
        else
        {
            //// Handle activation via ActivationHandlers.
            await HandleActivationAsync(activationArgs);

            // Activate the MainWindow.
            window.Content = view;
            window.Activate();

            //// Execute tasks after activation.
            await StartupAsync();
        }
          
   }
    public async void OpenWindow<WM,V>(WM viewModel, V view) where WM : class where V : UIElement
    {
        await InitializeAsync();
      
        //List<Type> lst = new List<Type>();
        //int count = 0;
        //foreach (var i in _mappings.Keys)
        //{
            
        //    if (_mappings.ContainsKey(i.GetType()) == true)
        //    {
        //        lst.Add(i.GetType());
        //        count++;
        //        if (count>1)
        //        {
        //            if (await _contentDialogExit.OpenContentDialog("Уже существует!!!") == true)
        //            {
        //                return;
        //            }
        //            else
        //            {
        //                return;
        //            }
        //        }
        //    }
         
        //}
        //if (lst.Count>0)
        //{
        //    count = 0;
        //    foreach (var i in lst)
        //    {
        //        if (_mappings.ContainsKey(i))
        //        {
        //            count++;
        //            while (count >= 1)
        //            {
        //                _mappings.Remove(i);
        //            }
        //        }
        //    }
        //}
       
     
            var windowtype = GetWindowTypeForViewModel(viewModel.GetType());
            if (windowtype != null)
            {
                //var win = Activator.CreateInstance(windowtype) as Window;
                if (windowtype is not null)
                {
                    windowtype.Content = view;
                    windowtype.Activate();
                }
            }
            await StartupAsync();
   
       
    }
  
    public void CloseWindow<T>() where T : class
    {
        //var windowtype = GetExistingWindow(viewModel.GetType());
        var windowtype = GetWindowTypeForViewModel(typeof(T));
        //var window =  GetExistingWindow(viewModel.GetType());
        ////var window = windowtype.Equals(AppWindow);
        //if (window.GetType() == windowtype)
        //{
        //    window.Close();
        //}

        //if (!_mappings.TryGetValue(windowtype, out var window) || window is null)
        //    return;



        // If we're already on the UI thread
        if (dispatcher.GetDispatcherQueue().HasThreadAccess)
        {
            try
            {
                if (windowtype!=null)
                {
                    windowtype.Close();
                }
                //_mappings.Remove(windowtype);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to close {windowtype}: {ex.Message}");
            }
        }
        else
        {
            // Run on the UI thread
            _ = dispatcher.GetDispatcherQueue().TryEnqueue(() =>
            {
                try
                {
                    if (windowtype != null)
                    {
                        windowtype.Close();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to close {windowtype}: {ex.Message}");
                }
            });
        }
    

        //var menu = windowtype;
        //switch (menu)
        //{
        //    case var value when value == window.GetType():
        //        {
        //            window.Close();
        //            break;
        //        }


        //}
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
