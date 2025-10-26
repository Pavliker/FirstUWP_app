using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Вопросы")]
    public partial class Вопросы : BaseClass
    {
        
        public Вопросы(int КодВопроса, int КодПользователя, string НазваниеВопроса)
        {
            this.КодВопроса = КодВопроса;
            this.КодПользователя = КодПользователя;
            this.НазваниеВопроса = НазваниеВопроса;
        }
        [Key]
        [ObservableProperty]
        public partial int КодВопроса
        {
            get;set;
        }
        [ForeignKey("Пользователи")]
        [ObservableProperty]
        public partial int КодПользователя
        {
            get;set;
        }
        public Пользователи Пользователи { get; set; }
        //[MaxLength(25)]
        [ObservableProperty]
        public partial string? НазваниеВопроса
        {
            get;set;
        }
    }
}
