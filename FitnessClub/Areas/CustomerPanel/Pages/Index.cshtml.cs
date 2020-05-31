using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Areas.CustomerPanel.Pages
{
    [Authorize(Roles = "Customer")]
    public class CustomerPanelModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
