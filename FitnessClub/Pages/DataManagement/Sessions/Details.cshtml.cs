﻿using System;
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
        private readonly ISessionRepository sessionRepository;

        public DetailsModel(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public Session Session { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session = await sessionRepository.GetByID(id.Value);

            if (Session == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
