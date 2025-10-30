using AlbumApp1._0._1.Helpers;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using AlbumApp1._0._1.Services.Exit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace AlbumApp1._0._1.Services.Users
{
    public class UserService:IUserService
    {
        private IUnitOfWork unitOfWork;
        private readonly IContentDialogErrorService errorContentDialog;
        private readonly IGenericRepository<Пользователи> userrep;
        //private readonly IGenericRepository<Роли> genericRepositoryRole;
        private readonly IRoleService _roleService;
        public UserService(IContentDialogErrorService errorContentDialog, IUnitOfWork unitOfWork, IGenericRepository<Пользователи> user,IRoleService _roleService)
        {
            this.unitOfWork = unitOfWork;
            this.errorContentDialog = errorContentDialog;
            this.userrep = user;
            this._roleService = _roleService;
        }

        public async Task<bool> UserAndGuestsChoose(string? Логин)
        {
            var user = unitOfWork.context.Пользователи.Select(o => o.Логин).Equals(Логин);
            var guests = unitOfWork.context.Гости.Select(o => o.Логин).Equals(Логин);
            
            //var guest = GuestsRepository.FindBy(o=>o.Логин == Логин);
             if (user == true || guests == true)
            {
                await errorContentDialog.ShowDialogWindow($"{Логин} == {user} [Пользователь уже существует]") ;
            }
            else
            {
                return false;
            }
             return true;
        }
      public async Task<Пользователи> GetUser1(string Логин)
        {

            if (!string.IsNullOrEmpty(Логин))
            {
                await foreach (var obj in userrep.FindBy(o => o.Логин == Логин))
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
           
            try
            {
               
                int code = await _roleService.GetRoleCode("Пользователь");


                if (await UserAndGuestsChoose(Логин) == true)
                {
                    if (await errorContentDialog.ShowDialogWindow("Пользователь уже существует!!") == true)
                    {
                        Console.WriteLine("Пользователь есть в базе данных");
                    }
                    else
                    {
                        
                    }
                }
                else {
                    if (НазваниеПочты == null)
                    {
                        НазваниеПочты = "example@gmail.com";
                    }
                    await unitOfWork.context.Database.ExecuteSqlRawAsync(
                         "EXEC InsertUser @КодРоли, @Логин, @ХешированныйПароль, @НазваниеПочты",
                         new SqlParameter("@КодРоли", code),
                         new SqlParameter("@Логин", Логин),
                         new SqlParameter("@ХешированныйПароль", ХешированныйПароль),
                         new SqlParameter("@НазваниеПочты", НазваниеПочты)
                         );
                }

            }
            catch (Exception ex)
            {
                if (await errorContentDialog.ShowDialogWindow(ex.Message) == true)
                {
                    throw new ContentDialogErrorService(ex.Message);
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
