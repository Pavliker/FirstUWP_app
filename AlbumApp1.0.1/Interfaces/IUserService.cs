using AlbumApp1._0._1.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Interfaces
{
    public interface IUserService
    {
        Task<Пользователи> AddUser(string Логин, string ХешированныйПароль, string НазваниеПочты);
        Task<bool> UserAndGuestsChoose(string? Логин);
        Task<Пользователи> GetUser(string username);
    }
}
