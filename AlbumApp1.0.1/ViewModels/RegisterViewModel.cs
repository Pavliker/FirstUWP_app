using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models;
using AlbumApp1._0._1.Models.Tables;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Media.AppBroadcasting;

namespace AlbumApp1._0._1.ViewModels;

public partial class RegisterViewModel :  ObservableObject
{

    private Пользователи user;
    public readonly IRegistrationService registrationService;
    private readonly IAuthenticationService authenticationService;
    public Пользователи Пользователи { get; set; }
    //private AsyncRelayCommand _registerCommand;
    
    //public IAsyncRelayCommand RegisterCommand => _registerCommand ??= new AsyncRelayCommand(RegisterUser);
    private readonly IContentDialogExit contentDialogExit;
    [ObservableProperty]
    public partial string RepeatedPassword {  get; set; }

    public RegisterViewModel(IRegistrationService registrationService, IContentDialogExit contentDialogExit, IAuthenticationService authenticationService)
    {
        this.registrationService = registrationService;
        Пользователи = new Пользователи();
        this.contentDialogExit = contentDialogExit;
        this.authenticationService = authenticationService;
        //App.identity = Thread.CurrentPrincipal as IdentityRolePrincipal;
        user = new Пользователи();
    }

    [RelayCommand]
    public async Task RegisterUser()
    {
        if (RepeatedPassword.Equals(Пользователи.ХешированныйПароль))
        {
            user =   await registrationService.RegisterUser(Пользователи.Логин, Пользователи.ХешированныйПароль, Пользователи.НазваниеПочты);
           authenticationService.AuthorizationUser(user);
        }
        else
        {
           await contentDialogExit.OpenContentDialog("Неправильно введён один из паролей!!!");
        }
        if (authenticationService.IsAuthenticated == true)
        {
            bool values = authenticationService.IsInRole(user.Логин);
            if (values == true)
            {
                //App.GetService<>
            }
            else
            {
                await contentDialogExit.OpenContentDialog("По неясным причинам авторизация не пройшла");
            }
        }
    }


  
}
