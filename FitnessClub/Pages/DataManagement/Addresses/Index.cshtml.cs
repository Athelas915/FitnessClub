﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Data.DAL.Interfaces;
using FitnessClub.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace FitnessClub.Pages.DataManagement.Addresses
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly IAddressRepository addressRepository;

        public IndexModel(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public IList<Address> Address { get;set; }

        public async Task OnGetAsync()
        {
            Address = await addressRepository.Get();
        }
    }
}
