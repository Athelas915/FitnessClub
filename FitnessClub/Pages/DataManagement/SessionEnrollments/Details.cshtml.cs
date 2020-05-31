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
    public class DetailsModel : PageModel
    {
        private readonly FitnessClub.Data.DAL.FCContext _context;

        public DetailsModel(FitnessClub.Data.DAL.FCContext context)
        {
            _context = context;
        }

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
    }
}
