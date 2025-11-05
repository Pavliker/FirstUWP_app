using AlbumApp1._0._1.Helpers;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services
{
    public partial class AuthService:IAuthService
    {
        private Пользователи user = new();
        private readonly IUserService _userService;
        private readonly IGuestService _guestsService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IContentDialogExit _controlDialogExit;
        public AuthService(IUserService userService, IAuthenticationService authenticationService,IGuestService guestsService, IContentDialogExit controldialog)
        {
            _userService = userService; 
            _authenticationService = authenticationService;
            _guestsService = guestsService;
            _controlDialogExit = controldialog;
        }

        public async Task<int> AuthorizationResult(string login, string password)
        {
            user = await _userService.GetUser1(login);
            int auth = 0;
            string hashed = string.Empty;

          
            var userIsExist = await _userService.UserAndGuestsChoose(login);
            if (userIsExist == false)
            {
                if (await _controlDialogExit.OpenContentDialog($"Введённого вами логина не существует!!!") == true)
                {
                    return -1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                var lst = await CryptographyHelper.DeserializeObject<HashWithSaltResult>();


                var salt = lst.Where(o => o.Логин == login).Select(o => o.Salt).ToList();
                foreach (var i in salt)
                {
                    hashed = CryptographyHelper.Verify(password, i);
                    var hashcheck = user.ХешированныйПароль.Equals(hashed, StringComparison.OrdinalIgnoreCase);
                    if (hashcheck == true)
                    {
                        break;
                    }
                }
            }
            if (user != null)
            {
                if (user.ХешированныйПароль.Equals(hashed, StringComparison.OrdinalIgnoreCase) == true)
                {
                    _authenticationService.AuthorizationUser(user);
                     auth = user.КодРоли;
                    return auth;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }
        public  int AuthorizationResult(Гости Guest)
        {

            if (Guest != null)
            {
             _authenticationService.AuthorizationGuest(Guest);
            }
            return Guest.КодРоли;
        }
        public async Task<Гости> RegisterGuest(string Логин)
        {
            return await _guestsService.AddGuest(Логин);
        }
        
    }
}
