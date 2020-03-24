using System;
using System.Threading.Tasks;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Repositories;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FCContext context;
        public IPersonRepository<Person> PersonRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public IPersonRepository<Customer> CustomerRepository { get; }
        public IMembershipRepository MembershipRepository { get; }
        public IPersonRepository<Employee> EmployeeRepository { get; }
        public IHolidayRepository HolidayRepository { get; }
        public IPersonRepository<Coach> CoachRepository { get; }
        public ICoachRatingRepository CoachRatingRepository { get; }
        public ISessionRepository SessionRepository { get; }
        public ISessionEnrollmentRepository SessionEnrollmentRepository { get; }

        public UnitOfWork(FCContext context)
        {
            this.context = context;
            PersonRepository  = new PersonRepository<Person>(this.context);
            AddressRepository = new AddressRepository(this.context);
            CustomerRepository = new PersonRepository<Customer>(this.context);
            MembershipRepository = new MembershipRepository(this.context);
            EmployeeRepository = new PersonRepository<Employee>(this.context);
            HolidayRepository = new HolidayRepository(this.context);
            CoachRepository = new PersonRepository<Coach>(this.context);
            CoachRatingRepository = new CoachRatingRepository(this.context);
            SessionRepository = new SessionRepository(this.context);
            SessionEnrollmentRepository = new SessionEnrollmentRepository(this.context);

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
