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

namespace FitnessClub.Areas.Account.Pages
{
    [AllowAnonymous]
    public class ConfirmEmailChangeModel : PageModel
    {
        private readonly IAccountService accountService;

        public ConfirmEmailChangeModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
        {
            var userIdInt = int.Parse(userId);
            if (userIdInt == -1 || email == null || code == null)
            {
                return RedirectToPage("/Index", new {  });
            }

            var result = await accountService.ChangeEmail(userIdInt, email, code);
            if (result == null)
            {
                return NotFound($"Unable to load user with ID '{userIdInt}'.");
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
