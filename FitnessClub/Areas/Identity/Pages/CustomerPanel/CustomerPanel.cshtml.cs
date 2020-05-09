using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitnessClub.Areas.Identity.Pages.CustomerPanel
{
    //[Authorize(Roles = "Customer")]
    public class CustomerPanelModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}