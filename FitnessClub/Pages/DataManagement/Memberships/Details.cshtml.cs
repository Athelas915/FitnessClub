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

namespace FitnessClub.Pages.DataManagement.Memberships
{
    [Authorize(Policy = "SignedIn")]
    public class DetailsModel : PageModel
    {
        private readonly IMembershipRepository membershipRepository;

        public DetailsModel(IMembershipRepository membershipRepository)
        {
            this.membershipRepository = membershipRepository;
        }

        public Membership Membership { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Membership = await membershipRepository.GetByID(id.Value);

            if (Membership == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
