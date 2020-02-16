using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;

namespace FitnessClub
{
    public class CreateModel : PageModel
    {
        private readonly IPersonRepository personRepository;

        public CreateModel(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Person Person { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            personRepository.Insert(Person);
            Person.CreatedOn = DateTime.Now;
            Person.CreatedBy = 0; // add currently logged user here
            await personRepository.Save();

            return RedirectToPage("./Index");
        }
    }
}
