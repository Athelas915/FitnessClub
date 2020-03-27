using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.Holidays
{
    [Authorize(Policy = "SignedIn")]
    public class CreateModel : PageModel
    {
        private readonly IHolidayRepository holidayRepository;

        public CreateModel(IHolidayRepository holidayRepository)
        {
            this.holidayRepository = holidayRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["PersonID"] = new SelectList(await holidayRepository.Get<Employee>(), "PersonID", "LastName");
            return Page();
        }

        [BindProperty]
        public Holiday Holiday { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            holidayRepository.Insert(Holiday);
            await holidayRepository.Submit();

            return RedirectToPage("./Index");
        }
    }
}
