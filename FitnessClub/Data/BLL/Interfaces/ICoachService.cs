using FitnessClub.Data.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ICoachService
    {
        Task<CoachViewModel> GetCoach(int userId);
        Task<CoachViewModel> GetWithAvgRating(int userId);
        Task<CoachViewModel> GetWithSessions(int userId);
        Task AssignCoach(SessionViewModel session, CoachViewModel coach);
        Task UnassignCoach(SessionViewModel session);
    }
}
