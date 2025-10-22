using AlbumApp1._0._1.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace AlbumApp1._0._1.Views;

public sealed partial class RegisterPageView : Page
{
    public RegisterViewModel ViewModel
    {
        get;
    }

    public RegisterPageView()
    {
        ViewModel = App.GetService<RegisterViewModel>();
        InitializeComponent();
    }
}
