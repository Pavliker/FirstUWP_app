using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Вопросы")]
    public class Вопросы 
    {
        private int КодВопроса_;
        private int КодПользователя_;
        private string? НазваниеВопроса_;
        public Вопросы(int КодВопроса, int КодПользователя, string НазваниеВопроса)
        {
            this.КодВопроса = КодВопроса;
            this.КодПользователя = КодПользователя;
            this.НазваниеВопроса = НазваниеВопроса;
        }
        public int КодВопроса
        {
            get => КодВопроса_;
            set
            {
                if (КодВопроса_!=value)
                {
                    КодВопроса_ = value;
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
        public string? НазваниеВопроса
        {
            get => НазваниеВопроса_;
            set
            {
                if (НазваниеВопроса_!=value)
                {
                    НазваниеВопроса_ = value;
                }
            }
        }
    }
}
