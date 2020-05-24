using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using FitnessClub.Data.BLL.Interfaces;

namespace FitnessClub.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailChangeModel : PageModel
    {
        private readonly IAccountManagementService accountManagementService;

        public ConfirmEmailChangeModel(IAccountManagementService accountManagementService)
        {
            this.accountManagementService = accountManagementService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
        {
            if (userId == null || email == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var result = await accountManagementService.ChangeEmail(userId, email, code);
            if (result == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            if (!result.Succeeded)
            {
                StatusMessage = "Error changing email.";
                return Page();
            }
            StatusMessage = "Thank you for confirming your email change.";
            return Page();
        }
    }
}
