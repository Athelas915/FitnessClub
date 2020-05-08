using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitnessClub.Pages.CustomerPanel
{
    [Authorize(Roles = "Customer")]
    public class _CustomerPanelModel : PageModel
    {
        public _PartialTestModel partialTestModel = new _PartialTestModel();
        public void OnGet()
        {

        }
    }
}