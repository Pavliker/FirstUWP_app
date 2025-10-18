using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Фотографии_Места")]
    public class Фотографии_Места
    {
        private int КодФотографии_Места_;
        private int КодФотографии_;
        private int КодМеста_;
        public Фотографии_Места(int КодФотографии_Места, int КодФотографии, int КодМеста )
        {
            this.КодФотографии_Места = КодФотографии_Места;
            this.КодФотографии = КодФотографии;
            this.КодМеста = КодМеста;
        }
        [Key]
        public int КодФотографии_Места
        {
            get
            {
                return КодФотографии_Места_;
            }
            set
            {
                if (КодФотографии_Места_!=value)
                {
                    КодФотографии_Места_ = value;
                }
            }
        }
        [ForeignKey("Фотографии")]
        public int КодФотографии
        {
            get
            {
                return КодФотографии_;
            }
            set
            {
                if (КодФотографии_!=value) 
                {
                    КодФотографии_ = value;
                }
            }
        }
        public Фотографии Фотографии { get; set; }
        [ForeignKey("Места")]
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
        public Места Места { get; set; }

    }
}
