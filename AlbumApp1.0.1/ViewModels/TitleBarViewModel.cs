using AlbumApp1._0._1.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.ViewModels
{
    public partial class TitleBarViewModel:ObservableObject
    {
        private AuthViewModel authViewModel;
        private RegisterViewModel registerViewModel;
        public readonly INavigationService navigationService;

        public RelayCommand? _NavigateToAuthViewCommand { get; set; }
        public RelayCommand? _NavigateToRegViewCommand { get; set; }

        public TitleBarViewModel(INavigationService navigationService)
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
}
