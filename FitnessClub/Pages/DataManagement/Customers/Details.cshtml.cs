using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.Customers
{
    [Authorize(Policy = "SignedIn")]
    public class DetailsModel : PageModel
    {
        private readonly IPersonRepository<Customer> customerRepository;

        public DetailsModel(IPersonRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await customerRepository.GetByID(id.Value);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
