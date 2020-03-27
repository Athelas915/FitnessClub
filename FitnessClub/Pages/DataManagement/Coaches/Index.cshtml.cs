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
    public class IndexModel : PageModel
    {
        private readonly IPersonRepository<Coach> coachRepository;

        public IndexModel(IPersonRepository<Coach> coachRepository)
        {
            this.coachRepository = coachRepository;
        }

        public IList<Coach> Coach { get;set; }

        public async Task OnGetAsync()
        {
            Coach = await coachRepository.Get();
        }
    }
}
