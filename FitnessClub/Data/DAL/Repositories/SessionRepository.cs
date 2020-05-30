using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace FitnessClub.Data.DAL.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        public SessionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Session> GetAllWithCoach() => Get(includeProperties: "Coach");
        public IEnumerable<Session> GetAllWithEnrollments() => Get(includeProperties: "SessionEnrollments");
        public IEnumerable<Session> GetAllWithRatings() => Get(includeProperties: "CoachRatings");
        public Session FindWithRatings(int id) => Get(filter: a => a.SessionID == id, includeProperties: "CoachRatings").FirstOrDefault();
        public Session FindWithEnrollments(int id) => Get(filter: a => a.SessionID == id, includeProperties: "SessionEnrollments").FirstOrDefault();
        public Session FindWithCoach(int id) => Get(filter: a => a.SessionID == id, includeProperties: "Coach").FirstOrDefault();
    }
}
