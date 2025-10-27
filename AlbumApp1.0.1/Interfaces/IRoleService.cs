using AlbumApp1._0._1.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Interfaces
{
    public interface IRoleService
    {
      Task<Роли> GetRoleUserNameByCode(int code);
        Task<int> GetRoleCode(string name);
    }
}
