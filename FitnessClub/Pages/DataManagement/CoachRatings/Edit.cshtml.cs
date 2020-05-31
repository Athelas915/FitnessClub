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

namespace FitnessClub.Pages.DataManagement.CoachRatings
{
    public class EditModel : PageModel
    {
        private readonly FitnessClub.Data.DAL.FCContext _context;

        public EditModel(FitnessClub.Data.DAL.FCContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CoachRating CoachRating { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CoachRating = await _context.CoachRatings
                .Include(c => c.Coach)
                .Include(c => c.Customer).FirstOrDefaultAsync(m => m.CoachRatingID == id);

            if (CoachRating == null)
            {
                return NotFound();
            }
           ViewData["CoachID"] = new SelectList(_context.Coaches, "PersonID", "LastName");
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

            _context.Attach(CoachRating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachRatingExists(CoachRating.CoachRatingID))
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

        private bool CoachRatingExists(int id)
        {
            return _context.CoachRatings.Any(e => e.CoachRatingID == id);
        }
    }
}
