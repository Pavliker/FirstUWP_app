using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.ViewModels.Basic
{
   public partial class BasicViewModel: BasedViewModelContext, IDisposable
    {
        private bool disposed;
        private TitleBarViewModel TitleBarViewModel { get; set; }
        private readonly IAuthenticationService authenticationService;
      
        [ObservableProperty]
        public partial ShellViewModel ViewModel { get; set; }
        public INavigationService NavigationService { get; set; }
        public string Username
        {
            get
            {
                if (authenticationService.AuthenticationName != null && authenticationService.AuthenticationName!=string.Empty && authenticationService.IsAuthenticated == true)
                {
                    return authenticationService.AuthenticationName;
                }
                else
                {
                    return string.Empty;
                }

            }
        }
        public BasicViewModel()
        {
            ViewModel = App.GetService<ShellViewModel>();
            NavigationService = App.GetService<INavigationService>();
            authenticationService = App.GetService<IAuthenticationService>();
            if (TitleBarViewModel == null)
            {
                TitleBarViewModel = App.GetService<TitleBarViewModel>();
            }
            else
            {
                TitleBarViewModel = App.GetService<TitleBarViewModel>();
            }
            //if (TitleBarViewModel.Enable == true)
            //{
            //    TitleBarViewModel.Enable = false;
            //}
            //else
            //{
            //    TitleBarViewModel.Enable = true;
            //}

            if (authenticationService.IsAuthenticated == false)
            {
                TitleBarViewModel.Enable = true;
            }
            else
            {
                TitleBarViewModel.Enable = false;
            }

        }

     protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (authenticationService!=null)
                    {
                        authenticationService.Dispose();
                    }
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~BasicViewModel()
        {
            Dispose(false);
        }
    }
}
