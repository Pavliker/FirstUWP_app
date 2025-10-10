using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Фотографии_Оборудование")]
    public class Фотографии_Оборудование
    {
        private int КодФотографии_Оборудование_;
        private int КодФотографии_;
        private int КодОборудования_;
        public Фотографии_Оборудование(int КодФотографии_Оборудование, int КодФотографии, int КодОборудования) {
            this.КодФотографии_Оборудование = КодФотографии_Оборудование;
            this.КодФотографии = КодФотографии;
            this.КодОборудования = КодОборудования;
        }
        public int КодФотографии_Оборудование
        {
            get
            {
                return КодФотографии_Оборудование_;
            }
            set
            {
                if (КодФотографии_Оборудование_!=value)
                {
                    КодФотографии_Оборудование_ = value;    
                }
            }
        }
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

    }


}
