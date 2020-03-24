using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;

namespace FitnessClub.Pages.DataManagement.Holidays
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Holiday Holiday { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Holiday = await unitOfWork.HolidayRepository.GetByID(id.Value);

            if (Holiday == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
