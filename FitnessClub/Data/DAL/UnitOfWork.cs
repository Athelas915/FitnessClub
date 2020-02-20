using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FCContext context;
        private IPersonRepository<Person> personRepository;
        private ISessionRepository sessionRepository;

        public UnitOfWork(FCContext context)
        {
            this.context = context;
        }

        public IPersonRepository<Person> PersonRepository
        {
            get
            {
                if (this.personRepository == null)
                {
                    this.personRepository = new PersonRepository<Person>(context);
                }
                return personRepository;
            }
        }
        public ISessionRepository SessionRepository
        {
            get
            {
                if (this.sessionRepository == null)
                {
                    this.sessionRepository = new SessionRepository(context);
                }
                return sessionRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
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
