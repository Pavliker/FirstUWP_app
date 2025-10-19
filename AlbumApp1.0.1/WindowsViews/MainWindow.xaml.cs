using AlbumApp1._0._1.Helpers;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using System.Drawing;
using Windows.UI.ViewManagement;

namespace AlbumApp1._0._1.WindowsViews;

public sealed partial class MainWindow /*: WindowEx*/
{
    private Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;

    //private UISettings settings;

    public MainWindow()
    {
        InitializeComponent();

     
            AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/WindowIcon.ico"));
            AppWindow.MoveAndResize(new Windows.Graphics.RectInt32(100, 100, 1920, 1080));
            ExtendsContentIntoTitleBar = true;
            //SetTitleBar(AppTitleBar);
            AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
          
            //TitleBarHelper.ApplySystemThemeToCaptionButtons();
            //TitleBarHelper.UpdateTitleBar(ElementTheme.Default);
                        //var titleBar = AppWindow.TitleBar;
                        //titleBar.ExtendsContentIntoTitleBar = true;

                        //if (AppWindowTitleBar.IsCustomizationSupported() && AppWindow.Presenter is OverlappedPresenter presenter)
                        //{
                        //    presenter.SetBorderAndTitleBar(false, true);

                        //    //var colorstitlebar = ApplicationView.GetForCurrentView().TitleBar;
                        //    titleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
                        //    titleBar.ButtonInactiveBackgroundColor = Colors.AliceBlue;

                        //}
                        Content = null;
            Title = "AppDisplayName".GetLocalized();

      
        //dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        //settings = new UISettings();
        //settings.ColorValuesChanged += Settings_ColorValuesChanged; 

    }

    //private bool SetTitleBarColors()
    //{
    //    // Check to see if customization is supported.
    //    // The method returns true on Windows 10 since Windows App SDK 1.2, and on all versions of
    //    // Windows App SDK on Windows 11.
    //    if (AppWindowTitleBar.IsCustomizationSupported())
    //    {
    //        if (m_AppWindow is null)
    //        {
    //            m_AppWindow = GetAppWindowForCurrentWindow();
    //        }
    //        var titleBar = m_AppWindow.TitleBar;

    //        // Set active window colors
    //        // Note: No effect when app is running on Windows 10 since color customization is not
    //        // supported.
    //        titleBar.ForegroundColor = Colors.White;
    //        titleBar.BackgroundColor = Colors.Green;
    //        titleBar.ButtonForegroundColor = Colors.White;
    //        titleBar.ButtonBackgroundColor = Colors.SeaGreen;
    //        titleBar.ButtonHoverForegroundColor = Colors.Gainsboro;
    //        titleBar.ButtonHoverBackgroundColor = Colors.DarkSeaGreen;
    //        titleBar.ButtonPressedForegroundColor = Colors.Gray;
    //        titleBar.ButtonPressedBackgroundColor = Colors.LightGreen;

    //        // Set inactive window colors
    //        // Note: No effect when app is running on Windows 10 since color customization is not
    //        // supported.
    //        titleBar.InactiveForegroundColor = Colors.Gainsboro;
    //        titleBar.InactiveBackgroundColor = Colors.SeaGreen;
    //        titleBar.ButtonInactiveForegroundColor = Colors.Gainsboro;
    //        titleBar.ButtonInactiveBackgroundColor = Colors.SeaGreen;
    //        return true;
    //    }
    //    return false;
    //}
     //this handles updating the caption button colors correctly when indows system theme is changed
     //while the app is open
    private void Settings_ColorValuesChanged(UISettings sender, object args)
    {
        // This calls comes off-thread, hence we will need to dispatch it to current app's thread
        dispatcherQueue.TryEnqueue(() =>
        {
            TitleBarHelper.ApplySystemThemeToCaptionButtons();
        });
    }
}
