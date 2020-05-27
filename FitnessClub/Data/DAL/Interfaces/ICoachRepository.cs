using FitnessClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface ICoachRepository : IPersonRepository<Coach>
    {
        IEnumerable<Coach> GetAllWithHolidays();
        IEnumerable<Coach> GetAllWithSessions();
        IEnumerable<Coach> GetAllWithRatings();
        Coach FindWithHolidays(int id);
        Coach FindWithSessions(int id);
        Coach FindWithRatings(int id);
    }
}
