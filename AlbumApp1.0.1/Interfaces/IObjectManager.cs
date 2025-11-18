using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.ViewModels.Basic;
using AlbumApp1._0._1.WindowsViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Interfaces
{
    public interface IObjectManager
    {
        
        object TakeObject<T>(T value) where T : class;
        Фотографии Photos { get; set; }
         BasicWindow BasicWindow { get; set; }
         BasicViewModel basicViewModel { get; set; }
         PhotosViewModel photosViewModel { get; set; }

        Фотографии TakephotoObject(Фотографии objectPhoto);
    }
}
