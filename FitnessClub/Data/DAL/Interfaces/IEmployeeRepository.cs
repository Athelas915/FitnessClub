﻿using FitnessClub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.DAL.Interfaces
{
    public interface IEmployeeRepository : IPersonRepository<Employee>
    {
        IEnumerable<Employee> GetAllWithHolidays();
        Employee FindWithHolidays(int id);
    }
}
