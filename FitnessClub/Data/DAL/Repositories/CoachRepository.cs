using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace FitnessClub.Data.DAL.Repositories
{
    public class CoachRepository : PersonRepository<Coach>, ICoachRepository
    {
        public CoachRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Coach> GetAllWithHolidays() => Get(includeProperties: "Holidays");
        public IEnumerable<Coach> GetAllWithRatings() => Get(includeProperties: "CoachRatings");
        public IEnumerable<Coach> GetAllWithSessions() => Get(includeProperties: "Sessions");
        public Coach FindWithHolidays(int id) => Get(filter: a => a.PersonID == id, includeProperties: "Holidays").FirstOrDefault();
        public Coach FindWithSessions(int id) => Get(filter: a => a.PersonID == id, includeProperties: "Sessions").FirstOrDefault();
        public Coach FindWithRatings(int id) => Get(filter: a => a.PersonID == id, includeProperties: "CoachRatings").FirstOrDefault();
    }
}
