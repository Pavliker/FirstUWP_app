using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.AllJoyn;
using Windows.System;
using Windows.UI.Input.Inking.Preview;

namespace AlbumApp1._0._1.Services.Role
{
    public partial class RoleService : IRoleService
    {
        public IGenericRepository<Роли> RoleRepository { get; private set; }
   
        public RoleService(  IGenericRepository<Роли> RoleRepository)
        {
            this.RoleRepository = RoleRepository;
          
        }
        public async Task<int> GetRoleCode(string name)
        {
            await foreach (var role in RoleRepository.FindBy(o => o.НазваниеРоли == name))
            {
                int code = role.КодРоли;
                return code;
            }
            return 0;
        }


        public async Task <Роли> GetRoleUserNameByCode(int code)
        {
          
                await foreach (var role in  RoleRepository.FindBy(o=>o.КодРоли == code))
            {
                 return role;
            }
            return null;
        }

    }
}
