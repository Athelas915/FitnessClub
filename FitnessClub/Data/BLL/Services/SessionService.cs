using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly int userId;
        public SessionService(ISessionRepository sessionRepository, ICustomerRepository customerRepository, UserResolverService userResolverService)
        {
            this.sessionRepository = sessionRepository;
            this.customerRepository = customerRepository;
            userId = userResolverService.GetUserId();
        }


        public CoachViewModel ViewCoach(int sessionId)
        {
            var session = sessionRepository.FindWithCoach(sessionId);
            if (session == null)
            {
                Serilog.Log.Information($"Couldn't find the session with id {sessionId}.");
                return null;
            }
            else if (session.Coach == null)
            {
                Serilog.Log.Information($"The session with id {sessionId} doesn't have assigned coach yet.");
                return null;
            }
            return new CoachViewModel(session.Coach);
        }

        public async Task<IEnumerable<CustomerViewModel>> ViewCustomers(int sessionId)
        {
            var session = sessionRepository.FindWithEnrollments(sessionId);
            if (session == null)
            {
                Serilog.Log.Information($"Couldn't find the session with id {sessionId}.");
                return null;
            }
            var enrollments = session.SessionEnrollments.AsEnumerable();
            var customerIds = enrollments.Select(a => a.CustomerID);
            var customers = customerRepository.Get(filter: a => customerIds.Contains(a.PersonID));

            return customers.Select(a => new CustomerViewModel(a));
        }

        public IDictionary<SessionViewModel, int> ViewRatings(int sessionId)
        {
            var session = sessionRepository.FindWithRatings(sessionId);
            if (session == null)
            {
                Serilog.Log.Information($"Couldn't find the session with id {sessionId}.");
                return null;
            }
            var sessionView = new SessionViewModel(session);
            var output = new Dictionary<SessionViewModel, int>();
            foreach (var r in session.CoachRatings)
            {
                output.Add(sessionView, r.Rating);
            }
            return output;
        }
    }
}
