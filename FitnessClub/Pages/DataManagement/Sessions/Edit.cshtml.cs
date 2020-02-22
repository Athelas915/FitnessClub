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

namespace FitnessClub.Pages.DataManagement.Sessions
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        public EditModel(IUnitOfWork unitOfWork)
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
           ViewData["CoachID"] = new SelectList(await unitOfWork.CoachRepository.Get(), "PersonID", "Discriminator");
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

            unitOfWork.SessionRepository.Update(Session);

            try
            {
                await unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(Session.SessionID))
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

        private bool SessionExists(int id)
        {
            return unitOfWork.SessionRepository.Any(id);

        }
    }
}
