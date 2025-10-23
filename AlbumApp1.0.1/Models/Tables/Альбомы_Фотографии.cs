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
    [Table("Альбомы_Фотографии")]
    public partial class Альбомы_Фотографии : ObservableObject
    {
       
        public Альбомы_Фотографии(int КодАльбома_Фотографии, int КодАльбома, int КодФотографии )
        {
            this.КодАльбома_Фотографии = КодАльбома_Фотографии;
            this.КодАльбома = КодАльбома;
            this.КодФотографии = КодФотографии;
        }
        [Key]
        [ObservableProperty]
        public partial int КодАльбома_Фотографии
        {
            get;set;
        }
        [ForeignKey("Альбомы")]
        [ObservableProperty]
        public partial int КодАльбома
        {
            get;set;
        }
        public Альбомы Альбомы {  get; set; }
        [ForeignKey("Фотографии")]
        [ObservableProperty]
        public partial int КодФотографии
        {
            get;set;
        }
        public Фотографии Фотографии { get; set; }
    }
}
