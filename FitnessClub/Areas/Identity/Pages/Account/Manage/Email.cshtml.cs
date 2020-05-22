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

namespace FitnessClub.Areas.Identity.Pages.Account.Manage
{
    public partial class EmailModel : PageModel
    {
        private readonly IAccountManagementService accountManagementService;
        private readonly IEmailSender emailSender;

        public EmailModel(IAccountManagementService accountManagementService, IEmailSender emailSender)
        {
            this.accountManagementService = accountManagementService;
            this.emailSender = emailSender;
        }

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
            var email = await accountManagementService.GetEmail(userId);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await accountManagementService.IsEmailConfirmed(userId);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = accountManagementService.GetUserId(User);

            await LoadAsync(userId);
            if (Email == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var userId = accountManagementService.GetUserId(User);
            if (userId == -1)
            {
                return NotFound($"Unable to load user.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(userId);
                return Page();
            }

            var email = await accountManagementService.GetEmail(userId);
            if (Input.NewEmail != email)
            {
                var code = await accountManagementService.GenerateChangeEmailToken(userId, Input.NewEmail);
                if (code == null)
                {
                    return NotFound($"Unable to load user with ID '{userId}'.");
                }
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);
                await emailSender.SendEmailAsync(
                    Input.NewEmail,
                    "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                StatusMessage = "Confirmation link to change email sent. Please check your email.";
                return RedirectToPage();
            }
            
            StatusMessage = "Your email is unchanged.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var userId = accountManagementService.GetUserId(User);

            if (!ModelState.IsValid)
            {
                await LoadAsync(userId);
                return Page();
            }

            var email = await accountManagementService.GetEmail(userId);
            var code = await accountManagementService.GenerateEmailConfirmationToken(userId);
            if (code == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);
            await emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
