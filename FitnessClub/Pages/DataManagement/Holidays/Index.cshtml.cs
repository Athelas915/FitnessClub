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

namespace FitnessClub.Pages.DataManagement.Holidays
{
    [Authorize(Policy = "SignedIn")]
    public class IndexModel : PageModel
    {
        private readonly IHolidayRepository holidayRepository;

        public IndexModel(IHolidayRepository holidayRepository)
        {
            this.holidayRepository = holidayRepository;
        }

        public IList<Holiday> Holiday { get; set; }

        public async Task OnGetAsync()
        {
            Holiday = await holidayRepository.Get();
        }
    }
}
