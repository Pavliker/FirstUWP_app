using AlbumApp1._0._1.Models;
using AlbumApp1._0._1.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Interfaces
{
    public interface IAuthenticationService:IDisposable
    {
         bool IsAuthenticated
        {
            get;
        }
        IdentityRole _identityRole { get; set; }
        string AuthenticationName {  get; }
        string AuthenticationEmail { get; }
        string RoleName {  get; }
        bool CanLogin();
        Task AuthorizationUser(Пользователи user);
        Task AuthorizationGuest(Гости guest);
        Task<bool> IsInRole(int code);
    }
}
