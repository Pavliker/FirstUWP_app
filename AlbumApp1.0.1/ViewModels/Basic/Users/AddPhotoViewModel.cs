using AlbumApp1._0._1.Collections;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Models.Views;
using AlbumApp1._0._1.Services;
using AlbumApp1._0._1.Services.Accessories;
using AlbumApp1._0._1.Views.Basic.Users;
using AlbumApp1._0._1.WindowsViews;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.Windows.Storage.Pickers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using WinRT.Interop;

namespace AlbumApp1._0._1.ViewModels.Basic.Users
{
    public partial class AddPhotoViewModel : BasedViewModelContext
    {
        private readonly IStyleService _styleService;
        private readonly IAccessoriesService accessoriesService;
        private readonly IPlaceService placeService;
        private readonly IObjectService objectService;
        private readonly IPhotoService photoService;
        private readonly IActivationService activationService;
        private Фотографии Photos;
        public PhotosViewModel PhotosViewModel;
        public Оборудование Accessories { get; set; }
        public Места Places { get; set; }
        public Объекты Object { get; set; }
        public Стили Styles { get; set; }
        private ObservableCollection<Оборудование> accessoryCollection;
        public ObservableCollection<Оборудование> AccessoryCollection
        {
            get => accessoryCollection;
            set
            {
                SetProperty(ref accessoryCollection, value);  
                OnPropertyChanged(nameof(AccessoryCollection));
            }
        }
        private string _accessoryname;
        public string NameAccessory
        {
            get => _accessoryname;
            set
            {
                if (_accessoryname!=value)
                {
                    _accessoryname = value;
                    OnPropertyChanged(nameof(NameAccessory));
                }
              
            }
        }
        private ObservableCollection<Места> placeCollection;
        public ObservableCollection<Места> PlaceCollection
        {
            get => placeCollection;
            set
            {
                SetProperty(ref placeCollection, value);
                OnPropertyChanged(nameof(PlaceCollection));
            }
        }
        private string _namePlace;
        public string NamePlace
        {
            get => _namePlace;
            set
            {
                if (_namePlace!=value)
                {
                    _namePlace = value;
                    OnPropertyChanged(nameof(NamePlace));
                }
            }
        }
        private string nameObject;
        public string NameObject
        {
            get => nameObject;
            set
            {
                if (nameObject!=value)
                {
                    nameObject = value;
                    OnPropertyChanged(nameof(NameObject));
                }
               
            }
        }
        private ObservableCollection<Объекты> objectCollection;
        public ObservableCollection<Объекты> ObjectCollection
        {
            get => objectCollection;
            set
            {
                SetProperty(ref objectCollection, value);
                OnPropertyChanged(nameof(ObjectCollection));
            }
        }
       
        public  ObservableCollection<Стили> StyleCollection
        {
            get; set;
        }
        public int CodeStyle
        {
            get
            {
                return Styles.КодСтиля;
            }
            set
            {
                Styles.КодСтиля = value;
                OnPropertyChanged(nameof(CodeStyle));   
            }
        }
        public string Quality
        {
            get => Photos.Качество;
            set
            {
                Photos.Качество = value;
                OnPropertyChanged(nameof(Quality));
            }
        }
        public int Unique
        {
            get => Photos.Уникальность;
            set
            {
                Photos.Уникальность = value;
                OnPropertyChanged(nameof(Unique));
            }
        }
        public byte[] Path
        {
            get => Photos.Путь;
            set
            {
                Photos.Путь = value;
                OnPropertyChanged(nameof(Path));
            }
        }
    
        public string InputObjectName
        {
            get => Object.НазваниеОбъекта;
            set
            {
                if (Object.НазваниеОбъекта != value)
                {
                   Object.НазваниеОбъекта = value;
                    OnPropertyChanged(nameof(InputObjectName));
                }
            }
        }
        
        public string InputPlaceName
        {
            get => Places.НазваниеМеста;
            set
            {
                if (Places.НазваниеМеста != value)
                {
                    Places.НазваниеМеста = value;
                    OnPropertyChanged(nameof(InputPlaceName));
                }
            }
        }
        public string InputAccessoryName
        {
            get => Accessories.НазваниеОборудования;
            set
            {
                if (Accessories.НазваниеОборудования != value)
                {
                    Accessories.НазваниеОборудования = value;
                    OnPropertyChanged(nameof(InputAccessoryName));
                }
            }
        }
        private BitmapImage _imageForDisplay;
        public BitmapImage? ImageForDisplay
        {
            get => _imageForDisplay; 
            set
            {
                if(!SetProperty(ref _imageForDisplay, value))
                {
                    SetProperty(ref _imageForDisplay, value);
                    OnPropertyChanged(nameof(ImageForDisplay));
                }
            }
        }
        private string textpath;
        public string TextPath
        {
            get => textpath;
            set
            {
                SetProperty(ref textpath, value);
                OnPropertyChanged(nameof(TextPath));
            }
        }
        public string NamePhoto
        {
            get => Photos.НазваниеФотографии;
            set
            {
                Photos.НазваниеФотографии = value;
                OnPropertyChanged(nameof(NamePhoto));
            }
        }
        public string Format
        {
            get;set;
        }
        public string Size
        {
            get;set;
        }
        public string Dimension
        {
            get;set;
        }

        public ICollectionView PhCol
        {
            get; set;
        }
        public string Discription
        {
            get => Photos.Описание;
            set
            {
                Photos.Описание = value;
                OnPropertyChanged(nameof(Discription));
            }
        }
        private readonly IObjectManager objectManager;
        private readonly IAuthenticationService authenticationService;
        public string Username
        {
            get => authenticationService.AuthenticationName;
        }
        public AddPhotoViewModel()
        {
            _styleService = App.GetService<IStyleService>();
            accessoriesService = App.GetService<IAccessoriesService>();
            placeService = App.GetService<IPlaceService>();
            objectService = App.GetService<IObjectService>();
            photoService = App.GetService<IPhotoService>(); 
            authenticationService = App.GetService<IAuthenticationService>();
            objectManager = App.GetService<IObjectManager>();
            PhotosViewModel = App.GetService<PhotosViewModel>();
            activationService = App.GetService<IActivationService>();
            Photos = (Фотографии?)objectManager.TakeObject(Photos);
            Accessories = (Оборудование?)objectManager.TakeObject(Accessories);
            Places = (Места?)objectManager.TakeObject(Places);
            Object = (Объекты?)objectManager.TakeObject(Object);
            Styles = (Стили?)objectManager.TakeObject(Styles);
            StyleCollection = new ObservableCollection<Стили>();
            accessoryCollection = new ObservableCollection<Оборудование>();
            placeCollection = new ObservableCollection<Места>();
            objectCollection = new ObservableCollection<Объекты>();
            FillStyleCollection();
            FillPlaceCollection();
            FillObjectCollection();
            FillAccessoriesCollection();
        }
        public async void FillStyleCollection()
        {
            StyleCollection = await _styleService.GetStyles();  
        }
        public async void FillPlaceCollection()
        {
            placeCollection = await placeService.GetPlaces();
        }
        public async void FillObjectCollection()
        {
            objectCollection = await objectService.GetObjects();
        }
        public async void FillAccessoriesCollection()
        {
            accessoryCollection = await accessoriesService.GetAccessories();
        }
        [RelayCommand]
        public async Task AddPhoto()
        {

        }
        [RelayCommand]
        public void CloseWindow()
        {
            PhotosViewModel.Include = true;
            activationService.CloseWindow<AddPhotoViewModel>();

        }
        [RelayCommand]
        public async Task AddAccessory()
        {
          
            if (!string.IsNullOrEmpty(InputAccessoryName) && Accessories.HasErrors == false)
            {
                await accessoriesService.AddAccessories(InputAccessoryName);
            }
            else
            {
                return;
            }
            int id =await accessoriesService.GetIdByAccessoryName(InputAccessoryName);
            Accessories.КодОборудования = id;
            var acs = new Оборудование();
            acs.КодОборудования = Accessories.КодОборудования;
            acs.НазваниеОборудования = Accessories.НазваниеОборудования;
            accessoryCollection.Add(acs);
          
        }
        [RelayCommand]
        public async Task AddPlace()
        {
            if (!string.IsNullOrEmpty(InputPlaceName) && Places.HasErrors == false)
            {
                await placeService.AddPlace(InputPlaceName);
            }
            else
            {
                return;
            }
            int id = await placeService.GetIdByPlaceName(InputPlaceName);
            Places.КодМеста = id;
            var acs = new Места();
            acs.КодМеста = Places.КодМеста;
            acs.НазваниеМеста = Places.НазваниеМеста;
            placeCollection.Add(acs);
        }
        [RelayCommand]
        public async Task AddObject()
        {
            if (!string.IsNullOrEmpty(InputObjectName) && Object.HasErrors == false)
            {
                await objectService.AddObject(InputObjectName);
            }
            else
            {
                return;
            }
            int id = await objectService.GetIdByObjectName(InputObjectName);
            Object.КодОбъекта = id;
            var acs = new Объекты();
            acs.КодОбъекта = Object.КодОбъекта;
            acs.НазваниеОбъекта = Object.НазваниеОбъекта;
            objectCollection.Add(acs);
        }
        [RelayCommand]
        public async Task LoadImage()
        {
            Windows.Storage.Pickers.FileOpenPicker open = new Windows.Storage.Pickers.FileOpenPicker();
            open.ViewMode = (Windows.Storage.Pickers.PickerViewMode)PickerViewMode.Thumbnail;
            open.SuggestedStartLocation = (Windows.Storage.Pickers.PickerLocationId)PickerLocationId.PicturesLibrary;
            open.FileTypeFilter.Add(".jpg");
            open.FileTypeFilter.Add(".jpeg");
            open.FileTypeFilter.Add(".png");
            open.FileTypeFilter.Add(".bmp");
            //((IInitializeWithWindow)(object)open).Initialize(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
            nint windowHandle = WindowNative.GetWindowHandle(PhotosViewModel.window);
            InitializeWithWindow.Initialize(open, windowHandle);

            if (open!=null)
            {
                StorageFile file = await open.PickSingleFileAsync();
                using (IRandomAccessStream s = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    _imageForDisplay = new BitmapImage();
                    await _imageForDisplay.SetSourceAsync(s);
                    OnPropertyChanged(nameof(ImageForDisplay));
                }
                Path = await File.ReadAllBytesAsync(file.Path);
                TextPath = file.Path;

            }




        }
    }
}
