using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FitnessClub.Data.Models.ViewModels
{
    public class MembershipViewModel
    {
        public MembershipViewModel(Membership membership)
        {
            MembershipID = membership.MembershipID;
            MembershipNo = membership.MembershipNo;
            Start = membership.Start.ToShortDateString();
            Finish = membership.Finish.ToShortDateString();
            var firstName = membership.Customer.FirstName;
            var lastName = membership.Customer.LastName;
            CustomerFullName = firstName + ' ' + lastName;
        }
        public int MembershipID { get; set; }
        public int MembershipNo { get; set; }
        public string Start { get; set; }
        public string Finish { get; set; }
        public string CustomerFullName { get; set; }
    }
}