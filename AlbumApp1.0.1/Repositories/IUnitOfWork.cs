using AlbumApp1._0._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        AlbumDbContext context { get; set; }
        Task Save();
    }
}
