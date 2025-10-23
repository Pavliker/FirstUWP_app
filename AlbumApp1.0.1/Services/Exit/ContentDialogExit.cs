using AlbumApp1._0._1.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services.Exit
{
    public partial class ContentDialogExit:IContentDialogExit
    {
        private  ContentDialogResult Result = new();
        public ContentDialogExit()
        {
        }
        public async Task  OpenContentDialog (string content)  
        {
            ContentDialog ContentDialog = new ContentDialog
            {
                 XamlRoot = App.Root,
                 Title = "Закрытие",
                Content = $"{content}",
                CloseButtonText = "Отмена",
                PrimaryButtonText = "Продолжить"
            }; 
            Result = await ContentDialog.ShowAsync();
            if (Result ==  ContentDialogResult.Primary)
            {

                ContentDialog.Hide();
            }
            else 
            {
                ContentDialog.Hide();
            }
          
        }
    }
}
