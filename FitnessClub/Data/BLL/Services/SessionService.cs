using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class SessionService : EmployeeService, ISessionService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly int userId;
        public SessionService(ISessionRepository sessionRepository, IEmployeeRepository employeeRepository, UserResolverService userResolverService) : base(sessionRepository, employeeRepository, userResolverService)
        {
            this.sessionRepository = sessionRepository;
            userId = userResolverService.GetUserId();
        }

        public async Task AssignCoach(int sessionId)
        {
            await Task.Yield();
            throw new NotImplementedException();
        }

        public async Task UnassignCoach(int sessionId)
        {
            await Task.Yield();
            throw new NotImplementedException();
        }

        public CoachViewModel ViewCoach(int sessionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerViewModel> ViewCustomers(int sessionId)
        {
            throw new NotImplementedException();
        }

        public IDictionary<SessionViewModel, int> ViewRatings(int sessionId)
        {
            throw new NotImplementedException();
        }
    }
}
