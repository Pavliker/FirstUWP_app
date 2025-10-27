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
    [Table("Оборудование")]
   public partial class Оборудование : ObservableValidator
    { 
        private int КодОборудования_;
        private string? НазваниеОборудования_;
        public Оборудование(int КодОборудования, string? НазваниеОборудования)
        {
            this.КодОборудования = КодОборудования;
            this.НазваниеОборудования = НазваниеОборудования;
        }
        [Key]
        [ObservableProperty]
        public partial int КодОборудования
        {
            get;set;
        }
        //[MaxLength(25)]
        [ObservableProperty]
        public partial string? НазваниеОборудования
        {
            get;set;
        }
        public ICollection<Фотографии_Оборудование> Фотографии_Оборудование { get; set; }
    }
}
