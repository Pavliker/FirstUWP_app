using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Альбомы")]
   public class Альбомы
    {
        private int КодАльбома_;
        private int КодПользователя_;
        private DateTime ДатаСоздания_;
        private string? НазваниеАльбома_;
        private string? КраткоеОписание_;
        public Альбомы(int КодАльбома, int КодПользователя, DateTime ДатаСоздания, string НазваниеАльбома, string КраткоеОписание)
        {
            this.КодАльбома = КодАльбома;
            this.КодПользователя = КодПользователя;
            this.ДатаСоздания = ДатаСоздания;
            this.НазваниеАльбома = НазваниеАльбома;
            this.КраткоеОписание = КраткоеОписание;
        }
        public int КодАльбома
        {
            get
            {
                return КодАльбома_;
            }
            set
            {
                if (КодАльбома_ != value)
                {
                    КодАльбома_ = value;
                }
            }
        }
        public int КодПользователя
        {
            get => КодПользователя_;
            set
            {
                if (КодПользователя_!=value)
                {
                    КодПользователя_ = value;
                }
            }
        }
        public DateTime ДатаСоздания
        {
            get => ДатаСоздания_;
            set
            {
                if (ДатаСоздания_!=value)
                {
                    ДатаСоздания_ = value;
                }
            }
        }
        public string? НазваниеАльбома
        {
            get
            {
                return НазваниеАльбома_; 
            }
            set
            {
                if (НазваниеАльбома_!=value)
                {
                    НазваниеАльбома_ = value;
                }
            }
        }
        public string? КраткоеОписание
        {
            get => КраткоеОписание_;
            set
            {
                if (КраткоеОписание_!=value)
                {
                    КраткоеОписание_ = value;
                }
            }
        }

        
    }
}
