using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.BLL.Services
{
    public class CoachRatingService : ICoachRatingService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ISessionRepository sessionRepository;
        public CoachRatingService(
            ICustomerRepository customerRepository, 
            ISessionRepository sessionRepository 
            )
        {
            this.customerRepository = customerRepository;
            this.sessionRepository = sessionRepository;
        }
        protected IEnumerable<int> GetRatedSessions(int userId)
        {
            var customer = customerRepository.Get(filter: a => a.UserID == userId, includeProperties: "CoachRatings").FirstOrDefault();

            var sessionIds = customer.CoachRatings.Select(a => a.SessionID);

            return sessionIds;
        }
        protected IEnumerable<int> GetAllSessions(int userId)
        {
            var customer = customerRepository.Get(filter: a => a.UserID == userId, includeProperties: "SessionEnrollments").FirstOrDefault();

            var sessionIds = customer.CoachRatings.Select(a => a.SessionID);

            return sessionIds;
        }
        public IList<Session> GetUnratedSessions(int userId)
        {
            var allSessionIds = GetAllSessions(userId);
            var allRatedSIds = GetRatedSessions(userId);
            var sessionIds = allSessionIds.Where(a => !allRatedSIds.Contains(a));

            var sessions = sessionRepository.Get(filter: a => sessionIds.Contains(a.SessionID));

            return sessions.OrderBy(a => a.Start).ToList();
        }
        public void CreateRating(
            int rating, 
            int coachId,
            int customerId,
            int sessionId
            )
        {
            var customer = customerRepository.Get(filter: a => a.PersonID == customerId, includeProperties: "CoachRatings").FirstOrDefault();

            customerRepository.Update(customer);

            var newRating = new CoachRating
            {
                Rating = rating,
                CoachID = coachId,
                CustomerID = customerId,
                SessionID = sessionId
            };

            customer.CoachRatings.Add(newRating);

            customerRepository.Commit();
        }
    }
}
