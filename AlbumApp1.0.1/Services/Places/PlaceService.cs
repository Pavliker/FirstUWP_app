using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services.Places
{
    public partial class PlaceService:IPlaceService
    {
        private IUnitOfWork unitOfWork;
        private readonly IContentDialogExit dialogExit;
        public IGenericRepository<Места> PlacesRepository { get; private set; }
        public PlaceService(IGenericRepository<Места> PlacesRepository )
        {
            dialogExit = App.GetService<IContentDialogExit>();  
            unitOfWork = App.GetService<IUnitOfWork>();
            this.PlacesRepository = PlacesRepository;
        }
        public async Task AddPlace(string PlaceName)
        {
            await unitOfWork.context.Database.ExecuteSqlRawAsync(
                       "EXEC InsertPlaces @НазваниеМеста",
                       new SqlParameter("@НазваниеМеста", PlaceName));
        }
        public async Task<int> GetIdByPlaceName(string placename)
        {
            var PlaceID = await unitOfWork.context.Места.Where(o => o.НазваниеМеста == placename).Select(o => o.КодМеста).FirstOrDefaultAsync();
            return PlaceID;
        }
        public async Task<ObservableCollection<Места>> GetPlaces()
        {
            var collection = new ObservableCollection<Места>();
            var lst = PlacesRepository.GetAll();
            await foreach (var obj in lst)
            {
                collection.Add(obj);    
            }
           
                return collection;
           
        }
        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}
