using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlbumApp1._0._1.ViewModels
{
    public abstract class DialogViewModel: BasedViewModelContext
    {
        private bool isPrimaryEnabled;
        private string primaryText, closeText;

        protected DialogViewModel()
        {
           

            isPrimaryEnabled = true;
            primaryText = "Ok";
            closeText = "Cancel";
        }

        private RelayCommand primaryCommand;

        public IRelayCommand PrimaryCommand => primaryCommand ??= new RelayCommand(async ()=>await OnPrimaryExecuted());

        public bool IsPrimaryEnabled
        {
            get => isPrimaryEnabled;
            protected set => SetProperty(ref isPrimaryEnabled, value);
        }

        public string PrimaryText
        {
            get => primaryText;
            protected set => SetProperty(ref primaryText, value);
        }

        public string CloseText
        {
            get => closeText;
            protected set => SetProperty(ref closeText, value);
        }

        protected virtual async Task OnPrimaryExecuted()
        {

        }
      
    }
}
