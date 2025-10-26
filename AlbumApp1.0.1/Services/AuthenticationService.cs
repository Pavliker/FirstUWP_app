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

        //private readonly IIdentity identity;
        //private readonly IPrincipal principal;  
        private readonly IRoleService _roleService;
        private readonly IdentityRolePrincipal identity;
        private readonly IGenericRepository<Роли> RolesRepository;
        public AuthenticationService(IRoleService roleService, IGenericRepository<Роли> RolesRepository)
        {
            ////identity= App.GetService<IIdentity>();
            //principal= App.GetService<IPrincipal>();
            identity = App.GetService<IdentityRolePrincipal>();
            AppDomain.CurrentDomain.SetThreadPrincipal(identity);
            _roleService = roleService;
            this.RolesRepository = RolesRepository; 
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
        private async Task<int> identefierRole(string username)
        {

                var  value =  await _roleService.GetRoleCodeByName(username);
            return value;
        }
        public async Task<bool> IsInRole(string username)
        {
            int val = await identefierRole( username);
            var name = await RolesRepository.GetById(val);

            return identity.IsInRole(name.НазваниеРоли);
        }
}
}
