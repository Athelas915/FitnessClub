﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.Sessions
{
    [Authorize(Policy = "SignedIn")]
    public class CreateModel : PageModel
    {
        private readonly ISessionRepository sessionRepository;
        public CreateModel(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> OnGet()
        {
        ViewData["PersonID"] = new SelectList(await sessionRepository.Get<Coach>(), "PersonID", "LastName");
            return Page();
        }

        [BindProperty]
        public Session Session { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            sessionRepository.Insert(Session);
            await sessionRepository.Submit();

            return RedirectToPage("./Index");
        }
    }
}
