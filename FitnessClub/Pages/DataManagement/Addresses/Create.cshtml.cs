using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.Addresses
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly IAddressRepository addressRepository;

        public CreateModel(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["Genders"] = new SelectList(await addressRepository.Get<Person>(), "PersonID", "LastName");
            return Page();
        }

        [BindProperty]
        public Address Address { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (addressRepository.Any(Address.AddressID) == true)
            {
                return RedirectToPage("./Error");
            }
            else
            {
                addressRepository.Insert(Address);
                await addressRepository.Submit();
            }

            return RedirectToPage("./Index");
        }
    }
}
