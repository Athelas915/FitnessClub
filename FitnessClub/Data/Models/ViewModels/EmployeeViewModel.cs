using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public class EmployeeViewModel : PersonViewModel<Employee>
    {
        public EmployeeViewModel(Employee employee) : base(employee) { }
    }
}
