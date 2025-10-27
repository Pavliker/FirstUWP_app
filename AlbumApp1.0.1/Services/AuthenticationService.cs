using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using AlbumApp1._0._1.Services.Role;
using Microsoft.WindowsAppSDK.Runtime;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;
using System.Security.Principal;


namespace AlbumApp1._0._1.Services
{
    public partial class AuthenticationService  : IAuthenticationService
    {


        private readonly IRoleService _roleService;
        private readonly IdentityRolePrincipal identity;
       
        public string AuthenticationName
        {
            get => identity.IdentityRole.Name;
        }
        
        public AuthenticationService(IRoleService roleService)
        {
            identity = App.GetService<IdentityRolePrincipal>();
            AppDomain.CurrentDomain.SetThreadPrincipal(identity);
            _roleService = roleService;
           
        }
        public bool IsAuthenticated
        {
            get { return identity.IdentityRole.IsAuthenticated; }
        }
        public bool CanLogin()
        {
            return !IsAuthenticated;
        }
        private async Task<string> NameRole(int code)
        {
            var role = await _roleService.GetRoleUserNameByCode(code);

            return  role.НазваниеРоли;
        }
        public void AuthorizationUser (Пользователи user)
        {
            //var obj = principal.Identity;
           identity.IdentityRole = new IdentityRole(user.Логин, "Пользователь");
           
        }
        public void AuthorizationGuest(Гости guest)
        {
            //var obj = principal.Identity;
            identity.IdentityRole = new IdentityRole(guest.Логин, "Гость");

        }
        public async Task<bool> IsInRole(int code)
        {
            string rolename = await NameRole(code);
            return identity.IsInRole(rolename);
        }
}
}
