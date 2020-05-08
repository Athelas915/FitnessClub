using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.People
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly IPersonRepository<Person> personRepository;

        public IndexModel(IPersonRepository<Person> personRepository)
        {
            this.personRepository = personRepository;
        }
        public IList<Person> Person { get; set; }

        public async Task OnGetAsync()
        {
            Person = await personRepository.Get();
        }
    }
}
