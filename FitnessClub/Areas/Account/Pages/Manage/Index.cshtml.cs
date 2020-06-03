using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using FitnessClub.Data.Models.ViewModels;
using FitnessClub.Data.BLL.Interfaces;

namespace FitnessClub.Areas.Account.Pages.Manage
{
    [Authorize(Roles = "Customer,Coach,Employee")]
    public class IndexModel : PageModel
    {
        public readonly IAccountService accountService;
        public IndexModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        public string LayoutString { get; private set; }
        public string ActivePageString { get; private set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            public AddressViewModel Address { get; set; }
        }
        public void OnGet(string layout, string active)
        {
            LayoutString = layout;
            ActivePageString = active;
        }
    }
}
