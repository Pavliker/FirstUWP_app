using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models;
using AlbumApp1._0._1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services.Permissions
{
    public partial class PermissionService:IPermissionService
    {
        public List<EnumPermission> GetPermissionsByRoleCode(int code)
        {
            // Fetch permissions for the role from the database or another data source
            // Placeholder implementation
            if (code == (int)EnumPermission.USER)
            {
                return new List<EnumPermission> { EnumPermission.USER };
            }
            else if (code == (int)EnumPermission.GUEST)
            {
                return new List<EnumPermission> { EnumPermission.GUEST };
            }
            return new List<EnumPermission>();
        }   
    }
}
