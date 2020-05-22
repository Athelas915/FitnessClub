using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitnessClub.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly IAccountManagementService accountManagementService;

        public IndexModel(IAccountManagementService accountManagementService)
        {
            this.accountManagementService = accountManagementService;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(int userId)
        {
            var userName = await accountManagementService.GetUsername(userId);
            var phoneNumber = await accountManagementService.GetPhoneNumber(userId);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = accountManagementService.GetUserId(User);

            await LoadAsync(userId);
            if (Username == null || Input.PhoneNumber == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = accountManagementService.GetUserId(User);

            if (!ModelState.IsValid)
            {
                await LoadAsync(userId);
                return Page();
            }

            var phoneNumber = await accountManagementService.GetPhoneNumber(userId);
            if (phoneNumber == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            else if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await accountManagementService.SetPhoneNumber(userId, Input.PhoneNumber);
                if (setPhoneResult == null)
                {
                    return NotFound($"Unable to load user with ID '{userId}'.");
                }
                else if (!setPhoneResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
