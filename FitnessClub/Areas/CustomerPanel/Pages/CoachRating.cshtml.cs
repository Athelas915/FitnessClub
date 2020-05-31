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
        private readonly ILogger<CoachRatingModel> logger;
        public CoachRatingModel(ICustomerService customerService, ILogger<CoachRatingModel> logger)
        {
            this.customerService = customerService;
            this.logger = logger;
        }
        public IEnumerable<SessionViewModel> Sessions { get; set; }
        [BindProperty]
        [Range(0,5)]
        public int Rating { get; set; }
        [BindProperty]
        public int ChosenSessionID { get; set; }
        public IActionResult OnGet()
        {
            var customerId = customerService.GetCurrentPersonId();
            if (customerId == -1)
            {
                logger.LogInformation($"Couldn't find id of the logged in customer.");
                return RedirectToPage("./Index");
            }
            Sessions = customerService.ViewUnratedSessions(customerId);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }
            var customerId = customerService.GetCurrentPersonId();
            if (customerId == -1)
            {
                logger.LogInformation($"Couldn't find id of the logged in customer.");
                return RedirectToPage("./Index");
            }

            await customerService.RateCoach(customerId, ChosenSessionID, Rating);

            return RedirectToPage();
        }
    }
}
