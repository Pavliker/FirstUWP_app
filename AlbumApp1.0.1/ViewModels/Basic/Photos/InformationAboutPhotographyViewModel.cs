using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Models.Views;
using AlbumApp1._0._1.Services.Accessories;
using AlbumApp1._0._1.Services.Objects;
using AlbumApp1._0._1.Services.Places;
using AlbumApp1._0._1.Services.Styles;
using AlbumApp1._0._1.Views.Basic;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.ViewModels.Basic.Photos
{
    public partial class InformationAboutPhotographyViewModel:BasedViewModelContext
    {
        public PhotosViewModel photosViewModel;
        private readonly IPhotoService photoService;
        private readonly IAuthenticationService authentication;
        private readonly IStyleService _styleService;
        private readonly IAccessoriesService accessoriesService;
        private readonly IPlaceService placeService;
        private readonly IObjectService objectService;
        private readonly IUserService userService;

        public Оборудование Accessories { get; set; }
        public Места Places { get; set; }
        public Объекты Object { get; set; }
        public Стили Styles { get; set; }
        private Пользователи Users;

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
        public ObservableCollection<Стили> StyleCollection
        {
            get; set;
        }
        private string _accessoryname;
        public string NameAccessory
        {
            get => _accessoryname;
            set
            {
                if (_accessoryname != value)
                {
                    _accessoryname = value;
                    OnPropertyChanged(nameof(NameAccessory));
                }

            }
        }
        private string _namePlace;
        public string NamePlace
        {
            get => _namePlace;
            set
            {
                if (_namePlace != value)
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
                if (nameObject != value)
                {
                    nameObject = value;
                    OnPropertyChanged(nameof(NameObject));
                }

            }
        }
        public int CodeStyle
        {
            get
            {
                return Styles.КодСтиля;
            }
            set
            {
                if (Styles.КодСтиля != value)
                {
                    Styles.КодСтиля = value;
                    OnPropertyChanged(nameof(CodeStyle));
                }

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
        public string Username
        {
            get
            {
                if (!string.IsNullOrEmpty(authentication.AuthenticationName))
                {
                    return authentication.AuthenticationName;
                }
                else
                {
                    return string.Empty;
                }

            }
        }
        public int PhotoCode
        {
            get => Photos.КодФотографии;
            set
            {
               
                    Photos.КодФотографии = value;
                    OnPropertyChanged(nameof(PhotoCode));
                
            }
        }
        public int UserCode
        {
            get => Photos.КодПользователя;
            set
            {
               
                    Photos.КодПользователя = value;
                    OnPropertyChanged(nameof(UserCode));
                
        
            }
        }
        public int CodeObject
        {
            get => Photos.КодОбъекта;
            set
            {
               
                    Photos.КодОбъекта = value;
                    OnPropertyChanged(nameof(CodeObject));
                

            }
        }
   

        public DateTime UploadDate
        {
            get => Photos.ДатаЗагрузки;
            set
            {
              
                    Photos.ДатаЗагрузки = value;
                    OnPropertyChanged(nameof(UploadDate));
                

            }
        }
        public string PhotoName
        {
            get => Photos.НазваниеФотографии;
            set
            {
              
                    Photos.НазваниеФотографии = value;
                    OnPropertyChanged(nameof(PhotoName));
                

            }
        }
        public string Description
        {
            get => Photos.Описание;
            set
            {
               
                    Photos.Описание = value;
                    OnPropertyChanged(nameof(Description));
                

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
        
       
        public Фотографии Photos
        {
            get => photosViewModel.Photos;
            set
            {
              
                    photosViewModel.Photos = value;
                    OnPropertyChanged(nameof(Photos));

                
            }
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
        public async Task ChangePhoto()
        {
            if (Photos!=null)
            {
                int Objectid = await objectService.GetIdByObjectName(InputObjectName);
                var userid = await userService.GetUser1(Username);
                await photoService.ChangePhoto(Photos, Objectid, CodeStyle, userid.КодПользователя);
            }
        }
        [RelayCommand]
        public async Task DeletePhoto()
        {
            if (Photos!=null)
            {
                await photoService.RemovePhoto(Photos);
                photoService.PhotographyCollection.Remove(Photos);
                OnPropertyChanged(nameof(photoService.PhotographyCollection));
            }
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
       public async Task DeleteObject()
        {
            int id = await objectService.GetIdByObjectName(NameObject);

            await objectService.RemoveObject(id);
            foreach (var i in ObjectCollection)
            {
                if (i.КодОбъекта == id)
                {
                    ObjectCollection.Remove(i);
                }
            }
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
           public async Task DeletePlace()
        {
            int id = await placeService.GetIdByPlaceName(NamePlace);
            await placeService.RemovePlace(id);
            foreach (var i in PlaceCollection)
            {
                if (i.КодМеста == id)
                {
                    PlaceCollection.Remove(i);
                }
            }
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
            int id = await accessoriesService.GetIdByAccessoryName(InputAccessoryName);
            Accessories.КодОборудования = id;
            var acs = new Оборудование();
            acs.КодОборудования = Accessories.КодОборудования;
            acs.НазваниеОборудования = Accessories.НазваниеОборудования;
            accessoryCollection.Add(acs);
        }
        [RelayCommand]
            public async Task DeleteAccessory()
        {
            int id = await accessoriesService.GetIdByAccessoryName(NameAccessory);
            await accessoriesService.RemoveAccessory(id);
            
            foreach (var i in accessoryCollection)
            {
                if (i.КодОборудования == id)
                {
                    accessoryCollection.Remove(i);
                }
            }
        

        }
        public InformationAboutPhotographyViewModel() 
        {
            authentication = App.GetService<IAuthenticationService>();
            photoService = App.GetService<IPhotoService>();
            photosViewModel = App.GetService<PhotosViewModel>();
            _styleService = App.GetService<IStyleService>();
            accessoriesService = App.GetService<IAccessoriesService>();
            placeService = App.GetService<IPlaceService>();
            objectService = App.GetService<IObjectService>();
            userService = App.GetService<IUserService>();
           
            var manager = App.GetService<IObjectManager>();
            //Photos = (Фотографии?)manager.TakeObject(Photos); 
            Accessories = (Оборудование?)manager.TakeObject(Accessories);
            Places = (Места?)manager.TakeObject(Places);
            Object = (Объекты?)manager.TakeObject(Object);
            Styles = (Стили?)manager.TakeObject(Styles);
            Users = (Пользователи?)manager.TakeObject(Users);
            StyleCollection = new ObservableCollection<Стили>();
            accessoryCollection = new ObservableCollection<Оборудование>();
            placeCollection = new ObservableCollection<Места>();
            objectCollection = new ObservableCollection<Объекты>();
            FillStyleCollection();
            FillPlaceCollection();
            FillObjectCollection();
            FillAccessoriesCollection();
        }

    }
}
