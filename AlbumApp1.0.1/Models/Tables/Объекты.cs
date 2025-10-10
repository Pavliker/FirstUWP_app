using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Объекты")]
    public class Объекты
    {
        private int КодОбъекта_;
        private string? НазваниеОбъекта_;
        public Объекты(int КодОбъекта, string НазваниеОбъекта)
        {
            this.КодОбъекта = КодОбъекта;
            this.НазваниеОбъекта = НазваниеОбъекта; 
        }
        public int КодОбъекта
        {
            get
            {
                return КодОбъекта_;
            }
            set
            {
                if (КодОбъекта_ != value)
                {
                    КодОбъекта_ = value;
                }
            }
        }
        public string? НазваниеОбъекта
        {
            get => НазваниеОбъекта_;
            set
            {
                if (НазваниеОбъекта_!=value)
                {
                    НазваниеОбъекта_= value;
                }
            }
        }

    }
}
