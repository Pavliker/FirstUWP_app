using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.ViewModels.Basic;
using AlbumApp1._0._1.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using Windows.UI.ViewManagement;
using WinRT.AlbumApp1_0_1VtableClasses;

namespace AlbumApp1._0._1.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private AuthViewModel authViewModel;
    private RegisterViewModel registerViewModel;
    public readonly INavigationService navigationService;
    
    public RelayCommand? _NavigateToAuthViewCommand { get; set; }
    public RelayCommand? _NavigateToRegViewCommand {  get; set; }

    public MainViewModel(INavigationService navigationService)
    {
        this.navigationService = navigationService;
        _NavigateToAuthViewCommand = new RelayCommand(NavigateToAuth);
        _NavigateToRegViewCommand = new RelayCommand(NavigateToRegistration);
    }
    public void NavigateToAuth()
    {
            authViewModel = App.GetService<AuthViewModel>();
            navigationService.NavigateTo(authViewModel.GetType());
        
    }
    public void NavigateToRegistration() 
    {
            registerViewModel = App.GetService<RegisterViewModel>();    
            navigationService.NavigateTo(registerViewModel.GetType());
        
    }
}
