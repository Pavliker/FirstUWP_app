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
   public partial class Оборудование : InputValidator
    { 
      
        public Оборудование()
        {
            
        }
        [Key]
        [ObservableProperty]
        public partial int КодОборудования
        {
            get;set;
        }
        [MaxLength(25)]
        [ObservableProperty]
        public partial string? НазваниеОборудования
        {
            get;set;
        }
        partial void OnНазваниеОборудованияChanged(string? value)
        {
            Validate(value, nameof(НазваниеОборудования));
        }
        public ICollection<Фотографии_Оборудование> Фотографии_Оборудование { get; set; }
    }
}
