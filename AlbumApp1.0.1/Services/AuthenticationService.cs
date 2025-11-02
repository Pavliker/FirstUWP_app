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
    public partial class  AuthenticationService  :IdentityRolePrincipal, IAuthenticationService
    {
        private bool disposed;
        private readonly IRoleService _roleService;
        public IdentityRole _identityRole { get; set; }
        private IdentityRolePrincipal identity;
        public string AuthenticationName
        {
            get => ApplicationPrincipal.Current.Identity.Name;
        }
        public string AuthenticationEmail
        {
            get => identity.IdentityRole.НазваниеПочты;
        }
        public string RoleName
        {
            get=> identity.IdentityRole.НазваниеРоли;
        }
        public AuthenticationService(IRoleService roleService):base()
        {
            identity = App.GetService<IdentityRolePrincipal>();
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
        
        public void AuthorizationUser (Пользователи user)
        {
            //var obj = principal.Identity;
            identity.IdentityRole = new IdentityRole(true, user.Логин, "Пользователь", user.НазваниеПочты);
           ApplicationPrincipal.SwitchCurrentPrincipal(() => identity);
        }
        public void AuthorizationGuest(Гости guest)
        {
            //var obj = principal.Identity;

            identity.IdentityRole = new IdentityRole(guest.Логин, "Гость");
            ApplicationPrincipal.SwitchCurrentPrincipal(() => identity);

        }
        public async Task<bool> IsInRole(int code)
        {
            var role = await _roleService.GetRoleUserNameByCode(code);
            return identity.IsInRole(role);
        }
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (identity != null)
                    {
                        identity.Dispose();
                        identity = null;
                        if (identity!=null)

                        {
                            identity.Dispose();
                            identity = null;
                        }
                        if (_identityRole!=null)
                        {
                            _identityRole.Dispose();
                            _identityRole = null;
                        }
                    }

                 
                }
                base.Dispose(disposing);    
            }
            disposed = true;
        }
     
    }
}
