using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.DAL;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessClub.Areas.Identity.Pages.CustomerPanel
{
    public class RateCoachesModel : PageModel
    {
        private readonly ICoachRatingRepository coachRatingRepository;
        private readonly int userId;
        public RateCoachesModel(ICoachRatingRepository coachRatingRepository, UserResolverService userResolver)
        {
            this.coachRatingRepository = coachRatingRepository;
            userId = userResolver.GetUserId();
        }
        public Customer Customer { get; set; }
        public IList<SessionEnrollment> SessionEnrollments { get; set; }
        public SessionViewModel ChosenSession { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Customer = await coachRatingRepository.GetUserByIdentityId<Customer>(userId);
            
            SessionEnrollments = await coachRatingRepository.GetUsersSessionEnrollments(Customer.PersonID);
            await coachRatingRepository.SetSessionsAndCoaches(SessionEnrollments); //without doing this, cshtml file was throwing "ObjectReferenceNull Exception"
            SessionEnrollments = SessionEnrollments.OrderBy(s => s.Session.Start).ToList();
            
            return Page();
        }
    }
}
