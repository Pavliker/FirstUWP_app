using AlbumApp1._0._1.Models;
using AlbumApp1._0._1.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Interfaces
{
    public interface IAuthenticationService
    {
         bool IsAuthenticated
        {
            get;
        }
        bool CanLogin();
        void AuthorizationUser(Пользователи user);
        bool IsInRole(string rolename);
    }
}
