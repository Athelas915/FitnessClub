using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IList<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        Task<TEntity> GetByID(int id);
        void Insert(TEntity entity);
        void Delete(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task Save();
        bool Any(int id);

    }
}
