using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.ViewModels.Basic.Photos;
using AlbumApp1._0._1.Views.Basic.Photos;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.ViewModels.Basic
{
    public partial class DetailedPhotosViewModel:BasedViewModelContext
    {

        public INavigationService navigationService { get; set; }
    
        [RelayCommand]
        public void NavigateToInformation()
        {
            navigationService.NavigateTo(typeof(InformationAboutPhotographyViewModel));
        }
        [RelayCommand]
        public void NavigateToAboutPhotoView()
        {
            navigationService.NavigateTo(typeof(AboutPhotoViewModel));
        }
        public DetailedPhotosViewModel() 
                {
    //photosViewModel = App.GetService<PhotosViewModel>();
            navigationService = App.GetService<INavigationService>();
            //navigationService.Frame = new Microsoft.UI.Xaml.Controls.Frame();

        }  
        
    }
}
