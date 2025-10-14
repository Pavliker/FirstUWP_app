using AlbumApp1._0._1.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Styles.Templates
{
    public partial class CustomTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item != null)
            {
                var viewModeltype = item.GetType();

                foreach (var rd in Application.Current.Resources.MergedDictionaries)
                {
                    foreach (var value in rd.Values)
                    {
                        if (value is DataTemplate dataTempate)
                        {
                            string contexType = dataTempate.GetValue(BasedViewModelContext.TypeProperty) as string;

                            if (contexType != null && contexType == viewModeltype.FullName)
                            {
                                return dataTempate;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
