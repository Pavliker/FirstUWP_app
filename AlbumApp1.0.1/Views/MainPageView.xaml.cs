using AlbumApp1._0._1.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AlbumApp1._0._1.Views;

public sealed partial class MainPageView : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPageView()
    {
        InitializeComponent();
        ViewModel = App.GetService<MainViewModel>();
        Loaded += RegisterPageView_Loaded;
    }
    private void RegisterPageView_Loaded(object sender, RoutedEventArgs e)
    {
        App.Root = this.XamlRoot;
    }
}
