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

namespace FitnessClub.Pages.DataManagement.Employees
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly IPersonRepository<Employee> employeeRepository;

        public CreateModel(IPersonRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            var users = await employeeRepository.Get<AspNetUser>();
            var people = await employeeRepository.Get<Person>();
            var usersToRemove = people.Select(c => c.AspNetUserId).ToList();

            foreach (int userToRemove in usersToRemove)
            {
                users.Remove(users.Single(u => u.Id == userToRemove));
            }

            ViewData["Users"] = new SelectList(users, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            employeeRepository.Insert(Employee);
            await employeeRepository.Submit();

            return RedirectToPage("./Index");
        }
    }
}
