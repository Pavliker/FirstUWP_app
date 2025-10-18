using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Пользователи")]
   public class Пользователи
    {
        private int КодПользователя_;
        private int КодРоли_;
        private string? Логин_;
        private string? ХешированныйПароль_;
        private string? НазваниеПочты_;
        public Пользователи(int КодПользователя, int КодРоли, string Логин, string ХешированныйПароль, string НазваниеПочты)
        {
            this.КодПользователя = КодПользователя;
            this.КодРоли = КодРоли;
            this.Логин = Логин;
            this.ХешированныйПароль = ХешированныйПароль;
            this.НазваниеПочты = НазваниеПочты;
        }
        [Key]
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
        [ForeignKey("Роли")]
        public int КодРоли
        {
            get => КодРоли_;
            set
            {
                if (КодРоли_!=value)
                {
                    КодРоли_ = value;
                }
            }
        }
        public Роли Роли { get; set; }
        [Index(IsUnique =true),MaxLength(15)]
        public string? Логин
        {
            get
            {
                return Логин_;
            }
            set
            {
                if (Логин_!=value)
                {
                    Логин_ = value; 
                }
            }
        }
        [Required,MaxLength(100)]
        public string? ХешированныйПароль
        {
            get
            {
                return ХешированныйПароль_;

            }
            set
            {
                if (ХешированныйПароль_!=value)
                {
                    ХешированныйПароль_ = value;    
                }
            }

        }
        [MaxLength(40)]
        public string? НазваниеПочты
        {

            get
            {
                return НазваниеПочты_;
            }
            set
            {
                if (НазваниеПочты_!=value)
                {
                    НазваниеПочты_ = value; 
                }
            }
        }
        public ICollection<Альбомы> Альбомы { get; set; }
        public ICollection<Вопросы> Вопросы { get; set; }
        public ICollection<Фотографии> Фотографии { get; set; }
    }   
}
