using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL;
using FitnessClub.Data.Models;

namespace FitnessClub
{
    public class DetailsModel : PageModel
    {
        private readonly FitnessClub.Data.DAL.FCContext _context;

        public DetailsModel(FitnessClub.Data.DAL.FCContext context)
        {
            _context = context;
        }

        public Person Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await _context.People.FirstOrDefaultAsync(m => m.PersonID == id);

            if (Person == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
