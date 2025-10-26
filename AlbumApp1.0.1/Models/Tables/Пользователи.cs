using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Пользователи")]

    public partial class Пользователи : BaseClass
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
