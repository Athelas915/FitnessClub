using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.Memberships
{
    [Authorize(Policy = "SignedIn")]
    public class EditModel : PageModel
    {
        private readonly IMembershipRepository membershipRepository;

        public EditModel(IMembershipRepository membershipRepository)
        {
            this.membershipRepository = membershipRepository;
        }

        [BindProperty]
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
            ViewData["PersonID"] = new SelectList(await membershipRepository.Get<Customer>(), "PersonID", "LastName");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            membershipRepository.Update(Membership);

            try
            {
                await membershipRepository.Submit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembershipExists(Membership.MembershipID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MembershipExists(int id)
        {
            return membershipRepository.Any(id);
        }
    }
}
