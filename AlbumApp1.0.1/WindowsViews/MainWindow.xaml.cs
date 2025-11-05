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

    private UISettings settings;

    public MainWindow()
    {
        InitializeComponent();

     
            AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/WindowIcon.ico"));
            AppWindow.MoveAndResize(new Windows.Graphics.RectInt32(100, 100, 1920, 1080));
     
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
            Title = "AppDisplayName".GetLocalized();
        dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        settings = new UISettings();
        settings.ColorValuesChanged += Settings_ColorValuesChanged;
        AppWindow.Closing += async (s, e) =>
        {
            e.Cancel = true;
            ContentDialog cd = new ContentDialog()
            {
                XamlRoot = App.Root,
                PrimaryButtonText = "Да",
                SecondaryButtonText = "Нет",
                Title = "Закрытие",
                Content = "Желаете выйти с программы?"
            };
            ContentDialogResult result = await cd.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Environment.Exit(0);
            }
            else
            {
                return;
            }
        };

    }


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
