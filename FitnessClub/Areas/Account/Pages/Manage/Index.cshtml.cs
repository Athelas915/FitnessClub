using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FitnessClub.Areas.Account.Pages.Manage
{
    [Authorize(Roles = "Customer,Coach,Employee")]
    public class IndexModel : PageModel
    {
        public readonly IAccountService accountService;
        public readonly IPersonService personService;
        public readonly int userId;
        public IndexModel(IAccountService accountService, IPersonService personService, UserResolverService userResolver)
        {
            this.accountService = accountService;
            this.personService = personService;
            userId = userResolver.GetUserId();
            
        }
        public LayoutData LayoutData { get; private set; }
        public string EmailAddress { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            public AddressViewModel Address { get; set; }
        }
        public async Task OnGetAsync(string layout, string active)
        {
            //allows displaying this page for different layouts
            LayoutData = new LayoutData(layout, active);
            EmailAddress = await accountService.GetEmail(userId);

            var address = personService.ViewAddress(userId);
            var phone = await accountService.GetPhoneNumber(userId);

            Input = new InputModel()
            {
                Address = address,
                PhoneNumber = phone
            };
        }
    }
}
