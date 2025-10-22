using AlbumApp1._0._1.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Media.AppBroadcasting;

namespace AlbumApp1._0._1.ViewModels;

public partial class RegisterViewModel :  ObservableObject
{
    
   
    public readonly IRegistrationService registrationService;

    private AsyncRelayCommand _registerCommand;
    public IAsyncRelayCommand RegisterCommand => _registerCommand ??= new AsyncRelayCommand(RegisterUser);

    public RegisterViewModel(IRegistrationService registrationService)
    {
        this.registrationService = registrationService;
    }
    public async Task RegisterUser()
    {

    }
  
}
