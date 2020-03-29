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

namespace FitnessClub.Pages.DataManagement.Employees
{
    [Authorize(Policy = "SignedIn")]
    public class DeleteModel : PageModel
    {
        private readonly IPersonRepository<Employee> employeeRepository;

        public DeleteModel(IPersonRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await employeeRepository.GetByID(id.Value);

            if (Employee == null)
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

            Employee = await employeeRepository.GetByID(id.Value);

            if (Employee != null)
            {
                employeeRepository.Delete(Employee);
                await employeeRepository.Submit();
            }

            return RedirectToPage("./Index");
        }
    }
}
