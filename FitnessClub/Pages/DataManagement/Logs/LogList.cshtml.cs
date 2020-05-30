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

namespace FitnessClub.Pages.DataManagement.Logs
{
    //[Authorize(Roles = "Administrator")]
    public class LogListModel: PageModel
    {
        private readonly ILogRepository logRepository;

        public LogListModel(ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        public IList<Log> Logs { get;set; }

        public async Task OnGetAsync()
        {
            Logs = await logRepository.Get();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Logs = await logRepository.Get();

            logRepository.DeleteAllLogs();

            await logRepository.Commit();

            return RedirectToPage("./LogList");
        }
    }
}
