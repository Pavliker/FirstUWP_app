using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.AccessControl;
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
            var checkExist = objectRepository.FindBy(o => o.НазваниеОбъекта == ObjectName);
            var task = Task.Run(async () =>
            {
                await foreach (var i in checkExist)
                {
                    if (i.НазваниеОбъекта == ObjectName)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;

            });
            try
            {
                if (await task == true)
                {
                    if (await dialogExit.OpenContentDialog("Запись имеется в базе данных") == true)
                    {
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    await unitOfWork.context.Database.ExecuteSqlRawAsync(
                       "EXEC InsertObject @НазваниеОбъекта",
                       new SqlParameter("@НазваниеОбъекта", ObjectName));
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new DbEntityValidationException(dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task RemoveObject(int objects)
        {
           
                await unitOfWork.context.Database.ExecuteSqlRawAsync(
                      "EXEC DeleteObject @КодОбъекта",
                      new SqlParameter("@КодОбъекта", objects));
            
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
