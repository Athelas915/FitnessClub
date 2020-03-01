using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;

namespace FitnessClub.Pages.DataManagement.Sessions
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        public DetailsModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

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
    }
}
