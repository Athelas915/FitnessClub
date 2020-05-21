using System;

namespace FitnessClub.Data.Models
{
    public class Membership : DataEntity
    {
        public int MembershipID { get; set; }
        public int MembershipNo { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public virtual Customer Customer { get; set; }
        public int CustomerID { get; set; }
    }
}
