using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitnessClub.Areas.Identity.Pages.Account.Manage
{
    public class ExternalLoginsModel : PageModel
    {
        private readonly IAccountManagementService accountManagementService;

        public ExternalLoginsModel(IAccountManagementService accountManagementService)
        {
            this.accountManagementService = accountManagementService;
        }

        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationScheme> OtherLogins { get; set; }

        public bool ShowRemoveButton { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = accountManagementService.GetUserId(User);
            (var currentLogins, var otherLogins) = await accountManagementService.GetLogins(userId);
            var hasPassword = await accountManagementService.HasPassword(userId);
            if (hasPassword == null || currentLogins == null || otherLogins == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            CurrentLogins = currentLogins;
            OtherLogins = otherLogins;
            ShowRemoveButton = hasPassword.Value || CurrentLogins.Count > 1;
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveLoginAsync(string loginProvider, string providerKey)
        {
            var userId = accountManagementService.GetUserId(User);

            var result = await accountManagementService.RemoveLogin(userId, loginProvider, providerKey);
            if (result == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            else if (!result.Succeeded)
            {
                StatusMessage = "The external login was not removed.";
                return RedirectToPage();
            }
            StatusMessage = "The external login was removed.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostLinkLoginAsync(string provider)
        {
            var userId = accountManagementService.GetUserId(User);
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Page("./ExternalLogins", pageHandler: "LinkLoginCallback");
            var properties = accountManagementService.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetLinkLoginCallbackAsync()
        {
            var userId = accountManagementService.GetUserId(User);

            var result = await accountManagementService.AddLogin(userId);
            if (result == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            else if (!result.Succeeded)
            {
                StatusMessage = "The external login was not added. External logins can only be associated with one account.";
                return RedirectToPage();
            }
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            StatusMessage = "The external login was added.";
            return RedirectToPage();
        }
    }
}
