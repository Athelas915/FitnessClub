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
        IRepository<TEntity> AddFilter(Expression<Func<TEntity, bool>> filter);
        IRepository<TEntity> OrderBy(Expression<Func<TEntity, object>> orderBy);
        IRepository<TEntity> Include(Expression<Func<TEntity, object>> include);
        IRepository<TEntity> Include(string include);
        Task<IList<TEntity>> Get();
        Task<IList<TEntity>> GetAll();
        Task<TEntity> FindById(int id);
        Task Insert(TEntity entity);
        Task Delete(params int[] Ids);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task<bool> Any(int id);
    }
}
