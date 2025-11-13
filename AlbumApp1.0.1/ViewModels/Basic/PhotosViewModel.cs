using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.ViewModels.Basic.Users;
using AlbumApp1._0._1.Views.Basic.Users;
using AlbumApp1._0._1.WindowsViews;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

using System.Collections.ObjectModel;
using System.ComponentModel;
using WinRT.AlbumApp1_0_1GenericHelpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace AlbumApp1._0._1.ViewModels.Basic
{
  public  partial  class PhotosViewModel : BasedViewModelContext
    {
        private bool _descending;
        public bool Descending { get => _descending; set{
                _descending = value;

                OnPropertyChanged(nameof(Descending));
            }}

        private string _selectVar;
        public string SelectVar
        {
            get => _selectVar;
            set
            {
                if (SetProperty(ref _selectVar, value))
                {
                    Descending = _selectVar == "По убыванию";
                    var ordered = photoService.PhotographyCollection
  .Select((item, index) => new { Item = item, OldIndex = index });
                    ordered = Descending ? ordered.OrderByDescending(x => x.Item.ДатаЗагрузки.Date) : ordered.OrderBy(x => x.Item.ДатаЗагрузки.Date);
                    var map = ordered.Select((tuple, index) => new { tuple.Item, tuple.OldIndex, NewIndex = index })
                                .Where(o => o.OldIndex != o.NewIndex).ToList();
                    //          var query = photoService.PhotographyCollection
                    //.Select(item => item.ДатаЗагрузки);


                    //query = [.. query.OrderBy(tuple => tuple.Date)];
                    using var enumerator = map.GetEnumerator();

                    if (enumerator.MoveNext())
                        {
                            photoService.PhotographyCollection.Move(enumerator.Current.OldIndex, enumerator.Current.NewIndex);
                        }

                        photoService.csv = new CollectionViewSource { Source = photoService.PhotographyCollection }.View;
                   

                       
                       
                        //query = query.OrderByDescending(tuple => tuple.Date);

                    
                    //OnPropertyChanged(nameof(Descending));
                    //OnPropertyChanged(nameof(SelectVar));

                }
            }
        }
        public ObservableCollection<string> SortCollection;
        private RelayCommand  _openPhotoCommand;
        public IRelayCommand OpenPhotoCommand => _openPhotoCommand ??= new RelayCommand(OpenPhoto);
        private readonly IDispatcherQueueService _queueService;
       
        public Window window { get; private set; }
        private Фотографии _photos;
        public Фотографии Photos { get => _photos;

            set
            {
                if (_photos!=value)
                {
                    _photos = value;
                    //Photos = objectManager.TakephotoObject(_photos);

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
        
        //private CollectionViewSource _csv;
        //public CollectionViewSource csv
        //{
        //    get => _csv; set
        //    {
        //        if (_csv!=value)
        //        {
        //            _csv = value;
        //            OnPropertyChanged(nameof(csv));
        //        }
        //    }
        //}
        public AddPhotoViewModel _addPhotoViewModel;
        public AddPhotoWindow addphotoWindow { get; set; }
        public DetailedWindow detailPhotoWindow { get; set; }
        private readonly IActivationService activationService;
        private readonly INavigationService navigationService;

        public PhotosViewModel()
        {
            //_csv = new CollectionViewSource();
           
            //_csv.IsSourceGrouped = true;
            SortCollection = new ObservableCollection<string>
            {
                "По возрастанию", 
                "По убыванию"
            };

            //Photos = new();
            contentDialogaExitService = App.GetService<IContentDialogExit>();
            photoService = App.GetService<IPhotoService>();
            activationService = App.GetService<IActivationService>();
            objectManager = App.GetService<IObjectManager>();
            navigationService = App.GetService<INavigationService>();
            _queueService = App.GetService<IDispatcherQueueService>();
            //photoService.csv.Source = photoService.PhotographyCollection;
            //_csv.Source = photoService.PhotographyCollection;
            //_csv.View.Add(photoService.PhotographyCollection);

            //_csv.DispatcherQueue.TryEnqueue(() =>
            //    {
            //        _csv.View = photoService.PhotographyCollection;
            //    });

            photoService.csv = new CollectionViewSource { Source = photoService.PhotographyCollection }.View;



            //Photos = objectManager.TakephotoObject(_photos); ;
            OnPropertyChanged(nameof(Photos));
            OnPropertyChanged(nameof(photoService.PhotographyCollection));
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
