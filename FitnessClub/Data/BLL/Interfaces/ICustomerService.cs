using FitnessClub.Data.Models;
using FitnessClub.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ICustomerService
    {
        int GetCurrentCustomerId();
        IEnumerable<MembershipViewModel> ViewMemberships(int customerId);
        IEnumerable<SessionViewModel> ViewSessions(int customerId);
        IEnumerable<SessionViewModel> ViewEnrolledUpcomingSessions(int customerId);
        IEnumerable<SessionViewModel> ViewUnenrolledUpcomingSessions(int customerId);
        IEnumerable<SessionViewModel> ViewPastSessions(int customerId);
        Task Enroll(int customerId, int sessionId);
        Task CancelEnrollment(int customerId, int sessionId);
        IEnumerable<SessionViewModel> ViewUnratedSessions(int customerId);
        Task RateCoach(int customerId, int sessionId, int inputRating);
    }
}
