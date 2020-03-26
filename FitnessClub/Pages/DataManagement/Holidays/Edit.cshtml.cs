using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.Holidays
{
    [Authorize(Policy = "SignedIn")]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Holiday Holiday { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Holiday = await unitOfWork.HolidayRepository.GetByID(id.Value);

            if (Holiday == null)
            {
                return NotFound();
            }
            ViewData["PersonID"] = new SelectList(await unitOfWork.CoachRepository.Get(), "PersonID", "LastName");
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

            unitOfWork.HolidayRepository.Update(Holiday);

            try
            {
                await unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HolidayExists(Holiday.HolidayID))
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

        private bool HolidayExists(int id)
        {
            return unitOfWork.HolidayRepository.Any(id);
        }
    }
}
