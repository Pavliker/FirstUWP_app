using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Repositories
{
    public interface  IGenericRepository <TEntity> where TEntity : class

    {
        Task<TEntity> FindBy(Expression <Func<TEntity,bool>>predicate);
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Add(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAll();
        void Update(TEntity entity);
    }
}
