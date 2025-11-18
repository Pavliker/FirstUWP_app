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
    private readonly IObjectManager ObjectManager;
   
    public Window window { get; private set; }

    //public ObservableCollection<ValidateInputModel> CurrentValidateList =>
    //ActiveUser ? Users.validateInputModels : Guests.validateInputModels;
    private readonly IContentDialogExit contentDialogExit;
    private readonly IAuthenticationService authentication;
    private readonly IAuthService authService;
    private readonly IActivationService activationService;
    public Гости Guests { get; set; }
    private Пользователи _users;
    public Пользователи Users { get=>_users; set {
            if (_users!=value)
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            } 
        
        } }
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
                Guests.Логин = string.Empty;
                OnPropertyChanged(nameof(ActiveUser));
            }
            else {
                _isUser = false;
                ActiveUser = false;
                _activeGuest = true;
                Users.ХешированныйПароль = "12345";
                Users.Логин = "12345";
                OnPropertyChanged(nameof(ActiveUser));
            }
            SetProperty(ref _guestLogin, value);
            OnPropertyChanged(nameof(GuestLogin));
            OnPropertyChanged(nameof(IsUser));
            OnPropertyChanged(nameof(ActiveUser));
            OnPropertyChanged(nameof(ActiveGuest));
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(Guests.Логин));

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
            OnPropertyChanged(nameof(Guests.Логин));
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
                //return _isUser;
        }
        set
        {
            if (!SetProperty(ref _isUser, value))
            {
                SetProperty(ref _isUser, value);
                OnPropertyChanged(nameof(IsUser));

            }


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
        ObjectManager = App.GetService<IObjectManager>();
        this.contentDialogExit = contentDialogExit;
        IsUser = true;
        ActiveUser = true;
      
        this.authService = authService;
        this.activationService = activationService;
        dialogService = DialogService;
        authentication = App.GetService<IAuthenticationService>();
        _users = (Пользователи?)ObjectManager.TakeObject(Users);

        Guests = (Гости?)ObjectManager.TakeObject(Guests);
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
                if (result >= 0)
                {
                    isinrole = await authentication.IsInRole(result);
                }
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
                int result =await authService.AuthorizationResult(guest);
                isinrole = await authentication.IsInRole(result);

            }
        }

        if (isinrole == true)
        {
           
            var view = App.GetService<BasicView>();
            //var main = App.GetService<MainViewModel>();
            //var basic = App.GetService<BasicWindow>();
            //basicViewModel = App.GetService<BasicViewModel>();

            //window = BasicWindow;
            ObjectManager.BasicWindow = App.GetService<BasicWindow>();
            ObjectManager.basicViewModel = App.GetService<BasicViewModel>();
            App.GetService<IActivationService>().RegisterMapping<BasicViewModel, BasicWindow>(ObjectManager.BasicWindow);

            activationService.OpenWindow(ObjectManager.basicViewModel, view);
            //activationService.RegisterInstance(basic.GetType(), basic);

            //var win = (App.Current as App)?.MainWindow;
            //activationService._MainWindow = win;
         
            activationService.CloseWindow<MainViewModel>();

        }
        else
        {
            if (await contentDialogExit.OpenContentDialog("Данный пользователь не был авторизован!! Неправильные данные") == true)
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
