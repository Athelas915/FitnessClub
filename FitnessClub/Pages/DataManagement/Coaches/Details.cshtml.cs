using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.Coaches
{
    [Authorize(Policy = "SignedIn")]
    public class DetailsModel : PageModel
    {
        private readonly IPersonRepository<Coach> coachRepository;

        public DetailsModel(IPersonRepository<Coach> coachRepository)
        {
            this.coachRepository = coachRepository;
        }

        public Coach Coach { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Coach = await coachRepository.GetByID(id.Value);

            if (Coach == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
