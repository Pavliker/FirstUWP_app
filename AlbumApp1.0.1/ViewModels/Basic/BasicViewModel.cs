using AlbumApp1._0._1.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.ViewModels.Basic
{
   public partial class BasicViewModel: ObservableObject
    {
        private readonly IAuthenticationService authentication;
        public BasicViewModel(IAuthenticationService authentication)
        {
            this.authentication = authentication;
        }
        public string Username
        {
            get { return authentication.AuthenticationName; }
        }
    }
}
