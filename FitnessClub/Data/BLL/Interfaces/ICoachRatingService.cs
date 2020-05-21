using FitnessClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ICoachRatingService
    {
        IList<Session> GetUnratedSessions(int userId);
        void CreateRating(
            int rating,
            int coachId,
            int customerId,
            int sessionId
            );
    }
}
