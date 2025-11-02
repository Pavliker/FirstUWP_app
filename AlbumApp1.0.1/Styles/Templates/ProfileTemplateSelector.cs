using AlbumApp1._0._1.ViewModels.Basic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AlbumApp1._0._1.Styles.Templates
{
    public class ProfileTemplateSelector:DataTemplateSelector
    {
        public DataTemplate UnauthorizedUserTemplate { get; set; }
        public DataTemplate IsInGuestRole { get; set; }
        public DataTemplate UserActivityTemplate { get; set; }


        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is ProfileViewModel template)
            {
                if (template.AuthenticationStatus == false) 
                {
                    return UnauthorizedUserTemplate;
                }
                else if (template.RoleName == "Гость")
                {
                    return IsInGuestRole;
                }
                else
                {
                    return UserActivityTemplate;
                }

            }
            else
            {
                return null;
            }

        }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }
    }
}
