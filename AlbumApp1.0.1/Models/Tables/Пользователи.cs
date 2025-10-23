using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Пользователи")]
   public partial class Пользователи:ObservableObject
    {
    
        public Пользователи()
        {
          
        }
        [Key]
        [ObservableProperty]
        public partial int КодПользователя

        { get; set; }
        [ForeignKey("Роли")]
        [ObservableProperty]
        public partial int КодРоли
        {
            get;set;
        }
        public Роли? Роли { get; set; }
        [Index(IsUnique =true)/*,MaxLength(15)*/]
        [ObservableProperty]
        public partial string? Логин
        {
            get;set;
        }
        //[Required, MaxLength(100)]
        [ObservableProperty]
        public partial string? ХешированныйПароль
        {
            get;set;
        }
        //[MaxLength(40)]
        [ObservableProperty]
        public partial string? НазваниеПочты
        {

            get;set;
        }
        public ICollection<Альбомы>? Альбомы { get; set; }
        public ICollection<Вопросы>? Вопросы { get; set; }
        public ICollection<Фотографии>? Фотографии { get; set; }
    }   
}
