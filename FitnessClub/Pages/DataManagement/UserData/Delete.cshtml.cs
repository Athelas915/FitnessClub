using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL;
using FitnessClub.Data.Models.Identity;

namespace FitnessClub.Pages.DataManagement.UserData
{
    public class DeleteModel : PageModel
    {
        private readonly FitnessClub.Data.DAL.FCContext _context;

        public DeleteModel(FitnessClub.Data.DAL.FCContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AspNetUser AspNetUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AspNetUser = await _context.AspNetUsers.FirstOrDefaultAsync(m => m.Id == id);

            if (AspNetUser == null)
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

            AspNetUser = await _context.AspNetUsers.FindAsync(id);

            if (AspNetUser != null)
            {
                _context.AspNetUsers.Remove(AspNetUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
