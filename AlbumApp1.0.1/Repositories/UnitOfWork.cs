using AlbumApp1._0._1.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        public AlbumDbContext context { get; set; }
        public UnitOfWork(AlbumDbContext context)
        {
            this.context = context;
        }
        public async  Task Save()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
               throw dbEx;
            }
        }



        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                   context = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
