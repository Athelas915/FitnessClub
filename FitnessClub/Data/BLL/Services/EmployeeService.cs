using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task CreateHoliday(EmployeeViewModel employee, HolidayViewModel holiday)
        {
            var e = employee.Model;
            employeeRepository.Update(e);
            e.Holidays.Add(holiday.Model);
            await employeeRepository.Commit();
        }

        public async Task EditHoliday(EmployeeViewModel employee, HolidayViewModel holiday)
        {
            var c = employee.Model;
            employeeRepository.Update(c);
            c.Holidays.Remove(c.Holidays.FirstOrDefault(a => a.EmployeeID == employee.PersonID));
            c.Holidays.Add(holiday.Model);
            await employeeRepository.Commit();

        }
        public async Task RemoveHoliday(EmployeeViewModel employee, HolidayViewModel holiday)
        {
            var c = employee.Model;
            employeeRepository.Update(c);
            c.Holidays.Remove(holiday.Model);
            await employeeRepository.Commit();
        }

        public async Task<EmployeeViewModel> GetEmployee(int userId)
            => new EmployeeViewModel(
                (await employeeRepository
                    .AddFilter(a => a.UserID == userId)
                    .Get())
                .FirstOrDefault()
                );

        public async Task<EmployeeViewModel> GetWithHolidays(int userId)
            => new EmployeeViewModel(
                (await employeeRepository
                    .AddFilter(a => a.UserID == userId)
                    .Include(a => a.Holidays)
                    .Get())
                .FirstOrDefault()
                );

    }
}
