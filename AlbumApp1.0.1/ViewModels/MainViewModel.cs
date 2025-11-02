using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.ViewModels.Basic;
using AlbumApp1._0._1.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using Windows.UI.ViewManagement;
using WinRT.AlbumApp1_0_1VtableClasses;

namespace AlbumApp1._0._1.ViewModels;

public partial class MainViewModel : BasedViewModelContext
{
    public readonly INavigationService NavigationService;

  
    public MainViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        //NavigationService.NavigateTo(TitleBarViewModel.GetType());
        App.GetService<TitleBarViewModel>().Enable = true;
    }
}
