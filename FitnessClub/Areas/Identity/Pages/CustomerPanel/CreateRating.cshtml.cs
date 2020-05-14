using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.ViewModels;
using FitnessClub.Data.DAL.Interfaces;

namespace FitnessClub.Areas.Identity.Pages.CustomerPanel
{
    public class CreateRatingModel : PageModel
    {
        private readonly ICoachRatingRepository coachRatingRepository;
        public CreateRatingModel(ICoachRatingRepository coachRatingRepository)
        {
            this.coachRatingRepository = coachRatingRepository;
        }
        public SessionViewModel ChosenSession { get; set; }
        [BindProperty]
        public CoachRating CoachRating { get; set; }
        public async Task OnGetAsync(int? id)
        {
            var session = await coachRatingRepository.GetByID<Session>(id);
            session.Coach = await coachRatingRepository.GetByID<Coach>(session.PersonID);
            ChosenSession = new SessionViewModel(session);
        }
        public void OnPost()
        {

        }
    }
}
