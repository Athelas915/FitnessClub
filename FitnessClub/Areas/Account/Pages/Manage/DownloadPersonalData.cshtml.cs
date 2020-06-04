using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FitnessClub.Areas.Account.Pages.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly int userId;

        public DownloadPersonalDataModel(IAccountService accountService, UserResolverService userResolver)
        {
            this.accountService = accountService;
            userId = userResolver.GetUserId();
        }
        public LayoutData LayoutData { get; private set; }
        public void OnGet(string layout, string active)
        {
            //allows displaying this page for different layouts
            LayoutData = new LayoutData(layout, active);
        }
            public async Task<IActionResult> OnPostAsync()
        {
            var json = await accountService.GetPersonalData(userId);

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(json, "text/json");
        }
    }
}
