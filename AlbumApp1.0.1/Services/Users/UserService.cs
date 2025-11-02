using AlbumApp1._0._1.Helpers;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using AlbumApp1._0._1.Services.Exit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Automation;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace AlbumApp1._0._1.Services.Users
{
    public class UserService:IUserService
    {
        private IUnitOfWork unitOfWork;
        private readonly IContentDialogExit errorContentDialog;
        private readonly IGenericRepository<Пользователи> userrep;
        //private readonly IGenericRepository<Роли> genericRepositoryRole;
        private readonly IRoleService _roleService;
        public UserService(IContentDialogExit errorContentDialog, IUnitOfWork unitOfWork, IGenericRepository<Пользователи> user,IRoleService _roleService)
        {
            this.unitOfWork = unitOfWork;
            this.errorContentDialog = errorContentDialog;
            this.userrep = user;
            this._roleService = _roleService;
        }

        public async Task<bool> UserAndGuestsChoose(string? Логин)
        {
            var user = unitOfWork.context.Пользователи.Where(o => o.Логин.Equals(Логин)).Any();
            var guests = unitOfWork.context.Гости.Where(o => o.Логин.Equals(Логин)).Any();
            
            //var guest = GuestsRepository.FindBy(o=>o.Логин == Логин);
             if (user == true || guests == true)
            {
                
                if(await errorContentDialog.OpenContentDialog($"{Логин} == {user} [Пользователь уже существует]") == true)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public async Task<Пользователи> GetUser1(string Логин)
        {
            var user = userrep.FindBy(o => o.Логин == Логин);
            if (string.IsNullOrEmpty(Логин))
            {
                return null;
            }
            else
            {
              var us =  await Task.Run(async () =>
               {
                   await foreach (var obj in user)
                   {

                       return obj;

                   }
                   return null;
               });
                return us;
            }
          
        }
        public async Task<Пользователи> GetUserByEmail(string Почта)
        {

            if (!string.IsNullOrEmpty(Почта))
            {
                await foreach (var obj in userrep.FindBy(o => o.НазваниеПочты == Почта))
                {
                    if (obj != null)
                    {
                        return obj;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return null;
        }
        //public async Task<int> GetRoleCode(string name)
        //{
        //    await foreach (var role in genericRepositoryRole.FindBy(o => o.НазваниеРоли == name))
        //    {
        //        int code = role.КодРоли;
        //        return code;
        //    }
        //    return 0;
        //}
        public async Task UpdateUser(Пользователи user, string хешированныйПароль)
        {
            var hash = CryptographyHelper.HashingPassword(user.Логин, хешированныйПароль,64, SHA512.Create());
            await CryptographyHelper.SerializeObject<HashWithSaltResult>(hash);
            var hashWithSalt = string.Concat(hash.Hash, hash.Salt);
            user.ХешированныйПароль = hashWithSalt; 
            userrep.Update(user);
        }
        public async Task<Пользователи> AddUser( string Логин, string ХешированныйПароль, string? НазваниеПочты)
        {

            var existUser = await UserAndGuestsChoose(Логин);

                if ( existUser == false) {
               
                int code = await _roleService.GetRoleCode("Пользователь");

                НазваниеПочты ??= "example@gmail.com";

                await unitOfWork.context.Database.ExecuteSqlRawAsync(
                       "EXEC InsertUser @КодРоли, @Логин, @ХешированныйПароль, @НазваниеПочты",
                       new SqlParameter("@КодРоли", code),
                       new SqlParameter("@Логин", Логин),
                       new SqlParameter("@ХешированныйПароль", ХешированныйПароль),
                       new SqlParameter("@НазваниеПочты", НазваниеПочты)
                       );
            }
            else
            {
                if (await errorContentDialog.OpenContentDialog("Пользователь уже существует!!") == true)
                {
                    Логин = string.Empty;
                    ХешированныйПароль = string.Empty;
                    НазваниеПочты = string.Empty;
                    return await GetUser1(Логин);
                }
                else
                {
                    Логин = string.Empty;
                    ХешированныйПароль = string.Empty;
                    НазваниеПочты = string.Empty;
                    return await GetUser1(Логин);
                }

            }
            return await GetUser1(Логин);
        }
        //public async Task<Пользователи> GetUser(string username)
        //{
        //    await foreach (var user1 in userrep.FindBy(o => o.Логин == username))
        //    {
        //        return user1;
        //    }
        //    return null;
        //}
        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }

    }
}
