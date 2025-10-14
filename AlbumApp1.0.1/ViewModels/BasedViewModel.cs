using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.ViewModels
{
    public  class BasedViewModelContext : Microsoft.UI.Xaml.DependencyObject
    {
        public static readonly DependencyProperty TypeProperty =
           DependencyProperty.RegisterAttached(
             "BasedViewModelContext",
             typeof(string), //we need string here 
             typeof(BasedViewModelContext),
             new PropertyMetadata(null)
           );
        public static void SetType(DependencyObject element, string value)
        {
            element.SetValue(TypeProperty, value);
        }
        public static string GetType(DependencyObject element)
        {
            return (string)element.GetValue(TypeProperty);
        }
    }
}
