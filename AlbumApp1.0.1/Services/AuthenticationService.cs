using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using AlbumApp1._0._1.Services.Role;
using Microsoft.WindowsAppSDK.Runtime;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;
using System.Security.Principal;
using System.Threading.Tasks;


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
            ApplicationPrincipal app = App.GetService<ApplicationPrincipal>();
            
            
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
        
        public async Task AuthorizationUser (Пользователи user)
        {
            //var obj = principal.Identity;
            int code = await _roleService.GetRoleCode("Пользователь");
            identity.IdentityRole = new IdentityRole(true, user.Логин, "Пользователь", user.НазваниеПочты, code);
           ApplicationPrincipal.SwitchCurrentPrincipal(() => identity);
        }
        public async Task AuthorizationGuest(Гости guest)
        {
            //var obj = principal.Identity;
            int code = await _roleService.GetRoleCode("Гость");
            identity.IdentityRole = new IdentityRole(guest.Логин, "Гость",code);
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
