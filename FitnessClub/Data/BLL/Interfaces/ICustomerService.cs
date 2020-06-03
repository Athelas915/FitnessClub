using FitnessClub.Data.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<MembershipViewModel> ViewMemberships(int userId);
        IEnumerable<SessionViewModel> ViewSessions(int userId);
        IEnumerable<SessionViewModel> ViewEnrolledUpcomingSessions(int userId);
        IEnumerable<SessionViewModel> ViewUnenrolledUpcomingSessions(int userId);
        IEnumerable<SessionViewModel> ViewPastSessions(int userId);
        Task Enroll(int userId, int sessionId);
        Task CancelEnrollment(int userId, int sessionId);
        IEnumerable<SessionViewModel> ViewUnratedSessions(int userId);
        Task RateCoach(int userId, int sessionId, int inputRating);
    }
}
