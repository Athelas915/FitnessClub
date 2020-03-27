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
    [Authorize(Policy = "SignedIn")]
    public class DetailsModel : PageModel
    {
        private readonly ISessionEnrollmentRepository sessionEnrollmentRepository;

        public DetailsModel(ISessionEnrollmentRepository sessionEnrollmentRepository)
        {
            this.sessionEnrollmentRepository = sessionEnrollmentRepository;
        }

        public SessionEnrollment SessionEnrollment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? PersonID, int? SessionID)
        {
            if ((PersonID == null) || (SessionID == null))
            {
                return NotFound();
            }

            SessionEnrollment = await sessionEnrollmentRepository.GetByID(PersonID.Value, SessionID.Value);

            if (SessionEnrollment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
