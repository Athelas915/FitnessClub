using System;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Repositories
{
    //As this entity has composite primary key instead of regular one, the program is overriding some GenericRepository methods so they throw exceptions, forcing user to use pair of IDs instead.
    public class SessionEnrollmentRepository : GenericRepository<SessionEnrollment>, ISessionEnrollmentRepository, IDisposable
    {
        public SessionEnrollmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public override void Delete(int id) 
        {
            throw new System.ArgumentException("Session Enrollment primary key parameter takes pair of PersonID and SessionID.");
        }
        public override async Task<SessionEnrollment> GetByID(int id)
        {
            await Task.FromResult<SessionEnrollment>(null);
            throw new System.ArgumentException("Session Enrollment primary key parameter takes pair of PersonID and SessionID.");
        }
        public override bool Any(int id)
        {
            throw new System.ArgumentException("Session Enrollment primary key parameter takes pair of PersonID and SessionID.");
        }

        public void Delete(int PersonID, int SessionID)
        {
            SessionEnrollment sessionEnrollment = dbSet.Find(PersonID, SessionID);
            dbSet.Remove(sessionEnrollment);
        }
        public virtual async Task<SessionEnrollment> GetByID(int PersonID, int SessionID)
        {
            return await dbSet.FindAsync(PersonID, SessionID);
        }
        public bool Any (int PersonID, int SessionID)
        {
            SessionEnrollment sessionEnrollment = dbSet.Find(PersonID, SessionID);
            if (sessionEnrollment == null) { return false; }
            else { return true; }
        }

        public override void Insert(SessionEnrollment sessionEnrollment)
        {
            if (Any(sessionEnrollment.PersonID.Value, sessionEnrollment.SessionID.Value) == true)
            {
                throw new System.InvalidOperationException("Connection between this customer and session already exists.");
            }
            else
            {
                base.Insert(sessionEnrollment);
            }
        }

    }
}