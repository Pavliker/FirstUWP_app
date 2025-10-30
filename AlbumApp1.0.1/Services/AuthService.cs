using AlbumApp1._0._1.Helpers;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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
            var check = await _userService.GetUser1(login);

            var lst = await CryptographyHelper.DeserializeObject<HashWithSaltResult>();
            var salt = lst.Where(o => o.Логин == login).Select(o=>o.Salt).ToList();
            string checkedhash = null;
            foreach (var i in salt)
            {
                var hashed = CryptographyHelper.Verify(password, i);

                if (check.ХешированныйПароль == hashed )
                {
                    checkedhash = hashed;
                    break;
                }

            }
            if (check != null)
            {
                var hashcheck = check.ХешированныйПароль.Equals(checkedhash, StringComparison.OrdinalIgnoreCase);
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
