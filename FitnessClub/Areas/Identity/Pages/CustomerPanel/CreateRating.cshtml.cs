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
        public Session ChosenSession { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return  RedirectToPage("./RateCoaches");
            }
            else
            { 
                ChosenSession = await coachRatingRepository.GetByID<Session>(id.Value);
                ChosenSession.Coach = await coachRatingRepository.GetByID<Coach>(ChosenSession.PersonID);

            }
            return Page();
        }
        [BindProperty]
        public CoachRating CoachRating { get; set; }
        
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("./RateCoaches");
            }
            CoachRating.Session = await coachRatingRepository.GetByID<Session>(id.Value);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Serilog.Log.Information("SessionID: " + id);

            coachRatingRepository.Insert(CoachRating);
            await coachRatingRepository.Submit();

            return RedirectToPage("./RateCoaches");
        }
    }
}
