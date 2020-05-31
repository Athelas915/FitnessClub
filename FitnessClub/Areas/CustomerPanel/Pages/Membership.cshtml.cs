using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.Models.ViewModels;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Areas.CustomerPanel.Pages
{
    [Authorize(Roles = "Customer")]
    public class MembershipModel : PageModel
    {
        private readonly ICustomerService customerService;
        private readonly ILogger<MembershipModel> logger;
        public MembershipModel(ICustomerService customerService, ILogger<MembershipModel> logger)
        {
            this.customerService = customerService;
            this.logger = logger;
        }

        public IEnumerable<MembershipViewModel> Memberships { get; set; }
        public IActionResult OnGet()
        {
            var customerId = customerService.GetCurrentPersonId();
            if (customerId == -1)
            {
                logger.LogInformation($"Couldn't find id of the logged in customer.");
                return RedirectToPage("./Index");
            }
            Memberships = customerService.ViewMemberships(customerId);

            return Page();
        }
    }
}
