using FitnessClub.Data.Models;
using FitnessClub.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface ISessionService
    {
        IDictionary<SessionViewModel, int> ViewRatings(int sessionId);
        IEnumerable<CustomerViewModel> ViewCustomers(int sessionId);
        CoachViewModel ViewCoach(int sessionId);
        Task AssignCoach(int sessionId);
        Task UnassignCoach(int sessionId);
    }
}
