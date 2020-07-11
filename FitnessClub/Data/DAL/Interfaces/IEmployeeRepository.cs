using FitnessClub.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IEmployeeRepository : IPersonRepository<Employee>
    {
    }
}
