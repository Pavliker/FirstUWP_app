using AlbumApp1._0._1.Collections;
using AlbumApp1._0._1.Models.Tables;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Interfaces
{
    public interface IPhotoService
    {
        Task AddPhoto(int КодПользователя, int КодОбъекта, int КодСтиля, DateTime ДатаЗагрузки, string НазваниеФотографии, string Описание, string Качество, string Формат, string Разрешение, int Уникальность, long Размер, byte[] Путь);
        Task<int> GetIdByPhotoName(string photoname);
        ObservableCollection<Фотографии> PhotographyCollection { get; set; }
        void GetAll();
        Task ChangePhoto(Фотографии photos, int objectID, int codeStyle, int userID);
        Task RemovePhoto(Фотографии photos);
        ICollectionView csv { get; set; }
    }
}
