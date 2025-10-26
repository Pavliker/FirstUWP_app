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
    public partial class RoleService:IRoleService
    {
        private readonly IContentDialogExit ExitDialog;
        public IGenericRepository<Роли> RoleRepository { get; private set; }
        private readonly IGuestService GuestsService ;
        private readonly IUserService UserService;
        public RoleService( IContentDialogExit exitDialog, IGenericRepository<Роли> RoleRepository, IGuestService guestsService, IUserService userService)
        {
            ExitDialog = exitDialog;
            this.RoleRepository = RoleRepository;
            GuestsService = guestsService;
            UserService = userService;
        }



        public async Task <int> GetRoleCodeByName(string username)
        {
            int code = 0;
          

            if (!string.IsNullOrEmpty(username))
            {
                var users = await UserService.GetUser(username);
                if (username == users.Логин || users.Логин != null)
                {
                    code = users.КодРоли;
                }
                else if (username != users.Логин)
                {
                    var guests = await GuestsService.GetGuest(username);

                    if (username == guests.Логин || guests.Логин != null)
                    {
                        code = guests.КодРоли;
                    }
                }
                else
                {
                    await ExitDialog.OpenContentDialog("Такого пользователя не существует!!!");
                }
            }
            
                return code;
        }
        
    }
}
