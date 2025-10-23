using AlbumApp1._0._1.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Runtime.CompilerServices;

namespace AlbumApp1._0._1.Views;

public sealed partial class RegisterPageView : Page
{
    public RegisterViewModel ViewModel
    {
        get;
    }

    public RegisterPageView()
    {
        InitializeComponent();
        ViewModel = App.GetService<RegisterViewModel>();
        DataContext = ViewModel; 
        Loaded += RegisterPageView_Loaded;
        //App.GetRequiredService<IApp>().Root = 
    }
    private void RegisterPageView_Loaded(object sender, RoutedEventArgs e)
    {
        App.Root = this.XamlRoot;
    }
}
