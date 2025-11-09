using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services.Accessories
{
    public partial class AccessoriesService:IAccessoriesService
    {
        private IUnitOfWork unitOfWork;
        private readonly IContentDialogExit dialogExit;
        public IGenericRepository<Оборудование> accessoriesRepository { get; private set; }
        public AccessoriesService(IGenericRepository<Оборудование> accessoriesRepository)
        {
            dialogExit = App.GetService<IContentDialogExit>();
            unitOfWork = App.GetService<IUnitOfWork>();
            this.accessoriesRepository = accessoriesRepository;
        }
        public async Task AddAccessories(string AccessoriesName)
        {

            var checkExist =  accessoriesRepository.FindBy(o=>o.НазваниеОборудования == AccessoriesName);
             var task = Task.Run(async () =>
            {
                 await foreach (var i in checkExist)
                {
                    if (i.НазваниеОборудования == AccessoriesName)
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
                      "EXEC InsertAccessories @НазваниеОборудования",
                      new SqlParameter("@НазваниеОборудования", AccessoriesName));
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
        public async Task RemoveAccessory(int Accessory)
        {
           
                await unitOfWork.context.Database.ExecuteSqlRawAsync(
                      "EXEC DeleteAccessories @КодОборудования",
                      new SqlParameter("@КодОборудования", Accessory));
            
        }

        public async Task<int> GetIdByAccessoryName(string accessoryname)
        {
            var AccessoryID = await unitOfWork.context.Оборудование.Where(o => o.НазваниеОборудования == accessoryname).Select(o => o.КодОборудования).FirstOrDefaultAsync();
            return AccessoryID;
        }
        public async Task<ObservableCollection<Оборудование>> GetAccessories()
        {
            var collection = new ObservableCollection<Оборудование>();
            var lst = accessoriesRepository.GetAll();
            await foreach (var obj in lst)
            {
                collection.Add(obj);
            }
          
                return collection;
            
        
        }
    }
}
