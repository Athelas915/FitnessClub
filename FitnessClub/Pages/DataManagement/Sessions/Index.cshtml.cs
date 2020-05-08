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

namespace FitnessClub.Pages.DataManagement.Sessions
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly ISessionRepository sessionRepository;
        public IndexModel(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public IList<Session> Session { get;set; }

        public async Task OnGetAsync()
        {
            Session = await sessionRepository.Get();
        }
    }
}
