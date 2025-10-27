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
        private int numberOfRole;
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
        private async Task<int> RoleCode(string rolename)
        {
            await foreach (var i in RoleSeRepository.FindBy(o=>o.НазваниеРоли == rolename))
            {
                return i.КодРоли;
            }
            return 0;
        }
        private List<EnumPermission> userPermission = new List<EnumPermission>();
        private async void LoaduserPermissions()
        {

            numberOfRole = await RoleCode(_identityRole.НазваниеРоли);
            if (_identityRole ==null) return;
            userPermission = PermissionService.GetPermissionsByRoleCode(numberOfRole);
        }
        public bool HasPermission(EnumPermission permission)
        {
            return permission == (EnumPermission)numberOfRole;
        }
        IIdentity? IPrincipal.Identity
        {
            get
            {
                return this.IdentityRole;
            }
        }
        public bool IsInRole(string role)
        {
            return IdentityRole.НазваниеРоли.Equals(role);
        }
    }
}
