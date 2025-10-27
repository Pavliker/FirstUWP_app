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
    public partial class RegistrationService:IRegistrationService
    {
        private readonly IUserService _userService;

        public RegistrationService(IUserService userService) 
        { 
         _userService = userService;
        }

        public  async Task <Пользователи> RegisterUser(string Логин, string ХешированныйПароль, string НазваниеПочты)
        {
          
            string hash = CryptographyHelper.HashingPassword(ХешированныйПароль);
            var user =   await _userService.AddUser(Логин, hash, НазваниеПочты);
            return user;

         }

    }
}
