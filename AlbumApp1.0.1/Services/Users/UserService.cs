using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services.Users
{
    public class UserService:IUserService
    {
        private IUnitOfWork unitOfWork;
        //private IGenericRepository<Пользователи> userRepository;
        public UserService(IUnitOfWork unitOfWork, IGenericRepository<Пользователи>userRepository)
        {
            this.unitOfWork = unitOfWork;
            //this.userRepository = userRepository;
        }
        public async Task AddUser(int КодРоли, string Логин, string ХешированныйПароль, string НазваниеПочты)
        {
            await unitOfWork.context.Database.ExecuteSqlRawAsync(
                "EXEC InsertUser @КодРоли, @Логин, @ХешированныйПароль, @НазваниеПочты",
                new SqlParameter("@КодРоли",КодРоли),
                new SqlParameter("@Логин", Логин),
                new SqlParameter("ХешированныйПароль", ХешированныйПароль),
                new SqlParameter("НазваниеПочты", НазваниеПочты)
                );
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }

    }
}
