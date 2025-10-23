using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models;
using AlbumApp1._0._1.Models.Tables;
using Microsoft.WindowsAppSDK.Runtime;
using System.Drawing.Printing;
using System.Security.Principal;


namespace AlbumApp1._0._1.Services
{
    public partial class AuthenticationService  : IAuthenticationService
    {

        //private readonly IIdentity identity;
        //private readonly IPrincipal principal;  
        private readonly IdentityRolePrincipal identity;
        public AuthenticationService()
        {
            ////identity= App.GetService<IIdentity>();
            //principal= App.GetService<IPrincipal>();
             identity = App.GetService<IdentityRolePrincipal>();
             AppDomain.CurrentDomain.SetThreadPrincipal(identity);
        }
        public bool IsAuthenticated
        {
            get { return identity.IdentityRole.IsAuthenticated; }
        }
        public bool CanLogin()
        {
            return !IsAuthenticated;
        }
        public void AuthorizationUser (Пользователи user)
        {
            //var obj = principal.Identity;
           identity.IdentityRole = new IdentityRole(user.Логин, user.КодРоли);
           
        }
        public bool IsInRole(string rolename)
        {
            return identity.IsInRole(rolename);
        }
}
}
