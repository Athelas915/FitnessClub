using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Areas.CustomerPanel.Pages
{
    [Authorize(Roles = "Customer")]
    public class AccountSettingsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
