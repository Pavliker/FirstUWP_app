using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models;
using AlbumApp1._0._1.Models.Tables;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRT.AlbumApp1_0_1VtableClasses;

namespace AlbumApp1._0._1.ViewModels.Basic
{
    public  partial class ProfileViewModel:BasedViewModelContext
    {
        private bool _isDisposed;
        public IAuthenticationService authentication { get; set; }
        private readonly IUserService userService;
        public Пользователи Users = new();

        public string Username
        {
            get
            {
                if (authentication.AuthenticationName != null && authentication.IsAuthenticated == true)
                {
                   
                        return authentication.AuthenticationName;
                }
                else
                {
                    return string.Empty;
                }

            }
        }

        public bool AuthenticationStatus
        {
            get
            {
                    return authentication.IsAuthenticated;
            }
        }
        public string Email
        {
            get
            {
               
                if (authentication.AuthenticationEmail != null  || !string.IsNullOrEmpty(Email))
                {
                    if (authentication.IsAuthenticated == true && IsPressed != true)
                    {
                        return authentication.AuthenticationEmail;
                    }
                    else
                    {
                        return Users.НазваниеПочты;
                    }
                }
             
                return string.Empty;
                
            }
            set
            {
                Users.НазваниеПочты = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Hash
        {
            get => Users.ХешированныйПароль;
            set
            {
                if (Users.ХешированныйПароль!=value)
                {
                    Users.ХешированныйПароль = value;
                    OnPropertyChanged(nameof(Hash));
                }
            }
        }
        private string _repeatePassword;
        public string RepeatePassword
        {
            get => _repeatePassword;
            set
            {
                if (_repeatePassword != value)
                {
                    _repeatePassword = value;
                    OnPropertyChanged(nameof(RepeatePassword));
                }
            }
        }
        public string RoleName
        {
            get
            {
                return authentication.RoleName;
            }
        }
        private bool _onlyReadText;
        public bool OnlyReadText
        {
            get
            {
                return _onlyReadText;
            }
            set
            {
                SetProperty(ref _onlyReadText, value);
                OnPropertyChanged(nameof(OnlyReadText));    
            }
        }
        private bool _onlyReadText1;
        public bool OnlyReadText1
        {
            get
            {
                return _onlyReadText1;
            }
            set
            {
                SetProperty(ref _onlyReadText1, value);
                OnPropertyChanged(nameof(OnlyReadText1));
            }
        }
        private bool _isPressed;
        public bool IsPressed
        {
            get => _isPressed;
            set
            {
                if (_isPressed!=value)
                {
                    _isPressed = value;
                    OnPropertyChanged(nameof(IsPressed));   
                }
            }
        }
        private bool _accessToButton;
        public bool AccessToButton
        {
            get=>_accessToButton;
            set
            {
                if (_accessToButton != value)
                {
                    _accessToButton = value;
                    OnPropertyChanged(nameof(AccessToButton));
                }
            }
        }
        private bool _accessToButton1;
        public bool AccessToButton1
        {
            get => _accessToButton1;
            set
            {
                if (_accessToButton1 != value)
                {
                    _accessToButton1 = value;
                    OnPropertyChanged(nameof(AccessToButton1));
                }
            }
        }
        public ProfileViewModel(IUserService userService):base()
        {
            if (authentication == null)
            {
                authentication = App.GetService<IAuthenticationService>(); 
            }
            this.userService = userService;
        }

        protected  override void Dispose(bool disposing)
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
                   
                }
                base.Dispose(disposing);
            }
            _isDisposed = true;
        }
        ~ProfileViewModel()
        {
            Dispose(false); 
        }
        [RelayCommand]
        public async Task ChangeMail()
        {

        }
        [RelayCommand]
        public async Task ChangePassword()
        {

        }
        [RelayCommand]
        public void Available()
        {
            if (IsPressed == true)
            {
                Hash = string.Empty;
                RepeatePassword = string.Empty;
                _onlyReadText = false;
                _onlyReadText1 = true;
                _accessToButton1 = false;
                _accessToButton = true;
                OnPropertyChanging(nameof(IsPressed));
                OnPropertyChanged(nameof(AccessToButton));
                OnPropertyChanged(nameof(AccessToButton1));
                OnPropertyChanged(nameof(OnlyReadText));
                OnPropertyChanged(nameof(OnlyReadText1));

            }
            else
            {
                Hash = string.Empty;
                RepeatePassword = string.Empty;
                _onlyReadText = true;
                _onlyReadText1 = false;
                _accessToButton1 = true;
                _accessToButton = false;
                OnPropertyChanging(nameof(IsPressed));
                OnPropertyChanged(nameof(AccessToButton));
                OnPropertyChanged(nameof(AccessToButton1));
                OnPropertyChanged(nameof(OnlyReadText));
                OnPropertyChanged(nameof(OnlyReadText1));

            }
        }
    }
}
