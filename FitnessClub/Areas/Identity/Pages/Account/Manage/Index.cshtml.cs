using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FitnessClub.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly string userId;

        public IndexModel(UserResolverService userResolver, IAccountService accountService)
        {
            this.accountService = accountService;
            userId = userResolver.GetUserId(User);
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

        private async Task LoadAsync(string userId)
        {
            var userName = await accountService.GetUsername(userId);
            var phoneNumber = await accountService.GetPhoneNumber(userId);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync(userId);
            if (Username == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadAsync(userId);
                return Page();
            }

            var phoneNumber = await accountService.GetPhoneNumber(userId);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await accountService.SetPhoneNumber(userId, Input.PhoneNumber);
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
