using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public class EmployeeViewModel<EEntity> : PersonViewModel<EEntity> where EEntity : Employee, new()
    {
        //The parameterless constructor is required for Model Binding on razor pages.
        public EmployeeViewModel() {}
        public EmployeeViewModel(EEntity employee) : base(employee) { }
        public ICollection<HolidayViewModel> Holidays
        {
            get => model.Holidays?.Select(a => new HolidayViewModel(a)).ToList();
        }
    }
    public class EmployeeViewModel : EmployeeViewModel<Employee>
    {
        public EmployeeViewModel() { }
        public EmployeeViewModel(Employee employee) : base(employee) { }
    }
}