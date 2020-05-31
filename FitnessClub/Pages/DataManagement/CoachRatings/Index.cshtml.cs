using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL;
using FitnessClub.Data.Models;

namespace FitnessClub.Pages.DataManagement.CoachRatings
{
    public class IndexModel : PageModel
    {
        private readonly FitnessClub.Data.DAL.FCContext _context;

        public IndexModel(FitnessClub.Data.DAL.FCContext context)
        {
            _context = context;
        }

        public IList<CoachRating> CoachRating { get;set; }

        public async Task OnGetAsync()
        {
            CoachRating = await _context.CoachRatings
                .Include(c => c.Coach)
                .Include(c => c.Customer).ToListAsync();
        }
    }
}
