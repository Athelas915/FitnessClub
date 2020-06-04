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
    public class ConfirmEmailModel : PageModel
    {
        private readonly IRegistrationService registrationService;

        public ConfirmEmailModel(IRegistrationService registrationService)
        {
             this.registrationService = registrationService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            var userIdInt = int.Parse(userId);
            if (userIdInt == -1 || code == null)
            {
                return RedirectToPage("/Index");
            }

            var result = await registrationService.ConfirmEmail(userIdInt, code);
            if (result == null)
            {
                return NotFound($"Unable to load user with ID '{userIdInt}'.");
            }
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return Page();
        }
    }
}
