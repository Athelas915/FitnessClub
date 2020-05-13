using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace FitnessClub.Pages.DataManagement.People
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly IPersonRepository<Person> personRepository;

        public EditModel(IPersonRepository<Person> personRepository)
        {
            this.personRepository = personRepository;
        }

        [BindProperty]
        public Person Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (Person == null)
            {
                return NotFound();
            }
            var users = await personRepository.Get<AspNetUser>();
            var people = await personRepository.Get();
            people.Remove(Person);
            var usersToRemove = people.Select(c => c.AspNetUserId).ToList();

            foreach (int userToRemove in usersToRemove)
            {
                users.Remove(users.Single(u => u.Id == userToRemove));
            }

            ViewData["Users"] = new SelectList(users, "Id", "Email");

            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            personRepository.Update(Person);

            try
            {
                await personRepository.Submit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(Person.PersonID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PersonExists(int id)
        {
            return personRepository.Any(id);
        }
    }
}
