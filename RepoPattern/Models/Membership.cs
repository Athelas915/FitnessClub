using System;

namespace FC.Data.Models
{
    class Membership : BaseEntity
    {
        public int MembershipID { get; set; }
        public int MembershipNo { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
}
}
