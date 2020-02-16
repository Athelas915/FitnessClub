using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;

namespace FitnessClub
{
    public class DeleteModel : PageModel
    {
        private readonly IPersonRepository personRepository;

        public DeleteModel(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [BindProperty]
        public Person Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await personRepository.GetByID(id.Value);

            if (Person == null)
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

            Person = await personRepository.GetByID(id.Value);

            if (Person != null)
            {
                personRepository.Delete(id.Value);
                await personRepository.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
