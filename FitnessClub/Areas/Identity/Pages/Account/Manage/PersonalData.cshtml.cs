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
        private readonly IAccountManagementService accountManagementService;

        public PersonalDataModel(IAccountManagementService accountManagementService)
        {
            this.accountManagementService = accountManagementService;
        }

        public IActionResult OnGet()
        {
            var userId = accountManagementService.GetUserId(User);
            if (userId == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            return Page();
        }
    }
}