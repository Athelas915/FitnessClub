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
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.SessionEnrollments
{
    [Authorize(Policy = "SignedIn")]
    public class EditModel : PageModel
    {
        private readonly ISessionEnrollmentRepository sessionEnrollmentRepository;

        public EditModel(ISessionEnrollmentRepository sessionEnrollmentRepository)
        {
            this.sessionEnrollmentRepository = sessionEnrollmentRepository;
        }

        [BindProperty]
        public SessionEnrollment SessionEnrollment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? PersonID, int? SessionID)
        {
            if ((PersonID == null) || (SessionID == null))
            {
                return NotFound();
            }

            SessionEnrollment = await sessionEnrollmentRepository.GetByID(PersonID.Value, SessionID.Value);

            if (SessionEnrollment == null)
            {
                return NotFound();
            }
            ViewData["PersonID"] = new SelectList(await sessionEnrollmentRepository.Get<Customer>(), "PersonID", "LastName");
            ViewData["SessionID"] = new SelectList(await sessionEnrollmentRepository.Get<Session>(), "SessionID", "SessionID");
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

            sessionEnrollmentRepository.Update(SessionEnrollment);

            try
            {
                await sessionEnrollmentRepository.Submit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionEnrollmentExists(SessionEnrollment.PersonID.Value, SessionEnrollment.SessionID.Value))
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

        private bool SessionEnrollmentExists(int PersonID, int SessionID)
        {
            return sessionEnrollmentRepository.Any(PersonID, SessionID);
        }
    }
}
