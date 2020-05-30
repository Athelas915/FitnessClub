using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitnessClub.Data.DAL;
using FitnessClub.Data.Models;

namespace FitnessClub.Pages.DataManagement.Addresses
{
    public class CreateModel : PageModel
    {
        private readonly FitnessClub.Data.DAL.FCContext _context;

        public CreateModel(FitnessClub.Data.DAL.FCContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PersonID"] = new SelectList(_context.People, "PersonID", "LastName");
            return Page();
        }

        [BindProperty]
        public Address Address { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Addresses.Add(Address);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
