using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Альбомы_Фотографии")]
    public class Альбомы_Фотографии 
    {
        private int КодАльбома_Фотографии_;
        private int КодАльбома_;
        private int КодФотографии_;
        public Альбомы_Фотографии(int КодАльбома_Фотографии, int КодАльбома, int КодФотографии )
        {
            this.КодАльбома_Фотографии = КодАльбома_Фотографии;
            this.КодАльбома = КодАльбома;
            this.КодФотографии = КодФотографии;
        }
        [Key]
        public int КодАльбома_Фотографии
        {
            get
            {
                return КодАльбома_Фотографии_;
            }
            set
            {
                if (КодАльбома_Фотографии_!=value)
                {
                    КодАльбома_Фотографии_ = value;
                }
            }
        }
        [ForeignKey("Альбомы")]
        public int КодАльбома
        {
            get => КодАльбома_;
            set
            {
                if (КодАльбома_!=value)
                {
                    КодАльбома_ = value;
                }
            }
        }
        public Альбомы Альбомы {  get; set; }
        [ForeignKey("Фотографии")]
        public int КодФотографии
        {
            get => КодФотографии_;
            set
            {
                if (КодФотографии_!=value)
                {
                    КодФотографии_ = value;
                }
            }
        }
        public Фотографии Фотографии { get; set; }
    }
}
