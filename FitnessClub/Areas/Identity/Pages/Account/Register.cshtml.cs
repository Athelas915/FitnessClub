using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.Identity;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Utility;

namespace FitnessClub.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly IRegistrationService registrationService;
        private readonly ITokenGenerator tokenGenerator;
        private readonly ISignInService signInService;
        private readonly IEmailSender emailSender;
        private readonly UserResolverService userResolver;

        public RegisterModel(
            IAccountService accountService,
            IRegistrationService registrationService,
            ITokenGenerator tokenGenerator,
            ISignInService signInService,
            IEmailSender emailSender,
            UserResolverService userResolver
            )
        {
            this.accountService = accountService;
            this.registrationService = registrationService;
            this.tokenGenerator = tokenGenerator;
            this.signInService = signInService;
            this.emailSender = emailSender;
            this.userResolver = userResolver;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public Address Address { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public IList<string> TestList { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await signInService.GetExternalAuthenticationSchemes()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await signInService.GetExternalAuthenticationSchemes()).ToList();


            if (ModelState.IsValid)
            {
                var result = await registrationService.CreateUser(Input.Email, Input.Password, Customer, "Customer");


                if (result.Succeeded)
                {
                    var userId = await userResolver.GetUserId(Input.Email);


                    var code = await tokenGenerator.GenerateEmailConfirmationToken(userId);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code },
                        protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    

                    if (await registrationService.ConfirmedAccountRequired(userId))
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
