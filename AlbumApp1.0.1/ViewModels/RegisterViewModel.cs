using AlbumApp1._0._1.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using System.ComponentModel;

namespace AlbumApp1._0._1.ViewModels;

public partial class RegisterViewModel :  ObservableObject,IDataErrorInfo
{
    [ObservableProperty]
    public partial string Логин { get; set; }
    [ObservableProperty]
    public partial string Почта { get; set; }
    [ObservableProperty]
    public partial string Пароль { get; set; }
    [ObservableProperty]
    public partial string ПовторныйПароль { get; set; }
    [ObservableProperty]
    public partial bool Конфиденциальность { get; set; }
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
