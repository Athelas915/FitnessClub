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
            Active = (DateTime.Now > membership.Start && DateTime.Now < membership.Finish) ? true : false;
        }
        public int MembershipID { get; set; }
        [DisplayName("Membership Number")]
        public int MembershipNo { get; set; }
        [DisplayName("Start Date")]
        public string Start { get; set; }
        [DisplayName("Expiration Number")]
        public string Finish { get; set; }
        [DisplayName("Customer")]
        public string CustomerFullName { get; set; }
        public bool Active { get; set; }
    }
}