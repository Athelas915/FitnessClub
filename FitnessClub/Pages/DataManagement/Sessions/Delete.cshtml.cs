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
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Session Session { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session = await unitOfWork.SessionRepository.GetByID(id.Value);

            if (Session == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session = await unitOfWork.SessionRepository.GetByID(id.Value);

            if (Session != null)
            {
                unitOfWork.SessionRepository.Delete(Session);
                await unitOfWork.Commit();
            }

            return RedirectToPage("./Index");
        }
    }
}
