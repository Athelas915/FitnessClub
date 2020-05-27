using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessClub.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FitnessClub.Data.BLL.Interfaces;

namespace FitnessClub.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly IUserService userService;

        public DownloadPersonalDataModel(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = userService.GetUserId(User);
            var json = await userService.GetPersonalData(userId);

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(json, "text/json");
        }
    }
}
