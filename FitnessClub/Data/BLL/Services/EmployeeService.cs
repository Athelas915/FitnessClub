using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ISessionRepository sessionRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly int userId;
        public EmployeeService(ISessionRepository sessionRepository, IEmployeeRepository employeeRepository, UserResolverService userResolverService)
        {
            this.sessionRepository = sessionRepository;
            this.employeeRepository = employeeRepository;
            userId = userResolverService.GetUserId();
        }

        public async Task AddHoliday(int employeeId, HolidayViewModel inputHoliday)
        {
            var employee = employeeRepository.FindWithHolidays(employeeId);
            if (employee == null)
            {
                Serilog.Log.Information($"Couldn't find the user with id {employeeId}");
                return;
            }
            var holiday = new Holiday()
            {
                EmployeeID = employeeId,
                Start = Convert.ToDateTime(inputHoliday.Start),
                Finish = Convert.ToDateTime(inputHoliday.Finish),
                CreatedBy = userId
            };
            employee.Holidays.Add(holiday);

            await employeeRepository.Commit();
        }

        public async Task<bool> EditHoliday(int employeeId, HolidayViewModel inputHoliday)
        {
            var employee = employeeRepository.FindWithHolidays(employeeId);
            var holiday = employee.Holidays.Where(a => a.HolidayID == inputHoliday.HolidayID).FirstOrDefault();
            if (employee == null || holiday == null)
            {
                Serilog.Log.Information($"Couldn't find the holiday with given session id {inputHoliday.HolidayID} and person id {employeeId}");
                return false;
            }
            else if (holiday.Finish < DateTime.Now)
            {
                Serilog.Log.Information($"The holidays already ended. Users can edit only the holidays that are upcoming.");
                return false;
            }
            if (holiday.Start > DateTime.Now)
            {
                holiday.Start = Convert.ToDateTime(inputHoliday.Start);
            }
            holiday.Finish = Convert.ToDateTime(inputHoliday.Finish);

            employeeRepository.Update(employee);

            await employeeRepository.Commit();
            return true;
        }

        public async Task RemoveHoliday(int employeeId, HolidayViewModel inputHoliday)
        {
            await Task.Yield();
            throw new NotImplementedException();
        }

        public IEnumerable<HolidayViewModel> ViewHolidays(int employeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HolidayViewModel> ViewPastHolidays(int employeeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HolidayViewModel> ViewUpcomingHolidays(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
