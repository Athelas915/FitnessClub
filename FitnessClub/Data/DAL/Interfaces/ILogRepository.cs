using FitnessClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface ILogRepository : IRepository
    {
        Task<IList<Log>> Get(
            Expression<Func<Log, bool>> filter = null,
            Func<IQueryable<Log>, IOrderedQueryable<Log>> orderBy = null);
        void DeleteAllLogs();
    }
}
