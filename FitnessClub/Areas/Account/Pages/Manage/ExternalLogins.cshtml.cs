using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Areas.Account.Pages.Manage;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitnessClub.Areas.Account.Pages
{
    public class ExternalLoginsModel : PageModel
    {
        private readonly IPasswordService passwordService;
        private readonly ISignInService signInService;
        private readonly int userId;
        public ExternalLoginsModel(IPasswordService passwordService, ISignInService signInService, UserResolverService userResolver)
        {
            this.passwordService = passwordService;
            this.signInService = signInService;
            userId = userResolver.GetUserId();
        }

        public LayoutData LayoutData { get; set; }

        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationScheme> OtherLogins { get; set; }

        public bool ShowRemoveButton { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string layout, string active)
        {
            LayoutData = new LayoutData(layout, active);
            (var currentLogins, var otherLogins) = await signInService.GetLogins(userId);
            var hasPassword = await passwordService.HasPassword(userId);
            if (hasPassword == null || currentLogins == null || otherLogins == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            CurrentLogins = currentLogins;
            OtherLogins = otherLogins;
            ShowRemoveButton = hasPassword.Value || CurrentLogins.Count > 1;
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveLoginAsync(string loginProvider, string providerKey, string layout, string active)
        {
            var result = await signInService.RemoveLogin(userId, loginProvider, providerKey);
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
            return RedirectToPage(new { layout = layout, active = active });
        }

        public async Task<IActionResult> OnPostLinkLoginAsync(string provider, string layout, string active)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Page("./ExternalLogins", pageHandler: "LinkLoginCallback", values: new { layout = layout, active = active });
            var properties = signInService.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId: userId);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetLinkLoginCallbackAsync(string layout, string active)
        {
            var result = await signInService.AddLogin(userId);
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
            return RedirectToPage(new { layout = layout, active = active });
        }
    }
}
