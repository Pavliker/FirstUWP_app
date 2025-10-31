using AlbumApp1._0._1.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AlbumApp1._0._1.WindowsViews;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class BasicWindow : Window
{
    private Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;

    private UISettings settings;
    public BasicWindow()
    {



        InitializeComponent();
        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/WindowIcon.ico"));
        AppWindow.MoveAndResize(new Windows.Graphics.RectInt32(100, 100, 1920, 1080));
        ExtendsContentIntoTitleBar = true;
        //SetTitleBar(AppTitleBar);
        AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
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
                Content = "Желаете закрыть текущее окно?"
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
    private void Settings_ColorValuesChanged(UISettings sender, object args)
    {
        // This calls comes off-thread, hence we will need to dispatch it to current app's thread
        dispatcherQueue.TryEnqueue(() =>
        {
            TitleBarHelper.ApplySystemThemeToCaptionButtons();
        });
    }
  
}
