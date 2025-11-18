using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using AlbumApp1._0._1.Services.Role;
using Microsoft.EntityFrameworkCore.Metadata;
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
    public partial class IdentityRolePrincipal : IdentityRole, IPrincipal,IDisposable
    {
        private bool _isDisposed;
        private readonly IPermissionService PermissionService;
        private readonly IGenericRepository<Роли> RoleSeRepository;
        private int numberOfRole;
        public IdentityRolePrincipal(IPermissionService PermissionService, IGenericRepository<Роли> RoleSeRepository)
        {
            this.PermissionService = PermissionService;
            this.RoleSeRepository = RoleSeRepository;
            _identityRole = new IdentityRole();

        }
        public IdentityRolePrincipal() { }
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
        //private async Task<int> RoleCode(string rolename)
        //{
            
        //    await foreach (var i in RoleSeRepository.FindBy(o=>o.НазваниеРоли == rolename))
        //    {
        //        return i.КодРоли;
        //    }
        //    return 0;
        //}
        private List<EnumPermission> userPermission = [];
        private async void LoaduserPermissions()
        {

            //numberOfRole = await RoleCode(_identityRole.НазваниеРоли);
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
                return IdentityRole;
            }
        }
        public bool IsInRole(string role)
        {
            return _identityRole.НазваниеРоли.Equals(role);
        }
     
        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (_identityRole!=null)
                    {
                        _identityRole.Dispose();
                        _identityRole = null;
                    }
                }
                base.Dispose(disposing);
            }
            _isDisposed = true;
        }
   
    }
}
