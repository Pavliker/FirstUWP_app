using AlbumApp1._0._1.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Interfaces
{
    public interface  IRegistrationService
    {
       Task <Пользователи> RegisterUser(string? Логин, string? ХешированныйПароль, string? НазваниеПочты);
    }
}
