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
    public class DetailsModel : PageModel
    {
        private readonly IPersonRepository personRepository;

        public DetailsModel(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

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
    }
}
