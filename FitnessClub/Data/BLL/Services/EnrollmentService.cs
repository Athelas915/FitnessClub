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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ISessionRepository sessionRepository;
        public EnrollmentService(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }
        public async Task CancelEnrollment(CustomerViewModel customer, SessionViewModel session)
        {
            var s = session.Model;
            sessionRepository.Update(s);
            s.SessionEnrollments.Remove(
                s.SessionEnrollments.FirstOrDefault(
                    a =>
                    a.CustomerID == customer.PersonID 
                    && 
                    a.SessionID == session.SessionID)
                );
            await sessionRepository.Commit();
        }

        public async Task Enroll(CustomerViewModel customer, SessionViewModel session)
        {
            var s = session.Model;
            sessionRepository.Update(s);
            s.SessionEnrollments.Add(
                new SessionEnrollment()
                {
                    SessionID = s.SessionID,
                    CustomerID = customer.PersonID
                });
            await sessionRepository.Commit();
        }

        public async Task<SessionViewModel> GetWithParticipants(int sessionId)
            => new SessionViewModel(
                (await sessionRepository
                    .Include(a => a.SessionEnrollments)
                    .Include(a => a.SessionEnrollments.Select(c => c.Customer))
                    .AddFilter(a => a.SessionID == sessionId)
                    .Get())
                .FirstOrDefault()
                );
        public async Task<IList<SessionViewModel>> GetInMonthWithParticipants(int month, int year)
            => (await sessionRepository
                .Include(a => a.SessionEnrollments)
                .Include(a => a.SessionEnrollments.Select(c => c.Customer))
                .AddFilter(a => a.Start.Month == month && a.Start.Year == year)
                .Get())
            .Select(a => new SessionViewModel(a))
            .ToList();
    }
}
