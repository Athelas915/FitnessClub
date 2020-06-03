using System.Threading.Tasks;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly string userId;

        public PersonalDataModel(UserResolverService userResolver)
        {
            userId = userResolver.GetUserId(User);
        }

        public IActionResult OnGet()
        {
            if (userId == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            return Page();
        }
    }
}