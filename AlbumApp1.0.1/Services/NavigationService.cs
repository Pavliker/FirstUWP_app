using System.Diagnostics.CodeAnalysis;
using AlbumApp1._0._1.Contracts.ViewModels;
using AlbumApp1._0._1.Helpers;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.ViewModels;
using AlbumApp1._0._1.ViewModels.Basic;
using AlbumApp1._0._1.WindowsViews;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace AlbumApp1._0._1.Services;

// For more information on navigation between pages see
// https://github.com/microsoft/TemplateStudio/blob/main/docs/WinUI/navigation.md
public partial class NavigationService : INavigationService
{
    private readonly IPageService _pageService;
  
    public  Type CurrentView { get; set; }


    private object? _lastParameterUsed;
    private Frame? _frame ;
    public MainWindow mainwindow { get; set; }
    public BasicWindow basicWindow { get; set; }

    public event NavigatedEventHandler? Navigated;

    public Window GetCurrentWindow()
    {
        return (Application.Current as App).MainWindow;
    }
   
    public Frame? Frame
    {
        get
        {
            _frame = new Frame();
            //if (_frame != null)
            //    return _frame;
            

                var activation = App.GetService<IActivationService>();
            var auth = App.GetService<AuthViewModel>();
            var reg = App.GetService<RegisterViewModel>();
            var detailed = App.GetService<PhotosViewModel>();
            if (auth != null)
            {
                activation.RegisterMapping<BasicViewModel, BasicWindow>(auth.BasicWindow);
            }
            if (reg != null)
            {
                activation.RegisterMapping<BasicViewModel, BasicWindow>(reg.BasicWindow);
            }
            if (detailed!=null)
            {
                activation.RegisterMapping<DetailedPhotosViewModel, DetailedWindow>(detailed.detailPhotoWindow);
            }
            if (_frame == null)
            {

                if (GetCurrentWindow() != null)
                {
                    _frame = GetCurrentWindow().Content as Frame;


                }
                else if (auth != null)
                {

                    var authwin = activation.GetWindowTypeForViewModel(auth.GetType());
                    if (authwin != null)
                    {
                        _frame = authwin.Content as Frame;

                    }
                }
                else if (reg != null)
                {

                    var regwin = activation.GetWindowTypeForViewModel(reg.GetType());
                    if (regwin!=null)
                    {
                        _frame = regwin.Content as Frame;
                    }

                }
                else if (detailed != null)
                {
                    var detailedwin = activation.GetWindowTypeForViewModel(detailed.GetType());
                    if (detailedwin!=null)
                    {
                        _frame = detailedwin.Content as Frame;

                    }
                }
                else
                {
                    return new Frame();
                }
          
            }
            RegisterFrameEvents();
            return _frame;
        }

        set
        {
            UnregisterFrameEvents();
            _frame = value;
            RegisterFrameEvents();
        }
    }

    [MemberNotNullWhen(true, nameof(Frame), nameof(_frame))]
    public bool CanGoBack => Frame != null && Frame.CanGoBack;

    public NavigationService(IPageService pageService)
    {
       
        _pageService = pageService;
    
    }

    private void RegisterFrameEvents()
    {
        if (_frame != null)
        {
            _frame.Navigated += OnNavigated;
        }
    }

    private void UnregisterFrameEvents()
    {
        if (_frame != null)
        {
            _frame.Navigated -= OnNavigated;
        }
    }

    public bool GoBack()
    {
        if (CanGoBack)
        {
            var vmBeforeNavigation = _frame.GetPageViewModel();
            _frame.GoBack();
            if (vmBeforeNavigation is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedFrom();
            }

            return true;
        }

        return false;
    }

    public bool NavigateTo(Type pageKey, object? parameter = null, bool clearNavigation = false)
    {
        var pageType = _pageService.GetPageType(pageKey);

        if (_frame != null && (_frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed))))
        {
            _frame.Tag = clearNavigation;
            var vmBeforeNavigation = _frame.GetPageViewModel();
            var navigated = _frame.Navigate(pageType, parameter);
            if (navigated)
            {
                _lastParameterUsed = parameter;
                if (vmBeforeNavigation is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedFrom();
                }
            }

            return navigated;
        }

        return false;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (sender is Frame frame)
        {
            var clearNavigation = (bool)frame.Tag;
            if (clearNavigation)
            {
                frame.BackStack.Clear();
            }

            if (frame.GetPageViewModel() is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(e.Parameter);
            }

            Navigated?.Invoke(sender, e);
        }
    }
}
