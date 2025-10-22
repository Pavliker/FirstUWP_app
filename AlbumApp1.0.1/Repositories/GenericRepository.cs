using AlbumApp1._0._1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
           return await _dbSet.SingleAsync(predicate);
        }
        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FindAsync(id); 
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
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
