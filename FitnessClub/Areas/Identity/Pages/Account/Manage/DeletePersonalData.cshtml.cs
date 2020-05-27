using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly IUserService userService;

        public DeletePersonalDataModel(IUserService userService)
        {
            this.userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = userService.GetUserId(User);
            var hasPassword = await userService.HasPassword(userId);
            if (hasPassword == null)
            {
                return NotFound("Unable to load user with given ID.");
            }
            RequirePassword = hasPassword.Value;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = userService.GetUserId(User);
            var result = await userService.DeleteSelfUser(userId, Input.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                return Page();
            }
            return Redirect("~/");
        }
    }
}
