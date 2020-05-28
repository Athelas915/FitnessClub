using FitnessClub.Data.Models;
using System.Collections.Generic;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IEmployeeRepository : IPersonRepository<Employee>
    {
        IEnumerable<Employee> GetAllWithHolidays();
        Employee FindWithHolidays(int id);
    }
}
