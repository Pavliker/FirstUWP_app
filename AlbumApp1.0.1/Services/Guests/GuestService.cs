using AlbumApp1._0._1.Helpers;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace AlbumApp1._0._1.Services.Guests
{
    public partial class GuestService:IGuestService

    {
        public IGenericRepository<Гости> GuestRepository { get; private set; }
        private readonly IContentDialogExit _dialogExit;
        private readonly IRoleService _roleService;
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
        public GuestService(IUnitOfWork unitOfWork, IGenericRepository<Гости> guestRepository, IRoleService roleService, IContentDialogExit dialogExit)
        {
            this.unitOfWork = unitOfWork;
            GuestRepository = guestRepository;
            _roleService = roleService;
            _dialogExit = dialogExit;
            //this.userRepository = userRepository;
        }
        private async Task<bool> GuestExist(string Логин)
        {
            var exist = await unitOfWork.context.Гости.AnyAsync(o=>o.Логин == Логин);
            return exist;
        }
        public async Task<Гости> AddGuest( string Логин)
        {
            Гости guest = new();
            try
            {
                if (await GuestExist(Логин) == true)
                {
                    if (await _dialogExit.OpenContentDialog($"Гость с именем {Логин} уже присутствует в базе данных") == true)
                    {

                    }
                    else
                    {
                        guest = await GetGuest(Логин);
                    }
                }
                else
                {
                    int code = await _roleService.GetRoleCode("Гость");
                    await unitOfWork.context.Database.ExecuteSqlRawAsync(
                        "EXEC InsertGuest @КодРоли, @Логин",
                        new SqlParameter("@КодРоли", code),
                        new SqlParameter("@Логин", Логин));
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                if (guest != null)
                {
                    string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\..\\..\\guest.json"));
                    CryptographyHelper.WriteToJsonFile(path, guest);
                }
            }
            guest = await GetGuest(Логин);
            return guest;
        }
        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
       
    }
}
