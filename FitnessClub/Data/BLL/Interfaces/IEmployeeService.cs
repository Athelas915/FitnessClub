using FitnessClub.Data.Models;
using FitnessClub.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<HolidayViewModel> ViewHolidays(int employeeId);
        IEnumerable<HolidayViewModel> ViewUpcomingHolidays(int employeeId);
        IEnumerable<HolidayViewModel> ViewPastHolidays(int employeeId);
        Task AddHoliday(int employeeId, HolidayViewModel inputHoliday);
        Task RemoveHoliday(int employeeId, HolidayViewModel inputHoliday);
        Task<bool> EditHoliday(int employeeId, HolidayViewModel inputHoliday);
    }
}
