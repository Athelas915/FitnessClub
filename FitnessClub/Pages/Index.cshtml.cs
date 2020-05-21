using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FitnessClub.Data.Models.Identity;
using FitnessClub.Data.DAL;

namespace FitnessClub.Pages
{
    public class IndexModel : PageModel
    {   
        public IndexModel()
        {

        }
        public void OnGet()
        {
        }
    }
}
