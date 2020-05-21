using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL;
using FitnessClub.Data.Models;

namespace FitnessClub.Pages.DataManagement.SessionEnrollments
{
    public class DeleteModel : PageModel
    {
        private readonly FitnessClub.Data.DAL.FCContext _context;

        public DeleteModel(FitnessClub.Data.DAL.FCContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SessionEnrollment = await _context.SessionEnrollments.FindAsync(id);

            if (SessionEnrollment != null)
            {
                _context.SessionEnrollments.Remove(SessionEnrollment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
