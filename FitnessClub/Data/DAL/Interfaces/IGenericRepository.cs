using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> : IRepository where TEntity : class
    {

        Task<IList<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        Task<IList<OtherEntity>> Get<OtherEntity>(
            Expression<Func<OtherEntity, bool>> filter = null,
            Func<IQueryable<OtherEntity>, IOrderedQueryable<OtherEntity>> orderBy = null,
            string includeProperties = "") where OtherEntity : class;
        Task<TEntity> GetByID(params object[] Ids);
        Task<OtherEntity> GetByID<OtherEntity>(params object[] Ids) where OtherEntity : class;
        Task<PersonEntity> GetUserByIdentityId<PersonEntity>(int aspNetUserId) where PersonEntity : Person;
        void Insert(TEntity entity);
        void Delete(params object[] Ids);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        bool Any(params object[] Ids);

    }
}
