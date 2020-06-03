using FitnessClub.Data.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<HolidayViewModel> ViewHolidays(int userId);
        IEnumerable<HolidayViewModel> ViewUpcomingHolidays(int userId);
        IEnumerable<HolidayViewModel> ViewPastHolidays(int userId);
        IEnumerable<HolidayViewModel> ViewAllCurrentHolidays();
        Task AddHoliday(int userId, HolidayViewModel inputHoliday);
        Task RemoveHoliday(int userId, HolidayViewModel inputHoliday);
        Task<bool> EditHoliday(int userId, HolidayViewModel inputHoliday);
    }
}
