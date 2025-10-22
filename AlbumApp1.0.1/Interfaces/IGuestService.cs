using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Interfaces
{
    public interface IGuestService
    {
        Task AddGuest(int КодРоли, string Логин);
    }
}
