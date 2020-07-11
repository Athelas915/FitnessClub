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
    public class SessionManagementService : ISessionManagementService
    {
        private readonly ISessionRepository sessionRepository;
        public SessionManagementService(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public async Task CreateSession(SessionViewModel session)
        {
            await sessionRepository.Insert(session.Model);
            await sessionRepository.Commit();
        }

        public async Task DeleteSession(SessionViewModel session)
        {
            sessionRepository.Delete(session.Model);
            await sessionRepository.Commit();
        }
        public async Task UpdateSession(SessionViewModel session)
        {
            sessionRepository.Update(session.Model);
            await sessionRepository.Commit();
        }

        public async Task<IList<SessionViewModel>> GetAllSessions() 
            => (await sessionRepository.GetAll())
            .Select(a => new SessionViewModel(a))
            .ToList();

        public async Task<IList<SessionViewModel>> GetSessionsForMonth(int year, int month)
        => (await sessionRepository
            .AddFilter(a => a.Start.Month == month && a.Start.Year == year)
            .Get())
            .Select(a => new SessionViewModel(a))
            .ToList();
        public async Task<SessionViewModel> GetById(int sessionId)
            => new SessionViewModel((
                await sessionRepository
                .AddFilter(a => a.SessionID == sessionId)
                .Include(a => a.Coach)
                .Get()
                ).FirstOrDefault());
    }
}