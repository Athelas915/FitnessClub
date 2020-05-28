using FitnessClub.Data.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ICoachService : IEmployeeService
    {
        Task AssignToSession(int coachId, int sessionId);
        Task UnassignFromSession(int coachId, int sessionId);
        IEnumerable<SessionViewModel> ViewAssignedSessions(int coachId);
        IEnumerable<SessionViewModel> ViewPastSessions(int coachId);
        IEnumerable<SessionViewModel> ViewUnassignedUpcomingSessions();
        IDictionary<SessionViewModel, int> ViewRatings(int coachId);
    }
}
