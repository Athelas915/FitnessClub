using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.Models.ViewModels;

namespace FitnessClub.Areas.CustomerPanel.Pages
{
    [Authorize(Roles = "Customer")]
    public class MembershipModel : PageModel
    {
        private readonly ICustomerService customerService;
        public MembershipModel(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IEnumerable<MembershipViewModel> Memberships { get; set; }
        public IActionResult OnGet()
        {
            var customerId = customerService.GetCurrentPersonId();
            if (customerId == -1)
            {
                Serilog.Log.Information($"Couldn't find id of the logged in customer.");
                return RedirectToPage("./Index");
            }
            Memberships = customerService.ViewMemberships(customerId);

            return Page();
        }
    }
}
