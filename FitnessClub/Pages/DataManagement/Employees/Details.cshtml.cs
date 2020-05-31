using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL;
using FitnessClub.Data.Models;

namespace FitnessClub.Pages.DataManagement.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly FitnessClub.Data.DAL.FCContext _context;

        public DetailsModel(FitnessClub.Data.DAL.FCContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await _context.Employees
                .Include(e => e.AspNetUser).FirstOrDefaultAsync(m => m.PersonID == id);

            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
