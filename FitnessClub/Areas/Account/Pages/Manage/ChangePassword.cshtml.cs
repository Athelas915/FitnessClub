using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Areas.Account.Pages.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IPasswordService passwordService;
        private readonly int userId;

        public ChangePasswordModel(IPasswordService passwordService, UserResolverService userResolver)
        {
            this.passwordService = passwordService;
            userId = userResolver.GetUserId();
        }
        public LayoutData LayoutData { get; private set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string layout, string active)
        {
            //allows displaying this page for different layouts
            LayoutData = new LayoutData(layout, active);
            var hasPassword = await passwordService.HasPassword(userId);

            if (hasPassword == null)
            {
                return NotFound("Unable to load user with given ID.");
            }
            else if (!hasPassword.Value)
            {
                return RedirectToPage("./SetPassword", new { layout = layout, active = active });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string layout, string active)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var changePasswordResult = await passwordService.ChangePassword(userId, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            StatusMessage = "Your password has been changed.";

            return RedirectToPage(new { layout = layout, active = active });
        }
    }
}
