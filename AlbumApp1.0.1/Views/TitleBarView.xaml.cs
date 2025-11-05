using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using AlbumApp1._0._1.ViewModels;
using AlbumApp1._0._1.ViewModels.Basic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AlbumApp1._0._1.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class TitleBarView : Page
{
    public TitleBarViewModel TitleBarViewModel { get; }
    //public ShellViewModel _ShellViewModel { get; set; }

    public TitleBarView()
    {
        InitializeComponent();
        TitleBarViewModel = App.GetService<TitleBarViewModel>();
        //_ShellViewModel = App.GetService<ShellViewModel>();

    }

    //private void TitleBar_PaneToggleRequested(TitleBar sender, object args)
    //{

    //    if (_ShellViewModel.IsPaneOpen == false)
    //    {
    //        _ShellViewModel.IsPaneOpen = true;
    //    }
    //    else
    //    {
    //        _ShellViewModel.IsPaneOpen = false;

    //    }


    //}
}
