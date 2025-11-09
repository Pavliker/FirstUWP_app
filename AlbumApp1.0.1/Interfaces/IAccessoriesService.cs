using AlbumApp1._0._1.Models.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Interfaces
{
    public interface IAccessoriesService
    {
        Task AddAccessories(string AccessoriesName);
        Task<int> GetIdByAccessoryName(string accessoryname);
        Task<ObservableCollection<Оборудование>> GetAccessories();
        Task RemoveAccessory(int Accessory);
    }
}
