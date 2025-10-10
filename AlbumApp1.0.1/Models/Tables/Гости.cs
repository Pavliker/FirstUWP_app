using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Гости")]
    public class Гости 
    {
        private int КодГостя_;
        private int КодРоли_;
        private string? Логин_;
        public Гости(int КодГостя, int КодРоли, string Логин)
        {
            this.КодГостя = КодГостя;
            this.КодРоли = КодРоли;
            this.Логин = Логин;
        }
        public int КодГостя
        {
            get
            {
                return КодГостя_;
            }
            set
            {
                if (КодГостя_!=value)
                {
                    КодГостя_= value;   
                }
            }
        }
        public int КодРоли
        {
            get
            {
                return КодРоли_;
            }
            set
            {
                if (КодРоли_!=value)
                {
                    КодРоли_ = value;
                }
            }
        }
        public string? Логин
        {
            get => Логин_;
            set
            {
                if (Логин_!=value)
                {
                    Логин_ = value; 
                }
            }
        }


    }
}
