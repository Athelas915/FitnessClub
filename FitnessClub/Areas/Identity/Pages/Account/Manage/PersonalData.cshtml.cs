using System.Threading.Tasks;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly IUserService userService;

        public PersonalDataModel(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult OnGet()
        {
            var userId = userService.GetUserId(User);
            if (userId == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            return Page();
        }
    }
}