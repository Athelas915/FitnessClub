using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public class LogRepository : ILogRepository
    {
        internal IUnitOfWork unitOfWork;
        internal DbSet<Log> dbSet;
        public LogRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            dbSet = unitOfWork.Context.Set<Log>();
        }
        public virtual async Task<IList<Log>> Get(
            Expression<Func<Log, bool>> filter = null,
            Func<IQueryable<Log>, IOrderedQueryable<Log>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<Log> query = dbSet;

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
        public virtual void DeleteAll(IList<Log> logs)
        {
            foreach (Log log in logs)
            {
                dbSet.Remove(log);
            }
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
