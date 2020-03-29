using System;

namespace FitnessClub.Data.Models
{
    public class Membership : BaseEntity
    {
        public int MembershipID { get; set; }
        public int MembershipNo { get; set; }
        public int PersonID { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
}
}
