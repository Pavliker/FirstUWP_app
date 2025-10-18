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
    public class Роли
    {
        private int КодРоли_;
       private string? НазваниеРоли_;
       public Роли(int КодРоли, string НазваниеРоли)
        {
            this.КодРоли = КодРоли;
            this.НазваниеРоли = НазваниеРоли;
        }
        [Key]
        public int КодРоли
        {
            get
            {
                return КодРоли_;

            }
            set
            {
                if (КодРоли_!=value)
                {
                    КодРоли_= value;
                }
            }
        }
        [MaxLength(20)]
        public string? НазваниеРоли
        {
            get
            {
                return НазваниеРоли_;
            }
            set
            {
                if (НазваниеРоли_!=value)
                {
                    НазваниеРоли_ = value;  
                }
            }
        }
        public ICollection<Гости> Гости { get; set; }
    }
}
