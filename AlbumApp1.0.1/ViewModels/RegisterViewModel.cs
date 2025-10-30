using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;

using AlbumApp1._0._1.Views.Basic;

using AlbumApp1._0._1.WindowsViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Windows.Foundation.Collections;

namespace AlbumApp1._0._1.ViewModels;
public partial class RegisterViewModel : BasedViewModelContext
{
    public Пользователи Users { get; set; } = new();

    private readonly IDispatcherQueueService _queueService;


    //[ObservableProperty]
    //public partial string ErrorMessage { get; protected set; } = string.Empty;

    public  string Login { 
        get=> Users.Логин;
        set 
        {
         Users.Логин = value;
         OnPropertyChanged(nameof(Login));
//OnPropertyChanged(nameof(Users.validateInputModels));
        }
         }
    public  string Hash
    {
        get=>Users.ХешированныйПароль;
        set 
        {
            Users.ХешированныйПароль = value;
            OnPropertyChanged(nameof(Hash));
        }
    }
    public  string Mail
    {
        get=> Users.НазваниеПочты;
        
        
        set { Users.НазваниеПочты = value;
            OnPropertyChanged(nameof(Mail));
        }
    }
    private bool _isChecked;
    public bool IsCheckConf
    {
        get
        {
            return _isChecked;
        }
        set
        {
            SetProperty(ref _isChecked, value);
            OnPropertyChanged(nameof(IsCheckConf));
        }
    }
    public readonly IRegistrationService registrationService;
    private readonly IAuthenticationService authenticationService;
    private readonly IActivationService activationService;
    //private AsyncRelayCommand _registerCommand;

    //public IAsyncRelayCommand RegisterCommand => _registerCommand ??= new AsyncRelayCommand(RegisterUser);
    private readonly IContentDialogExit contentDialogExit;
    [ObservableProperty]
    public partial string RepeatedPassword { get; set; }

    public RegisterViewModel(IRegistrationService registrationService, IContentDialogExit contentDialogExit, IAuthenticationService authenticationService, IActivationService activationService, IDispatcherQueueService dispatcherQueueService)
    {
        this.registrationService = registrationService;
        this.contentDialogExit = contentDialogExit;
        this.authenticationService = authenticationService;
        this.activationService = activationService;
        _queueService = dispatcherQueueService;
    }
    private string _text;
    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            SetProperty(ref _text, value);
            OnPropertyChanged(nameof(Text));
        }
    }
    public async void  IsWrong()
    {
            await Task.Run( () => 
            {    Task.Delay(500);
                _queueService.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.High, async () =>
                {
                    _text = "";
                    OnPropertyChanged(nameof(Text));
                });
            });
    }
   [RelayCommand]
    public async Task RegisterUser()
    {
        
        if (RepeatedPassword == null || Hash == null || IsCheckConf == false || Users.HasErrors == true)
        {
            _text = "Неправильный ввод";
            if (await contentDialogExit.OpenContentDialog("Ошибка ввода данных") == true)
            {
                await Task.Run(() =>
                {
                    Task.Delay(500);
                    IsWrong();
                });
                return;
            }
            else
            {
                await Task.Run(() =>
                {
                    Task.Delay(500);
                    IsWrong();
                });
                return;
            }
        }
        else if (RepeatedPassword.Equals(Hash) && IsCheckConf == true)
        {
            Users = await registrationService.RegisterUser(Login, Hash, Mail);
            authenticationService.AuthorizationUser(Users);
        
        }
        if (Users == null || authenticationService.IsAuthenticated == false)
        {
            if (await contentDialogExit.OpenContentDialog("Пользователь не был авторизован!!!") == true)
            {
                IsWrong();
            }
            else
            {
                IsWrong();
            }
        }
        else
        {
            bool values = await authenticationService.IsInRole(Users.КодРоли);
            if (values == true)
            {
               
                var window = App.GetService<BasicWindow>();
                var view = App.GetService<BasicView>();
                activationService.OpenWindow(window, view);
                var win = (App.Current as App)?.MainWindow;
                activationService._MainWindow = win;
                activationService.CloseWindow<MainWindow>();
            }
            else
            {
                await contentDialogExit.OpenContentDialog("По неясным причинам авторизация не пройшла");
            }
        }
    }
}