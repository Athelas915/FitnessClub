using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;

namespace FitnessClub.Areas.Identity.Pages.CustomerPanel
{
    public class RateCoachesModel : PageModel
    {
        public readonly ICoachRatingRepository coachRatingRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public RateCoachesModel(ICoachRatingRepository coachRatingRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.coachRatingRepository = coachRatingRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        public Customer Customer { get; set; }
        public int UserID { get; set; }
        public IList<Coach> CoachesList { get; set; }
        public IList<Session> SessionsList { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Customer = await coachRatingRepository.GetUserByIdentityId<Customer>(int.Parse(userId));

            SessionsList = await coachRatingRepository.GetUsersSessions(Customer.PersonID);

            await coachRatingRepository.SetCoaches(SessionsList); //without doing this, cshtml file was throwing "ObjectReferenceNull Exception"

            return Page();
        }
    }
}
