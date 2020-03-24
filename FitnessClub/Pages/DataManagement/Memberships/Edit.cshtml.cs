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

namespace FitnessClub.Pages.DataManagement.Memberships
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Membership Membership { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Membership = await unitOfWork.MembershipRepository.GetByID(id.Value);

            if (Membership == null)
            {
                return NotFound();
            }
            ViewData["PersonID"] = new SelectList(await unitOfWork.CustomerRepository.Get(), "PersonID", "LastName");
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

            unitOfWork.MembershipRepository.Update(Membership);

            try
            {
                await unitOfWork.Commit();
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
            return unitOfWork.MembershipRepository.Any(id);
        }
    }
}
