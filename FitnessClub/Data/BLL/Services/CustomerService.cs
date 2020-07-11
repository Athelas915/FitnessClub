using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ISessionRepository sessionRepository;
        private readonly int userId;
        public CustomerService(ICustomerRepository customerRepository, ISessionRepository sessionRepository, UserResolverService userResolver)
        {
            this.customerRepository = customerRepository;
            this.sessionRepository = sessionRepository;
            userId = userResolver.GetUserId();
        }

        public async Task CreateMembership(CustomerViewModel customer, MembershipViewModel membership)
        {
            var c = customer.Model;
            customerRepository.Update(c);
            c.Memberships.Add(membership.Model);
            await customerRepository.Commit();  
        }

        public async Task DeleteMembership(CustomerViewModel customer, MembershipViewModel membership)
        {
            var c = customer.Model;
            customerRepository.Update(c);
            c.Memberships.Remove(membership.Model);
            await customerRepository.Commit();
        }

        public async Task<CustomerViewModel> GetCustomer(int userId)
            => new CustomerViewModel(
                (await customerRepository
                    .AddFilter(a => a.UserID == userId)
                    .Get())
                .FirstOrDefault());

        public async Task<CustomerViewModel> GetWithSessions(int userId)
            => new CustomerViewModel((
                    await customerRepository
                    .AddFilter(a => a.UserID == userId)
                    .Include(a => a.SessionEnrollments)
                    .Include("SessionEnrollments.Session")
                    .Include(a => a.CoachRatings)
                    .Get())
                .FirstOrDefault()
                );

        public async Task<CustomerViewModel> GetWithMemberships(int userId)
            => new CustomerViewModel((
                    await customerRepository
                    .AddFilter(a => a.UserID == userId)
                    .Include(a => a.Memberships)
                    .Get())
                .FirstOrDefault()
                );

        public async Task UpdateMembership(CustomerViewModel customer, MembershipViewModel membership)
        {
            var c = customer.Model;
            customerRepository.Update(c);
            c.Memberships.Remove(c.Memberships.FirstOrDefault(a => a.CustomerID == customer.Model.PersonID));
            c.Memberships.Add(membership.Model);
            await customerRepository.Commit();
        }
        public async Task RateCoach(SessionViewModel session, CustomerViewModel customer, int rating)
        {
            var c = customer.Model;
            customerRepository.Update(c);
            var s = (await sessionRepository
                .Include(a => a.Coach)
                .AddFilter(a => a.SessionID == session.SessionID)
                .Get())
                .FirstOrDefault();
            if (s.Coach != null)
            {
                c.CoachRatings.Add(
                    new CoachRating()
                    {
                        SessionID = session.SessionID,
                        CoachID = session.Coach.PersonID,
                        CustomerID = customer.PersonID,
                        Rating = rating,
                        CreatedBy = userId
                    });;
                await customerRepository.Commit();
            }
        }
    }
}
