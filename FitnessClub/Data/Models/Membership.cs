using System;

namespace FitnessClub.Data.Models
{
    public class Membership : DataEntity
    {
        public Membership() { }

        public Membership(Membership m) : base(m)
        {
            MembershipID = m.MembershipID;
            MembershipNo = m.MembershipNo;
            Start = m.Start;
            Finish = m.Finish;
            CustomerID = m.CustomerID;
            if (m.Customer != null)
            {
                Customer = new Customer(m.Customer);
            }
        }

        public int MembershipID { get; set; }
        public int MembershipNo { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public virtual Customer Customer { get; set; }
        public int CustomerID { get; set; }
    }
}
