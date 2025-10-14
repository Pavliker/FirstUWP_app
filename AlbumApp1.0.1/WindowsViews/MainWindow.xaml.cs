using AlbumApp1._0._1.Helpers;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Diagnostics;
using System.Drawing;
using Windows.UI.ViewManagement;

namespace AlbumApp1._0._1.WindowsViews;

public sealed partial class MainWindow /*: WindowEx*/
{
    //private Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;

    //private UISettings settings;

    public MainWindow()
    {
        InitializeComponent();

     
        try
        {

            AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/WindowIcon.ico"));
            Content = null;
            Title = "AppDisplayName".GetLocalized();
            this.AppWindow.MoveAndResize(new Windows.Graphics.RectInt32(100, 100, 1920, 1080));

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
        }
        //dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        //settings = new UISettings();
        //settings.ColorValuesChanged += Settings_ColorValuesChanged; 

    }

    // this handles updating the caption button colors correctly when indows system theme is changed
    // while the app is open
    //private void Settings_ColorValuesChanged(UISettings sender, object args)
    //{
    //    // This calls comes off-thread, hence we will need to dispatch it to current app's thread
    //    dispatcherQueue.TryEnqueue(() =>
    //    {
    //        TitleBarHelper.ApplySystemThemeToCaptionButtons();
    //    });
    //}
}
