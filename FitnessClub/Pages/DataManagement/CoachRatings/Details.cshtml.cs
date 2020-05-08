using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.CoachRatings
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly ICoachRatingRepository coachRatingRepository;

        public DetailsModel(ICoachRatingRepository coachRatingRepository)
        {
            this.coachRatingRepository = coachRatingRepository;
        }

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
            return Page();
        }
    }
}
