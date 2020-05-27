using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
