using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;

namespace FitnessClub.Pages.DataManagement.Sessions
{
    public class CreateModel : PageModel
    {
        private readonly ISessionRepository sessionRepository;
        public CreateModel(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public IActionResult OnGet()
        {
        ViewData["CoachID"] = new SelectList(sessionRepository.GetCoaches(), "PersonID", "Discriminator");
            return Page();
        }

        [BindProperty]
        public Session Session { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            sessionRepository.Insert(Session);
            Session.CreatedOn = DateTime.Now;
            Session.CreatedBy = 0; // add currently logged user here
            await sessionRepository.Save();

            return RedirectToPage("./Index");
        }
    }
}
