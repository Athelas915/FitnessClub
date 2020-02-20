using System.Collections.Generic;
using System.Linq;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;

namespace FitnessClub.Data.DAL
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        public SessionRepository(FCContext context) : base(context)
        {
            this.context = context;
        }
        public IEnumerable<Coach> GetCoaches()
        {
            return context.Coaches.ToList();
        }
    }
}