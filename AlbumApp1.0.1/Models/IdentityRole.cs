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
        private bool _isDisposed;
        public  string Name { get; private set; }
        public string НазваниеРоли {  get; private set; }
        public string НазваниеПочты { get; private set; }

        public string AuthenticationType { get { return "Identity role"; } }
        private bool authStatus;
        public bool IsAuthenticated { 
            get {
                if (!string.IsNullOrEmpty(Name))
                {
                    authStatus = true;
                    return authStatus;
                }
                else
                {
                    authStatus = false;
                    return authStatus;
                }
            }
            set
            {
                authStatus = value;
            }
        } 
        public IdentityRole(bool isAuthenticated, string Name,  string НазваниеРоли, string НазваниеПочты)
        {
            this.Name = Name;
            this.НазваниеРоли = НазваниеРоли;
            this.НазваниеПочты = НазваниеПочты;
           authStatus = isAuthenticated;
        }
        public IdentityRole(string Name, string НазваниеРоли)
        {
            this.Name = Name;
            this.НазваниеРоли = НазваниеРоли;
        }
     
        public IdentityRole()
        {
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (disposing)
                {

                }
            }
        }
        ~IdentityRole()
        {
            Dispose(false);
        }
    }
}
