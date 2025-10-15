using AlbumApp1._0._1.Interfaces;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services.Subscription
{
   public partial class ContentSubscriptionDialogService:IContentSubscriptionDialogService
    {
        public string? Content;
        public string? Title;
        public ContentDialogResult Result { get; set; }
        private readonly ContentDialog _contentdialog;
        private Window? my_window;
        public ContentSubscriptionDialogService(string? _content, string? _title, Window _my_window)
        {
            Content = _content;
            Title = _title;
            my_window = _my_window;

            _contentdialog = new ContentDialog
            {
                XamlRoot = my_window.Content.XamlRoot,
                Title = $"{this.Title}",
                Content = $"{this.Content}",
                PrimaryButtonText = "Продолжить",
                CloseButtonText = "Отменить"
            };
        }
        public async void ShowDialogWindow()
        {
           Result =  await _contentdialog.ShowAsync();
            if (Result == ContentDialogResult.Primary)
            {
                my_window.Close();
            }
            else if (Result == ContentDialogResult.None)
            {
                _contentdialog.Hide();
            }
        }

    }
}
