using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.ViewModels.Basic.Photos
{
    public partial class  AboutPhotoViewModel:BasedViewModelContext
    {
        private readonly IObjectManager _ObjectManager;
        public Фотографии Photos
        {
            get => _ObjectManager.photosViewModel.Photos;
            set
            {
                _ObjectManager.photosViewModel.Photos = value;
                OnPropertyChanged(nameof(Photos));
            }
        }
        public AboutPhotoViewModel()
        {
            _ObjectManager = App.GetService<IObjectManager>();
        }
    }
}
