using AlbumApp1._0._1.Collections;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.ViewModels.Basic.Users;
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

namespace AlbumApp1._0._1.ViewModels.Basic
{
  public  partial  class PhotosViewModel : BasedViewModelContext
    {
        public Window window { get; private set; }
        private SynchronizedObservableCollection<Фотографии> photographyCollection;
        public SynchronizedObservableCollection<Фотографии> PhotographyCollection
        {
            get => photographyCollection;
            set
            {
                SetProperty(ref photographyCollection, value);
                OnPropertyChanged(nameof(PhotographyCollection));
            }
        }
        public ICollectionView PhColView { get; set; }
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
        private readonly IActivationService activationService;
        public PhotosViewModel()
        {
            activationService = App.GetService<IActivationService>();
            PhColView = new CollectionViewSource().View;
            _include = true;
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
