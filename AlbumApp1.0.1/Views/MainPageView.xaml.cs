using AlbumApp1._0._1.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace AlbumApp1._0._1.Views;

public sealed partial class MainPageView : UserControl
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPageView()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
