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
    [Authorize(Policy = "SignedIn")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IList<Session> Session { get;set; }

        public async Task OnGetAsync()
        {
            Session = await unitOfWork.SessionRepository.Get();
        }
    }
}
