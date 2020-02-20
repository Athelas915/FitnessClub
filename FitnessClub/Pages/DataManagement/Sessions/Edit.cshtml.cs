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

namespace FitnessClub.Pages.DataManagement.Sessions
{
    public class EditModel : PageModel
    {
        private readonly ISessionRepository sessionRepository;

        public EditModel(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        [BindProperty]
        public Session Session { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session = await sessionRepository.GetByID(id.Value);

            if (Session == null)
            {
                return NotFound();
            }
           ViewData["CoachID"] = new SelectList(sessionRepository.GetCoaches(), "PersonID", "Discriminator");
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

            sessionRepository.Update(Session);

            try
            {
                await sessionRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(Session.SessionID))
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

        private bool SessionExists(int id)
        {
            return sessionRepository.Any(id);

        }
    }
}
