using System.Threading.Tasks;
using FitnessClub.Data.Models;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IPersonRepository<Person> PersonRepository { get; }
        IAddressRepository AddressRepository { get; }
        IPersonRepository<Customer> CustomerRepository { get; }
        IPersonRepository<Employee> EmployeeRepository { get; }
        IPersonRepository<Coach> CoachRepository { get; }
        ISessionRepository SessionRepository { get; }
        Task Commit();
    }
}
