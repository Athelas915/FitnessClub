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
        public async Task SetSessionsAndCoaches(IList<SessionEnrollment> sessionEnrollments)
        {
            foreach (SessionEnrollment session in sessionEnrollments)
            {
                session.Session = await GetByID<Session>(session.SessionID);
                session.Session.Coach = await GetByID<Coach>(session.Session.PersonID);
            }
        }
    }
}