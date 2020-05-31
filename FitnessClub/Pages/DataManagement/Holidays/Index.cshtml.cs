using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL;
using FitnessClub.Data.Models;

namespace FitnessClub.Pages.DataManagement.Holidays
{
    public class IndexModel : PageModel
    {
        private readonly FitnessClub.Data.DAL.FCContext _context;

        public IndexModel(FitnessClub.Data.DAL.FCContext context)
        {
            _context = context;
        }

        public IList<Holiday> Holiday { get;set; }

        public async Task OnGetAsync()
        {
            Holiday = await _context.Holidays
                .Include(h => h.Employee).ToListAsync();
        }
    }
}
