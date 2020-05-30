using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IRepository : IDisposable
    {
        Task Commit();
        Task<IDbContextTransaction> BeginTransaction();
    }
    public interface IRepository<TEntity> : IRepository where TEntity : DataEntity
    {
        IEnumerable<TEntity> Get(
               Expression<Func<TEntity, bool>> filter = null,
               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
               string includeProperties = ""
            );
        Task<TEntity> GetById(params int[] Ids);
        Task Insert(TEntity entity);
        Task Delete(params int[] Ids);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task<bool> Any(params int[] Ids);
    }
}
