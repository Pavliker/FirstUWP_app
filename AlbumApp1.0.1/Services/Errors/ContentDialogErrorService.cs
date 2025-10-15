using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.WindowsViews;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services
{
    public partial class ContentDialogErrorService : Exception, IContentDialogErrorService
    {
        public List<ContentDialogResult> result { get; set; }
        public ContentDialogResult AnyResult;
        public override string Message { get; }
        private readonly ContentDialog ErrorDialog;
        public Window Window {get;}

        public ContentDialogErrorService(string _message, Window window):base() 
        {
           Message = _message;
            Window = window;

             result = new List<ContentDialogResult> {
            ContentDialogResult.Primary,
            ContentDialogResult.Secondary
            };
            ErrorDialog = new ContentDialog {
                XamlRoot = Window.Content.XamlRoot,
                Title = "Error",
                Content = $"{Message}",
                PrimaryButtonText = "Продолжить",
                CloseButtonText = "Закрыть"
            };
            
        }
        public async void ShowDialogWindow()
        {
            AnyResult = await ErrorDialog.ShowAsync();
        
            if (AnyResult == result[0])
            {
                throw new Exception(Message);
            }
            else if (AnyResult == result[1]){
                ErrorDialog.Hide();
            }
        }
    }
}
