using AlbumApp1._0._1.Collections;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Services;
using AlbumApp1._0._1.ViewModels.Basic.Users;
using AlbumApp1._0._1.Views.Basic;
using AlbumApp1._0._1.Views.Basic.Users;
using AlbumApp1._0._1.WindowsViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Printing;
using Windows.System;
using WinRT.AlbumApp1_0_1VtableClasses;

namespace AlbumApp1._0._1.ViewModels.Basic
{
  public  partial  class PhotosViewModel : BasedViewModelContext
    {
        private RelayCommand  _openPhotoCommand;
        public IRelayCommand OpenPhotoCommand => _openPhotoCommand ??= new RelayCommand(OpenPhoto);

       
        public Window window { get; private set; }
        private Фотографии _photos;
        public Фотографии Photos { get => _photos;

            set
            {
                if (_photos!=value)
                {
                    _photos = value;
                    Photos = objectManager.TakephotoObject(_photos);

                    OnPropertyChanged(nameof(Photos));
                }
            }
        }
        public ICollectionView PhColView { get; set; }
        public IPhotoService photoService { get; set; }
        private readonly IObjectManager objectManager;
        private readonly IContentDialogExit contentDialogaExitService;

        private bool _include;
        public bool Include
        {
            get => _include;
            set
            {
                if (_include != value)
                {
                   
                    
                    _include = value;

                    OnPropertyChanged(nameof(Include));
                }
            }
        }
        
        public AddPhotoViewModel _addPhotoViewModel;
        public AddPhotoWindow addphotoWindow { get; set; }
        public DetailedWindow detailPhotoWindow { get; set; }
        private readonly IActivationService activationService;
        private readonly INavigationService navigationService;

        public PhotosViewModel()
        {

            //Photos = new();

            contentDialogaExitService = App.GetService<IContentDialogExit>();
            photoService = App.GetService<IPhotoService>();
            activationService = App.GetService<IActivationService>();
            objectManager = App.GetService<IObjectManager>();
            navigationService = App.GetService<INavigationService>();
            //Photos = objectManager.TakephotoObject(_photos); ;
            OnPropertyChanged(nameof(Photos));
          
                _include = true;
     


        }

        [RelayCommand]
        public async void LoadPhotos()
        {       
                 photoService.GetAll();
        }
        
        private void OpenPhoto()
        {



            // update selected photo object
            //Photos = objectManager.TakephotoObject(_photos);
            //OnPropertyChanged(nameof(Photos));

            // get navigation and target VM

            // navigate in existing frame
            navigationService.NavigateTo(typeof(DetailedPhotosViewModel));

        }
        [RelayCommand]
        public void AddPhotoWindow()
        {
        
            addphotoWindow = App.GetService<AddPhotoWindow>();
            window = addphotoWindow;
            var win = addphotoWindow.GetAppWindowForCurrentWindow();
             _addPhotoViewModel = App.GetService<AddPhotoViewModel>();
             var view = App.GetService<AddPhotoView>();
            App.GetService<IActivationService>().RegisterMapping<AddPhotoViewModel, AddPhotoWindow>(addphotoWindow);
           
            activationService.OpenWindow(_addPhotoViewModel, view);

            Include = false;
            

            addphotoWindow.GetAppWindowForCurrentWindow().Closing += (sender, args) =>
            {
                Include = true;
            };



        }
    }
}
