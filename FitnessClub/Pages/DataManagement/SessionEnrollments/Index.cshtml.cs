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

namespace FitnessClub.Pages.DataManagement.SessionEnrollments
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly ISessionEnrollmentRepository sessionEnrollmentRepository;

        public IndexModel(ISessionEnrollmentRepository sessionEnrollmentRepository)
        {
            this.sessionEnrollmentRepository = sessionEnrollmentRepository;
        }

        public IList<SessionEnrollment> SessionEnrollment { get; set; }

        public async Task OnGetAsync()
        {
            SessionEnrollment = await sessionEnrollmentRepository.Get();
        }
    }
}
