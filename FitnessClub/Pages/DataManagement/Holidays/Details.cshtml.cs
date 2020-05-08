﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.Holidays
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly IHolidayRepository holidayRepository;

        public DetailsModel(IHolidayRepository holidayRepository)
        {
            this.holidayRepository = holidayRepository;
        }

        public Holiday Holiday { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Holiday = await holidayRepository.GetByID(id.Value);

            if (Holiday == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
