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
    public class CoachService : EmployeeService, ICoachService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly int userId;
        public CoachService(ISessionRepository sessionRepository, IEmployeeRepository employeeRepository, UserResolverService userResolverService) : base(sessionRepository, employeeRepository, userResolverService)
        {
            this.sessionRepository = sessionRepository;
            userId = userResolverService.GetUserId();
        }

        public async Task AssignToSession(int coachId, int sessionId)
        {
            await Task.Yield();
            throw new NotImplementedException();
        }

        public async Task UnassignFromSession(int coachId, int sessionId)
        {
            await Task.Yield();
            throw new NotImplementedException();
        }

        public IEnumerable<SessionViewModel> ViewAssignedSessions(int coachId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SessionViewModel> ViewPastSessions(int coachId)
        {
            throw new NotImplementedException();
        }

        public IDictionary<SessionViewModel, int> ViewRatings(int coachId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SessionViewModel> ViewUnassignedUpcomingSessions()
        {
            throw new NotImplementedException();
        }
    }
}
