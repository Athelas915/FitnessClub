using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.SessionEnrollments
{
    [Authorize(Policy = "SignedIn")]
    public class CreateModel : PageModel
    {
        private readonly ISessionEnrollmentRepository sessionEnrollmentRepository;

        public CreateModel(ISessionEnrollmentRepository sessionEnrollmentRepository)
        {
            this.sessionEnrollmentRepository = sessionEnrollmentRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["PersonID"] = new SelectList(await sessionEnrollmentRepository.Get<Customer>(), "PersonID", "LastName");
            ViewData["SessionID"] = new SelectList(await sessionEnrollmentRepository.Get<Session>(), "SessionID", "SessionID");
            return Page();
        }

        [BindProperty]
        public SessionEnrollment SessionEnrollment { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (sessionEnrollmentRepository.Any(SessionEnrollment.PersonID.Value, SessionEnrollment.SessionID.Value) == true)
            {
                return RedirectToPage("./Error");
            }
            else
            {
                sessionEnrollmentRepository.Insert(SessionEnrollment);
                await sessionEnrollmentRepository.Submit();
            }

            return RedirectToPage("./Index");
        }
    }
}
