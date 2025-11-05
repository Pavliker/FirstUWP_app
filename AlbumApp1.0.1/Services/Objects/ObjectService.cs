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

namespace AlbumApp1._0._1.Services.Objects
{
    public partial class ObjectService:IObjectService
    {
        private IUnitOfWork unitOfWork;
        private readonly IContentDialogExit dialogExit;
        public IGenericRepository<Объекты> objectRepository { get; private set; }
        public ObjectService(IGenericRepository<Объекты> objectRepository) 
        {
            dialogExit = App.GetService<IContentDialogExit>();
            unitOfWork = App.GetService<IUnitOfWork>();
            this.objectRepository = objectRepository;
        }  
        public async Task AddObject(string ObjectName)
        {
            await unitOfWork.context.Database.ExecuteSqlRawAsync(
                       "EXEC InsertObject @НазваниеОбъекта",
                       new SqlParameter("@НазваниеОбъекта", ObjectName));
        }
        public async Task<int> GetIdByObjectName(string objectname)
        {
            var ObjectID = await unitOfWork.context.Объекты.Where(o => o.НазваниеОбъекта== objectname).Select(o => o.КодОбъекта).FirstOrDefaultAsync();
            return ObjectID;
        }
        public async Task<ObservableCollection<Объекты>> GetObjects()
        {
            var collection = new ObservableCollection<Объекты>();
            var lst = objectRepository.GetAll();
            await foreach (var obj in lst)
            {
                collection.Add(obj);
            }
         
                return collection;
        
        }
    }
}
