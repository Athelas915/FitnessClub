using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public class EmployeeViewModel : PersonViewModel<Employee>
    {
        //The parameterless constructor is required for Model Binding on razor pages.
        public EmployeeViewModel()
        {

        }
        public EmployeeViewModel(Employee employee) : base(employee) { }
    }
}
