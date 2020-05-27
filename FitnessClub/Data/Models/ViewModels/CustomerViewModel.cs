using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Data.Models.ViewModels
{
    public class CustomerViewModel : PersonViewModel<Customer>
    {
        public CustomerViewModel(Customer customer) : base(customer) { }
    }
}
