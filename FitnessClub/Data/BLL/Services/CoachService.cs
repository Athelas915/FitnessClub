using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class CoachService : EmployeeService, ICoachService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly ICoachRepository coachRepository;
        private readonly ILogger<CoachService> logger;
        public CoachService(
            ISessionRepository sessionRepository,
            IEmployeeRepository employeeRepository,
            ICoachRepository coachRepository,
            UserResolverService userResolverService,
            ILogger<CoachService> logger,
            ILogger<EmployeeService> employeeLogger
            ) : base(sessionRepository, employeeRepository, userResolverService, employeeLogger)
        {
            this.sessionRepository = sessionRepository;
            this.coachRepository = coachRepository;
            this.logger = logger;
        }

        public async Task AssignToSession(int coachId, int sessionId)
        {
            var coach = await coachRepository.GetById(coachId);
            var session = sessionRepository.FindWithCoach(sessionId);
            if (coach == null )
            {
                logger.LogInformation($"Couldn't find the coach with id {coachId}.");
                return;
            }
            else if (session == null)
            {
                logger.LogInformation($"Couldn't find the session with id {sessionId}.");
                return;
            }
            if (session.Coach != null)
            {
                logger.LogInformation($"Session already has assigned coach. Removed coach with id {session.Coach.PersonID} from the session and assigned the new one.");
            }
            session.Coach = coach;
            session.CoachID = coach.PersonID;

            await sessionRepository.Commit();
        }

        public async Task UnassignFromSession(int coachId, int sessionId)
        {
            var coach = coachRepository.FindWithSessions(coachId);
            var session = coach.Sessions.Where(a => a.SessionID == sessionId).FirstOrDefault();
            if (coach == null || session == null)
            {
                logger.LogInformation($"Couldn't find the session with given session id {sessionId} and person id {coachId}");
                return;
            }

            var result = coach.Sessions.Remove(session);
            if (!result)
            {
                coachRepository.Dispose();
            }
            else
            {
                await sessionRepository.Commit();
            }
        }

        public IEnumerable<SessionViewModel> ViewAssignedSessions(int coachId)
        {
            var coach = coachRepository.FindWithSessions(coachId);
            if (coach == null)
            {
                logger.LogInformation($"Couldn't find the coach with given id {coachId}");
                return null;
            }
            var sessions = coach.Sessions.AsEnumerable();
            sessions = sessions.Where(a => a.Start > DateTime.Now);
            sessions = sessions.OrderBy(a => a.Start);
            foreach (var s in sessions)
            {
                s.Coach = coach;
            }
            return sessions.Select(a => new SessionViewModel(a));
        }

        public IEnumerable<SessionViewModel> ViewPastSessions(int coachId)
        {
            var coach = coachRepository.FindWithSessions(coachId);
            if (coach == null)
            {
                logger.LogInformation($"Couldn't find the coach with given id {coachId}");
                return null;
            }
            var sessions = coach.Sessions.AsEnumerable();
            sessions = sessions.Where(a => a.Finish < DateTime.Now);
            sessions = sessions.OrderBy(a => a.Start);
            foreach (var s in sessions)
            {
                s.Coach = coach;
            }
            return sessions.Select(a => new SessionViewModel(a));
        }

        public async Task<IDictionary<SessionViewModel, int>> ViewRatings(int coachId)
        {
            var coach = coachRepository.FindWithRatings(coachId);
            if (coach == null)
            {
                logger.LogInformation($"Couldn't find the coach with given id {coachId}");
                return null;
            }
            var ratings = coach.CoachRatings.AsEnumerable();
            foreach (var r in ratings)
            {
                r.Session = await sessionRepository.GetById(r.SessionID);
            }
            ratings.OrderBy(a => a.Session.Start);

            var output = new Dictionary<SessionViewModel, int>();
            foreach (var r in ratings)
            {
                output.Add(new SessionViewModel(r.Session), r.Rating);
            }
            return output;
        }

        public IEnumerable<SessionViewModel> ViewUnassignedUpcomingSessions() => sessionRepository.Get(filter: a => a.CoachID == null && a.Start > DateTime.Now, orderBy: a => a.OrderBy(b => b.Start)).Select(c => new SessionViewModel(c));
    }
}
