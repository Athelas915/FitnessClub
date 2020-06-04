using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public class CustomerViewModel : PersonViewModel<Customer>
    {
        //The parameterless constructor is required for Model Binding on razor pages.
        public CustomerViewModel()
        {

        }
        public CustomerViewModel(Customer customer) : base(customer) { }
    }
}
