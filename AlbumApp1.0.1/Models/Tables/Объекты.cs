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
    [Table("Объекты")]
    public partial class Объекты : ObservableObject
    {
     
        public Объекты(int КодОбъекта, string НазваниеОбъекта)
        {
            this.КодОбъекта = КодОбъекта;
            this.НазваниеОбъекта = НазваниеОбъекта; 
        }
        [Key]
        [ObservableProperty]
        public partial int КодОбъекта
        {

            get;set;
        }
        [Index(IsUnique = true)/*, MaxLength(25)*/]
        [ObservableProperty]
        public partial string? НазваниеОбъекта
        {
            get;set;
        }
        public ICollection<Фотографии> Фотографии { get; set; }

    }
}
