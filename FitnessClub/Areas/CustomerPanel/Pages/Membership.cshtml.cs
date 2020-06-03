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
using FitnessClub.Data.DAL.Utility;

namespace FitnessClub.Areas.CustomerPanel.Pages
{
    [Authorize(Roles = "Customer")]
    public class MembershipModel : PageModel
    {
        private readonly ICustomerService customerService;
        private readonly int userId;
        public MembershipModel(ICustomerService customerService, UserResolverService userResolver)
        {
            this.customerService = customerService;
            userId = userResolver.GetUserId();
        }

        public IEnumerable<MembershipViewModel> Memberships { get; set; }
        public IActionResult OnGet()
        {
            if (userId == -1)
            {
                return RedirectToPage("./Index");
            }
            Memberships = customerService.ViewMemberships(userId);

            return Page();
        }
    }
}
