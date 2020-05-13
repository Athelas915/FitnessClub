using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface ICoachRatingRepository : IGenericRepository<CoachRating>
    {
        Task<IList<Session>> GetUsersSessions(int id);
        Task<Coach> GetCoachById(int id);
        Task SetCoaches(IList<Session> sessions);
    }
}