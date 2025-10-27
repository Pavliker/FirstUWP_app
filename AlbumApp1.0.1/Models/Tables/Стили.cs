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
    [Table("Стили")]
   public partial class Стили : ObservableValidator
    {
      
        public Стили(int КодСтиля, string НазваниеСтиля)
        {
            this.КодСтиля = КодСтиля;
            this.НазваниеСтиля = НазваниеСтиля;
        }
        [Key]
        [ObservableProperty]
        public partial int КодСтиля
        {
            get;set;
        }
        [Index(IsUnique =true)/*,MaxLength(25)*/]
        [ObservableProperty]
        public partial string? НазваниеСтиля
        {
            get;set;
        }
        public ICollection<Фотографии> Фтографии { get; set; }
    }
}
