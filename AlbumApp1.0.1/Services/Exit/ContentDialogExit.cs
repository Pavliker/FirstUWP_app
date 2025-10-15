using AlbumApp1._0._1.Interfaces;
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
        private string Content;
        private readonly ContentDialog _ContentDialog;
        private Window _Window;
        private ContentDialogResult Result;
        private bool _IsClosing;
        public ContentDialogExit(string _content, Window window, bool closebtton)
        {
            Content = _content;
            _Window = window;
            _IsClosing = closebtton;
            _ContentDialog = new ContentDialog
            {
                XamlRoot = _Window.Content.XamlRoot,
                Title = "Закрытие",
                Content = $"{Content}",
                CloseButtonText = "Отмена",
                PrimaryButtonText = "Закрыть"
            };
        }
        public async void OpenContentDialog()
        {
            Result = await _ContentDialog.ShowAsync();
            if (Result ==  ContentDialogResult.Primary)
            {
                if (_IsClosing == true)
                {
                    _Window.Close();
                }
            }
            else 
            {
                _ContentDialog.Hide();
            }
          
        }
    }
}
