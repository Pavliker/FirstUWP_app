using AlbumApp1._0._1.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Styles.Templates
{
    public class UserGuestTemplateSelector : DataTemplateSelector
    {
        public DataTemplate UserTemplate { get; set; }
        public DataTemplate GuestTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is InputValidator model)
            {
                return model.IsGuest ? GuestTemplate : UserTemplate;
            }
            return base.SelectTemplateCore(item);
        }
    }
}
