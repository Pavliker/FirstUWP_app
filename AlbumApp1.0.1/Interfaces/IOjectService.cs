using AlbumApp1._0._1.Models.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Interfaces
{
    public interface IObjectService
    {
        Task AddObject(string ObjectName);
        Task<int> GetIdByObjectName(string objectname);
        Task<ObservableCollection<Объекты>> GetObjects();
    }
}
