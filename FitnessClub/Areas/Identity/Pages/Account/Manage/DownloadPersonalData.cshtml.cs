using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FitnessClub.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly string userId;

        public DownloadPersonalDataModel(IAccountService accountService, UserResolverService userResolver)
        {
            this.accountService = accountService;
            userId = userResolver.GetUserId(User);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var json = await accountService.GetPersonalData(userId);

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(json, "text/json");
        }
    }
}
