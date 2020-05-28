using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Repositories
{
    public class LogRepository : GenericRepository, ILogRepository
    {
        private readonly DbSet<Log> dbSet;
        public LogRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            dbSet = unitOfWork.Set<Log>();
        }
        public virtual async Task<IList<Log>> Get(
            Expression<Func<Log, bool>> filter = null,
            Func<IQueryable<Log>, IOrderedQueryable<Log>> orderBy = null)
        {
            IQueryable<Log> query = dbSet;
            
            if (filter != null)
            {
                query = query.Where(filter);
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
        public virtual void DeleteAllLogs()
        {
            foreach (var log in dbSet)
            {
                dbSet.Remove(log);
            }
        }
    }
}
