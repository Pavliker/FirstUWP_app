using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Services;
using AlbumApp1._0._1.Services.Exit;
using AlbumApp1._0._1.ViewModels.Basic;
using AlbumApp1._0._1.Views;
using AlbumApp1._0._1.Views.Basic;
using AlbumApp1._0._1.WindowsViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using Windows.ApplicationModel.UserDataAccounts;
using Windows.ApplicationModel.UserDataTasks;
using Windows.System;
using Windows.UI.Notifications;

namespace AlbumApp1._0._1.ViewModels;

public partial class AuthViewModel : BasedViewModelContext
{

    private BasicView view;
    public BasicWindow BasicWindow { get; set; }
    //public ObservableCollection<ValidateInputModel> CurrentValidateList =>
    //ActiveUser ? Users.validateInputModels : Guests.validateInputModels;
    private readonly IContentDialogExit contentDialogExit;
   private readonly IAuthenticationService authentication;
    private readonly IAuthService authService;
    private readonly IActivationService activationService;
    public Гости Guests { get; set; } = new();
    public Пользователи Users { get; set; } = new();
    private bool _activeUser;
    public bool ActiveUser
    {
        get
        {
            return _activeUser;
        }
        set
        {
            _activeUser = value;
            OnPropertyChanged(nameof(ActiveUser));
        }
    }
    private bool _activeGuest;
    public bool ActiveGuest
    {
        get
        {
            return _activeGuest;
        }
        set
        {
            _activeGuest = value;
            OnPropertyChanged(nameof(ActiveGuest));
        }
    }
    private bool _guestLogin;
    public bool GuestLogin
    {
        get => _guestLogin;
        set
        {
            if (GuestLogin == true)
            {
                _isUser = true;
                ActiveUser = true;
                _activeGuest = false;
                OnPropertyChanged(nameof(ActiveUser));

            }
            else {
                _isUser = false;
                ActiveUser = false;
                _activeGuest = true;
                Password = string.Empty;
                LoginUserOrGuest = string.Empty;
                OnPropertyChanged(nameof(ActiveUser));

            }
            SetProperty(ref _guestLogin, value);
            OnPropertyChanged(nameof(GuestLogin));
            OnPropertyChanged(nameof(IsUser));
            OnPropertyChanged(nameof(ActiveUser));
            OnPropertyChanged(nameof(ActiveGuest));
            OnPropertyChanged(nameof(Password));

        }
    }
    public string LoginUserOrGuest 
    {
        get
        {
            if (IsUser == false)
            {
                return Guests.Логин;
            }
            else
            {
                return Users.Логин;
            }
        }
        set
        {
            if (IsUser == false)
            {
                Guests.Логин = value;   
            }
            else
            {
                Users.Логин = value;
             
            
            }
            OnPropertyChanged(nameof(IsUser));
            OnPropertyChanged(nameof(LoginUserOrGuest));
        }
    }
    public string Password
    {
        get
        {
            return Users.ХешированныйПароль;
        }
        set
        {
            Users.ХешированныйПароль = value;
            OnPropertyChanged(nameof(Password));
        }
    }
    private bool _isUser;
    public bool IsUser
    {
        get
        {
            return _isUser;
        }
        set
        {
            SetProperty(ref _isUser, value);
            OnPropertyChanged(nameof(IsUser));  
        }
    }
    private string _text;
    public string Text
    {
        get=>_text;set {
        SetProperty(ref _text, value);
            OnPropertyChanged(nameof(Text));
        
        }
    }

    private readonly IDialogService dialogService;

    public AuthViewModel(IContentDialogExit contentDialogExit, IAuthService authService, IActivationService activationService, IDialogService DialogService)
    {
        this.contentDialogExit = contentDialogExit;
        IsUser = true;
        ActiveUser = true;
      
        this.authService = authService;
        this.activationService = activationService;
        dialogService = DialogService;
        authentication = App.GetService<IAuthenticationService>();
      //BasicWindow = App.GetService<BasicViewModel>();      
    }
    [RelayCommand]
    public async Task ShowContentDialogForBackUp()
    {
        
        var model = App.GetService<MailSendViewModel>();
        if (await dialogService.Show(model) == true)
        {
                _text = $"Вы нажали да, ваша почта была {model.Mail}";

        }
        else
        {
            _text = "Вы нажали  нет";
        }

    }

    [RelayCommand]
    public async Task LogInAsyncCommand()
    {
        bool isinrole = false;
        if (IsUser == true && Password != null)
        {
            if (Users.HasErrors == true)
            {
                if (await contentDialogExit.OpenContentDialog("Ошибка ввода данных") == true)
                {

                    return;
                }
                else
                {

                    return;
                }
            }
            else {
                int result = await authService.AuthorizationResult(LoginUserOrGuest, Password);
                 isinrole = await authentication.IsInRole(result);
            }

        }
        else
        {
            if (Guests.HasErrors == true)
            {
                if (await contentDialogExit.OpenContentDialog("Ошибка ввода данных") == true)
                {

                    return;
                }
                else
                {

                    return;
                }
            }
            else
            {

                var guest = await authService.RegisterGuest(LoginUserOrGuest);
                int result = authService.AuthorizationResult(guest);
                isinrole = await authentication.IsInRole(result);

            }
        }

        if (isinrole == true)
        {
           
            view = App.GetService<BasicView>();
            //var main = App.GetService<MainViewModel>();
            //var basic = App.GetService<BasicWindow>();
            var basicViewModel = App.GetService<BasicViewModel>();
            BasicWindow = App.GetService<BasicWindow>();
            App.GetService<IActivationService>().RegisterMapping<BasicViewModel, BasicWindow>(BasicWindow);

            activationService.OpenWindow(basicViewModel, view);
            //activationService.RegisterInstance(basic.GetType(), basic);

            //var win = (App.Current as App)?.MainWindow;
            //activationService._MainWindow = win;
         
            activationService.CloseWindow<MainViewModel>();

        }
        else
        {
            if (await contentDialogExit.OpenContentDialog("Данный пользователь не был авторизован") == true)
            {
                return;
            }
            else
            {
                return;
            }
        }
      
    }
}
