using FitnessClub.Data.Models;
using System.Collections.Generic;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        public IEnumerable<Coach> GetCoaches();
    }
}