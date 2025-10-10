using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Места")]
    public class Места 
    {
        private int КодМеста_;
        private string? НазваниеМеста_;
        public Места(int КодМеста, string НазваниеМеста)
        {
            this.КодМеста = КодМеста;
            this.НазваниеМеста = НазваниеМеста;
        }
        public int КодМеста
        {
            get
            {
                return КодМеста_;
            }
            set
            {
                if (КодМеста_!=value)
                {
                    КодМеста_ = value;
                }
            }
        }
        public string? НазваниеМеста
        {
            get => НазваниеМеста_;
            set
            {
                if (НазваниеМеста_!=value)
                {
                    НазваниеМеста_= value;  
                }
            }
        }
    }
}
