using AlbumApp1._0._1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services
{
    public partial class RegistrationService:IRegistrationService
    {
        public readonly IUserService _userService;
        public RegistrationService(IUserService userService) 
        { 
         _userService = userService;
        }

        public async Task RegisterUser(int КодРоли, string Логин, string ХешированныйПароль, string НазваниеПочты)
        {
           
        }

    }
}
