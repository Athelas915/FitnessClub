using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Areas.CustomerPanel.Pages
{
    [Authorize(Roles = "Customer")]
    public class CoachRatingModel : PageModel
    {
        private readonly ICustomerService customerService;
        private readonly ISessionManagementService sessionService;
        private readonly int userId;
        public CoachRatingModel(ICustomerService customerService, ISessionManagementService sessionService, UserResolverService userResolver)
        {
            this.customerService = customerService;
            this.sessionService = sessionService;
            userId = userResolver.GetUserId();
        }
        public IDictionary<SessionViewModel, int?> Sessions { get; set; }
        [BindProperty]
        [Range(0,5)]
        public int Rating { get; set; }
        [BindProperty]
        public int ChosenSessionID { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (userId == -1)
            {
                return RedirectToPage("./Index");
            }
            var s = (await customerService.GetWithSessions(userId)).Sessions.Where(a => a.Value == null).ToDictionary(x => x.Key, x=> x.Value);
            Sessions = s;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }
            if (userId == -1)
            {
                return RedirectToPage("./Index");
            }

            var c = await customerService.GetWithSessions(userId);
            var s = await sessionService.GetById(ChosenSessionID);
            await customerService.RateCoach(s, c, Rating);

            return RedirectToPage();
        }
    }
}
