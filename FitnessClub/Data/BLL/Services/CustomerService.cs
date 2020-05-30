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
    public class CustomerService : ICustomerService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly int userId;
        public CustomerService(ISessionRepository sessionRepository, ICustomerRepository customerRepository, UserResolverService userResolverService)
        {
            this.sessionRepository = sessionRepository;
            this.customerRepository = customerRepository;
            userId = userResolverService.GetUserId();
        }
        public int GetCurrentPersonId()
        {
            var customer = customerRepository.Get(filter: a => a.UserID == userId).FirstOrDefault();
            if (customer == null)
            {
                Serilog.Log.Information($"Couldn't find the currently logged in user.");
                return -1;
            }
            return customer.PersonID;
        }
        public async Task CancelEnrollment(int customerId, int sessionId)
        {
            var customer = customerRepository.FindWithEnrollments(customerId);
            var enrollment = customer.SessionEnrollments.Where(a => a.CustomerID == customerId && a.SessionID == sessionId).FirstOrDefault();
            if (customer == null || enrollment == null)
            {
                Serilog.Log.Information($"Couldn't find the enrollment with given session id {sessionId} and person id {customerId}");
                return;
            }

            var result = customer.SessionEnrollments.Remove(enrollment);
            if (!result)
            {
                customerRepository.Dispose();
            }
            else
            {
                await sessionRepository.Commit();
            }

        }
        public async Task Enroll(int customerId, int sessionId)
        {
            var customer = customerRepository.FindWithEnrollments(customerId);
            if (customer == null)
            {
                Serilog.Log.Information($"Couldn't find the user with id {customerId}");
                return;
            }
            var enrollment = new SessionEnrollment() 
            {
                CustomerID = customerId,
                SessionID = sessionId,
                CreatedBy = userId
            };
            customer.SessionEnrollments.Add(enrollment);

            await customerRepository.Commit();
        }

        public async Task RateCoach(int customerId, int sessionId, int inputRating)
        {
            var customer = customerRepository.FindWithRatings(customerId);
            if (customer == null)
            {
                Serilog.Log.Information($"Couldn't find the user with id {customerId}");
                return;
            }
            var coachId = (await sessionRepository.GetById(sessionId)).CoachID;
            if (coachId == null)
            {
                Serilog.Log.Information($"Couldn't find coach for session with id {sessionId}. Perhaps coach was deleted from the databse.");
                return;
            }

            var rating = new CoachRating()
            {
                Rating = inputRating,
                CoachID = coachId.Value,
                CustomerID = customerId,
                SessionID = sessionId,
                CreatedBy = userId
            };
            customer.CoachRatings.Add(rating);

            await customerRepository.Commit();
        }

        public IEnumerable<SessionViewModel> ViewEnrolledUpcomingSessions(int customerId)
        {
            var customer = customerRepository.FindWithEnrollments(customerId);
            if (customer == null)
            {
                Serilog.Log.Information($"Couldn't find the user with id {customerId}");
                return null;
            }
            var sessionIds = customer.SessionEnrollments.Select(a => a.SessionID);

            var sessions = sessionRepository.Get(filter: a => sessionIds.Contains(a.SessionID) && a.Start > DateTime.Now, includeProperties: "Coach");
            return sessions.Select(a => new SessionViewModel(a));
        }

        public IEnumerable<MembershipViewModel> ViewMemberships(int customerId)
        {
            var customer = customerRepository.FindWithMemberships(customerId);
            if (customer == null)
            {
                Serilog.Log.Information($"Couldn't find the user with id {customerId}");
                return null;
            }
            var memberships = customer.Memberships.AsEnumerable();
            foreach (var m in memberships)
            {
                m.Customer = customer;
            }
            memberships = memberships.OrderBy(a => a.Start);
            return memberships.Select(a => new MembershipViewModel(a));
        }

        public IEnumerable<SessionViewModel> ViewPastSessions(int customerId)
        {
            var customer = customerRepository.FindWithEnrollments(customerId);
            if (customer == null)
            {
                Serilog.Log.Information($"Couldn't find the user with id {customerId}");
                return null;
            }
            var sessionIds = customer.SessionEnrollments.Select(a => a.SessionID);

            var sessions = sessionRepository.Get(filter: a => sessionIds.Contains(a.SessionID) && a.Finish < DateTime.Now, includeProperties: "Coach");
            return sessions.Select(a => new SessionViewModel(a));
        }

        public IEnumerable<SessionViewModel> ViewSessions(int customerId)
        {
            var customer = customerRepository.FindWithEnrollments(customerId);
            if (customer == null)
            {
                Serilog.Log.Information($"Couldn't find the user with id {customerId}");
                return null;
            }
            var sessionIds = customer.SessionEnrollments.Select(a => a.SessionID);

            var sessions = sessionRepository.Get(filter: a => sessionIds.Contains(a.SessionID), includeProperties: "Coach");
            return sessions.Select(a => new SessionViewModel(a));
        }

        public IEnumerable<SessionViewModel> ViewUnenrolledUpcomingSessions(int customerId)
        {
            var customer = customerRepository.FindWithEnrollments(customerId);
            if (customer == null)
            {
                Serilog.Log.Information($"Couldn't find the user with id {customerId}");
                return null;
            }
            var sessionIds = customer.SessionEnrollments.Select(a => a.SessionID);

            var sessions = sessionRepository.Get(filter: a => !sessionIds.Contains(a.SessionID) && a.Start > DateTime.Now, includeProperties: "Coach");
            return sessions.Select(a => new SessionViewModel(a));
        }

        public IEnumerable<SessionViewModel> ViewUnratedSessions(int customerId)
        {
            var customer = customerRepository.FindWithRatings(customerId);
            customer = customerRepository.FindWithEnrollments(customerId);
            if (customer == null)
            {
                Serilog.Log.Information($"Couldn't find the user with id {customerId}");
                return null;
            }
            var enrolledSessionIds = customer.SessionEnrollments.Select(a => a.SessionID);
            var ratedSessionIds = customer.CoachRatings.Select(a => a.SessionID);

            var sessions = sessionRepository.Get(
                filter: a => 
                    !ratedSessionIds.Contains(a.SessionID) &&
                    enrolledSessionIds.Contains(a.SessionID) && 
                    a.Finish < DateTime.Now, includeProperties: "Coach",
                orderBy: a =>
                    a.OrderBy(b => b.Start)
                );
            return sessions.Select(a => new SessionViewModel(a));
        }
    }
}
