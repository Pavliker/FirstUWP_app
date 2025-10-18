using AlbumApp1._0._1.Services.Exit;
using AlbumApp1._0._1.ViewModels.SplashScreen;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.PointOfService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.UI.Notifications;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AlbumApp1._0._1.WindowsViews
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashScreenMainWindow : Window
    {

        public SplashScreenMainWindow()
        {
            AppWindow.MoveAndResize(new Windows.Graphics.RectInt32(200, 100, 1920, 1080));
            ExtendsContentIntoTitleBar = true;
           

            if (AppWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.IsResizable = false;
                presenter.IsMaximizable = false;
                presenter.IsMinimizable = false;
                presenter.SetBorderAndTitleBar(true, true);
              
            }
            AppWindow.Closing += (async (args, e) =>
            {
                e.Cancel = true;
                ContentDialog contentDialog = new ContentDialog
                {
                    XamlRoot = this.Content.XamlRoot,
                    Title = AppWindow.Title,
                    Content = "Нельзя закрывать окно!!!",
                    CloseButtonText = "Отмена",
                    PrimaryButtonText = "Закрыть"
                };
               ContentDialogResult Result =  await contentDialog.ShowAsync();
                if (Result== ContentDialogResult.Primary)
                {
                    App.Current.Exit(); 
                }
                else
                {
                    return;
                }
            });
        }
    }
}
