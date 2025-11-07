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
        Task<Пользователи> GetUser1(string Логин);
        Task<Пользователи> GetUserByEmail(string Почта);
        Task UpdateUser(Пользователи user, string хешированныйПароль);
        void  UpdateEmailUser(Пользователи user, string названиеПочты);
        //Task<int> GetUserIdByUserName(string Логин);
    }
}
