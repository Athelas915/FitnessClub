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

namespace FitnessClub.Pages.DataManagement.CoachRatings
{
    [Authorize(Policy = "SignedIn")]
    public class EditModel : PageModel
    {
        private readonly ICoachRatingRepository coachRatingRepository;

        public EditModel(ICoachRatingRepository coachRatingRepository)
        {
            this.coachRatingRepository = coachRatingRepository;
        }

        [BindProperty]
        public CoachRating CoachRating { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CoachRating = await coachRatingRepository.GetByID(id.Value);

            if (CoachRating == null)
            {
                return NotFound();
            }
            ViewData["PersonID"] = new SelectList(await coachRatingRepository.Get<Coach>(), "PersonID", "LastName");
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

            coachRatingRepository.Update(CoachRating);

            try
            {
                await coachRatingRepository.Submit();
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
            return coachRatingRepository.Any(id);
        }
    }
}
