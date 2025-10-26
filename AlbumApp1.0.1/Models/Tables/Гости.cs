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
    [Table("Гости")] 
    public partial class Гости : BaseClass
    {
        
        public Гости()
        {
      
        }
        [Key]
        [ObservableProperty]
        public partial int КодГостя
        {
            get;set;
        }
        [ForeignKey("Роли")]
        [ObservableProperty]
        public partial int КодРоли
        {
            get;set;
        }
        public Роли Роли { get; set; }
        //[MaxLength(15)]
        [ObservableProperty]
        public partial string? Логин
        {
            get;set;
        }


    }
}
