using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models
{
    public partial class IdentityRole:IIdentity
    {
        public  string Name { get; private set; }
        public int КодРоли {  get; private set; }
        public  bool IsAuthenicated {  get; set; }
        public string AuthenticationType { get { return "Identity role"; } }
        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }
        public IdentityRole(string Name,  int КодРоли)
        {
            this.Name = Name;
            this.КодРоли = КодРоли;
        }

    }
}
