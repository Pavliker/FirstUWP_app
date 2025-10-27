using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models.Tables
{
    [Table("Гости")] 
    public partial class Гости: InputValidator
    {
        
        public Гости()
        {
      
        }
        [Key]        
        public  int КодГостя
        {
            get;set;
        }
        [ForeignKey("Роли")]
        public  int КодРоли
        {
            get;set;
        }
        public Роли Роли { get; set; }
        //[MaxLength(15)]
        [ObservableProperty]
        [Required(ErrorMessage = "Логин является обязательным полем")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Длина логина должна быть от 4 до 15 символов")]
        public partial string Логин
        {
            get;set;
        }
        partial void OnЛогинChanged(string value)
        {
            Validate(value, nameof(Логин));
        }
        

    }
}
