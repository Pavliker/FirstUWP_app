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
    [Table("Фотографии_Оборудование")]
    public partial class Фотографии_Оборудование : BaseClass
    {
   
        public Фотографии_Оборудование(int КодФотографии_Оборудование, int КодФотографии, int КодОборудования) {
            this.КодФотографии_Оборудование = КодФотографии_Оборудование;
            this.КодФотографии = КодФотографии;
            this.КодОборудования = КодОборудования;
        }
        [Key]
        [ObservableProperty]
        public partial int КодФотографии_Оборудование
        {
            get;set;
        }
        [ForeignKey("Фотографии")]
        [ObservableProperty]
        public partial int КодФотографии
        {
            get;set;
        }
        public Фотографии Фотографии { get; set; }
        [ForeignKey("Оборудование")]
        [ObservableProperty]
        public partial int КодОборудования
        {
            get;set;
        }
        public Оборудование  Оборудование {get;set;}

    }


}
