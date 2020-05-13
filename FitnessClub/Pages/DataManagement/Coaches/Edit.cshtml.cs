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

namespace FitnessClub.Pages.DataManagement.Coaches
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly IPersonRepository<Coach> coachRepository;

        public EditModel(IPersonRepository<Coach> coachRepository)
        {
            this.coachRepository = coachRepository;
        }

        [BindProperty]
        public Coach Coach { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {if (id == null)
            {
                return NotFound();
            }

            Coach = await coachRepository.GetByID(id.Value);

            if (Coach == null)
            {
                return NotFound();
            }

            var users = await coachRepository.Get<AspNetUser>();
            var people = await coachRepository.Get<Person>();
            people.Remove(Coach);
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

            coachRepository.Update(Coach);

            try
            {
                await coachRepository.Submit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachExists(Coach.PersonID))
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

        private bool CoachExists(int id)
        {
            return coachRepository.Any(id);
        }
    }
}
