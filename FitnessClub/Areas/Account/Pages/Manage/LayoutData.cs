using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessClub.Areas.Account.Pages.Manage
{
    public class LayoutData
    {
        public LayoutData(string layout, string activePage)
        {
            Layout = layout;
            ActivePage = activePage;
        }
        public string Layout { get; }
        public string ActivePage { get; }
    }
}
