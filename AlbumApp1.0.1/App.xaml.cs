using AlbumApp1._0._1.Activation;
using AlbumApp1._0._1.Core.Contracts.Services;
using AlbumApp1._0._1.Core.Services;
using AlbumApp1._0._1.Helpers;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models;
using AlbumApp1._0._1.Models.Views;
using AlbumApp1._0._1.Repositories;
using AlbumApp1._0._1.Services;
using AlbumApp1._0._1.ViewModels;
using AlbumApp1._0._1.ViewModels.Basic;
using AlbumApp1._0._1.ViewModels.SplashScreen;
using AlbumApp1._0._1.Views;
using AlbumApp1._0._1.Views.Basic;
using AlbumApp1._0._1.WindowsViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.Services.Maps;
using Windows.System;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.WebUI;
using WinRT.AlbumApp1_0_1VtableClasses;
namespace AlbumApp1._0._1;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    private MainWindow MainWindow;

    private  MainPageView? _mainview;
    private Frame frame;

    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }
    internal Window m_window;

    public Window Window => m_window;

    public static UIElement? AppTitlebar { get; set; }
    public App()
    {

        InitializeComponent();
            Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder().UseContentRoot(AppContext.BaseDirectory).ConfigureServices((context, services) =>
            {

                // Default Activation Handler
                services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

                // Other Activation Handlers

                // Services
                services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
                services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
                services.AddTransient<INavigationViewService, NavigationViewService>();

                services.AddSingleton<IActivationService, ActivationService>();
                services.AddSingleton<IPageService, PageService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ISqlConnectionStatus, SqlServerConnectionStatus>();


                //services.AddSingleton<IWindowManagerServices, WindowManagerService>();
                services.AddTransient<IDispatcherQueueService, DispatcherQueueService>();

                // Core Services
                services.AddSingleton<IFileService, FileService>();

                // Views and ViewModels
                services.AddTransient<MainViewModel>();
                services.AddTransient<RegisterViewModel>();
                services.AddTransient<AuthViewModel>();
                services.AddTransient<QuestionViewModel>();
                services.AddTransient<PhotosViewModel>();
                services.AddTransient<FeedbackViewModel>();
                services.AddTransient<FavouritesViewModel>();
                services.AddTransient<BasicViewModel>();
                services.AddTransient<ArchiveViewModel>();
                services.AddTransient<AlbumsViewModel>();
                services.AddTransient<AboutProjectViewModel>();
                services.AddTransient<SplashScreenViewModel>();
                services.AddTransient<TitleBarViewModel>();


                services.AddTransient<MainPageView>();
                services.AddTransient<AuthPageView>();
                services.AddTransient<RegisterPageView>();
                services.AddTransient<ShellView>();
                services.AddTransient<QuestionsView>();
                services.AddTransient<PhotosView>();
                services.AddTransient<FeedbackView>();
                services.AddTransient<FavouritesView>();
                services.AddTransient<ArchiveView>();
                services.AddTransient<AlbumsView>();
                services.AddTransient<AboutProjectView>();
                services.AddTransient <SplashScreenView>();
                services.AddTransient<TitleBarView>();


                services.AddTransient<MainWindow>();
                services.AddTransient<BasicWindow>();
                services.AddTransient<SplashScreenMainWindow>();
                //RepositoriesServices
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                
                // Configuration
                services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
                //services.AddDbContext<AlbumDbContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
                Microsoft.Extensions.Options.OptionsBuilder<SqlServerConnectionStatus> optionsBuilder = services.AddOptions<SqlServerConnectionStatus>()
                .BindConfiguration("ConnectionStrings")
                .Validate(c => c.Validate(), "Invalid connection string")
                .ValidateOnStart();
                //services.AddScoped<IDbContext, AlbumDbContext>();

            }).
         
            Build();
            UnhandledException += App_UnhandledException;
    }
    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        try
        {
            throw new Exception("An unhandled exception occurred!");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    
    }
    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        var mainInstance = Microsoft.Windows.AppLifecycle.AppInstance.FindOrRegisterForKey("main");
        if (!mainInstance.IsCurrent)
        {
            var activatedEventArgs =
                Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs();
            await mainInstance.RedirectActivationToAsync(activatedEventArgs);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            return;
        }

        MainWindow = App.GetService<MainWindow>();
        _mainview = GetService<MainPageView>();
        SplashScreenViewModel splashscreenViewModel = App.GetService<SplashScreenViewModel>();
        SplashScreenView splashscreenview = new SplashScreenView(splashscreenViewModel);
        SplashScreenMainWindow s_window = App.GetService<SplashScreenMainWindow>();
        
                s_window.Content = splashscreenview;
                App.GetService<IActivationService>().ActivateAsync(s_window, splashscreenview, args);
                await splashscreenViewModel.StartLoadingAsync();
     
            s_window.Close();

      
 
       
           
       
      

        //var activatedEventArgs = Microsoft.Windows.AppLifecycle.AppInstance.GetCurrent().GetActivatedEventArgs();
        //if (activatedEventArgs.Kind == Microsoft.Windows.AppLifecycle.ExtendedActivationKind.File)
        //{
        //    //await App.GetService<IActivationService>().ActivateAsync(args);
        //    MainWindow = App.GetService<MainWindow>();
        //    MainWindow.Content = rootFrame = new Frame();
        //    _dispatcherQueue.TryEnqueue(() => { MainWindow.Activate(); });
        //    rootFrame.Navigate(typeof(MainPageView));
        //}

      


        //if (MainWindow.Content==null)
        //{
            
        //    MainWindow.Content = _mainview;

        //}
        App.GetService<IActivationService>().ActivateAsync(MainWindow, _mainview, args);
        //var window = (Application.Current as App)?.MainWindow as MainWindow;


        MainWindow.Closed += async (s, e) =>
        {
            e.Handled = true;
            ContentDialog cd = new ContentDialog()
            {
              
                XamlRoot = MainWindow.Content.XamlRoot,
                PrimaryButtonText = "Да",
                SecondaryButtonText = "Нет",
                Title = "Выход",
                Content = "Желаете выйти?"

            };
            ContentDialogResult result = await cd.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                e.Handled = false;
                Environment.Exit(0);
            }
        };



    }
  
 

}
