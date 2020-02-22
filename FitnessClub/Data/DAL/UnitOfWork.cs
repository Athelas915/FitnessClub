using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Data.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FCContext context;
        public IPersonRepository PersonRepository { get; }
        public ISessionRepository SessionRepository { get; }
        public ICoachRepository CoachRepository { get; }

        public UnitOfWork(FCContext context)
        {
            this.context = context;
            PersonRepository = new PersonRepository(this.context);
            SessionRepository = new SessionRepository(this.context);
            CoachRepository = new CoachRepository(this.context);
        }
        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
