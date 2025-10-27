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
        public string НазваниеРоли {  get; private set; }
        public  bool IsAuthenicated {  get; set; }
        public string AuthenticationType { get { return "Identity role"; } }
        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }
        public IdentityRole(string Name,  string НазваниеРоли)
        {
            this.Name = Name;
            this.НазваниеРоли = НазваниеРоли;
        }

    }
}
