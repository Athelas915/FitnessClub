using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.Employees
{
    [Authorize(Policy = "SignedIn")]
    public class EditModel : PageModel
    {
        private readonly IPersonRepository<Employee> employeeRepository;

        public EditModel(IPersonRepository<Employee> employeeRepository)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            employeeRepository.Update(Employee);

            try
            {
                await employeeRepository.Submit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.PersonID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmployeeExists(int id)
        {
            return employeeRepository.Any(id);
        }
    }
}
