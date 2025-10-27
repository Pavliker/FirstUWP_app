using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Альбомы")]
   public partial class Альбомы : ObservableValidator
    {
       
       
        public Альбомы(int КодАльбома, int КодПользователя, DateTime ДатаСоздания, string НазваниеАльбома, string КраткоеОписание)
        {
            this.КодАльбома = КодАльбома;
            this.КодПользователя = КодПользователя;
            this.ДатаСоздания = ДатаСоздания;
            this.НазваниеАльбома = НазваниеАльбома;
            this.КраткоеОписание = КраткоеОписание;
        }
        [Key]
        [ObservableProperty]
        public partial int КодАльбома
        {
            get;set;
        }
        [ForeignKey("Пользователи")]
        [ObservableProperty]
        public partial int КодПользователя
        {
            get;set;
        }
        public Пользователи Пользователи { get; set; }
        [ObservableProperty]
        public partial DateTime ДатаСоздания
        {
            get;set;
        }
        [Index(IsUnique = true)/*, MaxLength(25)*/]
        [ObservableProperty]
        public partial string? НазваниеАльбома
        {
            get;set;
        }
        //[MaxLength(100)]
        [ObservableProperty]
        public partial string? КраткоеОписание
        {
            get;set;
        }

        public ICollection<Альбомы_Фотографии> Альбомы_Фотографии { get; set; }
    }
}
