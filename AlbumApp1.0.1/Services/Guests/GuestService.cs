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

namespace AlbumApp1._0._1.Services.Guests
{
    public partial class GuestService:IGuestService
    {
        private IUnitOfWork unitOfWork;
        //private IGenericRepository<Пользователи> userRepository;
        public GuestService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            //this.userRepository = userRepository;
        }
        public async Task AddGuest(int КодРоли, string Логин)
        {
            await unitOfWork.context.Database.ExecuteSqlRawAsync(
                "EXEC InsertGuest @КодРоли, @Логин",
                new SqlParameter("@КодРоли", КодРоли),
                new SqlParameter("@Логин", Логин)

                );
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}
