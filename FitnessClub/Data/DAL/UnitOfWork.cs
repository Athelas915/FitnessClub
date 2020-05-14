using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Repositories;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public FCContext Context { get; }
        public int LoggedUserId { get; }

        public UnitOfWork(FCContext context, UserResolverService userResolver)
        {
            Context = context;
            LoggedUserId = userResolver.GetUserId();
        }
    
        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
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
