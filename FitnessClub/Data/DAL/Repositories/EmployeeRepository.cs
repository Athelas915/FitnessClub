using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace FitnessClub.Data.DAL.Repositories
{
    public class EmployeeRepository : PersonRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Employee> GetAllWithHolidays() => Get(includeProperties: "Holidays");
        public Employee FindWithHolidays(int id) => Get(filter: a => a.PersonID == id, includeProperties: "Holidays").FirstOrDefault();
    }
}
