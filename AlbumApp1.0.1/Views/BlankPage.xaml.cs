using AlbumApp1._0._1.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace AlbumApp1._0._1.Views;

public sealed partial class BlankPage : Page
{
    public BlankViewModel ViewModel
    {
        get;
    }

    public BlankPage()
    {
        ViewModel = App.GetService<BlankViewModel>();
        InitializeComponent();
    }
}
