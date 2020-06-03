﻿using FitnessClub.Data.BLL.Interfaces;
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
        private readonly ISessionRepository sessionRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly ILogger<EmployeeService> logger;
        private readonly int userId;
        public EmployeeService(ISessionRepository sessionRepository, IEmployeeRepository employeeRepository, UserResolverService userResolverService, ILogger<EmployeeService> logger)
        {
            this.sessionRepository = sessionRepository;
            this.employeeRepository = employeeRepository;
            this.logger = logger;
            userId = userResolverService.GetUserId();
        }

        public async Task AddHoliday(int userId, HolidayViewModel inputHoliday)
        {
            var employeeId = employeeRepository.GetPersonIdByUserId(userId);
            var employee = employeeRepository.FindWithHolidays(employeeId);
            if (employee == null)
            {
                logger.LogInformation($"Couldn't find the user with id {employeeId}");
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

        public async Task<bool> EditHoliday(int userId, HolidayViewModel inputHoliday)
        {
            var employeeId = employeeRepository.GetPersonIdByUserId(userId);
            var employee = employeeRepository.FindWithHolidays(employeeId);
            var holiday = employee.Holidays.Where(a => a.HolidayID == inputHoliday.HolidayID).FirstOrDefault();
            if (employee == null || holiday == null)
            {
                logger.LogInformation($"Couldn't find the holiday with given session id {inputHoliday.HolidayID} and person id {employeeId}");
                return false;
            }
            else if (holiday.Finish < DateTime.Now)
            {
                logger.LogInformation($"The holidays already ended. Users can edit only the holidays that are upcoming.");
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

        public async Task RemoveHoliday(int userId, HolidayViewModel inputHoliday)
        {
            var employeeId = employeeRepository.GetPersonIdByUserId(userId);
            var employee = employeeRepository.FindWithHolidays(employeeId);
            var holiday = employee.Holidays.Where(a => a.HolidayID == inputHoliday.HolidayID).FirstOrDefault();
            if (employee == null || holiday == null)
            {
                logger.LogInformation($"Couldn't find the holiday with given session id {inputHoliday.HolidayID} and person id {employeeId}");
                return;
            }
            var result = employee.Holidays.Remove(holiday);
            if (!result)
            {
                employeeRepository.Dispose();
            }
            else
            {
                await employeeRepository.Commit();
            }
        }

        public IEnumerable<HolidayViewModel> ViewHolidays(int userId)
        {
            var employeeId = employeeRepository.GetPersonIdByUserId(userId);
            var employee = employeeRepository.FindWithHolidays(employeeId);
            if (employee == null)
            {
                logger.LogInformation($"Couldn't find the user with id {employeeId}");
                return null;
            }
            var holidays = employee.Holidays.AsEnumerable();
            foreach (var h in holidays)
            {
                h.Employee = employee;
            }
            holidays = holidays.OrderBy(a => a.Start);
            return holidays.Select(a => new HolidayViewModel(a));
        }

        public IEnumerable<HolidayViewModel> ViewPastHolidays(int userId)
        {
            var employeeId = employeeRepository.GetPersonIdByUserId(userId);
            var employee = employeeRepository.FindWithHolidays(employeeId);
            if (employee == null)
            {
                logger.LogInformation($"Couldn't find the user with id {employeeId}");
                return null;
            }
            var holidays = employee.Holidays.AsEnumerable().Where(a => a.Finish < DateTime.Now);
            foreach (var h in holidays)
            {
                h.Employee = employee;
            }
            holidays = holidays.OrderBy(a => a.Start);
            return holidays.Select(a => new HolidayViewModel(a));
        }

        public IEnumerable<HolidayViewModel> ViewUpcomingHolidays(int userId)
        {
            var employeeId = employeeRepository.GetPersonIdByUserId(userId);
            var employee = employeeRepository.FindWithHolidays(employeeId);
            if (employee == null)
            {
                logger.LogInformation($"Couldn't find the user with id {employeeId}");
                return null;
            }
            var holidays = employee.Holidays.AsEnumerable().Where(a => a.Start > DateTime.Now);
            foreach (var h in holidays)
            {
                h.Employee = employee;
            }
            holidays = holidays.OrderBy(a => a.Start);
            return holidays.Select(a => new HolidayViewModel(a));
        }
        public IEnumerable<HolidayViewModel> ViewAllCurrentHolidays()
        {
            var employees = employeeRepository.GetAllWithHolidays();
            foreach (var e in employees)
            {
                foreach (var h in e.Holidays)
                {
                    if (h.Start < DateTime.Now && h.Finish > DateTime.Now)
                    {
                        h.Employee = e;
                        yield return new HolidayViewModel(h);
                    }
                }
            }
        }
    }
}
