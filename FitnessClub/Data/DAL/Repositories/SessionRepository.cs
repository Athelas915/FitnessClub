using System;
using FitnessClub.Data.Models;
using FitnessClub.Data.DAL.Interfaces;

namespace FitnessClub.Data.DAL.Repositories
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository, IDisposable
    {
        public SessionRepository(FCContext context) : base(context)
        {
            this.context = context;
        }
    }
}