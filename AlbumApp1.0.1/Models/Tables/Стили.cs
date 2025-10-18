using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Стили")]
   public class Стили
    {
        private int КодСтиля_;
        private string? НазваниеСтиля_;
        public Стили(int КодСтиля, string НазваниеСтиля)
        {
            this.КодСтиля = КодСтиля;
            this.НазваниеСтиля = НазваниеСтиля;
        }
        [Key]
        public int КодСтиля
        {
            get => КодСтиля_;
            set
            {
                if (КодСтиля_!=value)
                {
                    КодСтиля_ = value;  
                }
            }
        }
        [Index(IsUnique =true),MaxLength(25)]
        public string? НазваниеСтиля
        {
            get => НазваниеСтиля_;
            set
            {
                if (НазваниеСтиля_!=value)
                {
                    НазваниеСтиля_ = value;
                }
            }
        }
        public ICollection<Фотографии> Фтографии { get; set; }
    }
}
