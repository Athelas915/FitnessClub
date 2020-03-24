using System;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;

namespace FitnessClub.Data.DAL.Repositories
{
    public class SessionEnrollmentRepository : GenericRepository<SessionEnrollment>, ISessionEnrollmentRepository, IDisposable
    {
        public SessionEnrollmentRepository(FCContext context) : base(context)
        {
            this.context = context;
        }
    }
}