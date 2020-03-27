using System.Threading.Tasks;
using System.Collections.Generic;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public FCContext Context { get; }
        /*
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
        */
        Task Save();
    }
}
