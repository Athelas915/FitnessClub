using FitnessClub.Data.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitnessClub.Data.BLL.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeViewModel> GetEmployee(int userId);
        Task<EmployeeViewModel> GetWithHolidays(int userId);
        Task CreateHoliday(EmployeeViewModel employee, HolidayViewModel inputHoliday);
        Task RemoveHoliday(EmployeeViewModel employee, HolidayViewModel inputHoliday);
        Task EditHoliday(EmployeeViewModel employee, HolidayViewModel inputHoliday);
    }
}
