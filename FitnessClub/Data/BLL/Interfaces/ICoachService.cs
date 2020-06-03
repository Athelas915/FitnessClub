using FitnessClub.Data.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ICoachService
    {
        Task AssignToSession(int userId, int sessionId);
        Task UnassignFromSession(int userId, int sessionId);
        IEnumerable<SessionViewModel> ViewAssignedSessions(int userId);
        IEnumerable<SessionViewModel> ViewPastSessions(int userId);
        IEnumerable<SessionViewModel> ViewUnassignedUpcomingSessions();
        Task<IDictionary<SessionViewModel, int>> ViewRatings(int userId);
    }
}
