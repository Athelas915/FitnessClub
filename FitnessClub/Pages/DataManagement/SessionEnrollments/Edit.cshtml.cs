using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL;
using FitnessClub.Data.Models;

namespace FitnessClub.Pages.DataManagement.SessionEnrollments
{
    public class EditModel : PageModel
    {
        private readonly FitnessClub.Data.DAL.FCContext _context;

        public EditModel(FitnessClub.Data.DAL.FCContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SessionEnrollment SessionEnrollment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SessionEnrollment = await _context.SessionEnrollments
                .Include(s => s.Customer)
                .Include(s => s.Session).FirstOrDefaultAsync(m => m.CustomerID == id);

            if (SessionEnrollment == null)
            {
                return NotFound();
            }
           ViewData["CustomerID"] = new SelectList(_context.Customers, "PersonID", "LastName");
           ViewData["SessionID"] = new SelectList(_context.Sessions, "SessionID", "SessionID");
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

            _context.Attach(SessionEnrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionEnrollmentExists(SessionEnrollment.CustomerID))
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
            return _context.SessionEnrollments.Any(e => e.CustomerID == id);
        }
    }
}
