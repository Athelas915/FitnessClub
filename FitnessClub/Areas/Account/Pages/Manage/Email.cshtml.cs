using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Utility;

namespace FitnessClub.Areas.Account.Pages.Manage
{
    public partial class EmailModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly ITokenGenerator tokenGenerator;
        private readonly int userId;
        private readonly IEmailSender emailSender;

        public EmailModel(IAccountService accountService, ITokenGenerator tokenGenerator, IEmailSender emailSender, UserResolverService userResolver)
        {
            this.accountService = accountService;
            this.tokenGenerator = tokenGenerator;
            userId = userResolver.GetUserId();
            this.emailSender = emailSender;
        }
        public LayoutData LayoutData { get; private set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "New email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(int userId)
        {
            var email = await accountService.GetEmail(userId);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await accountService.IsEmailConfirmed(userId);
        }

        public async Task<IActionResult> OnGetAsync(string layout, string active)
        {
            //allows displaying this page for different layouts
            LayoutData = new LayoutData(layout, active);
            await LoadAsync(userId);
            if (Email == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync(string layout, string active)
        {
            if (userId == -1)
            {
                return NotFound($"Unable to load user.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(userId);
                return Page();
            }

            var email = await accountService.GetEmail(userId);
            if (Input.NewEmail != email)
            {
                var code = await tokenGenerator.GenerateChangeEmailToken(userId, Input.NewEmail);
                if (code == null)
                {
                    return NotFound($"Unable to load user with ID '{userId}'.");
                }
                var callbackUrl = Url.Page(
                    "/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { area = "Account", userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);
                await emailSender.SendEmailAsync(
                    Input.NewEmail,
                    "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                StatusMessage = "Confirmation link to change email sent. Please check your email.";
                return RedirectToPage(new { layout = layout, active = active });
            }
            
            StatusMessage = "Your email is unchanged`.";
            return RedirectToPage(new { layout = layout, active = active });
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync(string layout, string active)
        {
            if (!ModelState.IsValid)
            {
                await LoadAsync(userId);
                return Page();
            }

            var email = await accountService.GetEmail(userId);
            var code = await tokenGenerator.GenerateEmailConfirmationToken(userId);
            if (code == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Account", userId = userId, code = code },
                protocol: Request.Scheme);
            await emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage(new { layout = layout, active = active });
        }
    }
}
