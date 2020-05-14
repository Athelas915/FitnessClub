using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface ICoachRatingRepository : IGenericRepository<CoachRating>
    {
        Task<IList<SessionEnrollment>> GetUsersSessionEnrollments(int personId);
        Task SetSessionsAndCoaches(IList<SessionEnrollment> sessionEnrollments);
    }
}