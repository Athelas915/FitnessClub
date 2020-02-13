using System.Collections.Generic;

namespace FC.Data.Models
{
    class Customer : Person
    {
        public int CustomerID { get; set; }
        public virtual ICollection<SessionEnrollment> SessionEnrollments { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
    }
}
