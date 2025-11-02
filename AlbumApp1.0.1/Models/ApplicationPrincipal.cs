using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models
{
    public class ApplicationPrincipal
    {
        public static Func<IPrincipal> _current = () => Thread.CurrentPrincipal;
        public static IPrincipal Current
        {
            get
            {
                { return _current(); }
            }
        }
        public static void SwitchCurrentPrincipal(Func<IPrincipal>principal)
        {
            _current = principal;
        }
    }
}
