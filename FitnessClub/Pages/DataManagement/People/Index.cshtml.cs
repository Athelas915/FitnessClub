using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL;
using FitnessClub.Data.Models;

namespace FitnessClub
{
    public class IndexModel : PageModel
    {
        private readonly IPersonRepository personRepository;

        public IndexModel(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public IList<Person> Person { get; set; }

        public async Task OnGetAsync()
        {
            Person = await personRepository.GetPeople();
        }
    }
}
