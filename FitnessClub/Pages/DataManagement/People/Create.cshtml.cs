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

namespace FitnessClub.Pages.DataManagement.People
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly IPersonRepository<Person> personRepository;
        public CreateModel(IPersonRepository<Person> personRepository)
        {
            this.personRepository = personRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            var users = await personRepository.Get<AspNetUser>();
            var people = await personRepository.Get();
            var usersToRemove = people.Select(c => c.AspNetUserId).ToList();

            foreach (int userToRemove in usersToRemove)
            {
                users.Remove(users.Single(u => u.Id == userToRemove));
            }

            ViewData["Users"] = new SelectList(users, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Person Person { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            personRepository.Insert(Person);
            await personRepository.Submit();

            return RedirectToPage("./Index");
        }
    }
}
