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
        public async Task<IList<Session>> GetUsersSessions(int personId)
        {
            IQueryable<SessionEnrollment> query1 = unitOfWork.Context.Set<SessionEnrollment>(); //here we're accessing all sessions that user participated in
            query1 = query1.Where(p => p.PersonID == personId);
            var sessionIdsList = query1.Select(se => se.SessionID).ToList();

            IQueryable<Session> query2 = unitOfWork.Context.Set<Session>(); //here we're trying to get a list of the sessions from the list of IDs
            query2 = query2.Where(s => sessionIdsList.Contains(s.SessionID));

            query2 = query2.OrderBy(p => p.Start);

            return await query2.ToListAsync();
        }
        public async Task<Coach> GetCoachById(int id)
        {
            var coaches = await Get<Coach>();
            var query = coaches.AsQueryable();
            query = query.Where(p => p.PersonID == id);

            if (query.ToList().Count == 0)
            {
                throw new NullReferenceException("User with given ID doesn't exist");
            }
            else
            {
                return await query.FirstAsync();
            }
        }
        public async Task SetCoaches(IList<Session> sessions)
        {
            var coaches = await Get<Coach>();
            foreach (Session session in sessions)
            {
                session.Coach = coaches.Single(p => p.PersonID == session.PersonID);
            }
        }
    }
}