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
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly IMembershipRepository membershipRepository;

        public IndexModel(IMembershipRepository membershipRepository)
        {
            this.membershipRepository = membershipRepository;
        }

        public IList<Membership> Membership { get; set; }

        public async Task OnGetAsync()
        {
            Membership = await membershipRepository.Get();
        }
    }
}
