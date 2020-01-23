using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HelloWorld.Pages
{
    public class GoogleSigninModel : PageModel
    {
        private readonly ILogger<GoogleSigninModel> _logger;

        public GoogleSigninModel(ILogger<GoogleSigninModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
