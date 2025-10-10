using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("АрхивФотографий")]
    public class АрхивФотографий
    {
        private int КодФотографии_;
        private Guid КодСтроки_;
        private DateTime ДатаЗагрузки_;
        private string? НазваниеФотографии_;
        private string? Описание_;
        private string? Формат_;
        private string? Разрешение_;
        private int Уникальность_;
        private byte[]? Путь_;
        public АрхивФотографий(int КодФотографии, Guid КодСтроки, DateTime ДатаЗагрузки, string НазваниеФотографии, string Описание, string Формат, string Разрешение, int Уникальность, byte[] Путь)
        {
            this.КодФотографии = КодФотографии;
            this.КодСтроки = КодСтроки;
            this.ДатаЗагрузки = ДатаЗагрузки;
            this.НазваниеФотографии = НазваниеФотографии;
            this.Описание = Описание;
            this.Формат = Формат;
            this.Разрешение = Разрешение;
            this.Уникальность = Уникальность;
            this.Путь = Путь;
        }
        public int КодФотографии
        {
            get
            {
                return КодФотографии_;
            }
            set
            {
                if (КодФотографии_ != value)
                {
                    КодФотографии_ = value;
                }
            }
        }
        public Guid КодСтроки
        {
            get
            {
                return КодСтроки_;
            }
            set
            {
                if (КодСтроки_ != value)
                {
                    КодСтроки_ = value;
                }
            }
        }
      
        public DateTime ДатаЗагрузки
        {
            get
            {
                return ДатаЗагрузки_;
            }
            set
            {
                if (ДатаЗагрузки_ != value)
                {
                    ДатаЗагрузки_ = value;
                }
            }
        }
        public string? НазваниеФотографии
        {
            get
            {
                return НазваниеФотографии_;
            }
            set
            {
                if (НазваниеФотографии_ != value)
                {
                    НазваниеФотографии_ = value;
                }
            }
        }
        public string? Описание
        {
            get
            {
                return Описание_;

            }
            set
            {
                if (Описание_ != value)
                {
                    Описание_ = value;
                }
            }
        }
        public string? Формат
        {
            get
            {
                return Формат_;
            }
            set
            {
                if (Формат_ != value)
                {
                    Формат_ = value;
                }
            }
        }
        public string? Разрешение
        {
            get
            {
                return Разрешение_;
            }
            set
            {
                if (Разрешение_ != value)
                {
                    Разрешение_ = value;
                }
            }
        }
        public int Уникальность
        {
            get
            {
                return Уникальность_;
            }
            set
            {
                if (Уникальность_ != value)
                {
                    Уникальность_ = value;
                }
            }
        }
        public byte[]? Путь
        {
            get
            {
                return Путь_;
            }
            set
            {
                if (Путь_ != value)
                {
                    Путь_ = value;
                }
            }
        }
    }
}
