using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;

namespace FitnessClub.Pages.DataManagement.Memberships
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["PersonID"] = new SelectList(await unitOfWork.CustomerRepository.Get(), "PersonID", "LastName");
            return Page();
        }

        [BindProperty]
        public Membership Membership { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            unitOfWork.MembershipRepository.Insert(Membership);
            await unitOfWork.Commit();

            return RedirectToPage("./Index");
        }
    }
}
