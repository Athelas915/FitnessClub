using FitnessClub.Data.Models;
using System.Collections.Generic;

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
