using AlbumApp1._0._1.ViewModels;
using AlbumApp1._0._1.Views;
using AlbumApp1._0._1.WindowsViews;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlbumApp1._0._1.Interfaces;
namespace AlbumApp1._0._1.Services
{
    class WindowManagerService:IWindowManagerServices
    {
        private UIElement? mainViewModel = null;

        public WindowManagerService()
        {
        }

        public    void  WindowOpen()
        {
            var window = App.GetService<MainWindow>();
             
                if (window.Content == null && window != null)
                {
                    window.Content = mainViewModel ?? new Frame();
                    window.Activate();
                
                }
             
           
        }
    }
}
