using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.UI.Xaml;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Пользователи")]

    public partial class Пользователи : InputValidator
    {

        [ObservableProperty]
        [Required(ErrorMessage = "Логин является обязательным полем")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Длина логина должна быть от 4 до 15 символов")]
        [Index(IsUnique = true)]
        public partial string Логин { get; set; }
        [ObservableProperty]
        [Required(ErrorMessage = "Хешированный пароль является обязательным полем")]
        public partial string ХешированныйПароль { get; set; }
        [ObservableProperty]
        [StringLength(40, MinimumLength = 4, ErrorMessage = "Длина почты должна быть от 4 ло 40 символов")]
        public partial string НазваниеПочты { get; set; }

        partial void OnЛогинChanged(string value)
        {
            //ValidateClear(nameof(Логин));
            //ValidateProperty(value, nameof(Логин));
            Validate(value, nameof(Логин));
            
            //if (ErrorsDictionary != null)
            //{
            //    foreach (var i in this.GetErrors(nameof(Логин)))
            //    {
            //        ErrorsDictionary.Add(i.ErrorMessage);
            //    }

            //    //ErrorsDictionary.Add(Users.GetErrors(nameof(Users.Логин)).First().ErrorMessage);
            //}
            //if (ErrorsDictionary != null)
            //{
            //    _errorMessage = ErrorsDictionary.FirstOrDefault();

            //}
 
          
              
          
            //if (HasErrors == false)
            //{
            //    foreach (var item in ErrorsDictionary.ToList())
            //    {
            //        ErrorsDictionary.Remove(item);
            //        _errorMessage = string.Empty;
            //    }

            //}
           
            //    OnPropertyChanged(nameof(ErrorsDictionary));
            //OnPropertyChanged(nameof(errorMessage));

        }
        //private string _errorMessage;
        //public string errorMessage
        //{
        //    get => _errorMessage;
        //    set
        //    {
        //        _errorMessage = value;
        //        OnPropertyChanged(nameof(errorMessage));
        //    }
    
        //}
        partial void OnХешированныйПарольChanged(string value)
        {
            //ValidateProperty(value, nameof(ХешированныйПароль));
            //ValidateClear(nameof(ХешированныйПароль));
            Validate(value, nameof(ХешированныйПароль));
        }
        partial void OnНазваниеПочтыChanged(string value)
        {
            //ValidateProperty(value, nameof(НазваниеПочты));
            //ValidateClear(nameof(ХешированныйПароль));
            Validate(value, nameof(НазваниеПочты));
        }


        //[ObservableProperty]
        //public partial ObservableCollection<string?> ErrorsDictionary
        //{
        //    get; set;
        //} = new();
        public Пользователи( )
        {
        
           
        }
        [Key]
        public int КодПользователя

        { get; set; }

        
        [ForeignKey("Роли")]
        public  int КодРоли
        {
            get;set;
        }
        public Роли? Роли { get; set; }
      /*,MaxLength(15)*/
        //public  string Логин
        //{
        //    get => _Логин; 
        //    set
        //    {
        //        SetProperty(ref _Логин, value, true);
        //        OnPropertyChanged(nameof(Логин));

        //    }
        //}
        //[Required, MaxLength(100)]
        //[Required(ErrorMessage = "Хешированный пароль является обязательным полем")]
        //public  string ХешированныйПароль
        //{
        //    get=>_ХешированныйПароль;set {
        //        SetProperty(ref _ХешированныйПароль,value,true);
        //        OnPropertyChanged(nameof(ХешированныйПароль));
        //    }
        //}
        //[StringLength(40, MinimumLength = 4, ErrorMessage = "Длина почты должна быть от 4 ло 40 символов")]
        //public  string НазваниеПочты
        //{
        //    get => _НазваниеПочты;
        //    set
        //    {
        //        SetProperty(ref _НазваниеПочты, value, true);
        //        OnPropertyChanged(nameof(НазваниеПочты));
        //    }
        //}
        public ICollection<Альбомы>? Альбомы { get; set; }
        public ICollection<Вопросы>? Вопросы { get; set; }
        public ICollection<Фотографии>? Фотографии { get; set; }
    }   
}
