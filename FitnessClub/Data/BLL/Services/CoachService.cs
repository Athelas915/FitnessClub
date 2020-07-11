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
    public class CoachService : ICoachService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly ICoachRepository coachRepository;
        public CoachService(
            ISessionRepository sessionRepository,
            ICoachRepository coachRepository
            )
        {
            this.sessionRepository = sessionRepository;
            this.coachRepository = coachRepository;
        }

        public async Task AssignCoach(SessionViewModel session, CoachViewModel coach)
        {
            var s = session.Model;
            sessionRepository.Update(s);
            s.Coach = coach.Model;
            await sessionRepository.Commit();
        }

        public async Task<CoachViewModel> GetCoach(int userId)
            => new CoachViewModel(
                (await coachRepository
                .AddFilter(a => a.UserID == userId)
                .Get())
                .FirstOrDefault());

        public async Task<CoachViewModel> GetWithAvgRating(int userId)
            => new CoachViewModel(
                (await coachRepository
                .AddFilter(a => a.UserID == userId)
                .Include(a => a.CoachRatings)
                .Get())
                .FirstOrDefault());

        public async Task<CoachViewModel> GetWithSessions(int userId)
            => new CoachViewModel(
                (await coachRepository
                .AddFilter(a => a.UserID == userId)
                .Include(a => a.Sessions)
                .Get())
                .FirstOrDefault());


        public async Task UnassignCoach(SessionViewModel session)
        {
            var s = session.Model;
            sessionRepository.Update(s);
            s.Coach = null;
            await sessionRepository.Commit();
        }
    }
}