using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Services.Role;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models
{
    public partial class IdentityRolePrincipal : IPrincipal
    {
        private int value;
        private readonly IPermissionService PermissionService;
        private readonly IRoleService RoleService;
        public IdentityRolePrincipal(IPermissionService PermissionService,IRoleService roleService)
        {
            this.PermissionService = PermissionService;
            RoleService = roleService;
        }

        private IdentityRole _identityRole;
        public IdentityRole IdentityRole
        {
            get
            {
                return _identityRole;
            }
            set
            {
                _identityRole = value;
                LoaduserPermissions();
            }
        }
        private List<EnumPermission> userPermission = new List<EnumPermission>();
        private void LoaduserPermissions()
        {
            if (_identityRole ==null) return;
            userPermission = PermissionService.GetPermissionsByRoleCode(_identityRole.КодРоли);
        }
        public bool HasPermission(EnumPermission permission)
        {
            return permission == (EnumPermission)_identityRole.КодРоли;
        }
        IIdentity? IPrincipal.Identity
        {
            get
            {
                return this.IdentityRole;
            }
            
        }
        public async Task CallFunc(string username)
        {
             value =  await RoleService.GetRoleCodeByName(username);

        }
        public bool IsInRole(string username)
        {
                
                CallFunc(username).GetAwaiter();
                return _identityRole.КодРоли.Equals(value);
        }
    }
}
