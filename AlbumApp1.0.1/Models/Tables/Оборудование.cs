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
   public class Оборудование
    {
        private int КодОборудования_;
        private string? НазваниеОборудования_;
        public Оборудование(int КодОборудования, string? НазваниеОборудования)
        {
            this.КодОборудования = КодОборудования;
            this.НазваниеОборудования = НазваниеОборудования;
        }
        [Key]
        public int КодОборудования
        {
            get
            {
                return КодОборудования_;
            }
            set
            {
                if (КодОборудования_!=value)
                {
                    КодОборудования_ = value;   
                }
            }
        }
        [MaxLength(25)]
        public string? НазваниеОборудования
        {
            get
            {
                return НазваниеОборудования_;
            }
            set 
            {
                if (НазваниеОборудования_!=value)
                {
                    НазваниеОборудования_ = value;  
                }
            }
        }
        public ICollection<Фотографии_Оборудование> Фотографии_Оборудование { get; set; }
    }
}
