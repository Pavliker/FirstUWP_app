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
        public IGenericRepository<Гости> GuestRepository { get; private set; }
        public async Task<Гости> GetGuest(string username)
        {
            await foreach (var guest1 in GuestRepository.FindBy(o => o.Логин == username))
            {
                return guest1;
            }
            return null;
        }
        private IUnitOfWork unitOfWork;
        //private IGenericRepository<Пользователи> userRepository;
        public GuestService(IUnitOfWork unitOfWork, IGenericRepository<Гости> guestRepository)
        {
            this.unitOfWork = unitOfWork;
            GuestRepository = guestRepository;
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
