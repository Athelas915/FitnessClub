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
    public class PersonalDataModel : PageModel
    {
        private readonly int userId;

        public PersonalDataModel(UserResolverService userResolver)
        {
            userId = userResolver.GetUserId();
        }
        public LayoutData LayoutData { get; private set; }

        public IActionResult OnGet(string layout, string active)
        {
            //allows displaying this page for different layouts
            LayoutData = new LayoutData(layout, active);
            if (userId == -1)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            return Page();
        }
    }
}