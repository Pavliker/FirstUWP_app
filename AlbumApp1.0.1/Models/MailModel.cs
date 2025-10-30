using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models
{
        public partial class MailModel:ObservableValidator
    {
        [ObservableProperty]
        public partial string Email { get; set; }
        [ObservableProperty]
        public partial int Code { get; set; }
        [ObservableProperty]
        public partial string Pass { get; set; }
        [ObservableProperty]
        public partial int RandomPass {  get; set; }
        public MailModel()
        {

        }
    }
}
