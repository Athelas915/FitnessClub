using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitnessClub.Areas.Identity.Pages.CustomerPanel
{
    public class CoachRatingModel : PageModel
    {
        private readonly int userId;
        private readonly ICoachRatingService coachRatingService;
        public CoachRatingModel(ICoachRatingService coachRatingService, UserResolverService userResolver)
        {
            this.coachRatingService = coachRatingService;
            userId = userResolver.GetUserId();
        }
        public IList<Session> Sessions { get; set;  }
        public void OnGet()
        {
            Sessions = coachRatingService.GetUnratedSessions(userId);
        }
    }
}
