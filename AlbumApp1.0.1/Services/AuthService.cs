using AlbumApp1._0._1.Helpers;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services
{
    public partial class AuthService:IAuthService
    {
        private readonly IUserService _userService;
        private readonly IGuestService _guestsService;
        private readonly IAuthenticationService _authenticationService;
        public AuthService(IUserService userService, IAuthenticationService authenticationService,IGuestService guestsService)
        {
            _userService = userService; 
            _authenticationService = authenticationService;
            _guestsService = guestsService;
        }

        public async Task<bool> AuthorizationResult(string login, string password)
        {
            bool auth = false;
            var hash = CryptographyHelper.HashingPassword(login);
            var check = await _userService.GetUser(login);
            if (check != null) {
                var hashcheck = check.ХешированныйПароль.Equals(hash);
                if (hashcheck == true)
                {
                    _authenticationService.AuthorizationUser(check);
                    if (await _authenticationService.IsInRole(check.КодРоли) == true)
                    {
                        auth = true;
                    }
                }
            }
            else
            {
                return false;
            }
            return auth;
        }
        public  bool AuthorizationResult(Гости Guest)
        {

            if (Guest != null)
            {
             _authenticationService.AuthorizationGuest(Guest);
            }
            else
            {
                return false;
            }
            return true;
        }
        public async Task<Гости> RegisterGuest(string Логин)
        {
            return await _guestsService.AddGuest(Логин);
        }
        
    }
}
