using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.ViewModels.Basic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.PointOfService.Provider;
using WinRT.AlbumApp1_0_1VtableClasses;

namespace AlbumApp1._0._1.ViewModels
{
    public partial class TitleBarViewModel: BasedViewModelContext
    {
        private bool _isDisposed;
        public IAuthenticationService authentication { get; set; }
        public string Username
        {
            get
            {
                if (!string.IsNullOrEmpty(authentication.AuthenticationName) )
                {
                    return  authentication.AuthenticationName.Substring(0,2).ToUpper();
                }
                else
                {
                    return string.Empty;
                }

            }
        }
        public string Role
        {
            get
            {
                if (authentication.RoleName != null && authentication.IsAuthenticated == true)
                {
                    return authentication.RoleName;
                }
                else
                {
                    return string.Empty;
                }

            }
        }
        private bool _enable;
        public bool Enable
        {
            get => _enable;
            set
            {

                _enable = value;
                OnPropertyChanged(nameof(Enable));
            }
        }
        private AuthViewModel authViewModel;
        private RegisterViewModel registerViewModel;
        private ProfileViewModel profileViewModel;
        public readonly INavigationService navigationService;

        public RelayCommand? _NavigateToAuthViewCommand { get; set; }
        public RelayCommand? _NavigateToRegViewCommand { get; set; }

        public TitleBarViewModel( INavigationService navigationService)
        {
            authentication = App.GetService<IAuthenticationService>();
            authViewModel = App.GetService<AuthViewModel>();
            registerViewModel = App.GetService<RegisterViewModel>();
            profileViewModel = App.GetService<ProfileViewModel>();
            this.navigationService = navigationService;
            _NavigateToAuthViewCommand = new RelayCommand(NavigateToAuth);
            _NavigateToRegViewCommand = new RelayCommand(NavigateToRegistration);
            Enable = true;
           
            //if (!App.GetService<BasicViewModel>().IsPaneOpen)
            //{
            //    App.GetService<BasicViewModel>().IsPaneOpen = true;
            //}
            //if (authentication.IsAuthenticated == false)
            //{
            //    Enable = true;
            //}
            //else
            //{
            //    Enable = false;
            //}

        }
        public void NavigateToAuth()
        {
            navigationService.NavigateTo(authViewModel.GetType());

        }
        public void NavigateToRegistration()
        {
            navigationService.NavigateTo(registerViewModel.GetType());

        }
        [RelayCommand]
        public void ProfileSettings()
        {
            navigationService.NavigateTo(profileViewModel.GetType());
        }
        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (authentication != null)
                    {
                        authentication._identityRole.Dispose();
                        authentication.Dispose();
                    }
                    if (authViewModel !=null) 
                    {
                        authViewModel.Dispose();
                        authViewModel = null;
                    }
                    if (registerViewModel!=null)
                    {
                        registerViewModel.Dispose();
                        registerViewModel = null;
                    }
                    if (profileViewModel!=null)
                    {
                        profileViewModel.Dispose();
                        profileViewModel = null;
                    }

                }
                base.Dispose(disposing);
            }
            _isDisposed = true;
        }
        ~TitleBarViewModel()
        {
            Dispose(false);
        }
    }
}
