using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace FitnessClub.Pages.DataManagement.Customers
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly IPersonRepository<Customer> customerRepository;

        public CreateModel(IPersonRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            var users = await customerRepository.Get<AspNetUser>();
            var people = await customerRepository.Get<Person>();
            var usersToRemove = people.Select(c => c.AspNetUserId).ToList();

            foreach (int userToRemove in usersToRemove)
            {
                users.Remove(users.Single(u => u.Id == userToRemove));
            }

            ViewData["Users"] = new SelectList(users, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            customerRepository.Insert(Customer);
            await customerRepository.Submit();

            return RedirectToPage("./Index");
        }
    }
}
