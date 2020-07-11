using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessClub.Data.BLL.Interfaces;
using FitnessClub.Data.DAL.Utility;
using FitnessClub.Data.Models;
using FitnessClub.Data.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly ISessionManagementService sessionManagement; 
        private readonly int userId;
        public CalendarController(ICustomerService customerService, ISessionManagementService sessionManagement , UserResolverService userResolver)
        {
            this.customerService = customerService;
            this.sessionManagement = sessionManagement;
            userId = userResolver.GetUserId();
        }
        [HttpGet]
        public async Task<IList<SessionViewModel>> GetCustomers()
        {
            return await sessionManagement.GetAllSessions();
        }
    }
}