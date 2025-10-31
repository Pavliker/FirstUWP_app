using AlbumApp1._0._1.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.ViewModels.Basic
{
   public partial class BasicViewModel: BasedViewModelContext
    {
        [ObservableProperty]
        public partial ShellViewModel ViewModel { get; set; }
        public INavigationService NavigationService { get; set; }

        public BasicViewModel()
        {
  
            ViewModel = App.GetService<ShellViewModel>();
            NavigationService = App.GetService<INavigationService>();   
        }
   
     
    }
}
