using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL;
using FitnessClub.Data.Models;

namespace FitnessClub.Pages.DataManagement.CoachRatings
{
    public class DeleteModel : PageModel
    {
        private readonly FitnessClub.Data.DAL.FCContext _context;

        public DeleteModel(FitnessClub.Data.DAL.FCContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CoachRating = await _context.CoachRatings.FindAsync(id);

            if (CoachRating != null)
            {
                _context.CoachRatings.Remove(CoachRating);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
