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
using FitnessClub.Data.DAL.Utility;

namespace FitnessClub.Areas.Account.Pages.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly IPasswordService passwordService;
        private readonly int userId;

        public DeletePersonalDataModel(IAccountService accountService, IPasswordService passwordService, UserResolverService userResolver)
        {
            this.accountService = accountService;
            this.passwordService = passwordService;
            userId = userResolver.GetUserId();
        }

        public LayoutData LayoutData { get; private set; }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGetAsync(string layout, string active)
        {
            //allows displaying this page for different layouts
            LayoutData = new LayoutData(layout, active);
            var hasPassword = await passwordService.HasPassword(userId);
            if (hasPassword == null)
            {
                return NotFound("Unable to load user with given ID.");
            }
            RequirePassword = hasPassword.Value;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string layout, string active)
        {
            var hasPassword = await passwordService.HasPassword(userId);
            if (hasPassword == null)
            {
                return NotFound("Unable to load user with given ID.");
            }
            var result = hasPassword.Value ? await accountService.DeleteSelfUser(userId, Input.Password) : await accountService.DeleteSelfUser(userId);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                return RedirectToPage(new { layout = layout, active = active });
            }
            return Redirect("~/");
        }
    }
}
