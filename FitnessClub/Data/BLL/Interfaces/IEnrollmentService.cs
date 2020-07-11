using FitnessClub.Data.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IEnrollmentService
    {
        Task Enroll(CustomerViewModel customer, SessionViewModel session);
        Task CancelEnrollment(CustomerViewModel customer, SessionViewModel session);
        Task<SessionViewModel> GetWithParticipants(int sessionId);
    }
}
