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
    [Table("Места")]
    public partial class Места : InputValidator
    {
        
        public Места()
        {
            
        }
        [Key]
        [ObservableProperty]
        public partial int КодМеста
        {
            get;set;
        }
        [MaxLength(25)]
        [ObservableProperty]
        public partial string? НазваниеМеста
        {
            get;set;
        }
        partial void OnНазваниеМестаChanged(string? value)
        {
            Validate(value, nameof(НазваниеМеста));

        }
        public ICollection<Фотографии_Места> Фотографии_Места { get; set; }
    }
}
