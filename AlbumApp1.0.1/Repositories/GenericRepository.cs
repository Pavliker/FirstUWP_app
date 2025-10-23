using AlbumApp1._0._1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AlbumApp1._0._1.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class 
    {
        //private readonly AlbumDbContext AlbumDbContext;
        private readonly Microsoft.EntityFrameworkCore.DbSet<TEntity> _dbSet;
        public AlbumDbContext Context { get; set; } 
        private readonly IUnitOfWork _unitOfWork;
        public GenericRepository(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            Context = _unitOfWork.context;
            _dbSet = Context.Set<TEntity>();
        }
        public async IAsyncEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            await foreach (var obj in _dbSet.Where(predicate).AsAsyncEnumerable())
            {
                    yield return obj;
            }
        }
        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FindAsync(id); 
        }
        public async IAsyncEnumerable<TEntity> GetAll()
        {
             await foreach (var obj in _dbSet.AsAsyncEnumerable())
            {
                yield return obj;
            }
        }
        public async Task Add(TEntity entity)
        {
             await _dbSet.AddAsync(entity);
        }
        public void Delete(TEntity entity)
        {
             _dbSet.Remove(entity);
        }
        public async Task DeleteAll()
        {
           var res =  await _dbSet.ToListAsync();
            foreach (var i in res)
            {
                int index =  res.IndexOf(i);
                res.RemoveAt(index);
            }

        }
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);  
        }
    }
}
