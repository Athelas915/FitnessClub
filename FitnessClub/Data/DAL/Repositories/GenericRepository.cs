using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Repositories
{
    public abstract class GenericRepository : IRepository
    {
        private readonly IUnitOfWork unitOfWork;
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            unitOfWork.Register(this);
        }
        public async Task Commit()
        {
            await unitOfWork.Save();
        }
        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
    public abstract class GenericRepository<TEntity> : GenericRepository, IRepository<TEntity> where TEntity : DataEntity
    {
        private readonly DbSet<TEntity> dbSet;
        public GenericRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            dbSet = unitOfWork.Set<TEntity>();
        }
        public IQueryable<TEntity> Get(
               Expression<Func<TEntity, bool>> filter = null,
               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
               string includeProperties = ""
            )
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
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
        public async Task<TEntity> GetById(params int[] Ids)
        {
            return await dbSet.FindAsync(Ids);
        }
        public async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }
        public async Task Delete(params int[] Ids)
        {
            var entity = await dbSet.FindAsync(Ids);
            dbSet.Remove(entity);
        }
        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }
        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
        }
        public async Task<bool> Any(params int[] Ids)
        {
            var entity = await dbSet.FindAsync(Ids);
            if (entity == null)
                {return false;}
            else
                {return true;}
        }
    }
}
