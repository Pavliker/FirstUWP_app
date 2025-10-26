using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using AlbumApp1._0._1.Services.Role;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models
{
    public partial class IdentityRolePrincipal : IPrincipal
    {
        private readonly IPermissionService PermissionService;
        private readonly IGenericRepository<Роли> RoleSeRepository;

        public IdentityRolePrincipal(IPermissionService PermissionService, IGenericRepository<Роли> RoleSeRepository)
        {
            this.PermissionService = PermissionService;
            this.RoleSeRepository = RoleSeRepository;
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
        private int ID;
        private async Task<int> Id( string role)
        {
            await foreach (var i in RoleSeRepository.FindBy(o => o.НазваниеРоли == role))
            
                {
                 ID = i.КодРоли;
                return ID;
            }
            return 0;
        }
        public bool IsInRole(string role)
        {
            if (role == null) return false;
            bool b = IdentityRole.КодРоли.Equals(Id(role).GetAwaiter());

            return b;
        }
    }
}
