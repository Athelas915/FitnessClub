using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL;
using FitnessClub.Data.Models.Identity;

namespace FitnessClub.Pages.DataManagement.UserData
{
    public class IndexModel : PageModel
    {
        private readonly FitnessClub.Data.DAL.FCContext _context;

        public IndexModel(FitnessClub.Data.DAL.FCContext context)
        {
            _context = context;
        }

        public IList<AspNetUser> AspNetUser { get;set; }

        public async Task OnGetAsync()
        {
            AspNetUser = await _context.AspNetUsers.ToListAsync();
        }
    }
}
