using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FitnessClub.Data.DAL.Interfaces;

namespace FitnessClub.Data.DAL.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal IUnitOfWork unitOfWork;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            dbSet = unitOfWork.Context.Set<TEntity>();
        }

        //when not given a type, this returns a list of type TEntity
        public virtual async Task<IList<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
        //when given a type 'OtherEntity', Get<OtherEntity> returns a list of that type.
        public virtual async Task<IList<OtherEntity>> Get<OtherEntity>(
            Expression<Func<OtherEntity, bool>> filter = null,
            Func<IQueryable<OtherEntity>, IOrderedQueryable<OtherEntity>> orderBy = null,
            string includeProperties = "") where OtherEntity : class
        {
            IQueryable<OtherEntity> query = unitOfWork.Context.Set<OtherEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
        public virtual async Task<TEntity> GetByID(int id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Delete(int id)
        {
            TEntity entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }
        public virtual void Delete(TEntity entity)
        {
            if (unitOfWork.Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            unitOfWork.Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual bool Any(int id)
        {
            TEntity entity = dbSet.Find(id);
            if (entity == null) { return false; }
            else { return true; }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    unitOfWork.Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task Submit()
        {
            await unitOfWork.Save();
        }
    }
}