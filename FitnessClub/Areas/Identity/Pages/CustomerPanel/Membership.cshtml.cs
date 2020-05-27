using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Areas.Identity.Pages.CustomerPanel
{
    [Authorize(Roles = "Customer")]
    public class MembershipModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
