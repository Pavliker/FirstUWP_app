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
    [Table("Фотографии_Места")]
    public partial class Фотографии_Места : ObservableObject
    {
 
        public Фотографии_Места(int КодФотографии_Места, int КодФотографии, int КодМеста )
        {
            this.КодФотографии_Места = КодФотографии_Места;
            this.КодФотографии = КодФотографии;
            this.КодМеста = КодМеста;
        }
        [Key]
        [ObservableProperty]
        public partial int КодФотографии_Места
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
        [ForeignKey("Места")]
        [ObservableProperty]
        public partial int КодМеста
        {
            get;set;
        }
        public Места Места { get; set; }

    }
}
