using AlbumApp1._0._1.Helpers;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services
{
    public partial class RegistrationService:IRegistrationService
    {
        private readonly IUserService _userService;

        public RegistrationService(IUserService userService) 
        { 
         _userService = userService;
        }

        public  async Task <Пользователи> RegisterUser(string Логин, string ХешированныйПароль, string НазваниеПочты)
        {
          
            var hash = CryptographyHelper.HashingPassword(Логин, ХешированныйПароль, 64, SHA512.Create());
            await CryptographyHelper.SerializeObject<HashWithSaltResult>(hash);
            var hashWithSalt = string.Concat(hash.Hash, hash.Salt);
            var user =   await _userService.AddUser(Логин, hashWithSalt, НазваниеПочты);
            return user;
         }


        
    }
}
