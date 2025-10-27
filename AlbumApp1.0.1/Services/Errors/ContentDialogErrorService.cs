using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.WindowsViews;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.CodeDom;
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

        public ContentDialogErrorService(string _message):base() 
        {
           Message = _message;

             result = new List<ContentDialogResult> {
            ContentDialogResult.Primary,
            ContentDialogResult.Secondary
            };
        
            
        }
        public async Task<bool>  ShowDialogWindow(string message)
        {
            ContentDialog ErrorDialog = new ContentDialog
            {
                XamlRoot = App.Root,
                Title = "Error",
                Content = $"{Message}",
                PrimaryButtonText = "Продолжить",
                CloseButtonText = "Закрыть"
            };
            AnyResult = await ErrorDialog.ShowAsync();
            return AnyResult == ContentDialogResult.Primary;
         
        }
    }
}
