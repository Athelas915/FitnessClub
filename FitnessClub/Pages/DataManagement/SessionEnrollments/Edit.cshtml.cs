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

namespace FitnessClub.Pages.DataManagement.SessionEnrollments
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public SessionEnrollment SessionEnrollment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SessionEnrollment = await unitOfWork.SessionEnrollmentRepository.GetByID(id.Value);

            if (SessionEnrollment == null)
            {
                return NotFound();
            }
            ViewData["PersonID"] = new SelectList(await unitOfWork.CustomerRepository.Get(), "PersonID", "LastName");
            ViewData["SessionID"] = new SelectList(await unitOfWork.SessionRepository.Get(), "SessionID", "SessionID");
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

            unitOfWork.SessionEnrollmentRepository.Update(SessionEnrollment);

            try
            {
                await unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionEnrollmentExists(SessionEnrollment.SessionEnrollmentID))
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

        private bool SessionEnrollmentExists(int id)
        {
            return unitOfWork.SessionEnrollmentRepository.Any(id);
        }
    }
}
