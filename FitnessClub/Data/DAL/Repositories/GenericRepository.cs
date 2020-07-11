using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Repositories
{
    public abstract class GenericRepository : IRepository
    {
        protected readonly IUnitOfWork unitOfWork;
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            unitOfWork.Register(this);

        }
        public async Task Commit()
        {
            await unitOfWork.Save();
        }

        public async Task<IDbContextTransaction> BeginTransaction() => await unitOfWork.BeginTransaction();


        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
    public abstract class GenericRepository<TEntity> : GenericRepository, IRepository<TEntity> where TEntity : DataEntity
    {
        private readonly DbSet<TEntity> dbSet;
        private IQueryable<TEntity> query = null;

        public GenericRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            dbSet = unitOfWork.Set<TEntity>();
        }
        private void NewQuery() => query = unitOfWork.Set<TEntity>().AsQueryable();
        private void InitQuery()
        {
            if (query == null) NewQuery();
        }
        public IRepository<TEntity> AddFilter(Expression<Func<TEntity, bool>> filter)
        {
            InitQuery();
            query = query.Where(filter);
            return this;
        }
        public IRepository<TEntity> OrderBy(Expression<Func<TEntity, object>> orderBy)
        {
            InitQuery();
            query = query.OrderBy(orderBy);
            return this;
        }
        public IRepository<TEntity> Include(Expression<Func<TEntity, object>> include)
        {
            InitQuery();
            query = query.Include(include);
            return this;
        }
        public IRepository<TEntity> Include(string include)
        {
            InitQuery();
            query = query.Include(include);
            return this;
        }
        public virtual async Task<IList<TEntity>> Get()
        {
            InitQuery();
            var result = await query.ToListAsync();
            query = null;
            return result;
        }
        public async Task<IList<TEntity>> GetAll()
        {
            NewQuery();
            return await Get();
        }
        public async Task<TEntity> FindById(int id)
            => await dbSet.FindAsync(id);

        public virtual async Task Insert(TEntity entity)
            => await dbSet.AddAsync(entity);
        public virtual async Task Delete(params int[] Ids)
            => dbSet.Remove(
            await dbSet
            .FindAsync(Ids)
            );
        public virtual void Delete(TEntity entity)
            => dbSet.Remove(entity);
        public virtual void Update(TEntity entity)
            => dbSet.Attach(entity);
        public virtual async Task<bool> Any(int id) => (await FindById(id)) == null ? false : true;

        
    }
}
