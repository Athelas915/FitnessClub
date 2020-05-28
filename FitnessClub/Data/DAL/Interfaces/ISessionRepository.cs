using FitnessClub.Data.Models;
using System.Collections.Generic;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        IEnumerable<Session> GetAllWithRatings();
        IEnumerable<Session> GetAllWithEnrollments();
        IEnumerable<Session> GetAllWithCoach();
        Session FindWithRatings(int id);
        Session FindWithEnrollments(int id);
        Session FindWithCoach(int id);
    }
}
