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
    public class IndexModel : PageModel
    {
        private readonly ICoachRatingRepository coachRatingRepository;

        public IndexModel(ICoachRatingRepository coachRatingRepository)
        {
            this.coachRatingRepository = coachRatingRepository;
        }

        public IList<CoachRating> CoachRating { get;set; }

        public async Task OnGetAsync()
        {
            CoachRating = await coachRatingRepository.Get();
        }
    }
}
