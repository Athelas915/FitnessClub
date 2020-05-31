using FitnessClub.Data.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ISessionService
    {
        IDictionary<SessionViewModel, int> ViewRatings(int sessionId);
        IEnumerable<CustomerViewModel> ViewCustomers(int sessionId);
        CoachViewModel ViewCoach(int sessionId);
    }
}
