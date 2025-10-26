using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Views.Basic;
using AlbumApp1._0._1.WindowsViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AlbumApp1._0._1.ViewModels;
public partial class RegisterViewModel : ObservableRecipient
{

    public string Login { get=> Users.Логин; set { Users.Логин = value; OnPropertyChanged(nameof(Login)); } }
    public string Hash { get => Users.ХешированныйПароль; set
        {
            Users.ХешированныйПароль = value;
            OnPropertyChanged(nameof(Hash));
        }
    }
    public string Mail
    {
        get
        {
            return Users.НазваниеПочты;
        }
        set
        {
            Users.НазваниеПочты = value;
            OnPropertyChanged(nameof(Mail));
        }
    }
    private  bool  _isChecked;
    public bool IsCheckConf {
        get {
            return _isChecked;
        } 
        set {

                SetProperty(ref _isChecked, value);
                OnPropertyChanged(nameof(IsCheckConf));
            
            } 
    }
    public readonly IRegistrationService registrationService;
    private readonly IAuthenticationService authenticationService;
    private readonly IActivationService activationService;
    public Пользователи Users { get; set; } = new();
    //private AsyncRelayCommand _registerCommand;
    
    //public IAsyncRelayCommand RegisterCommand => _registerCommand ??= new AsyncRelayCommand(RegisterUser);
    private readonly IContentDialogExit contentDialogExit;
    [ObservableProperty]
    public partial string RepeatedPassword {  get; set; }

    public RegisterViewModel(IRegistrationService registrationService, IContentDialogExit contentDialogExit, IAuthenticationService authenticationService, IActivationService activationService)
    {
        this.registrationService = registrationService;
        this.contentDialogExit = contentDialogExit;
        this.authenticationService = authenticationService;
        //App.identity = Thread.CurrentPrincipal as IdentityRolePrincipal;
        this.activationService = activationService;
    }

    [RelayCommand]
    public async Task RegisterUser()
    {
        

        if (RepeatedPassword.Equals(Hash) && IsCheckConf == true)
        {
            Users =   await registrationService.RegisterUser(Login, Hash, Mail);
           authenticationService.AuthorizationUser(Users);
        }
        else
        {
           await contentDialogExit.OpenContentDialog("Неправильный ввод");
        }
        if (authenticationService.IsAuthenticated == true)
        {
            bool values = await authenticationService.IsInRole(Users.Логин);
            if (values == true)
            {
                //App.GetService<>
                var window = App.GetService<BasicWindow>();
                var view = App.GetService<BasicView>();
                 activationService.OpenWindow(window, view);
                
            }
            else
            {
                await contentDialogExit.OpenContentDialog("По неясным причинам авторизация не пройшла");
            }
        }
    }


  
}
