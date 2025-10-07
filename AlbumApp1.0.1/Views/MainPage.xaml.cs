using AlbumApp1._0._1.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace AlbumApp1._0._1.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
