using AlbumApp1._0._1.Helpers;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.ViewModels;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System.Xml.Schema;
using Windows.System;

namespace AlbumApp1._0._1.Views;

// TODO: Update NavigationViewItem titles and icons in ShellPage.xaml.
public sealed partial class AuthPageView : Page
{
 public AuthViewModel ViewModel { get; set; }
    public AuthPageView()
    {
        
        ViewModel = App.GetService<AuthViewModel>();
        Loaded += RegisterPageView_Loaded;

        InitializeComponent();

        //ViewModel.NavigationService.Frame = NavigationFrame;
        //ViewModel.NavigationViewService.Initialize(NavigationViewControl);

        // TODO: Set the title bar icon by updating /Assets/WindowIcon.ico.
        // A custom title bar is required for full window theme and Mica support.
        // https://docs.microsoft.com/windows/apps/develop/title-bar?tabs=winui3#full-customization
      
        //mainwindow.SetTitleBar(AppTitleBar);
        //mainwindow.Activated += MainWindow_Activated;
        //AppTitleBarText.Text = "AppDisplayName".GetLocalized();
    }


    private void RegisterPageView_Loaded(object sender, RoutedEventArgs e)
    {
        App.Root = this.XamlRoot;
    }

}
