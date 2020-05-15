using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Data.DAL.Repositories
{
    public class CoachRatingRepository : GenericRepository<CoachRating>, ICoachRatingRepository, IDisposable
    {
        public CoachRatingRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<IList<SessionEnrollment>> GetUsersSessionEnrollments(int personId)
        {
            IQueryable<SessionEnrollment> query = unitOfWork.Context.Set<SessionEnrollment>(); //here we're accessing all sessions that user participated in
            query = query.Where(p => p.PersonID == personId);

            return await query.ToListAsync();
        }
        //simple method that sets Coach and Session objects in SessionEnrollment properties. 
        //I tried doing a deep copy instead of editting the parameters, but it makes the code hard to read.
        public async Task SetSessionAndCoach(SessionEnrollment sessionEnrollment)
        {
            sessionEnrollment.Session = await GetByID<Session>(sessionEnrollment.SessionID);
            sessionEnrollment.Session.Coach = await GetByID<Coach>(sessionEnrollment.Session.PersonID);
        }
        public async Task<IList<SessionEnrollment>> GetUsersUnratedEnrollments(int userId)
        {
            var customer = await GetUserByIdentityId<Customer>(userId);

            var enrollments = await Get<SessionEnrollment>();
            enrollments = enrollments.Where(a => a.PersonID == customer.PersonID).ToList();

            var usersEnrollmentIds = enrollments.Select(b => b.SessionID).ToList();

            var ratings = await Get<CoachRating>();

            ratings = ratings.Where(c => c.CreatedBy == userId && usersEnrollmentIds.Contains(c.SessionID)).ToList();

            var ratedSessions = ratings.Select(d => d.SessionID).ToList();

            IList<SessionEnrollment> result = new List<SessionEnrollment>();

            foreach (var enrollment in enrollments)
            {
                if (!ratedSessions.Contains(enrollment.SessionID.Value))
                {
                    result.Add(enrollment);
                }
            }

            return result;
        }
    }
}