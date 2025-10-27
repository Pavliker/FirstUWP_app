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
    [Table("Роли")]
    public partial class Роли : ObservableObject
    {
      
       public Роли(int КодРоли, string НазваниеРоли)
        {
            this.КодРоли = КодРоли;
            this.НазваниеРоли = НазваниеРоли;
        }
        [Key]
        [ObservableProperty]
        public partial int КодРоли
        {
            get;set;
        }
        //[MaxLength(20)]
        [ObservableProperty]
        public partial string? НазваниеРоли
        {
            get;set;
        }
        public ICollection<Гости> Гости { get; set; }
    }
}
